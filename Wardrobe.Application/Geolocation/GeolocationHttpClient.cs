using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Wardrobe.CrossCutting.Exceptions;
using Wardrobe.Domain.Entities.Geolocation;

namespace Wardrobe.Application.Geolocation;

public class GeolocationHttpClient : IGeolocationHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<GeolocationHttpClient> _logger;

    private const string apiKey = "AIzaSyB8nqHcKUPWcZKg-TI8z0TZOGOuVwAnsJY";
    
    public GeolocationHttpClient(HttpClient httpClient, ILogger<GeolocationHttpClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<(string formattedAddress, double latitude, double longitude)> GetCoordinates(string address)
    {
        var response = await this._httpClient.GetAsync($"maps/api/geocode/json?address={address}&key={apiKey}");
        response.EnsureSuccessStatusCode();

        var geolocationResult = JsonConvert.DeserializeObject<GeolocationResult>(await response.Content.ReadAsStringAsync());
        if (geolocationResult == null && geolocationResult.Results.Count > 0) throw new InvalidJsonException();

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