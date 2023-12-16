using ASWA.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASWA.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntityAudit, IAggregateRoot
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}