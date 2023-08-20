using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Wardrobe.Domain.Entities.Forecast;

public record Location(
    [property: JsonProperty("name")]
    [property: JsonPropertyName("name")] string Name,
    [property: JsonProperty("region")]
    [property: JsonPropertyName("region")] string Region,
    [property: JsonProperty("country")]
    [property: JsonPropertyName("country")] string Country,
    [property: JsonProperty("lat")]
    [property: JsonPropertyName("lat")] double Lat,
    [property: JsonProperty("lon")]
    [property: JsonPropertyName("lon")] double Lon,
    [property: JsonProperty("tz_id")]
    [property: JsonPropertyName("tz_id")] string TzId,
    [property: JsonProperty("localtime_epoch")]
    [property: JsonPropertyName("localtime_epoch")] int LocaltimeEpoch,
    [property: JsonProperty("localtime")]
    [property: JsonPropertyName("localtime")] string Localtime
);