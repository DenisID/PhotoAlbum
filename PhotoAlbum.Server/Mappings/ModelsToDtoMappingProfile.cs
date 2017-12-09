using AutoMapper;
using PhotoAlbum.Server.Dto;
using PhotoAlbum.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoAlbum.Server.Mappings
{
    public class ModelsToDtoMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ModelsToDtoMappings"; }
        }

        public ModelsToDtoMappingProfile()
        {
            CreateMap<RegisterBindingModel, CreateUserInfoDto>();
        }
    }
}