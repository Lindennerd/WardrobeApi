using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Wardrobe.Domain.Entities.Forecast;

public record Forecastday(
    [property: JsonProperty("date")]
    [property: JsonPropertyName("date")] string Date,
    [property: JsonProperty("date_epoch")]
    [property: JsonPropertyName("date_epoch")] int DateEpoch,
    [property: JsonProperty("day")]
    [property: JsonPropertyName("day")] Day Day,
    [property: JsonProperty("astro")]
    [property: JsonPropertyName("astro")] Astro Astro,
    [property: JsonProperty("hour")]
    [property: JsonPropertyName("hour")] IReadOnlyList<Hour> Hour
);