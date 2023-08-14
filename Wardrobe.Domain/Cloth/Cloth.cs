using Wardrobe.Domain.SeedWork;

namespace Wardrobe.Domain.Cloth;

public class Cloth : EntityBase
{
    public string? Name { get; set; }
    public string? MimeType { get; set; }
    public string? Image { get; set; }
    public Classification? Classification { get; set; }

    public void SetClassification(string description, float confidence)
    {
        Classification = new Classification(description, confidence);
    }
}