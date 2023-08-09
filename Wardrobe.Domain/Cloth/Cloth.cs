using Wardrobe.Domain.SeedWork;

namespace Wardrobe.Domain.Cloth;

public class Cloth : EntityBase
{
    public string? Name { get; set; }
    public string? MimeType { get; set; }
    public string? Image { get; set; }
    public string? Classification { get; set; }
    public float Confidence { get; set; }
}