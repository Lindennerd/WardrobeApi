using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Wardrobe.Domain.Entities.Cloth;

public class Classification
{
    private readonly Dictionary<string, (BodyPart bodyPart, AppropriateWeather appropiateWeather)>
        _clothClassification = new() {
            {"dress", (BodyPart.Body, AppropriateWeather.Warm)},
            {"longsleeve", (BodyPart.Body, AppropriateWeather.Cold)},
            {"pants", (BodyPart.Legs,AppropriateWeather.Cold)},
            {"shoes", (BodyPart.Feet, AppropriateWeather.Both)},
            {"skirt", (BodyPart.Body, AppropriateWeather.Warm)},
            {"t-shirt", (BodyPart.Body, AppropriateWeather.Warm)},
            {"hat", (BodyPart.Head, AppropriateWeather.Both)},
            {"outwear", (BodyPart.Body, AppropriateWeather.Cold)},
            {"shirt", (BodyPart.Body, AppropriateWeather.Warm)},
            {"shorts", (BodyPart.Legs, AppropriateWeather.Warm)}
        };

    public Classification(string description, float confidence)
    {
        Description = description;
        Confidence = confidence;

        BodyPart = _clothClassification[description].bodyPart;
        AppropriateWeather = _clothClassification[description].appropiateWeather;
    }

    public string Description { get; set; }
    public float Confidence { get; set; }
    [BsonRepresentation(BsonType.String)]
    public AppropriateWeather AppropriateWeather { get; set; }
    [BsonRepresentation(BsonType.String)]
    public BodyPart BodyPart { get; set; }
}

