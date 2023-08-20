using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Wardrobe.Domain.Entities.Forecast;

public record Condition(
    [property: JsonProperty("text")]
    [property: JsonPropertyName("text")] string Text,
    [property: JsonProperty("icon")]
    [property: JsonPropertyName("icon")] string Icon,
    [property: JsonProperty("code")]
    [property: JsonPropertyName("code")] int Code
);