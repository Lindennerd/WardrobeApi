using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Wardrobe.Domain.SeedWork;

public class EntityBase
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
}