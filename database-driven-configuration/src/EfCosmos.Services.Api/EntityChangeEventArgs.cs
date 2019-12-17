using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfCosmos.Services.Api
{
    public class EntityChangeEventArgs
    {
        public EntityEntry entry;

        public EntityChangeEventArgs(EntityEntry entry)
        {
            this.entry = entry;
        }
    }
}
