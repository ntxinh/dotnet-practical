using Microsoft.EntityFrameworkCore;

namespace EfCosmos.Services.Api.Entities
{
    public class CosmosContext : DbContext
    {
        public DbSet<Template> Templates { get; set; }

        public CosmosContext(DbContextOptions<CosmosContext> options) : base(options)
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
                .HasPartitionKey(o => o.PartitionKey);
        }
    }
}