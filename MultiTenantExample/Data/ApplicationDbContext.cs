using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiTenantExample.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantExample.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private string ConnectionString;

        public ApplicationDbContext(TenantService tenantService)
        {
            ConnectionString = tenantService?.GetConnectionString();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (ConnectionString != null)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }
    }
}
