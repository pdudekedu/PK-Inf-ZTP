using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace WorkManager.Persistence.Entities
{
    public class Resource : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
    public class ResourceModelConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.ToTable("Resources", "dbo");
            builder.HasKey(x => x.Id);


        }
    }
}
