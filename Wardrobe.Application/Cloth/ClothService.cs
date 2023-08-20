using Wardrobe.Infra.Database.Cloth;

namespace Wardrobe.Application.Cloth;

public class ClothService : IClothService
{
    private readonly IClothesRepository _clothesRepository;

    public ClothService(IClothesRepository clothesRepository)
    {
        _clothesRepository = clothesRepository;
    }

    public async Task<IEnumerable<Domain.Entities.Cloth.Cloth>> GetClothesForUser(string user)
    {
        return await _clothesRepository.GetByUser(user);
    }
}

public interface IClothService
{
    Task<IEnumerable<Domain.Entities.Cloth.Cloth>> GetClothesForUser(string user);
}