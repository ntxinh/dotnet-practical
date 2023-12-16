using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCorePostgres.Controllers;

[Produces(MediaTypeNames.Application.Json)]
[Route("[controller]")]
public class TestController : MyControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    public TestController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Create()
    {
        var acc = new Account
        {
            UserId = 100,
            Username = "ntxinh",
            Password = "123456",
            Email = "sample@email.com",
        };
        _dbContext.Accounts.Add(acc);
        var affectedRows = _dbContext.SaveChanges();

        return Ok(affectedRows);
    }
}