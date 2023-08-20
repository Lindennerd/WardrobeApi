using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Wardrobe.Domain.Entities.Cloth;

namespace Wardrobe.Infra.Database.Cloth;

public class ClothesRepository : RepositoryBase<Domain.Entities.Cloth.Cloth>, IClothesRepository
{
    public ClothesRepository(IOptions<MongoConnectionSettings> mongoConnectionSettings) 
        : base(mongoConnectionSettings, "cloth")
    {
    }

    public async Task UpdateClassification(string id, Classification classification)
    {
        if (string.IsNullOrEmpty(id) || classification == null)
            throw new ArgumentNullException();
        
        var updateDefinition = Builders<Domain.Entities.Cloth.Cloth>.Update
            .Set<Classification>(x => x.Classification, classification);
        await Update(id, updateDefinition);
    }

    public async Task<IEnumerable<Domain.Entities.Cloth.Cloth>> GetByUser(string user)
    {
        var userFilter = Builders<Domain.Entities.Cloth.Cloth>.Filter.Eq(c => c.Owner, user);
        return await Get(userFilter);
    }
}