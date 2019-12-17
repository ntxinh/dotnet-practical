using Microsoft.Extensions.Configuration;

namespace EfCosmos.Services.Api
{
    public class ConfigEntityConfigurationSource : IConfigurationSource
    {
        public bool ReloadOnChange { get; set; }
        // Number of milliseconds that reload will wait before calling Load. This helps avoid triggering a reload before a change is completely saved. Default is 500.
        public int ReloadDelay { get; set; } = 500;
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ConfigEntityConfigurationProvider(this);
        }
    }
}
