using WorkManager.Persistence.Entities;

namespace WorkManager.Presentation.Authorization
{
    public class LoginResponseDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public string Token { get; set; }
    }
}
