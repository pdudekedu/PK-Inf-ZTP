using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

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
        public User User { get; set; }
        public List<Resource> Resources { get; set; }
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

            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.Resources).WithMany(x => x.Tasks).UsingEntity<Task_Resource>(
                x => x.HasOne(x => x.Resource).WithMany().HasForeignKey(x => x.ResourceId),
                x => x.HasOne(x => x.Task).WithMany().HasForeignKey(x => x.TaskId));
        }
    }
}
