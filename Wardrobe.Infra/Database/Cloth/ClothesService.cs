using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Wardrobe.Infra.Database.Cloth;

public class ClothesService
{
    private readonly IMongoCollection<ClothesModel> _clothesCollection;

    public ClothesService(IOptions<MongoConnectionSettings> mongoConnectionSettings)
    {
        var mongoClient = new MongoClient(mongoConnectionSettings.Value.ConnectionString);
        var database = mongoClient.GetDatabase(mongoConnectionSettings.Value.DatabaseName);
        _clothesCollection = database.GetCollection<ClothesModel>(mongoConnectionSettings.Value.CollectionName);
    }
    
    public async Task SaveClothes(ClothesModel clothesModel)
    {
        await _clothesCollection.InsertOneAsync(clothesModel);
    }
}