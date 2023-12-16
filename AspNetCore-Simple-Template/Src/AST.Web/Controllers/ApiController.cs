using Microsoft.AspNetCore.Mvc;

namespace AST.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        public ApiController()
        {
        }
    }
}
