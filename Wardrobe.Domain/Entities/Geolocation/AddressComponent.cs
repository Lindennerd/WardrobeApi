using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Wardrobe.Domain.Entities.Geolocation;

public record AddressComponent(
    [property: JsonProperty("long_name")]
    [property: JsonPropertyName("long_name")] string LongName,
    [property: JsonProperty("short_name")]
    [property: JsonPropertyName("short_name")] string ShortName,
    [property: JsonProperty("types")]
    [property: JsonPropertyName("types")] IReadOnlyList<string> Types
);