using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EchoController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Echo()
    {
        using var reader = new StreamReader(Request.Body, Encoding.UTF8);
        var requestBody = await reader.ReadToEndAsync();
        return Ok(new { echoed = requestBody });
    }
}