using ASWA.Core.Entities;
using ASWA.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ASWA.Infra.Data
{
    public class CustomerRepository : EfRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Customer> GetByEmailAsync(string email)
        {
            return await _dbContext.Customers
                .FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}