using AuthServer.Data;
using AuthServer.Models;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace AuthServer.Extensions
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
                        }, "admin");
                    }
                }).GetAwaiter().GetResult();

            }
        }
    }
}
