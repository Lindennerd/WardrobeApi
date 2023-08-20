using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Wardrobe.Domain.Entities.Geolocation;

public record PlusCode(
    [property: JsonProperty("compound_code")]
    [property: JsonPropertyName("compound_code")] string CompoundCode,
    [property: JsonProperty("global_code")]
    [property: JsonPropertyName("global_code")] string GlobalCode
);