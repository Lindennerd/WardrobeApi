using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Wardrobe.Domain.Entities.Geolocation;

public record Result(
    [property: JsonProperty("address_components")]
    [property: JsonPropertyName("address_components")] IReadOnlyList<AddressComponent> AddressComponents,
    [property: JsonProperty("formatted_address")]
    [property: JsonPropertyName("formatted_address")] string FormattedAddress,
    [property: JsonProperty("geometry")]
    [property: JsonPropertyName("geometry")] Geometry Geometry,
    [property: JsonProperty("place_id")]
    [property: JsonPropertyName("place_id")] string PlaceId,
    [property: JsonProperty("plus_code")]
    [property: JsonPropertyName("plus_code")] PlusCode PlusCode,
    [property: JsonProperty("types")]
    [property: JsonPropertyName("types")] IReadOnlyList<string> Types
);