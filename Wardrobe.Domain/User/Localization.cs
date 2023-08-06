namespace Wardrobe.Domain.User;

public class Localization
{
    public Localization(string latitude, string longitude, string fullAddress)
    {
        Latitude = latitude;
        Longitude = longitude;
        FullAddress = fullAddress;
    }

    public string Latitude { get; private set; }
    public string Longitude { get; private set; }
    public string FullAddress { get; private set; }
}