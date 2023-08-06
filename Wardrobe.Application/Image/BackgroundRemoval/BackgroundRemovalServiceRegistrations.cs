using Microsoft.Extensions.DependencyInjection;
using Wardrobe.Application.Image.BackgroundRemoval.Strategies.Http;
using Wardrobe.Application.Image.BackgroundRemoval.Strategies.SharpImage;

namespace Wardrobe.Application.Image.BackgroundRemoval;

public static class BackgroundRemovalServiceRegistrations
{
    public static IServiceCollection AddBackgroundRemovalService(this IServiceCollection services, Action<IBackgroundRemovalServiceConfigurator> configure)
    {
        if (services.Any(d => d.ServiceType == typeof(IBackgroundRemoval)))
        {
            throw new ApplicationException("BackgroundRemoval Service already registered");
        }

        var configurator = new BackgroundRemovalServiceConfigurator(services);
        configure.Invoke(configurator);

        return services;
    }
    
    public static void UseHttpClient(this IBackgroundRemovalServiceConfigurator configurator, Action<BackgroundRemovalHttpClientConfigurator> configure)
    {
        var httpClientConfigurator = new BackgroundRemovalHttpClientConfigurator(configurator.Services);
        configure.Invoke(httpClientConfigurator);
    }
    
    public static void UseImageSharp(this IBackgroundRemovalServiceConfigurator configurator, Action<BackgroundRemovalImageSharpConfigurator> configure)
    {
        var imageSharpConfigurator = new BackgroundRemovalImageSharpConfigurator(configurator.Services);
        configure.Invoke(imageSharpConfigurator);
    }
}