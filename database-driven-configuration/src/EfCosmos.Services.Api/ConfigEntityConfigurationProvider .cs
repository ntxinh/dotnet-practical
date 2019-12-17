using EfCosmos.Services.Api.Entities;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading;

namespace EfCosmos.Services.Api
{
    public class ConfigEntityConfigurationProvider : ConfigurationProvider
    {
        private readonly ConfigEntityConfigurationSource _source;
        public ConfigEntityConfigurationProvider(ConfigEntityConfigurationSource source)
        {
            _source = source;

            if (_source.ReloadOnChange)
                EntityChangeObserver.Instance.Changed += EntityChangeObserver_Changed;
        }

        public override void Load()
        {
            using (var db = new CosmosContext())
            {
                db.Database.EnsureCreated();
                Data = db.Configs.ToDictionary(c => c.Id.ToString(), c => c.Name);
            }
        }

        private void EntityChangeObserver_Changed(object sender, EntityChangeEventArgs e)
        {
            if (e.entry.Entity.GetType() != typeof(Config))
                return;

            Thread.Sleep(_source.ReloadDelay);
            Load();
        }
    }
}
