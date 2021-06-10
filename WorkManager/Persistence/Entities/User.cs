namespace WorkManager.Persistence.Entities
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; } = UserRole.Worker;
    }

    public enum UserRole
    {
        Worker = 0,
        Manager = 1
    }
}
