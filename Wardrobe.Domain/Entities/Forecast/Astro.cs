using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Wardrobe.Domain.Entities.Forecast;

public record Astro(
    [property: JsonProperty("sunrise")]
    [property: JsonPropertyName("sunrise")] string Sunrise,
    [property: JsonProperty("sunset")]
    [property: JsonPropertyName("sunset")] string Sunset,
    [property: JsonProperty("moonrise")]
    [property: JsonPropertyName("moonrise")] string Moonrise,
    [property: JsonProperty("moonset")]
    [property: JsonPropertyName("moonset")] string Moonset,
    [property: JsonProperty("moon_phase")]
    [property: JsonPropertyName("moon_phase")] string MoonPhase,
    [property: JsonProperty("moon_illumination")]
    [property: JsonPropertyName("moon_illumination")] string MoonIllumination,
    [property: JsonProperty("is_moon_up")]
    [property: JsonPropertyName("is_moon_up")] int IsMoonUp,
    [property: JsonProperty("is_sun_up")]
    [property: JsonPropertyName("is_sun_up")] int IsSunUp
);