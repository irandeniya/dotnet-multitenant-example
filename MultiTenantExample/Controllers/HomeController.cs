using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MultiTenantExample.Models;
using MultiTenantExample.Services;
using MultiTenantExample.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private TenantSettings tenantSettings;
        private TenantService tenantService;

        public HomeController(ILogger<HomeController> logger, IOptions<TenantSettings> options, TenantService tenantService)
        {
            _logger = logger;
            tenantSettings = options.Value;
            this.tenantService = tenantService;
        }

        public IActionResult Index()
        {
            var site = tenantService.GetTenant();
            if (site != null)
            {
                ViewBag.SelectedSite = new TenantSiteModel { 
                    Key = tenantService.GetTenantCode(),
                    Logo = site.logo,
                    Name = site.name
                };
            }

            var sites = tenantSettings.Sites.Select(s => new TenantSiteModel
            {
                Key = s.Key,
                Logo = s.Value.logo,
                Name = s.Value.name
            }).ToList();

            return View(sites);
        }

        public IActionResult SelectSite(string site)
        {
            if (tenantSettings.Sites.ContainsKey(site))
            {
                Response.Cookies.Append("tenant-code", site);
            }

            return RedirectToAction("Index");
        }

        public IActionResult UnselectSite(string site)
        {
            if (tenantSettings.Sites.ContainsKey(site))
            {
                Response.Cookies.Delete("tenant-code");
            }

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
