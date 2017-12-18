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
using Swashbuckle.AspNetCore.Swagger;
using SalesApi.Shared.Settings;
using SharedSettings.Settings;
using IdentityServer4.AccessTokenValidation;
using Newtonsoft.Json.Serialization;
using IdentityModel;

namespace SalesApi.Web
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
            //services.AddDbContext<SalesContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Add Cors support to the service
            services.AddCors();
            var policy = new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicy();
            policy.Headers.Add("*");
            policy.Methods.Add("*");
            policy.Origins.Add("*");
            //policy.SupportsCredentials = true;
            services.AddCors(x => x.AddPolicy(SalesApiSettings.CorsPolicyName, policy));
            //var guestPolicy = new AuthorizationPolicyBuilder()
            //   .RequireAuthenticatedUser()
            //   .RequireClaim("scope", "dataEventRecords")
            //   .Build();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = SalesApiSettings.ApiResource.DisplayName, Version = "v1" });
            });

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options=> 
                {
                    options.Authority = AuthorizationServerSettings.AuthorizationServerBase;
                    options.RequireHttpsMetadata = false;

                    options.ApiName = SalesApiSettings.ApiResource.Name;
                });

            services.AddAuthorization(options => 
            {
                //options.AddPolicy("dataEventRecordsAdmin", policyAdmin =>
                //{
                //    policyAdmin.RequireClaim("role", "dataEventRecords.admin");
                //});
                //options.AddPolicy("dataEventRecordsUser", policyUser =>
                //{
                //    policyUser.RequireClaim("role", "dataEventRecords.user");
                //});
                //options.AddPolicy("dataEventRecords", policyUser =>
                //{
                //    policyUser.RequireClaim("scope", "dataEventRecords");
                //});
                options.AddPolicy("admin", policyAdmin =>
                {
                    policyAdmin.RequireClaim(JwtClaimTypes.Role, "admin");
                });
                options.AddPolicy("user", policyUser =>
                {
                    policyUser.RequireClaim(JwtClaimTypes.Role,"user");
                });
            });

            services.AddMvc(options=> 
            {
                //options.Filters.Add(new AuthorizeFilter(guestPolicy));
            }).AddJsonOptions(options=> 
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            })
            ;
            services.AddMvc();
            //services.AddMvc(options =>
            //{
            //    options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());

            //    // set authorize on all controllers
            //    var policy = new AuthorizationPolicyBuilder()
            //        .RequireAuthenticatedUser()
            //        .Build();
            //    options.Filters.Add(new AuthorizeFilter(policy));
            //})
            //.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RetailPromotionSeriesViewModelValidator>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseCors(SalesApiSettings.CorsPolicyName);
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", SalesApiSettings.Client.ClientName + " API v1");
            });
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
