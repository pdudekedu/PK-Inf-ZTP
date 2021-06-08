using AutoMapper;
using WorkManager.Persistence.Entities;

namespace WorkManager.Presentation.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
    }

    public class UserDtoMappingProfile : Profile
    {
        public UserDtoMappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
