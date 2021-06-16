using AutoMapper;
using WorkManager.Persistence.Entities;

namespace WorkManager.Presentation.Projects
{
    public class NamesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    class ResourceNamesDtoMappingProfile : Profile
    {
        public ResourceNamesDtoMappingProfile()
        {
            CreateMap<Resource, NamesDto>();
        }
    }
}
