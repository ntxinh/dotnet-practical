using AST.Core.Common.Utils;
using AST.Core.Entities;
using AST.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AST.Infrastructure.Data
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntityAudit, IAggregateRoot
    {
        protected readonly AppDbContext _dbContext;

        public EfRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // --- Basic operations ---

        // Get
        public async Task<TEntity> GetAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetAsync(TEntity entity)
        {
            ThrowIf.Argument.IsNull(entity);

            return await GetAsync(entity.Id);
        }

        public IEnumerable<TEntity> Get(IEnumerable<int> ids)
        {
            ThrowIf.Argument.IsNull(ids);

            return _dbContext.Set<TEntity>().Where(x => ids.Contains(x.Id)).AsEnumerable();
        }

        public IEnumerable<TEntity> Get(params int[] ids)
        {
            ThrowIf.Argument.IsNull(ids);

            return Get(ids.AsEnumerable());
        }

        public IEnumerable<TEntity> Get(IEnumerable<TEntity> entities)
        {
            ThrowIf.Argument.IsNull(entities);

            var ids = entities.Select(x => x.Id);
            return Get(ids);
        }

        public IEnumerable<TEntity> Get(params TEntity[] entities)
        {
            ThrowIf.Argument.IsNull(entities);

            var ids = entities.Select(x => x.Id);
            return Get(ids);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }

        // Add
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            ThrowIf.Argument.IsNull(entity);

            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<int> AddAsync(IEnumerable<TEntity> entities)
        {
            ThrowIf.Argument.IsNull(entities);

            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> AddAsync(params TEntity[] entities)
        {
            ThrowIf.Argument.IsNull(entities);

            return await AddAsync(entities.AsEnumerable());
        }

        // Update
        public async Task<int> UpdateAsync(TEntity entity)
        {
            ThrowIf.Argument.IsNull(entity);

            _dbContext.Entry(entity).State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(IEnumerable<TEntity> entities)
        {
            ThrowIf.Argument.IsNull(entities);

            _dbContext.Set<TEntity>().UpdateRange(entities);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(params TEntity[] entities)
        {
            ThrowIf.Argument.IsNull(entities);

            return await UpdateAsync(entities.AsEnumerable());
        }

        // Delete
        public async Task<int> DeleteAsync(int id)
        {
            var existed = await GetAsync(id);
            _dbContext.Set<TEntity>().Remove(existed);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            ThrowIf.Argument.IsNull(entity);

            return await DeleteAsync(entity.Id);
        }

        public async Task<int> DeleteAsync(IEnumerable<TEntity> entities)
        {
            ThrowIf.Argument.IsNull(entities);

            _dbContext.Set<TEntity>().RemoveRange(entities);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(params TEntity[] entities)
        {
            ThrowIf.Argument.IsNull(entities);

            return await DeleteAsync(entities.AsEnumerable());
        }

        public async Task<int> DeleteAsync(IEnumerable<int> ids)
        {
            ThrowIf.Argument.IsNull(ids);

            var existed = _dbContext.Set<TEntity>().Where(x => ids.Contains(x.Id));
            _dbContext.Set<TEntity>().RemoveRange(existed);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(params int[] ids)
        {
            ThrowIf.Argument.IsNull(ids);

            return await DeleteAsync(ids.AsEnumerable());
        }

        public async Task<int> DeleteAllAsync()
        {
            var existed = _dbContext.Set<TEntity>();
            _dbContext.Set<TEntity>().RemoveRange(existed);
            return await _dbContext.SaveChangesAsync();
        }

        // --- Specification operations ---
        public async Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<TEntity> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_dbContext.Set<TEntity>().AsQueryable(), spec);
        }
    }
}
