using AutoMapper;
using PhotoAlbum.Server.Mappings;
using PhotoAlbum.Server.Model.Mappings;

namespace PhotoAlbum.Server.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                // Server
                x.AddProfile<DtoToEntitiesMappingProfile>();
                x.AddProfile<EntitiesToDtoMappingProfile>();
                x.AddProfile<ClassesToDtoMappingProfile>();

                // Model
                x.AddProfile<ModelsToDtoMappingProfile>();
            });
        }
    }
}