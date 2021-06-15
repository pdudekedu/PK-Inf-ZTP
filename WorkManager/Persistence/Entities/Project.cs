using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace WorkManager.Persistence.Entities
{
    public class Project : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int TeamId { get; set; }
        public List<Resource> Resources { get; set; }
        public Team Team { get; set; }
        public int? UserId { get; set; }
    }
    public class ProjectModelConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects", "dbo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);

            builder.HasMany(x => x.Resources).WithMany(x => x.Projects).UsingEntity<Project_Resource>(
                x => x.HasOne(x => x.Resource).WithMany().HasForeignKey(x => x.ResourceId),
                x => x.HasOne(x => x.Project).WithMany().HasForeignKey(x => x.ProjectId));

            builder.HasOne(x => x.Team).WithMany().HasForeignKey(x => x.TeamId);
        }
    }
}
