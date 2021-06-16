using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace WorkManager.Persistence.Entities
{
    public class TaskTime
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public DateTime DateTime { get; set; }
        public TaskTimeType Type { get; set; }
        public Task Task { get; set; }
    }
    public enum TaskTimeType
    {
        Start = 0,
        Suspend = 1,
        Resume = 2,
        End = 3
    }
    public class TaskTimeModelConfiguration : IEntityTypeConfiguration<TaskTime>
    {
        public void Configure(EntityTypeBuilder<TaskTime> builder)
        {
            builder.ToTable("TaskTimes", "dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type).HasConversion<int>();
        }
    }
}
