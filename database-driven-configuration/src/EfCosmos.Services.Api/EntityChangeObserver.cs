using System;
using System.Threading;

namespace EfCosmos.Services.Api
{
    public class EntityChangeObserver
    {
        public event EventHandler<EntityChangeEventArgs> Changed;

        public void OnChanged(EntityChangeEventArgs e)
        {
            ThreadPool.QueueUserWorkItem((_) => Changed?.Invoke(this, e));
        }

        #region singleton

        private static readonly Lazy<EntityChangeObserver> lazy = new Lazy<EntityChangeObserver>(() => new EntityChangeObserver());

        private EntityChangeObserver() { }

        public static EntityChangeObserver Instance => lazy.Value;

        #endregion singleton
    }
}
