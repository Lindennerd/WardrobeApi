using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Wardrobe.Domain.Repository;

namespace Wardrobe.Infra.Database;

public class ModelBase : IModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
}