using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wardrobe.Domain.Configurations;
using Wardrobe.Infra.HttpClients;

namespace Wardrobe.CrossCutting.Registrations;

public static class HttpClientRegistration
{
    public static IServiceCollection UseGeolocationHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<GeolocationHttpConfiguration>(opt =>
        {
            var config = new GeolocationHttpConfiguration();
            configuration.GetSection("GeolocationClient").Bind(config);
            return config;
        });

        services
            .AddHttpClient<IGeolocationHttpClient, GeolocationHttpClient>();
        return services;
    }

    public static IServiceCollection UseWeatherHttpClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<WeatherHttpConfiguration>(opt =>
        {
            var config = new WeatherHttpConfiguration();
            configuration.GetSection("WeatherClient").Bind(config);
            return config;
        });
        
        services.AddHttpClient<IWeatherApiClient, WeatherApiClient>();
        return services;
    }
}