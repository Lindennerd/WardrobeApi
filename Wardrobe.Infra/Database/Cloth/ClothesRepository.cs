using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Wardrobe.Domain.Cloth;
using Wardrobe.Domain.Repository;

namespace Wardrobe.Infra.Database.Cloth;

public class ClothesRepository : RepositoryBase<Domain.Cloth.Cloth>, IClothesRepository
{
    public ClothesRepository(IOptions<MongoConnectionSettings> mongoConnectionSettings) 
        : base(mongoConnectionSettings)
    {
    }

    public async Task UpdateClassification(string id, Classification classification)
    {
        if (string.IsNullOrEmpty(id) || classification == null)
            throw new ArgumentNullException();
        
        var updateDefinition = Builders<Domain.Cloth.Cloth>.Update
            .Set<Classification>(x => x.Classification, classification);
        await Update(id, updateDefinition);
    }
}