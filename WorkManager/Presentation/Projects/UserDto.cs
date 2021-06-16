using AutoMapper;
using WorkManager.Persistence.Entities;

namespace WorkManager.Presentation.Projects
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    class UserDtoMappingProfile : Profile
    {
        public UserDtoMappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
