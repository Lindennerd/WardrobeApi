namespace Wardrobe.Domain.User;

public class User
{
    public User(string name, string email, string phone)
    {
        Name = name;
        Email = email;
        Phone = phone;
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public Localization? Localization { get; private set; }
    
    public void SetLocalization(Localization localization)
    {
        Localization = localization;
    }
}