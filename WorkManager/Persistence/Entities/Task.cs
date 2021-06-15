using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace WorkManager.Persistence.Entities
{
    public class Task : Entity
    {
        public int ProjectId { get; set; }
        public int? UserId { get; set; }
        public TaskState State { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? EstimateStart { get; set; }
        public DateTime? EstimateEnd { get; set; }
    }
    public enum TaskState
    {
        New,
        InProgress,
        Waiting,
        Done
    }
    public class TaskModelConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.ToTable("Tasks", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.State).HasConversion<int>();
        }
    }
}
