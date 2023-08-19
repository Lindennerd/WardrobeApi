using Wardrobe.Domain.SeedWork;

namespace Wardrobe.Domain.User;

public class User : EntityBase
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public Localization? Localization { get; private set; }
    
    public void SetLocalization(Localization localization)
    {
        Localization = localization;
    }
}