using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EFCoreConfigProvider
{
    public class EntityChangeEventArgs : EventArgs
    {
        public EntityChangeEventArgs(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; set; }
        
    }
}