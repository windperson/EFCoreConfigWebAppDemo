using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCoreConfigProvider
{
    public class FeatureEntityConfigurationSource : IConfigurationSource
    {
        public Action<DbContextOptionsBuilder> OptionsAction { get; set; }

        public bool ReloadOnChange { get; set; }

        public int ReloadDelay { get; set; } = 500;

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new FeatureEntityConfigurationProvider(this);
        }
    }
}