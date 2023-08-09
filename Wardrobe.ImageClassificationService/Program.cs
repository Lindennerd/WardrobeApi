using MassTransit;
using Microsoft.Extensions.ML;
using Wardrobe.Application.Image.Classification;
using Wardrobe.CrossCutting;
using Wardrobe.ImageClassificationService;
using Wardrobe.Infra.Database;
using Wardrobe.Infra.Database.Cloth;
using Wardrobe.Infra.ML;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {

        var modelConfiguration = context.Configuration.GetSection("ML");
      
        services.Configure<MongoConnectionSettings>(
            context.Configuration.GetSection("MongoDB")
        );
        
        //https://github.com/dotnet/docs/blob/main/docs/machine-learning/how-to-guides/serve-model-web-api-ml-net.md
        services.AddPredictionEnginePool<ImageData, ImagePrediction>()
            .FromFile(modelConfiguration["ModelPath"]);
        
        services.AddScoped<ClassificationService>();
        services.AddScoped<IClothesRepository, ClothesRepository>();

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