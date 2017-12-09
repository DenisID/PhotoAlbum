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
    public class DtoToEntitiesMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DtoToEntitiesMappings"; }
        }

        public DtoToEntitiesMappingProfile()
        {
            CreateMap<PhotoVoteDto, PhotoVote>();
            CreateMap<CreatePhotoDto, Photo>();
            CreateMap<CreatePhotoDto, PhotoContent>();
        }
    }
}
