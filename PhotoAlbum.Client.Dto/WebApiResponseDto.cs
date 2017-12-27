using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Client.Dto
{
    public class WebApiResponseDto<T>
    {
        public int StatusCode { get; set; }
        
        public string ErrorMessage { get; set; }
        
        public T Result { get; set; }
    }
}
