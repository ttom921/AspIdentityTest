using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuthServer.Data;
using AuthServer.Models;
using AuthServer.Services;
using System.Reflection;
using AuthServer.Extensions;
using AuthServer.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using SharedSettings.Policies;

namespace AuthServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        string connectionString = "";
        string migrationsAssembly = "";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //設定DB連接字串
            connectionString = Configuration.GetConnectionString("DefaultConnection");
            migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            //設定Asp Net Identity
            SetUpAspIdentity(services);

            services.AddMvc();
            //加入 IdentityServer4
            SetUpIdentityServer(services);
        }
        //加入 IdentityServer4
        private void SetUpIdentityServer(IServiceCollection services)
        {
            //string filepath = Path.Combine(AppContext.BaseDirectory, "socialnetwork.pfx");
            services.AddIdentityServer()
             //#if DEBUG
             .AddDeveloperSigningCredential()
             //#else
             //.AddSigningCredential(new X509Certificate2(Path.Combine(AppContext.BaseDirectory, "socialnetwork.pfx"), "12345678"))
             //#endif

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
                   //this enables automatic token cleanup. this is optional.
                   options.EnableTokenCleanup = true;
                 options.TokenCleanupInterval = 30;
             })
              //加入asp identity
              .AddAspNetIdentity<ApplicationUser>()
              ;
            //加入授權admin
            services.AddAuthorization(options =>
            {
                options.AddPolicy(CoreApiAuthorizationPolicy.PolicyName, policy =>
                    policy.RequireClaim(CoreApiAuthorizationPolicy.ClaimName, CoreApiAuthorizationPolicy.ClaimValue));
            });
        }

        //設定Asp Net Identity
        private void SetUpAspIdentity(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

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
                options.Password.RequiredUniqueChars = 0;
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
                //options.User.RequireUniqueEmail = false;
            })
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "GomoAuthorizationServerCookie";
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //自動db migration
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
            //使用 id4
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
