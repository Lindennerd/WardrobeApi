using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Wardrobe.Domain.Configurations;
using Wardrobe.Domain.Entities.Geolocation;

namespace Wardrobe.Infra.HttpClients;

public class GeolocationHttpClient : IGeolocationHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly GeolocationHttpConfiguration _options;
    private readonly ILogger<GeolocationHttpClient> _logger;

    
    public GeolocationHttpClient(HttpClient httpClient,
        GeolocationHttpConfiguration options,
        ILogger<GeolocationHttpClient> logger)
    {
        _httpClient = httpClient;
        _options = options;
        _logger = logger;

        _httpClient.BaseAddress = new Uri(options.BaseUrl);
    }

    public async Task<(string formattedAddress, double latitude, double longitude)> GetCoordinates(string address)
    {
        var response =
            await _httpClient.GetAsync($"maps/api/geocode/json?address={address}&key={_options.ApiKey}");
        response.EnsureSuccessStatusCode();

        var geolocationResult = JsonConvert.DeserializeObject<GeolocationResult>(await response.Content.ReadAsStringAsync());
        if (geolocationResult == null && geolocationResult.Results.Count > 0) throw new ArgumentNullException();

        var firstResult = geolocationResult.Results.FirstOrDefault();
        return (firstResult.FormattedAddress, 
            firstResult.Geometry.Location.Lat,
            firstResult.Geometry.Location.Lng);
    }
}

public interface IGeolocationHttpClient
{
    Task<(string formattedAddress, double latitude, double longitude)> GetCoordinates(string address);
}