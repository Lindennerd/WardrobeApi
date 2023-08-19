using Wardrobe.Domain.SeedWork;

namespace Wardrobe.Domain.Identity;

public class Identity : EntityBase
{
    public User.User User { get; set; }
    public string Password { get; set; }
    public string Token { get; set; }
    public DateTime Validation { get; set; }
}