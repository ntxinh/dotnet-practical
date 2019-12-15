using Microsoft.EntityFrameworkCore;

namespace EfCosmos.Services.Api.Entities
{
    public class CosmosContext : DbContext
    {
        public DbSet<Template> Templates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseCosmos(
                    "https://YOUR_SERVICE.documents.azure.com:443/",
                    "YOUR_PRIMARY_KEY",
                    databaseName: "YOUR_DATABASE");

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