using MongoDB.Driver;

namespace Wardrobe.Domain.SeedWork.Repository;

public interface IRepositoryBase<TCollection> where TCollection : class
{
    Task<TCollection> Save(TCollection model);
    Task<TCollection> GetByIdAsync(string id);
    Task Upsert(string id, TCollection model);
    Task<IEnumerable<TCollection>> Get(FilterDefinition<TCollection> filter);
}