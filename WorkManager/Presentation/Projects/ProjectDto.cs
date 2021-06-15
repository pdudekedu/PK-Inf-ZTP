using AutoMapper;
using System.Collections.Generic;
using WorkManager.Persistence.Entities;

namespace WorkManager.Presentation.Projects
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<NamesDto> Resources { get; set; }
        public NamesDto Team { get; set; }
    }

    public class ProjectDtoMappingProfile : Profile
    {
        public ProjectDtoMappingProfile()
        {
            CreateMap<Project, ProjectDto>()
                .ForMember(dest => dest.Resources, opt => opt.MapFrom<ResourcesResolver>())
                .ForMember(dest => dest.Team, opt => opt.MapFrom<TeamResolver>());
        }
    }
    public class ResourcesResolver : IValueResolver<Project, ProjectDto, IEnumerable<NamesDto>>
    {
        public IEnumerable<NamesDto> Resolve(Project source, ProjectDto destination, IEnumerable<NamesDto> destMember, ResolutionContext context)
        {
            if (source.Resources != null)
                foreach (var item in source.Resources)
                {
                    yield return new()
                    {
                        Name = item.Name,
                        Id = item.Id
                    };
                }

        }
    }
    public class TeamResolver : IValueResolver<Project, ProjectDto, NamesDto>
    {
        public NamesDto Resolve(Project source, ProjectDto destination, NamesDto destMember, ResolutionContext context)
        {
            return new()
            {
                Name = source.Team.Name,
                Id = source.Team.Id
            };
        }
    }
}
