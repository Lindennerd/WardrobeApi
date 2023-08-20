using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Wardrobe.Domain.Entities.Geolocation;

public record Geometry(
    [property: JsonProperty("location")]
    [property: JsonPropertyName("location")] Location Location,
    [property: JsonProperty("location_type")]
    [property: JsonPropertyName("location_type")] string LocationType,
    [property: JsonProperty("viewport")]
    [property: JsonPropertyName("viewport")] Viewport Viewport
);