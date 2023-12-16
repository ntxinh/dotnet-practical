using Microsoft.AspNetCore.Mvc;

namespace ASWA.Web.Controllers
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
