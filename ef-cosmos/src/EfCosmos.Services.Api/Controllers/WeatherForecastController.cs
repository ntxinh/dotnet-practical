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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public bool Get()
        {
            try
            {
                using (var context = new CosmosContext())
                {
                    //context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    var newId = new Guid();
                    context.Templates.Add(new Template
                    {
                        Id = newId,
                        PartitionKey = newId.ToString(),
                        Name = "Template 1",
                    });

                    context.SaveChanges();
                }

                using (var context = new CosmosContext())
                {
                    var template = context.Templates.FirstOrDefault();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
