using AST.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AST.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntityAudit, IAggregateRoot
    {
        // --- Basic operations ---

        // Get
        Task<TEntity> GetAsync(int id);
        Task<TEntity> GetAsync(TEntity entity);
        IEnumerable<TEntity> Get(IEnumerable<int> ids);
        IEnumerable<TEntity> Get(params int[] ids);
        IEnumerable<TEntity> Get(IEnumerable<TEntity> entities);
        IEnumerable<TEntity> Get(params TEntity[] entities);
        IEnumerable<TEntity> GetAll();

        // Add
        Task<TEntity> AddAsync(TEntity entity);
        Task<int> AddAsync(IEnumerable<TEntity> entities);
        Task<int> AddAsync(params TEntity[] entities);

        // Update
        Task<int> UpdateAsync(TEntity entity);
        Task<int> UpdateAsync(IEnumerable<TEntity> entities);
        Task<int> UpdateAsync(params TEntity[] entities);

        // Delete
        Task<int> DeleteAsync(int id);
        Task<int> DeleteAsync(TEntity entity);
        Task<int> DeleteAsync(IEnumerable<TEntity> entities);
        Task<int> DeleteAsync(params TEntity[] entities);
        Task<int> DeleteAsync(IEnumerable<int> ids);
        Task<int> DeleteAsync(params int[] ids);
        Task<int> DeleteAllAsync();

        // // --- Advanced operations ---

        // // Delete soft
        // Task<int> DeleteSoftAsync(int id);
        // Task<int> DeleteSoftAsync(TEntity entity);
        // Task<int> DeleteSoftAsync(params TEntity[] entities);
        // Task<int> DeleteSoftAsync(params int[] ids);
        // Task<int> DeleteSoftAsync(IEnumerable<TEntity> entities);
        // Task<int> DeleteSoftAsync(IEnumerable<int> ids);
        // Task<int> DeleteSoftAllAsync();

        // // Get soft
        // Task<IEnumerable<TEntity>> GetAllSoftDeletedAsync();

        // --- Specification operations ---
        Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity> spec);
        Task<int> CountAsync(ISpecification<TEntity> spec);
    }
}
