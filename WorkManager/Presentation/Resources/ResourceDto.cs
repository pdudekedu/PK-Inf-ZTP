using AutoMapper;
using WorkManager.Persistence.Entities;

namespace WorkManager.Presentation.Resources
{
    public class ResourceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ResourceDtoMappingProfile : Profile
    {
        public ResourceDtoMappingProfile()
        {
            CreateMap<Resource, ResourceDto>();
        }
    }
}
