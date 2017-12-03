using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace WebClient
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
           

            services.AddMvc();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                //使用Cookie作为验证用户的首选方式
                options.DefaultScheme = "Cookies";
                //用户需要登陆的时候, 将使用的是OpenId Connect Scheme
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.SignInScheme = "Cookies";

                //Authority是指信任的Identity Server
                options.Authority = "http://localhost:5000";
                //注意正式生产环境要使用https, 这里就不用了.
                options.RequireHttpsMetadata = false;

                //Client名字也暗示了我们要使用的是implicit flow
                //options.ClientId = "mvc_implicit";

                //Client名字也暗示了我们要使用的是hybird flow
                options.ClientId = "testWebClient";
                //options.ClientSecret = "secret";
                //既做Authentication又做Authorization. 也就是说我们既要id_token还要token本身
                //options.ResponseType= "id_token token";
                //options.ResponseType = "id_token code";

                //options.Scope.Add("socialnetwork");
                //options.Scope.Add("offline_access");
                //options.Scope.Add("email");
                //从Authorization Server的Reponse中返回的token们持久化在cookie中
               // options.SaveTokens = true;

                //options.GetClaimsFromUserInfoEndpoint = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //确保每次请求都执行authentication
            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
