using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMongoDB.Controllers;

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
        var acc = new Planet
        {
            name = "Pluto",
            hasRings = false,
            orderFromSun = 9,
        };
        _dbContext.Planets.Add(acc);
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

        var planets = _dbContext.Planets.Where(p => !p.hasRings).ToList();

        return Ok(planets);
    }
}