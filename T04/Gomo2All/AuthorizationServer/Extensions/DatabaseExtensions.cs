using AuthorizationServer.Configuration;
using AuthorizationServer.Data;
using AuthorizationServer.Models;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Extensions
{
    public static class DatabaseExtensions
    {
        public static void InitializeDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var applicationDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var persistedGrantDbContext = serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();

                applicationDbContext.Database.Migrate();
                persistedGrantDbContext.Database.Migrate();
                ////
                var configContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                if (!configContext.Clients.Any())
                {
                    foreach (var client in Config.GetClients())
                    {
                        configContext.Clients.Add(client.ToEntity());
                    }
                    configContext.SaveChanges();
                }
                if (!configContext.IdentityResources.Any())
                {
                    foreach (var resource in Config.GetIdentityResources())
                    {
                        configContext.IdentityResources.Add(resource.ToEntity());
                    }
                    configContext.SaveChanges();
                }
                ////
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                Task.Run(async () =>
                {
                    var admin = await userManager.FindByNameAsync("admin");
                    if (admin == null)
                    {
                        await userManager.CreateAsync(new ApplicationUser
                        {
                            UserName = "ttom@gomo2o.com",
                            Email = "ttom@gomo2o.com"
                        }, "1234");
                    }
                }).GetAwaiter().GetResult();


                if (configContext.ApiResources.Any()) return;

                foreach (var resource in Config.GetApiResources())
                {
                    configContext.ApiResources.Add(resource.ToEntity());
                }

                configContext.SaveChanges();

            }
        }
    }
}
