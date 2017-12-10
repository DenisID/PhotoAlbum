using AutoMapper;
using PhotoAlbum.Server.Dto;
using PhotoAlbum.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoAlbum.Server.Mappings
{
    public class DtoToModelsMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DtoToModelsMappings"; }
        }

        public DtoToModelsMappingProfile()
        {
            CreateMap<ChangeUserProfileDto, ChangeUserProfileBindingModel>();
        }
    }
}