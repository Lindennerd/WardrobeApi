using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Wardrobe.Infra.Database.Cloth;

public class ClothesModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Image { get; set; }
    public string Classification { get; set; }
    public float Confidence { get; set; }
}