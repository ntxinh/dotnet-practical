using AST.Core.Entities;
using AST.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AST.Infrastructure.Data
{
    public class FooRepository : EfRepository<Foo>, IFooRepository
    {
        public FooRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
