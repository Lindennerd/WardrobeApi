using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Wardrobe.Domain.Entities.Geolocation;

public record Viewport(
    [property: JsonProperty("northeast")]
    [property: JsonPropertyName("northeast")] Northeast Northeast,
    [property: JsonProperty("southwest")]
    [property: JsonPropertyName("southwest")] Southwest Southwest
);