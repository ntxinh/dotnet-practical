using System.Threading.Tasks;
using AST.Application.Interfaces;
using AST.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AST.Web.Controllers
{
    public class FooController : ApiController
    {
        private readonly IFooService _fooService;

        public FooController(IFooService fooService)
        {
            _fooService = fooService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_fooService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _fooService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(FooFormModel model)
        {
            return Ok(await _fooService.Add(model));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById(int id, FooFormModel model)
        {
            return Ok(await _fooService.UpdateById(id, model));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            return Ok(await _fooService.DeleteById(id));
        }
    }
}
