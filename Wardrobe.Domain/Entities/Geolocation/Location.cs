using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Wardrobe.Domain.Entities.Geolocation;

public record Location(
    [property: JsonProperty("lat")]
    [property: JsonPropertyName("lat")] double Lat,
    [property: JsonProperty("lng")]
    [property: JsonPropertyName("lng")] double Lng
);