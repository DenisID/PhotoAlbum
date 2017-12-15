using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Client.Dto
{
    //[DataContract]
    public class WebApiResponseDto<T>
    {
        //[DataMember]
        public int StatusCode { get; set; }

        //[DataMember(EmitDefaultValue = false)]
        public string ErrorMessage { get; set; }

        //[DataMember(EmitDefaultValue = false)]
        public T Result { get; set; }
    }
}
