using AutoMapper;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgt.Business.Interfaces;
using UserMgt.Business.Logger;
using UserMgt.Service.Extensions;

namespace UserMgt.Service
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
            string baseURL = Convert.ToString(Configuration["baseURL"]);

            services.ConfigureCors();
            services.ConfigureIISIntegration();
            //services.ConfigureLoggerService();
            services.ConfigureSqlServerContext(Configuration);
            ////services.AddScoped<HelpDeskDBContext>(provider => provider.GetService<HelpDeskDBContext>());
            services.ConfigureSqlServerContextForIdentity(Configuration);
            services.ConfigureIdentityWrapper();

            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = baseURL,          //"https://localhost:44377",
                    ValidAudience = baseURL,        //"https://localhost:44377",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
                };
            });

            services.ConfigureRepositoryWrapper();
            services.AddAutoMapper(typeof(Startup));  //this package automapper.extensions.microsoft.dependencyinjection is needed for AddAutoMapper(typeof(Startup))
            //services.ConfigureEmailWrapper(Configuration);
            //services.AddScoped<IEmailSender, EmailSender>();
            services.AddSingleton<ILoggerManagerRepository, NlogLoggerManagerRepository>();
            //services.AddControllers();
            services.AddControllers()
                .AddNewtonsoftJson(
                  options => {
                      options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                  });

            //services.AddHangfire(x => x
            //   .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            //   .UseSimpleAssemblyNameTypeSerializer()
            //   .UseRecommendedSerializerSettings()
            //    .UseSqlServerStorage(Configuration.GetConnectionString("FintrakPASDBConnection"), new SqlServerStorageOptions
            //    {
            //        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            //        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            //        QueuePollInterval = TimeSpan.Zero,
            //        UseRecommendedIsolationLevel = true,
            //        DisableGlobalLocks = true
            //    }));
            ////services.AddHangfire(x =>
            ////{
            ////    x.UseSqlServerStorage(Configuration.GetConnectionString("DBConnection"));
            ////});

            ////// Add the processing server as IHostedService
            ////services.AddHangfireServer();

        }

        //// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundJobClient backgroundJobs, IRepositoryWrapper repository, IEmailSender emailSender, IConfiguration config, ILoggerManagerRepository loggermanager)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRepositoryWrapper repository, IConfiguration config, ILoggerManagerRepository loggermanager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            ////app.UseForwardedHeaders will forward proxy headers to the current request. This will help us during the Linux deployment.
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseHangfireDashboard();     // Map the Dashboard to the root URL
            //app.UseHangfireDashboard("/dashboard");     // Map to the '/dashboard' URL
                                                        //////////backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));
                                                        ////////backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));
            //Hangfire.GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 1 });


            app.UseEndpoints(endpoints =>
            {
                ////endpoints.MapControllers();
                endpoints.MapControllers().RequireAuthorization();
                //endpoints.MapHangfireDashboard();
            });
        }
    }
}
