using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using SharedSettings.Settings;
using CoreApi.Web.MyConfigurations;
using Swashbuckle.AspNetCore.Swagger;

namespace CoreApi.Web
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
            //加入資料庫

            //加入MVC的框架
            services.AddMvc().AddJsonOptions(options =>
            {
                //使json的字串不轉換
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });
            //addswagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = CoreApiSettings.ApiResource.DisplayName, Version = "v1" });
            });
            //
            services.AddMvcCore()
               .AddAuthorization()
               .AddJsonFormatters();

            services.AddAuthentication("Bearer")
               .AddIdentityServerAuthentication(options =>
               {
                   options.Authority = AuthorizationServerSettings.AuthorizationServerBase;
                   options.RequireHttpsMetadata = false;

                   options.ApiName = CoreApiSettings.ApiResource.Name;
               });

            services.AddCors(options =>
            {
                options.AddPolicy(CoreApiSettings.CorsPolicyName, policy =>
                {
                    policy.WithOrigins(CoreApiSettings.CorsOrigin)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(CoreApiSettings.CorsPolicyName);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core APIs V1");
            });

            // Config Serilog
            app.ConfigureSerilog(Configuration);

            // Identity Server
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
