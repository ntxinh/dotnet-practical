using EfCosmos.Services.Api.Entities;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace EfCosmos.Services.Api
{
    public class MyDatabaseConfigProvider : ConfigurationProvider
    {
        public override void Load()
        {
            //Data.Add("xinh", "xunh");

            using (var db = new CosmosContext())
            {
                db.Database.EnsureCreated();
                Data = db.Templates.ToDictionary(c => c.Id.ToString(), c => c.Name);
            }
        }
    }
}
