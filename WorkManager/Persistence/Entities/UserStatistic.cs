using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace WorkManager.Persistence.Entities
{
    public class UserStatistic
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public TimeSpan WorkTime { get; set; }
        public TimeSpan EstimateWorkTime { get; set; }
        public decimal Punctuality { get; set; }
        public int TaskCount { get; set; }
        public int ProjectCount { get; set; }
    }
    public class UserStatisticModelConfiguration : IEntityTypeConfiguration<UserStatistic>
    {
        public void Configure(EntityTypeBuilder<UserStatistic> builder)
        {
            builder.ToTable("UsersStatistics", "dbo");
            builder.HasNoKey();

            builder.Property(x => x.WorkTime).HasConversion(x => (int)x.TotalSeconds, x => TimeSpan.FromSeconds(x));
            builder.Property(x => x.EstimateWorkTime).HasConversion(x => (int)x.TotalSeconds, x => TimeSpan.FromSeconds(x));
        }
    }
}
