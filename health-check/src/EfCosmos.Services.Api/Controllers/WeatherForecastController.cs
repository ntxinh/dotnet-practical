using EfCosmos.Services.Api.Controllers.Responses;
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
        private readonly ApplicationDbContext _applicationDbContext;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IConfiguration configuration,
            ApplicationDbContext applicationDbContext
        )
        {
            _logger = logger;
            _configuration = configuration;
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public object Get()
        {
            try
            {
                var data = _configuration.AsEnumerable();
                return new ApiResponseBuilder().Data(data).BuildOk();
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
                _applicationDbContext.Database.EnsureCreated();
                var c = new Config();
                c.Name = "Config 1";
                _applicationDbContext.Configs.Add(c);
                _applicationDbContext.SaveChanges();

                return new ApiResponseBuilder().BuildOk();
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
                _applicationDbContext.Database.EnsureCreated();
                var c = new Template();
                c.Name = "Template 1";
                _applicationDbContext.Templates.Add(c);
                _applicationDbContext.SaveChanges();

                return new ApiResponseBuilder().BuildOk();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
