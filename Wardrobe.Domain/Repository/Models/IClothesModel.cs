namespace Wardrobe.Domain.Repository.Models;

public interface IClothesModel
{
    public string Name { get; set; }
    public string MimeType { get; set; }
    public string Image { get; set; }
    public string? Classification { get; set; }
    public float Confidence { get; set; }
}