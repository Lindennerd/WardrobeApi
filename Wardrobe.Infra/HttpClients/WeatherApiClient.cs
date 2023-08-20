using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Wardrobe.Domain.Configurations;
using Wardrobe.Domain.Entities.Forecast;

namespace Wardrobe.Infra.HttpClients;

public class WeatherApiClient : IWeatherApiClient
{
    private readonly HttpClient _httpClient;
    private readonly WeatherHttpConfiguration _options;
    private readonly IMemoryCache _cache;
    private readonly ILogger<WeatherApiClient> _logger;
    
    public WeatherApiClient(HttpClient httpClient,
        WeatherHttpConfiguration options,
        IMemoryCache cache,
        ILogger<WeatherApiClient> logger)
    {
        _httpClient = httpClient;
        _options = options;
        _cache = cache;
        _logger = logger;
        
        _httpClient.BaseAddress = new Uri(options.BaseUrl);
    }

    public async Task<ForecastResult?> GetForecast(double latitude, double longitude, int days)
    {
        var cacheKey = $"{latitude},{longitude}-{days}";
        if (_cache.TryGetValue<ForecastResult>(cacheKey, out ForecastResult forecast)) return forecast;
        
        var response =
            await this._httpClient.GetAsync(
                $"/forecast.json?q={latitude},{longitude}&days={days}&key={_options.ApiKey}");
        response.EnsureSuccessStatusCode();

        var forecastResult = JsonConvert.DeserializeObject<ForecastResult>(
            await response.Content.ReadAsStringAsync());

        _cache.Set(cacheKey, forecastResult, TimeSpan.FromDays(days));
        return forecastResult;
    }
}

public interface IWeatherApiClient
{
    Task<ForecastResult?> GetForecast(double latitude, double longitude, int days);
}