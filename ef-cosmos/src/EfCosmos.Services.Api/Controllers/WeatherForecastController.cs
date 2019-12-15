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
                //context.Database.EnsureDeleted();
                _cosmosContext.Database.EnsureCreated();

                var newId = new Guid();
                _cosmosContext.Templates.Add(new Template
                {
                    //Id = newId,
                    PartitionKey = newId.ToString(),
                    Name = $"Template {newId}",
                });

                _cosmosContext.SaveChanges();

                var template = _cosmosContext.Templates.FirstOrDefault();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
