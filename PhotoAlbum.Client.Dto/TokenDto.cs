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
        //"access_token": "vRDBThDzRV870V_SkJ2r-YZvm7pfk98Mg2I4Jg5HxdFkps4LWnhQ370o_U6nBMERswOmMhmqO4IZMvwnVMbO5K80o2u4Fs5NNL5nWPkmTHm6HJh8x7vGuWIqmrN2ucOlT-vSZ-dP461q1l0gylhkGZ_fdMjpBrBLMh-WcaNnrxCg_fD7DikwWUBlnUSTAhZYMl1ya5-Awe6k0AklP6iiSJCoRfb0Wk4J0UA5d5OUIa6trzs9asO2e50u-reLorhS0Wqjv1-zGIQY1a0g3_cr0MOWibvE1kUHkc7rYNBISy-OHTZATmxe-EPJyP5vjqAn3Wx19gw6ORJiYCDtjBaD86ZASqnG6g5EhLNw8OGMZ_S50Flq9KNCPUuwLa8RrP4c1cuHJ8N6SZdHIXYcXqC_qZsFJdNe04MDh8YJDbd-R0apIuF-pq8kcQ8S4bFxIXmBMo3o_AU-1sWyKioJhA-7ZcwPUV78p1whwbHwPvlvS1c",
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        //"token_type": "bearer",
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        //"expires_in": 1209599,
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        //"userName": "Den@gmail.com",
        [JsonProperty("userName")]
        public string UserName { get; set; }

        //".issued": "Mon, 13 Nov 2017 08:48:19 GMT",
        [JsonProperty(".issued")]
        public DateTime Issued { get; set; }

        //".expires": "Mon, 27 Nov 2017 08:48:19 GMT"
        [JsonProperty(".expires")]
        public DateTime Expires { get; set; }
    }
}
