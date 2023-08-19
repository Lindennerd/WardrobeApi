using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Wardrobe.Domain.Cloth;

public class Classification
{
    private readonly Dictionary<string, (BodyPart bodyPart, AppropriateWeather appropiateWeather)>
        _clothClassification = new() {
            {"dress", (BodyPart.Body, Domain.Cloth.AppropriateWeather.Warm)},
            {"longsleeve", (BodyPart.Body, Domain.Cloth.AppropriateWeather.Cold)},
            {"pants", (BodyPart.Legs,Domain.Cloth.AppropriateWeather.Cold)},
            {"shoes", (BodyPart.Feet, Domain.Cloth.AppropriateWeather.Both)},
            {"skirt", (BodyPart.Body, Domain.Cloth.AppropriateWeather.Warm)},
            {"t-shirt", (BodyPart.Body, Domain.Cloth.AppropriateWeather.Warm)},
            {"hat", (BodyPart.Head, Domain.Cloth.AppropriateWeather.Both)},
            {"outwear", (BodyPart.Body, Domain.Cloth.AppropriateWeather.Cold)},
            {"shirt", (BodyPart.Body, Domain.Cloth.AppropriateWeather.Warm)},
            {"shorts", (BodyPart.Legs, Domain.Cloth.AppropriateWeather.Warm)}
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

