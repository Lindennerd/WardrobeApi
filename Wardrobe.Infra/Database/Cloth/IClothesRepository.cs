using Wardrobe.Domain.Entities.Cloth;
using Wardrobe.Domain.SeedWork.Repository;

namespace Wardrobe.Infra.Database.Cloth;

public interface IClothesRepository : IRepositoryBase<Domain.Entities.Cloth.Cloth>
{
    Task UpdateClassification(string id, Classification classification);
    Task<IEnumerable<Domain.Entities.Cloth.Cloth>> GetByUser(string user);
    Task<IEnumerable<Domain.Entities.Cloth.Cloth>> GetByTemperature(string user, AppropriateWeather weather);
}