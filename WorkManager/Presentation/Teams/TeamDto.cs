using AutoMapper;
using System.Collections.Generic;
using WorkManager.Persistence.Entities;

namespace WorkManager.Presentation.Teams
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<UserDto> Users { get; set; }
    }

    public class TeamDtoMappingProfile : Profile
    {
        public TeamDtoMappingProfile()
        {
            CreateMap<Team, TeamDto>()
                .ForMember(dest => dest.Users, opt => opt.MapFrom<UserDtoResolver>());
        }
    }
    public class UserDtoResolver : IValueResolver<Team, TeamDto, IEnumerable<UserDto>>
    {
        public IEnumerable<UserDto> Resolve(Team source, TeamDto destination, IEnumerable<UserDto> destMember, ResolutionContext context)
        {
            foreach (var item in source.Users)
            {
                yield return new()
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Id = item.Id
                };
            }

        }
    }
}
