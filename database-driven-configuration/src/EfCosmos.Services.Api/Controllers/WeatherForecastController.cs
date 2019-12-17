using EfCosmos.Services.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace EfCosmos.Services.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
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
                //return _cosmosContext.Templates.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public object Test()
        {
            try
            {
                using (var _cosmosContext = new CosmosContext())
                {
                    _cosmosContext.Database.EnsureCreated();
                    var c = new Config();
                    c.Name = "Config 1";
                    _cosmosContext.Configs.Add(c);
                    _cosmosContext.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public object AddTemplate()
        {
            try
            {
                using (var _cosmosContext = new CosmosContext())
                {
                    _cosmosContext.Database.EnsureCreated();
                    var c = new Template();
                    c.Name = "Template 1";
                    _cosmosContext.Templates.Add(c);
                    _cosmosContext.SaveChanges();
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
