using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Wardrobe.Domain.Entities.Forecast;

namespace Wardrobe.Application.Weather;

public class WeatherApiClient : IWeatherApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<WeatherApiClient> _logger;

    private const string apiKey = "feecbbcb9778472392f190039231908";
    
    public WeatherApiClient(HttpClient httpClient, ILogger<WeatherApiClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<ForecastResult?> GetForecast(double latitude, double longitude, int days)
    {
        var response =
            await this._httpClient.GetAsync($"/forecast.json?q={latitude},{longitude}&days={days}&key={apiKey}");
        response.EnsureSuccessStatusCode();

        return JsonConvert.DeserializeObject<ForecastResult>(await response.Content.ReadAsStringAsync());
    }
}

public interface IWeatherApiClient
{
    Task<ForecastResult?> GetForecast(double latitude, double longitude, int days);
}