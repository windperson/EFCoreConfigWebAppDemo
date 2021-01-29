using EFCoreConfigProvider.Entities;
using EFCoreConfigWebAppDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreConfigWebAppDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IFeatureManager _featureManager;
        // private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IFeatureManager featureManager) // IConfiguration configuration)
        {
            _logger = logger;
            // _configuration = configuration;
            _featureManager = featureManager;
        }

        public async Task<IActionResult> Index()
        {
            // var featuresSection = _configuration.GetSection(nameof(FeatureManagement));
            //
            // _logger.LogInformation($"MyFeatureA-1: {featuresSection["MyFeatureA-1"]}");
            // _logger.LogInformation($"MyFeatureA-2: {featuresSection["MyFeatureA-2"]}");
            // _logger.LogInformation($"MyFeatureB-1: {featuresSection["MyFeatureB-1"]}");
            // _logger.LogInformation($"MyFeatureB-2: {featuresSection["MyFeatureB-2"]}");

            if (await _featureManager.IsEnabledAsync("MyFeatureA-1")) {
                _logger.LogInformation("MyFeature A-1 is enabled");
            }
            else {
                _logger.LogInformation("MyFeature A-1 is disabled");
            }

            if (await _featureManager.IsEnabledAsync("MyFeatureA-2")) {
                _logger.LogInformation("MyFeature A-2 is enabled");
            }
            else {
                _logger.LogInformation("MyFeature A-2 is disabled");
            }

            if (await _featureManager.IsEnabledAsync("MyFeatureB-1")) {
                _logger.LogInformation("MyFeature B-1 is enabled");
            }
            else {
                _logger.LogInformation("MyFeature B-1 is disabled");
            }

            if (await _featureManager.IsEnabledAsync("MyFeatureB-2")) {
                _logger.LogInformation("MyFeature B-2 is enabled");
            }
            else {
                _logger.LogInformation("MyFeature B-2 is disabled");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}