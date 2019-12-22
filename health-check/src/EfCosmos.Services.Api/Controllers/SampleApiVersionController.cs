using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EfCosmos.Services.Api.Controllers
{
    [ApiController]
    [ApiVersion("0.0", Deprecated = true)]
    [Route("api/Values")]
    public class ValuesV0Controller : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[]
            {
                "Value1 from Version 0",
                "value2 from Version 0"
            };
        }
    }

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/Values")]
    public class ValuesV1Controller : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[]
            {
                "Value1 from Version 1",
                "value2 from Version 1"
            };
        }
    }

    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/Values")]
    public class ValuesV2Controller : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[]
            {
                "value1 from Version 2",
                "value2 from Version 2"
            };
        }
    }
}
