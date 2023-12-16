using AST.Core.Entities;
using AST.Infrastructure.Data.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AST.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Foo> Foos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customizations must go after base.OnModelCreating(builder)
            builder.ApplyConfiguration(new FooConfig());
        }

        public override int SaveChanges()
        {
            OnBeforeSaving();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return await base.SaveChangesAsync(cancellationToken);
        }

        // public override int SaveChanges(bool acceptAllChangesOnSuccess)
        // {
        //     OnBeforeSaving();
        //     return base.SaveChanges(acceptAllChangesOnSuccess);
        // }

        // public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        // {
        //     OnBeforeSaving();
        //     return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        // }

        private void OnBeforeSaving()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntityAudit)
                .ToList();
            UpdateSoftDelete(entities);
            UpdateTimestamps(entities);
        }

        private void UpdateSoftDelete(List<EntityEntry> entries)
        {
            var filtered = entries
                .Where(x => x.State == EntityState.Added
                    || x.State == EntityState.Deleted);

            foreach (var entry in filtered)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        //entry.CurrentValues["IsDeleted"] = false;
                        ((BaseEntityAudit)entry.Entity).IsDeleted = false;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        //entry.CurrentValues["IsDeleted"] = true;
                        ((BaseEntityAudit)entry.Entity).IsDeleted = true;
                        break;
                }
            }
        }

        private void UpdateTimestamps(List<EntityEntry> entries)
        {
            var filtered = entries
                .Where(x => x.State == EntityState.Added
                    || x.State == EntityState.Modified);

            // TODO: Get real current user id
            var currentUserId = 1;

            foreach (var entry in filtered)
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseEntityAudit)entry.Entity).CreatedAt = DateTime.UtcNow;
                    ((BaseEntityAudit)entry.Entity).CreatedBy = currentUserId;
                }

                ((BaseEntityAudit)entry.Entity).UpdatedAt = DateTime.UtcNow;
                ((BaseEntityAudit)entry.Entity).UpdatedBy = currentUserId;
            }
        }
    }
}
