using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Wardrobe.Infra.Database.Cloth;

public class ClothesRepository : RepositoryBase<ClothesModel>
{
    public ClothesRepository(IOptions<MongoConnectionSettings> mongoConnectionSettings) 
        : base(mongoConnectionSettings)
    {
    }
}