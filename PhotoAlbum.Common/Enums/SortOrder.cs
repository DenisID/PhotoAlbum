using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Common.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SortOrder
    {
        ByCreationDate = 0,
        ByRating = 1
    }
}
