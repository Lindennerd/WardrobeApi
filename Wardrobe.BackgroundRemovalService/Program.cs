using MassTransit;
using Wardrobe.Application.Image.BackgroundRemoval;
using Wardrobe.Application.Image.Classification;
using Wardrobe.BackgroundRemovalService;
using Wardrobe.CrossCutting;
using Wardrobe.CrossCutting.Configurations;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging((_, builder) => builder.AddConsole())
    .ConfigureHostConfiguration(c => 
        c.AddJsonFile("appsettings.json")
            .AddEnvironmentVariables("WARDROBE_"))
    .ConfigureServices((context, services) =>
    {
        services.AddScoped<IClassificationQueueService, ClassificationQueueService>();
        
        var bgRemovalApi =  context.Configuration.GetSection("BackgroundRemovalApi");

        services.AddBackgroundRemovalService(cfg =>
        {
            cfg.UseHttpClient(httpcfg =>
            {
                httpcfg.BaseUrl = bgRemovalApi["BaseUrl"];
                httpcfg.ApiKey = bgRemovalApi["ApiKey"];
                httpcfg.ApiHost = bgRemovalApi["ApiHost"];
            });
            
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

public class BackgroundRemovalConfiguration
{
    public string? BaseUrl { get; set; }
    public string? ApiKey { get; set; }
    public string? ApiHost { get; set; }
}