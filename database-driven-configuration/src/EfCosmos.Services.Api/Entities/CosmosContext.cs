using Microsoft.EntityFrameworkCore;

namespace EfCosmos.Services.Api.Entities
{
    public class CosmosContext : DbContext
    {
        public DbSet<Template> Templates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=test;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}