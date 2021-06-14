using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace WorkManager.Persistence.Entities
{
    public class Team_User
    {
        public int TeamId { get; set; }
        public int UserId { get; set; }
        public Team Team { get; set; }
        public User User { get; set; }
    }
    public class TeamUserModelConfiguration : IEntityTypeConfiguration<Team_User>
    {
        public void Configure(EntityTypeBuilder<Team_User> builder)
        {
            builder.ToTable("Team_User", "dbo");
            builder.HasKey(x => new { x.TeamId, x.UserId });
        }
    }
}
