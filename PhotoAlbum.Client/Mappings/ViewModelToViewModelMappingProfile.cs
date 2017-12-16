using AutoMapper;
using PhotoAlbum.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoAlbum.Client.Mappings
{
    public class ViewModelToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToViewModelMappings"; }
        }

        public ViewModelToViewModelMappingProfile()
        {
            CreateMap<RegisterViewModel, LoginViewModel>().ReverseMap();
            CreateMap<EditUserInfoViewModel, EditUserProfileViewModel>().ReverseMap();
        }
    }
}