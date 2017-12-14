using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PhotoAlbum.Common.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SortOrder
    {
        ByCreationDate = 0,
        ByRating = 1
    }
}
