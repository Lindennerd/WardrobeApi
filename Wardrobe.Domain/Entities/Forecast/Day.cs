using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Wardrobe.Domain.Entities.Forecast;

public record Day(
    [property: JsonProperty("maxtemp_c")]
    [property: JsonPropertyName("maxtemp_c")] double MaxtempC,
    [property: JsonProperty("maxtemp_f")]
    [property: JsonPropertyName("maxtemp_f")] double MaxtempF,
    [property: JsonProperty("mintemp_c")]
    [property: JsonPropertyName("mintemp_c")] double MintempC,
    [property: JsonProperty("mintemp_f")]
    [property: JsonPropertyName("mintemp_f")] double MintempF,
    [property: JsonProperty("avgtemp_c")]
    [property: JsonPropertyName("avgtemp_c")] double AvgtempC,
    [property: JsonProperty("avgtemp_f")]
    [property: JsonPropertyName("avgtemp_f")] double AvgtempF,
    [property: JsonProperty("maxwind_mph")]
    [property: JsonPropertyName("maxwind_mph")] double MaxwindMph,
    [property: JsonProperty("maxwind_kph")]
    [property: JsonPropertyName("maxwind_kph")] double MaxwindKph,
    [property: JsonProperty("totalprecip_mm")]
    [property: JsonPropertyName("totalprecip_mm")] double TotalprecipMm,
    [property: JsonProperty("totalprecip_in")]
    [property: JsonPropertyName("totalprecip_in")] double TotalprecipIn,
    [property: JsonProperty("totalsnow_cm")]
    [property: JsonPropertyName("totalsnow_cm")] int TotalsnowCm,
    [property: JsonProperty("avgvis_km")]
    [property: JsonPropertyName("avgvis_km")] double AvgvisKm,
    [property: JsonProperty("avgvis_miles")]
    [property: JsonPropertyName("avgvis_miles")] int AvgvisMiles,
    [property: JsonProperty("avghumidity")]
    [property: JsonPropertyName("avghumidity")] int Avghumidity,
    [property: JsonProperty("daily_will_it_rain")]
    [property: JsonPropertyName("daily_will_it_rain")] int DailyWillItRain,
    [property: JsonProperty("daily_chance_of_rain")]
    [property: JsonPropertyName("daily_chance_of_rain")] int DailyChanceOfRain,
    [property: JsonProperty("daily_will_it_snow")]
    [property: JsonPropertyName("daily_will_it_snow")] int DailyWillItSnow,
    [property: JsonProperty("daily_chance_of_snow")]
    [property: JsonPropertyName("daily_chance_of_snow")] int DailyChanceOfSnow,
    [property: JsonProperty("condition")]
    [property: JsonPropertyName("condition")] Condition Condition,
    [property: JsonProperty("uv")]
    [property: JsonPropertyName("uv")] int Uv
);