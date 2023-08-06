namespace Wardrobe.Domain.Image;

public class Image
{
    public Image(string url, string description)
    {
        Url = url;
        Description = description;
    }

    public string Url { get; private set; }
    public string Description { get; private set; }
    public bool Favorite { get; private set; } = false;
    public Classification? Classification { get; private set; }
    
    public void SetClassification(Classification classification)
    {
        Classification = classification;
    }
    
    public void SetFavorite(bool favorite)
    {
        Favorite = favorite;
    }
    
}