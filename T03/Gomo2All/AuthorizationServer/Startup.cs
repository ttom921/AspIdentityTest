﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuthorizationServer.Data;
using AuthorizationServer.Models;
using AuthorizationServer.Services;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using AuthorizationServer.Configuration;
using AuthorizationServer.Extensions;

namespace AuthorizationServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //設定DB連接字串
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            //設定asp identity
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString));

            //設定身分識別
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                //password settings密碼原則
                //需要密碼中的 0-9 之間的數字。 預設為 true。
                options.Password.RequireDigit = false;
                //最小密碼長度。 預設為 6。
                options.Password.RequiredLength = 4;
                //需要非英數字元的密碼。 預設為 true。
                options.Password.RequireNonAlphanumeric = false;
                //需要密碼中的大寫字元。 預設為 true。
                options.Password.RequireUppercase = false;
                //需要密碼中的小寫字元。 預設為 true。
                options.Password.RequireLowercase = false;
                //需要密碼中不同的字元的數。 預設值為 1。
                options.Password.RequiredUniqueChars = 2;
                // Lockout settings使用者的鎖定
                //的時間量的使用者便會鎖定發生鎖定時。 預設為 5 分鐘。
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                //失敗的存取嘗試次數直到使用者鎖定，如果已啟用鎖定。 預設為 5。
                options.Lockout.MaxFailedAccessAttempts = 5;
                //決定是否新的使用者可能會鎖定。預設為 true。
                options.Lockout.AllowedForNewUsers = true;
                // Signin settings登入設定
                //需要登入的確認電子郵件。 預設為 false。
                options.SignIn.RequireConfirmedEmail = false;
                //需要確認的電話號碼登入。 預設為 false。
                options.SignIn.RequireConfirmedPhoneNumber = false;
                // User settings使用者驗證設定
                //需要每位使用者擁有唯一的電子郵件。 預設為 false。
                options.User.RequireUniqueEmail = false;
            })
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders()
           ;
            services.ConfigureApplicationCookie(options =>
             {
                 options.Cookie.Name = "MLHAuthorizationServerCookie";
                 options.Cookie.HttpOnly = true;
                 options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                 options.LoginPath = "/Account/Login";
                 options.LogoutPath = "/Account/Logout";
                 options.AccessDeniedPath = "/Account/AccessDenied";
                 options.SlidingExpiration = true;
                 options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
             });
            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();

            //設定id4
            services.AddIdentityServer()
#if DEBUG
                .AddDeveloperSigningCredential()
#else
                .AddSigningCredential(new System.Security.Cryptography.X509Certificates.X509Certificate2(
                    SharedSettings.Settings.AuthorizationServerSettings.Certificate.Path, 
                    SharedSettings.Settings.AuthorizationServerSettings.Certificate.Password))
#endif
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                      builder.UseMySql(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseMySql(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                })
                .AddAspNetIdentity<ApplicationUser>();

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy(CoreApiAuthorizationPolicy.PolicyName, policy =>
            //        policy.RequireClaim(CoreApiAuthorizationPolicy.ClaimName, CoreApiAuthorizationPolicy.ClaimValue));
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.InitializeDatabase();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            //app.UseAuthentication();
            app.UseIdentityServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
