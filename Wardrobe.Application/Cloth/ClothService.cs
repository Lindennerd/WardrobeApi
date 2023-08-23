using System.Linq;
using System.Collections;
using Wardrobe.Domain.Entities.Cloth;
using Wardrobe.Infra.Database;
using Wardrobe.Infra.Database.Cloth;
using Wardrobe.Infra.HttpClients;

namespace Wardrobe.Application.Cloth;

public class ClothService : IClothService
{
    private readonly IClothesRepository _clothesRepository;
    private readonly IUserRepository _userRepository;
    private readonly IWeatherApiClient _weatherApiClient;

    public ClothService(
        IClothesRepository clothesRepository,
        IUserRepository userRepository,
        IWeatherApiClient weatherApiClient)
    {
        _clothesRepository = clothesRepository;
        _userRepository = userRepository;
        _weatherApiClient = weatherApiClient;
    }

    public async Task<IEnumerable<Domain.Entities.Cloth.Cloth>> GetClothesForUser(string user)
    {
        return await _clothesRepository.GetByUser(user);
    }

    public async Task<IEnumerable<Domain.Entities.Cloth.Cloth>> GetClothesForToday(string user)
    {
        var userInfo = await _userRepository.GetByIdAsync(user);
        if (userInfo.Localization == null) return Enumerable.Empty<Domain.Entities.Cloth.Cloth>();
        
        var forecast =
            await _weatherApiClient.GetForecast(
                userInfo.Localization.Latitude,
                userInfo.Localization.Longitude, 1);
        var avgTemperature = forecast.Forecast.Forecastday.Average(x => x.Day.AvgtempC);

        return await _clothesRepository.GetByTemperature(user,
            avgTemperature > 19 ? AppropriateWeather.Warm : AppropriateWeather.Cold);
    }
}

public interface IClothService
{
    Task<IEnumerable<Domain.Entities.Cloth.Cloth>> GetClothesForUser(string user);
    Task<IEnumerable<Domain.Entities.Cloth.Cloth>> GetClothesForToday(string user);
}