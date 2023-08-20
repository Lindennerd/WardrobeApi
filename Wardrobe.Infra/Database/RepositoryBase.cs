using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Wardrobe.Domain.SeedWork;
using Wardrobe.Domain.SeedWork.Repository;

namespace Wardrobe.Infra.Database;

public abstract class RepositoryBase<TCollection> : IRepositoryBase<TCollection>
    where TCollection : EntityBase,  new()
{
    private readonly IMongoCollection<TCollection> _collection;

    protected RepositoryBase(IOptions<MongoConnectionSettings> mongoConnectionSettings, string collectionName)
    {
        var mongoClient = new MongoClient(mongoConnectionSettings.Value.ConnectionString);
        var database = mongoClient.GetDatabase(mongoConnectionSettings.Value.DatabaseName);
        _collection = database.GetCollection<TCollection>(collectionName);
    }

    public async Task<TCollection> Save(TCollection model)
    {
        await _collection.InsertOneAsync(model);
        return model;
    }
    
    public async Task<TCollection> GetByIdAsync(string id)
    {
        var filter = Builders<TCollection>.Filter.Eq("_id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TCollection>> Get(FilterDefinition<TCollection> filter)
    {
        var result =  await _collection.FindAsync(filter);
        return await result.ToListAsync();
    }
    
    public async Task Upsert(string id, TCollection model)
    {
        var filter = Builders<TCollection>.Filter.Eq("_id", id);
        await _collection.ReplaceOneAsync(filter, model, new ReplaceOptions { IsUpsert = true });
    }

    protected async Task Update(string id, UpdateDefinition<TCollection> updateDefinition)
    {
        var filter = Builders<TCollection>.Filter.Eq(e => e.Id, id);
        await _collection.FindOneAndUpdateAsync(filter, updateDefinition);
    }
}