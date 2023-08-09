using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Wardrobe.Domain.Repository;

namespace Wardrobe.Infra.Database.Cloth;

public class ClothesRepository : RepositoryBase<Domain.Cloth.Cloth>, IClothesRepository
{
    public ClothesRepository(IOptions<MongoConnectionSettings> mongoConnectionSettings) 
        : base(mongoConnectionSettings)
    {
    }

    public async Task UpdateClassification(string id, string classification, float confidence)
    {
        var updateDefinition = Builders<Domain.Cloth.Cloth>.Update
            .Set(x => x.Classification, classification)
            .Set(x => x.Confidence, confidence);
        await Update(id, updateDefinition);
    }
}