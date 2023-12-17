using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreSQLite.Controllers;

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
        // Console.WriteLine($"Database path: {_dbContext.DbPath}.");

        var acc = new Blog
        {
            Url = "http://blogs.msdn.com/adonet",
        };
        _dbContext.Blogs.Add(acc);
        var affectedRows = _dbContext.SaveChanges();

        return Ok(affectedRows);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Get()
    {
        // var planet = _dbContext.Planets.FirstOrDefault(p => p.name == "Pluto");
        // Console.WriteLine(planet.name);

        var planets = _dbContext.Blogs.ToList();

        return Ok(planets);
    }
}