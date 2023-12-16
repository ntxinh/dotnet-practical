using ASWA.Core.Entities;
using System.Threading.Tasks;

namespace ASWA.Core.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetByEmailAsync(string email);
    }
}