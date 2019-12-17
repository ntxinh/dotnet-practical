using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EfCosmos.Services.Api.Entities
{
    public class CosmosContext : DbContext
    {
        public DbSet<Template> Templates { get; set; }
        public DbSet<Config> Configs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=test;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        public override int SaveChanges()
        {
            OnEntityChange();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            OnEntityChange();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void OnEntityChange()
        {
            foreach (var entry in ChangeTracker.Entries()
                .Where(i => i.State == EntityState.Modified || i.State == EntityState.Added))
            {
                EntityChangeObserver.Instance.OnChanged(new EntityChangeEventArgs(entry));
            }
        }
    }
}