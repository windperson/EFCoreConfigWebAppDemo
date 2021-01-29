using System.Collections.Generic;
using System.Linq;
using System.Threading;
using EFCoreConfigProvider.Entities;
using EFCoreConfigProvider.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCoreConfigProvider
{
    public class FeatureEntityConfigurationProvider : ConfigurationProvider
    {
        private readonly FeatureEntityConfigurationSource _source;

        public FeatureEntityConfigurationProvider(FeatureEntityConfigurationSource source)
        {
            _source = source;

            if (_source.ReloadOnChange)
            {
                EntityChangeObserver.Instance.Changed += EntityChangeObserver_Changed;
            }
        }

        private void EntityChangeObserver_Changed(object sender, EntityChangeEventArgs e)
        {
            if (e.Entry.Entity.GetType() != typeof(FeatureManagement))
            {
                return;
            }
            
            Thread.Sleep(_source.ReloadDelay);
            Load();
        }

        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<FeatureEntityDbContext>();
            _source.OptionsAction(builder);

            using var context = new FeatureEntityDbContext(builder.Options);
            context.Database.EnsureCreated();

            var config = context.FeatureManagements.SingleOrDefault();

            if (config == null)
            {
                return;
            }

            Data = new Dictionary<string, string>();

            foreach (var feature in config.Features)
            {
                Data.Add($"FeatureManagement:{feature.Key}", feature.Value);
            }
        }
    }
}