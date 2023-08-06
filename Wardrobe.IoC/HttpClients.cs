using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wardrobe.Application.Image;
using Wardrobe.Application.Image.BackgroundRemoval;

namespace Wardrobe.IoC;

public static class HttpClients
{
    public static IServiceCollection AddBackgroundRemovalHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        var config = new BackgroundRemovalConfiguration();
        configuration.Bind("BackgroundRemoval", config);
        services.AddHttpClient<IBackgroundRemovalHttpClient, BackgroundRemovalHttpClient>(client =>
        {
            client.BaseAddress = new Uri(config.BaseUrl);
            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", config.ApiKey);
            client.DefaultRequestHeaders.Add("X-RapidAPI-Host", config.ApiHost);
        });
        
        return services;
    }
}

public class BackgroundRemovalConfiguration
{
    public string BaseUrl { get; set; }
    public string ApiKey { get; set; }
    public string ApiHost { get; set; }
}