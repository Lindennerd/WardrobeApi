using Microsoft.Extensions.DependencyInjection;

namespace Wardrobe.Application.Image.BackgroundRemoval.Strategies.Http;

public class BackgroundRemovalHttpClientConfigurator
{
    public string? BaseUrl { get; set; }
    public string? ApiKey { get; set; }
    public string? ApiHost { get; set; }
    public BackgroundRemovalHttpClientConfigurator(IServiceCollection services)
    {
        services.AddHttpClient<IBackgroundRemovalHttpClient, BackgroundRemovalHttpClient>(client =>
        {
            client.BaseAddress = new Uri(BaseUrl!);
            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", ApiKey);
            client.DefaultRequestHeaders.Add("X-RapidAPI-Host", ApiHost);
            
            if(string.IsNullOrEmpty(BaseUrl) || string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(ApiHost))
                throw new ArgumentNullException("BackgroundRemovalHttpClientConfigurator not configured");
        });

        services.AddScoped<IBackgroundRemoval, BackgroundRemovalHttp>();
    }
}