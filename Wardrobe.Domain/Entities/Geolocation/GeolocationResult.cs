using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Wardrobe.Domain.Entities.Geolocation;

public record GeolocationResult(
    [property: JsonProperty("results")]
    [property: JsonPropertyName("results")] IReadOnlyList<Result> Results,
    [property: JsonProperty("status")]
    [property: JsonPropertyName("status")] string Status
);