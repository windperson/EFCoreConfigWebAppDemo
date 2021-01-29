using EFCoreConfigProvider;
using EFCoreConfigProvider.Entities;
using EFCoreConfigProvider.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreConfigWebAppDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            // await SeedDefaultDataAsync(host);
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, configBuilder) => {
                    configBuilder.Add(new FeatureEntityConfigurationSource {
                        OptionsAction =
                            optionsBuilder => {
                                optionsBuilder.UseCosmos(
                                    @"AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
                                    @"JFJAlbumWebsiteDb_local");
                                optionsBuilder.EnableDetailedErrors();
                                optionsBuilder.EnableSensitiveDataLogging();
                            },
                        ReloadOnChange = true
                    });
                })
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
        }

        private static async Task SeedDefaultDataAsync(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var context = scope.ServiceProvider.GetService<FeatureEntityDbContext>();
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            var featureMgmt = new FeatureManagement {Id = "1"};
            var features = new SortedSet<Feature> {
                new Feature {Key = "MyFeatureA-1", Value = "true"},
                new Feature {Key = "MyFeatureA-2", Value = "true"},
                new Feature {Key = "MyFeatureB-1", Value = "false"},
                new Feature {Key = "MyFeatureB-2", Value = "true"}
            };
            featureMgmt.Features = features;

            await context.FeatureManagements.AddAsync(featureMgmt);

            await context.SaveChangesAsync();
        }
    }
}