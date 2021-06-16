using AutoMapper;
using WorkManager.Persistence.Entities;

namespace WorkManager.Presentation.Users
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ProjectDtoMappingProfile : Profile
    {
        public ProjectDtoMappingProfile()
        {
            CreateMap<Project, ProjectDto>();
        }
    }
}
