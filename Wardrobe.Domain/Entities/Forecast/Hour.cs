using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Wardrobe.Domain.Entities.Forecast;

public record Hour(
    [property: JsonProperty("time_epoch")]
    [property: JsonPropertyName("time_epoch")] int TimeEpoch,
    [property: JsonProperty("time")]
    [property: JsonPropertyName("time")] string Time,
    [property: JsonProperty("temp_c")]
    [property: JsonPropertyName("temp_c")] double TempC,
    [property: JsonProperty("temp_f")]
    [property: JsonPropertyName("temp_f")] double TempF,
    [property: JsonProperty("is_day")]
    [property: JsonPropertyName("is_day")] int IsDay,
    [property: JsonProperty("condition")]
    [property: JsonPropertyName("condition")] Condition Condition,
    [property: JsonProperty("wind_mph")]
    [property: JsonPropertyName("wind_mph")] double WindMph,
    [property: JsonProperty("wind_kph")]
    [property: JsonPropertyName("wind_kph")] double WindKph,
    [property: JsonProperty("wind_degree")]
    [property: JsonPropertyName("wind_degree")] int WindDegree,
    [property: JsonProperty("wind_dir")]
    [property: JsonPropertyName("wind_dir")] string WindDir,
    [property: JsonProperty("pressure_mb")]
    [property: JsonPropertyName("pressure_mb")] int PressureMb,
    [property: JsonProperty("pressure_in")]
    [property: JsonPropertyName("pressure_in")] double PressureIn,
    [property: JsonProperty("precip_mm")]
    [property: JsonPropertyName("precip_mm")] double PrecipMm,
    [property: JsonProperty("precip_in")]
    [property: JsonPropertyName("precip_in")] double PrecipIn,
    [property: JsonProperty("humidity")]
    [property: JsonPropertyName("humidity")] int Humidity,
    [property: JsonProperty("cloud")]
    [property: JsonPropertyName("cloud")] int Cloud,
    [property: JsonProperty("feelslike_c")]
    [property: JsonPropertyName("feelslike_c")] double FeelslikeC,
    [property: JsonProperty("feelslike_f")]
    [property: JsonPropertyName("feelslike_f")] double FeelslikeF,
    [property: JsonProperty("windchill_c")]
    [property: JsonPropertyName("windchill_c")] double WindchillC,
    [property: JsonProperty("windchill_f")]
    [property: JsonPropertyName("windchill_f")] double WindchillF,
    [property: JsonProperty("heatindex_c")]
    [property: JsonPropertyName("heatindex_c")] double HeatindexC,
    [property: JsonProperty("heatindex_f")]
    [property: JsonPropertyName("heatindex_f")] double HeatindexF,
    [property: JsonProperty("dewpoint_c")]
    [property: JsonPropertyName("dewpoint_c")] double DewpointC,
    [property: JsonProperty("dewpoint_f")]
    [property: JsonPropertyName("dewpoint_f")] double DewpointF,
    [property: JsonProperty("will_it_rain")]
    [property: JsonPropertyName("will_it_rain")] int WillItRain,
    [property: JsonProperty("chance_of_rain")]
    [property: JsonPropertyName("chance_of_rain")] int ChanceOfRain,
    [property: JsonProperty("will_it_snow")]
    [property: JsonPropertyName("will_it_snow")] int WillItSnow,
    [property: JsonProperty("chance_of_snow")]
    [property: JsonPropertyName("chance_of_snow")] int ChanceOfSnow,
    [property: JsonProperty("vis_km")]
    [property: JsonPropertyName("vis_km")] int VisKm,
    [property: JsonProperty("vis_miles")]
    [property: JsonPropertyName("vis_miles")] int VisMiles,
    [property: JsonProperty("gust_mph")]
    [property: JsonPropertyName("gust_mph")] double GustMph,
    [property: JsonProperty("gust_kph")]
    [property: JsonPropertyName("gust_kph")] double GustKph,
    [property: JsonProperty("uv")]
    [property: JsonPropertyName("uv")] int Uv
);