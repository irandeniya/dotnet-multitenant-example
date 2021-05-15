using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MultiTenantExample.Models;
using MultiTenantExample.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantExample.Services
{
    public class TenantService
    {
        private TenantSettings tenantSettings;
        private HttpContext httpContext;
        private TenantData tenant;
        private string tenantCode;

        public TenantService(IOptions<TenantSettings> options, IHttpContextAccessor contextAccessor)
        {
            tenantSettings = options.Value;
            httpContext = contextAccessor.HttpContext;

            if (httpContext.Request.Cookies.TryGetValue("tenant-code", out string site) && tenantSettings.Sites.ContainsKey(site))
            {
                tenantCode = site;
                tenant = tenantSettings.Sites[site];
            }
        }

        public string GetConnectionString()
        {
            return tenant?.connectionString;
        }

        public string GetTenantCode()
        {
            return tenantCode;
        }

        public TenantData GetTenant()
        {
            return tenant;
        }
    }
}
