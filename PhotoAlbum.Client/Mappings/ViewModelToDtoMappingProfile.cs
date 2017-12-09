using AutoMapper;
using PhotoAlbum.Client.Dto;
using PhotoAlbum.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoAlbum.Client.Mappings
{
    public class ViewModelToDtoMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDtoMappings"; }
        }

        public ViewModelToDtoMappingProfile()
        {
            CreateMap<EditPhotoViewModel, EditPhotoDto>();
            CreateMap<RegisterViewModel, RegisterUserDto>();
            CreateMap<LoginViewModel, GetTokenDto>();
            CreateMap<PhotoVoteViewModel, PhotoVoteDto>();
        }
    }
}