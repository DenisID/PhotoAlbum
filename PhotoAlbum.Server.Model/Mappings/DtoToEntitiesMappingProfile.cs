using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Server.Model.Mappings
{
    public class DtoToEntitiesMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DtoToEntitiesMappings"; }
        }

        public DtoToEntitiesMappingProfile()
        {
        }
    }
}
