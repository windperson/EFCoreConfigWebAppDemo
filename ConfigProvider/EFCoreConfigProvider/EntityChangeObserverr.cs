using System;
using System.Threading;

namespace EFCoreConfigProvider
{
    public class EntityChangeObserver
    {
        public event EventHandler<EntityChangeEventArgs> Changed;

        public void OnChanged(EntityChangeEventArgs e)
        {
            ThreadPool.QueueUserWorkItem((_) => Changed?.Invoke(this, e));
        }

        #region singleton instance

        private static readonly Lazy<EntityChangeObserver> _lazy =
            new Lazy<EntityChangeObserver>(() => new EntityChangeObserver());

        private EntityChangeObserver() { }

        public static EntityChangeObserver Instance => _lazy.Value;

        #endregion
    }
}