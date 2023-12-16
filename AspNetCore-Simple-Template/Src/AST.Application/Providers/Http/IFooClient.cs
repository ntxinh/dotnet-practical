using System.Threading.Tasks;
using Refit;

namespace AST.Application.Providers
{
    public interface IFooClient
    {
        [Get("/")]
        Task<object> GetAll();
    }
}
