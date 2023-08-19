using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Wardrobe.Domain.Identity;
using Wardrobe.Domain.SeedWork.Repository;

namespace Wardrobe.Infra.Database;

public class IdentityRepository : RepositoryBase<Identity>, IIdentityRepository
{
    public IdentityRepository(IOptions<MongoConnectionSettings> mongoConnectionSettings) : base(mongoConnectionSettings)
    {
    }

    public async Task<Identity?> GetUserIdentity(string userId)
    {
        var userFilter = Builders<Identity>.Filter
            .Eq(u => u.User.Id, userId);
        IEnumerable<Identity?> userIdentity = await Get(userFilter);
        return userIdentity.FirstOrDefault();
    }
}

public interface IIdentityRepository : IRepositoryBase<Identity>
{
    Task<Identity?> GetUserIdentity(string userId);
}