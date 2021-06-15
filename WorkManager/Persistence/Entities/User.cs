
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace WorkManager.Persistence.Entities
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; } = UserRole.Worker;
        public ICollection<Team> Teams { get; set; }
        public ICollection<Project> Projects { get; set; }
    }

    public enum UserRole
    {
        Worker = 0,
        Manager = 1
    }

    public class UserModelConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "dbo");
            builder.HasKey(x => x.Id);
        }
    }
}
