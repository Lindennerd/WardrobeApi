using Wardrobe.Domain.Cloth;
using Wardrobe.Domain.Repository;

namespace Wardrobe.Infra.Database.Cloth;

public interface IClothesRepository : IRepositoryBase<Domain.Cloth.Cloth>
{
    Task UpdateClassification(string id, Classification classification);
}