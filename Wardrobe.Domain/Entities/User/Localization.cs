namespace Wardrobe.Domain.Entities.User;

public class Localization
{
    public Localization(string address, string latitude, string longitude)
    {
        FullAddress = address;
        Latitude = latitude;
        Longitude = longitude;
    }

    public string Latitude { get;  set; }
    public string Longitude { get;  set; }
    public string FullAddress { get; set; }
}