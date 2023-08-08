using MassTransit;
using Wardrobe.Application.Image.Classification;
using Wardrobe.Application.Image.Database;
using Wardrobe.CrossCutting;
using Wardrobe.ImageClassificationService;
using Wardrobe.Infra.ML;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {

        var modelConfiguration = context.Configuration.GetSection("ML");
      
        services.AddSingleton<ClassificationPredictionService>(x =>
            new ClassificationPredictionService(modelConfiguration["ModelPath"]));

        services.AddScoped<ClassificationService>();
        services.AddScoped<IUpdateClassification, UpdateClassification>();

        services.AddMassTransit(cfg =>
        {
            var rabbitMqConfiguration = context.Configuration.GetSection("RabbitMQ");

            cfg.AddConsumer<ImageClassificationConsumer>();
            cfg.UsingRabbitMq((ctx, rabbitMqBusFactoryConfigurator) =>
            {
                rabbitMqBusFactoryConfigurator.Host(rabbitMqConfiguration["Host"]!);
                rabbitMqBusFactoryConfigurator.ConfigureEndpoints(ctx);
                rabbitMqBusFactoryConfigurator.ReceiveEndpoint(Queues.Classification.GetDescription()!,
                    c =>
                    {
                        c.PrefetchCount = 1;
                        c.UseMessageRetry(r => r.Interval(2, 100));
                        c.ConfigureConsumer<ImageClassificationConsumer>(ctx);
                    });
            });
        });

    })
    .Build();

host.Run();