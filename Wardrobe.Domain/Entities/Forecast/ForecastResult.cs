using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Wardrobe.Domain.Entities.Forecast;

public record ForecastResult(
    [property: JsonProperty("location")]
    [property: JsonPropertyName("location")] Location Location,
    [property: JsonProperty("current")]
    [property: JsonPropertyName("current")] Current Current,
    [property: JsonProperty("forecast")]
    [property: JsonPropertyName("forecast")] Forecast Forecast
);