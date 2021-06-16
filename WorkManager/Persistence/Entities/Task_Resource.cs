using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkManager.Persistence.Entities
{
    public class Task_Resource
    {
        public int TaskId { get; set; }
        public int ResourceId { get; set; }
        public Task Task { get; set; }
        public Resource Resource { get; set; }
    }
    public class TaskResourceModelConfiguration : IEntityTypeConfiguration<Task_Resource>
    {
        public void Configure(EntityTypeBuilder<Task_Resource> builder)
        {
            builder.ToTable("Task_Resource", "dbo");
            builder.HasKey(x => new { x.TaskId, x.ResourceId });


        }
    }
}
