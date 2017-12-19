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
                //var configContext = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                //if (!configContext.Clients.Any())
                //{
                //    foreach (var client in Config.GetClients())
                //    {
                //        configContext.Clients.Add(client.ToEntity());
                //    }
                //    configContext.SaveChanges();
                //}
                //if (!configContext.IdentityResources.Any())
                //{
                //    foreach (var resource in Config.GetIdentityResources())
                //    {
                //        configContext.IdentityResources.Add(resource.ToEntity());
                //    }
                //    configContext.SaveChanges();
                //}
                ////

                //建製角色
                Task.Run(async () =>
                {
                    await BulderRolesAsync(serviceScope);
                }).GetAwaiter().GetResult();


                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                Task.Run(async () =>
                {
                    var admin = await UserManagerExtensions.FindByEmailAsync(userManager,"ttom@gomo2o.com");
                    if (admin == null)
                    {
                        //建立使用者
                        var applicationUser = new ApplicationUser
                        {
                            UserName = "tomtang",
                            Email = "ttom@gomo2o.com"
                        };
                        var createUser = await userManager.CreateAsync(applicationUser, "1234");
                        if (createUser.Succeeded)
                        {
                            //here we tie the new user to the "Admin" role 
                            await userManager.AddToRoleAsync(applicationUser, "Admin");
                        }
                    }
                    //
                    //var user = await UserManagerExtensions.FindByEmailAsync(userManager, "test@gomo2o.com");
                    //if (user == null)
                    //{
                    //    //建立使用者
                    //    var applicationUser = new ApplicationUser
                    //    {
                    //        UserName = "testtom",
                    //        Email = "test@gomo2o.com"
                    //    };
                    //    var createUser = await userManager.CreateAsync(applicationUser, "1234");
                    //    if (createUser.Succeeded)
                    //    {
                    //        //建立角色
                    //        await userManager.AddToRoleAsync(applicationUser, "General");
                    //        await userManager.AddToRoleAsync(applicationUser, "ShopManager");
                    //    }
                    //}
                    //測試相同的email delflag不同
                    var user = await UserManagerExtensions.FindByEmailAsync(userManager, "test@gomo2o.com");
                    if (user == null)
                    {
                        //建立使用者
                        var applicationUser = new ApplicationUser
                        {
                            UserName = "testtomdel",
                            Email = "test@gomo2o.com"
                        };
                        var createUser = await userManager.CreateAsync(applicationUser, "1234");
                        if (createUser.Succeeded)
                        {
                            //建立角色
                            await userManager.AddToRoleAsync(applicationUser, "General");
                            await userManager.AddToRoleAsync(applicationUser, "ShopManager");
                        }
                    }

                }).GetAwaiter().GetResult();


                //if (configContext.ApiResources.Any()) return;

                //foreach (var resource in Config.GetApiResources())
                //{
                //    configContext.ApiResources.Add(resource.ToEntity());
                //}

                //configContext.SaveChanges();

            }
        }

        private static async Task BulderRolesAsync(IServiceScope serviceScope)
        {
            //最高管理者  Admin 1
            //代理商     Agent 2
            //經銷商     Dealer 3
            //車廠管理者 ShopManager 4
            //車廠技師 ShopTechnician 5
            //車廠員工 ShopStaff 6
            //會員 User 7
            //一般 General 8
            //測試人員1 Test1 16
            //測試人員2 Test2 17
            var RoleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Admin", "Agent", "Dealer", "ShopManager",
                                    "ShopTechnician", "ShopStaff", "User","General", "Test1","Test2"};
            IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //IdentityRole idrole = new IdentityRole(roleName);
                   // idrole.Id = "1";
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            
        }

    }
}
