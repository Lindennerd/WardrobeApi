using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Wardrobe.Domain.SeedWork.Repository;
using Wardrobe.Domain.User;

namespace Wardrobe.Infra.Database;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(IOptions<MongoConnectionSettings> mongoConnectionSettings) : base(mongoConnectionSettings)
    {
    }

    public async Task<User?> GetByEmail(string email)
    {
        var emailFilter = Builders<User>.Filter
            .Eq(x => x.Email, email);
        IEnumerable<User?> result = await Get(emailFilter);
        return result.FirstOrDefault();
    }
}

public interface IUserRepository : IRepositoryBase<User>
{
    Task<User?> GetByEmail(string email);
}