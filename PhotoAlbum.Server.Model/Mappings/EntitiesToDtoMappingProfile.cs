using AutoMapper;
using PhotoAlbum.Server.Dto;
using PhotoAlbum.Server.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoAlbum.Server.Model.Mappings
{
    public class EntitiesToDtoMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "EntitiesToDtoMappings"; }
        }

        public EntitiesToDtoMappingProfile()
        {
            CreateMap<Photo, PhotoDto>().ForMember()
        }
    }
}
