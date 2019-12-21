using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EfCosmos.Services.Api.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Template> Templates { get; set; }
        public DbSet<Config> Configs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}