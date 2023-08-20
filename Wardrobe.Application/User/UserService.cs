using Microsoft.Extensions.Logging;
using Wardrobe.Application.Geolocation;
using Wardrobe.Application.Weather;
using Wardrobe.Infra.Database;

namespace Wardrobe.Application.User;

public class UserService : IUserService
{
    private readonly IWeatherApiClient _weatherApiClient;
    private readonly IGeolocationHttpClient _geolocationHttpClient;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;

    public UserService(IWeatherApiClient weatherApiClient, 
        IGeolocationHttpClient geolocationHttpClient,
        IUserRepository userRepository,
        ILogger<UserService> logger)
    {
        _weatherApiClient = weatherApiClient;
        _geolocationHttpClient = geolocationHttpClient;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task EditAddress(string address, string userId)
    {
        var (formattedAddress, latitude, longitude) = await _geolocationHttpClient.GetCoordinates(address);
        await _userRepository.EditAddress(userId, formattedAddress, latitude, longitude);
    }
}

public interface IUserService
{
    Task EditAddress(string address, string userId);
}