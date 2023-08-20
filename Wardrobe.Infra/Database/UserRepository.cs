using System.Globalization;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Wardrobe.Domain.Entities.User;
using Wardrobe.Domain.SeedWork.Repository;

namespace Wardrobe.Infra.Database;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(IOptions<MongoConnectionSettings> mongoConnectionSettings) 
        : base(mongoConnectionSettings, "user")
    {
    }

    public async Task<User?> GetByEmail(string email)
    {
        var emailFilter = Builders<User>.Filter
            .Eq(x => x.Email, email);
        IEnumerable<User?> result = await Get(emailFilter);
        return result.FirstOrDefault();
    }

    public async Task EditAddress(string userId, string address, double latitude, double longitude)
    {
        var updateAddress = Builders<User>.Update
            .Set(user => user.Localization,
                new Localization(address, 
                    latitude.ToString(CultureInfo.InvariantCulture),
                    longitude.ToString(CultureInfo.InvariantCulture)));
        
        await Update(userId, updateAddress);
    }
}

public interface IUserRepository : IRepositoryBase<User>
{
    Task<User?> GetByEmail(string email);
    Task EditAddress(string userId, string address, double latitude, double longitude);
}