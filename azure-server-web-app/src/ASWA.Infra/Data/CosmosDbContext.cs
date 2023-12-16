using ASWA.Core.Containers;
using Microsoft.EntityFrameworkCore;

namespace ASWA.Infra.Data
{
    public class CosmosDbContext : DbContext
    {
        public DbSet<Template> Templates { get; set; }

        public CosmosDbContext(DbContextOptions<CosmosDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("Store");

            modelBuilder.Entity<Template>()
                .ToContainer("Template");

            modelBuilder.Entity<Template>()
                .HasNoDiscriminator();

            modelBuilder.Entity<Template>()
                .Property(x => x.Id)
                .ToJsonProperty("id");

            // modelBuilder.Entity<Template>()
            //     .HasPartitionKey(o => o.PartitionKey);

            //modelBuilder.Entity<Template>()
            //    .HasKey(x => x.Id);
        }
    }
}