using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace WorkManager.Persistence.Entities
{
    public class Team : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<User> Users { get; set; }
    }
    public class TeamModelConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable("Teams", "dbo");
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Users).WithMany(x => x.Teams).UsingEntity<Team_User>(
                x => x.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId),
                x => x.HasOne(x => x.Team).WithMany().HasForeignKey(x => x.TeamId));

        }
    }
}
