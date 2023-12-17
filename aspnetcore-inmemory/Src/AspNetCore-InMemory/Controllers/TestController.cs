using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreInMemory.Controllers;

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
        var acc = new Todo
        {
            Name = "Pluto",
            IsComplete = false,
        };
        _dbContext.Todos.Add(acc);
        var affectedRows = _dbContext.SaveChanges();

        return Ok(affectedRows);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Get()
    {
        var planets = _dbContext.Todos.ToList();

        return Ok(planets);
    }
}