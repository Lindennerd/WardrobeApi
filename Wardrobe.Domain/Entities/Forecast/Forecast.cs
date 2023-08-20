using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Wardrobe.Domain.Entities.Forecast;

public record Forecast(
    [property: JsonProperty("forecastday")]
    [property: JsonPropertyName("forecastday")] IReadOnlyList<Forecastday> Forecastday
);