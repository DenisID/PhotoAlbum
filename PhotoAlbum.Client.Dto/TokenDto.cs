using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Client.Dto
{
    public class TokenDto
    {
        //"access_token": "vRDBThDzRV870V..."
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        //"token_type": "bearer"
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        //"expires_in": 1209599
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        //"userName": "Den"
        [JsonProperty("userName")]
        public string UserName { get; set; }

        //".issued": "Mon, 13 Nov 2017 08:48:19 GMT"
        [JsonProperty(".issued")]
        public DateTime Issued { get; set; }

        //".expires": "Mon, 27 Nov 2017 08:48:19 GMT"
        [JsonProperty(".expires")]
        public DateTime Expires { get; set; }
    }
}
