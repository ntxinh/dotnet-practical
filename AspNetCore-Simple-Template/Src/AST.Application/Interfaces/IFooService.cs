using System.Collections.Generic;
using System.Threading.Tasks;
using AST.Application.ViewModels;
using AST.Core.Common.ResponseBuilder;
using AST.Core.Entities;

namespace AST.Application.Interfaces
{
    public interface IFooService
    {
        ApiResponse<IEnumerable<Foo>> GetAll();
        Task<ApiResponse<Foo>> GetById(int id);
        Task<ApiResponse<Foo>> Add(FooFormModel model);
        Task<ApiResponse<bool>> UpdateById(int id, FooFormModel model);
        Task<ApiResponse<bool>> DeleteById(int id);
    }
}
