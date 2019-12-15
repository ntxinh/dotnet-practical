using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EfCosmos.Services.Api.Entities;

namespace EfCosmos.Services.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly CosmosContext _cosmosContext;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            CosmosContext cosmosContext
        )
        {
            _logger = logger;
            _cosmosContext = cosmosContext;
        }

        [HttpGet]
        public bool Get()
        {
            try
            {
                // _cosmosContext.Database.EnsureDeleted();
                // _cosmosContext.Database.EnsureCreated();

                // Find By Id
                var template = _cosmosContext.Templates
                    .Where(x => x.Id == new Guid("54347566-3afd-44d4-9b97-f7066494c4f1"))
                    .FirstOrDefault();

                // Find All
                var templates = _cosmosContext.Templates.ToList();

                // Add
                var templateNew = new Template();
                template.Name = "Template 1";
                _cosmosContext.Templates.Add(template);
                _cosmosContext.SaveChanges();

                // Update
                _cosmosContext.Templates.Update(template);
                _cosmosContext.SaveChanges();

                // Remove
                _cosmosContext.Templates.Remove(template);
                _cosmosContext.SaveChanges();

                // AddRange
                // UpdateRange
                // RemoveRage

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
