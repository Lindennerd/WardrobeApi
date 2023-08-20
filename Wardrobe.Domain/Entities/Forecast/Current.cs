using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Wardrobe.Domain.Entities.Forecast;

public record Current(
    [property: JsonProperty("last_updated_epoch")]
    [property: JsonPropertyName("last_updated_epoch")] int LastUpdatedEpoch,
    [property: JsonProperty("last_updated")]
    [property: JsonPropertyName("last_updated")] string LastUpdated,
    [property: JsonProperty("temp_c")]
    [property: JsonPropertyName("temp_c")] int TempC,
    [property: JsonProperty("temp_f")]
    [property: JsonPropertyName("temp_f")] double TempF,
    [property: JsonProperty("is_day")]
    [property: JsonPropertyName("is_day")] int IsDay,
    [property: JsonProperty("condition")]
    [property: JsonPropertyName("condition")] Condition Condition,
    [property: JsonProperty("wind_mph")]
    [property: JsonPropertyName("wind_mph")] double WindMph,
    [property: JsonProperty("wind_kph")]
    [property: JsonPropertyName("wind_kph")] int WindKph,
    [property: JsonProperty("wind_degree")]
    [property: JsonPropertyName("wind_degree")] int WindDegree,
    [property: JsonProperty("wind_dir")]
    [property: JsonPropertyName("wind_dir")] string WindDir,
    [property: JsonProperty("pressure_mb")]
    [property: JsonPropertyName("pressure_mb")] int PressureMb,
    [property: JsonProperty("pressure_in")]
    [property: JsonPropertyName("pressure_in")] double PressureIn,
    [property: JsonProperty("precip_mm")]
    [property: JsonPropertyName("precip_mm")] int PrecipMm,
    [property: JsonProperty("precip_in")]
    [property: JsonPropertyName("precip_in")] int PrecipIn,
    [property: JsonProperty("humidity")]
    [property: JsonPropertyName("humidity")] int Humidity,
    [property: JsonProperty("cloud")]
    [property: JsonPropertyName("cloud")] int Cloud,
    [property: JsonProperty("feelslike_c")]
    [property: JsonPropertyName("feelslike_c")] int FeelslikeC,
    [property: JsonProperty("feelslike_f")]
    [property: JsonPropertyName("feelslike_f")] double FeelslikeF,
    [property: JsonProperty("vis_km")]
    [property: JsonPropertyName("vis_km")] int VisKm,
    [property: JsonProperty("vis_miles")]
    [property: JsonPropertyName("vis_miles")] int VisMiles,
    [property: JsonProperty("uv")]
    [property: JsonPropertyName("uv")] int Uv,
    [property: JsonProperty("gust_mph")]
    [property: JsonPropertyName("gust_mph")] double GustMph,
    [property: JsonProperty("gust_kph")]
    [property: JsonPropertyName("gust_kph")] double GustKph
);