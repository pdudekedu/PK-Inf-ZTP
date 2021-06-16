using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManager.Persistence.Entities
{
    public class ProjectStatistic
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int New { get; set; }
        public int Active { get; set; }
        public int Suspend { get; set; }
        public int Complete { get; set; }
        public int State { get; set; }
        public string Team { get; set; }
        public TimeSpan WorkTime { get; set; }
        public TimeSpan EstimateWorkTime { get; set; }
        public decimal Punctuality { get; set; }
    }
    public class ProjectStatisticModelConfiguration : IEntityTypeConfiguration<ProjectStatistic>
    {
        public void Configure(EntityTypeBuilder<ProjectStatistic> builder)
        {
            builder.ToTable("ProjectsStatistics", "dbo");
            builder.HasNoKey();

            builder.Property(x => x.WorkTime).HasConversion(x => (int)x.TotalSeconds, x => TimeSpan.FromSeconds(x));
            builder.Property(x => x.EstimateWorkTime).HasConversion(x => (int)x.TotalSeconds, x => TimeSpan.FromSeconds(x));


        }
    }
}
