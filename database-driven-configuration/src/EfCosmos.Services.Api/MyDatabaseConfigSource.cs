using EfCosmos.Services.Api.Entities;
using Microsoft.Extensions.Configuration;

namespace EfCosmos.Services.Api
{
    public class MyDatabaseConfigSource : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new MyDatabaseConfigProvider();
        }
    }
}
