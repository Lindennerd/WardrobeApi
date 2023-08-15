using MassTransit;
using NLog.Extensions.Logging;
using Wardrobe.Application.Image.BackgroundRemoval;
using Wardrobe.Application.Image.Classification;
using Wardrobe.BackgroundRemovalService;
using Wardrobe.CrossCutting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging((_, builder) => builder.AddConsole())
    .ConfigureServices((context, services) =>
    {
        services.AddLogging(logging =>
        {
            logging.AddNLog(context.Configuration.GetSection("Logging"));
        });
        
        services.AddScoped<IClassificationQueueService, ClassificationQueueService>();

        services.AddBackgroundRemovalService(cfg =>
        {
            cfg.UseImageSharp(c => c.BrightnessThreshHold = 0.8f);
        });
        
        services.AddMassTransit((cfg) =>
        {
            var rabbitMqConfiguration = context.Configuration.GetSection("RabbitMQ");
            
            cfg.AddConsumer<BackgroundRemovalConsumer>();
            cfg.UsingRabbitMq((ctx, rabbitMqBusFactoryConfigurator) =>
            {
                rabbitMqBusFactoryConfigurator.Host(rabbitMqConfiguration["Host"]!);
                rabbitMqBusFactoryConfigurator.ConfigureEndpoints(ctx);
                rabbitMqBusFactoryConfigurator.ReceiveEndpoint(Queues.BackgroundRemoval.GetDescription()!,
                    c =>
                    {
                        c.PrefetchCount = 1;
                        c.UseMessageRetry(r => r.Interval(2, 100));
                        c.ConfigureConsumer<BackgroundRemovalConsumer>(ctx);
                    });
            });
        });

    })
    .Build();

host.Run();