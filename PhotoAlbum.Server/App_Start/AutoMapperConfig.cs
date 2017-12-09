using AutoMapper;
using PhotoAlbum.Server.Mappings;
using PhotoAlbum.Server.Model.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoAlbum.Server.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DtoToEntitiesMappingProfile>();
                x.AddProfile<EntitiesToDtoMappingProfile>();
                x.AddProfile<ModelsToDtoMappingProfile>();
            });
        }
    }
}