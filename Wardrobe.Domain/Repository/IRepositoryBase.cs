namespace Wardrobe.Domain.Repository;

public interface IRepositoryBase<TCollection> where TCollection : class
{
    Task<TCollection> Save(TCollection model);
    Task<TCollection> GetByIdAsync(string id);
    Task Upsert(string id, TCollection model);
}