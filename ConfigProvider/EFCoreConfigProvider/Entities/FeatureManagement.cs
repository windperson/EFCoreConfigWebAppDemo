using System.Collections.Generic;

namespace EFCoreConfigProvider.Entities
{
    public class FeatureManagement
    {
        public string Id { get; set; }
        
        // ReSharper disable once CollectionNeverUpdated.Global
        public ISet<Feature> Features { get; set; }
    }
}