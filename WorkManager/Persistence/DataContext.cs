using Microsoft.EntityFrameworkCore;
using WorkManager.Persistence.Entities;

namespace WorkManager.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Resource> Resources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(user =>
            {
                user.Property(x => x.Role).HasConversion(d => (int)d, s => (UserRole)s);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
