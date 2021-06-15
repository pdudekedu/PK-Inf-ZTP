using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace WorkManager.Persistence.Entities
{
    public class Project_Resource
    {
        public int ProjectId { get; set; }
        public int ResourceId { get; set; }
        public Project Project { get; set; }
        public Resource Resource { get; set; }
    }
    public class ProjectResourceModelConfiguration : IEntityTypeConfiguration<Project_Resource>
    {
        public void Configure(EntityTypeBuilder<Project_Resource> builder)
        {
            builder.ToTable("Project_Resource", "dbo");
            builder.HasKey(x => new { x.ProjectId, x.ResourceId });


        }
    }
}
