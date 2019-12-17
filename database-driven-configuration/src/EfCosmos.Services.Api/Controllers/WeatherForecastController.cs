using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace EfCosmos.Services.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration _configuration;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IConfiguration configuration
        )
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public object Get()
        {
            try
            {
                return _configuration.AsEnumerable();
                // _cosmosContext.Database.EnsureDeleted();
                //_cosmosContext.Database.EnsureCreated();
                //var template = new Template();
                //template.Name = "Template 3";
                //_cosmosContext.Templates.Add(template);
                //_cosmosContext.SaveChanges();
                //return _cosmosContext.Templates.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
