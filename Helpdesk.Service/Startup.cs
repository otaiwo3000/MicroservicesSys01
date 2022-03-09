using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
//using System.Collections.Generic;

using AutoMapper;
using EmailService;
using Hangfire;
using Hangfire.SqlServer;
using Helpdesk.Repositories.DataRepositories;
using Helpdesk.Service.Extensions;

using Helpdesk.Service.Impl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
//using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
//using NLog;
using System.Text;
using Helpdesk.Business.Logger;
using Helpdesk.Business.Interfaces;

namespace Helpdesk.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;

            //added on 4Nov2021
            //  //Reading apsettings.json Configuration without IoC: Using ConfigurationBuilder to load the configuration
            //  var builder = new ConfigurationBuilder()
            //.SetBasePath(System.IO.Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
            //.AddEnvironmentVariables();
            //  _configStartup = builder.Build();
        }
        //public IConfiguration _configStartup;  //added on 4Nov2021

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
            services.AddAutoMapper(typeof(Startup));
            services.ConfigureEmailWrapper(Configuration);
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddSingleton<ILoggerManagerRepository, LoggerManagerRepository>();
            //services.ConfigureLoggerManagerRepository();
            services.AddControllers();
            //services.AddControllers().AddNewtonsoftJson();

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HangfireApplication", Version = "v1" });
            //});
            // Add Hangfire services.
            services.AddHangfire(x => x
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                 //.UseSqlServerStorage(Configuration.GetConnectionString("HangfireDBConnection"), new SqlServerStorageOptions
                 .UseSqlServerStorage(Configuration.GetConnectionString("HelpDeskDBConnection"), new SqlServerStorageOptions
                 {
                     CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                     SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                     QueuePollInterval = TimeSpan.Zero,
                     UseRecommendedIsolationLevel = true,
                     DisableGlobalLocks = true
                 }));

            //services.AddHangfire(x =>
            //{
            //    x.UseSqlServerStorage(Configuration.GetConnectionString("DBConnection"));
            //});

            // Add the processing server as IHostedService
            services.AddHangfireServer();

        }

        //// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundJobClient backgroundJobs, IRepositoryWrapper repository, IEmailSender emailSender, IConfiguration config, ILoggerManagerRepository loggermanager)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //app.UseForwardedHeaders will forward proxy headers to the current request. This will help us during the Linux deployment.
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHangfireDashboard();     // Map the Dashboard to the root URL
            app.UseHangfireDashboard("/dashboard");     // Map to the '/dashboard' URL
                                                        ////backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));
                                                        //backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));
            Hangfire.GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 1 });

            string ScheduleForSendPendingEmail = Convert.ToString(Configuration["ScheduleForSendPendingEmail:Schedule"]);
            string ScheduleForSendPendingEmail_IsActive = Convert.ToString(Configuration["ScheduleForSendPendingEmail:Active"]);
            int toppendingemails = int.Parse(Configuration["toppendingemails"]);
            //Misc.PendingEmailCall pec = new Misc.PendingEmailCall(emailSender, repository, loggermanager);
            ////RecurringJob.AddOrUpdate(() => pec.SendPendingEmail(), "*/1 * * * *");
            //RecurringJob.AddOrUpdate(() => pec.SendPendingEmail(toppendingemails), ScheduleForSendPendingEmail);

            if (ScheduleForSendPendingEmail_IsActive.Trim().ToLower() == "true")
            {
                NetMailImpl nmi = new NetMailImpl(repository, config, loggermanager);
                RecurringJob.AddOrUpdate(() => nmi.SendPendingEmails(), ScheduleForSendPendingEmail);
            }

            //string ScheduleForGetMail = Convert.ToString(Configuration["ScheduleForGetMail:Schedule"]);
            //Impl.Emails receiveemail = new Impl.Emails(repository, config, loggermanager);
            ////RecurringJob.AddOrUpdate(() => receiveemail.GetMail(), "*/1 * * * *");
            //RecurringJob.AddOrUpdate(() => receiveemail.GetMail(), ScheduleForGetMail);

            string ScheduleForGetMail = Convert.ToString(Configuration["ScheduleForGetMail:Schedule"]);
            string ScheduleForGetMail_IsActive = Convert.ToString(Configuration["ScheduleForGetMail:Active"]);

            if (ScheduleForGetMail_IsActive.Trim().ToLower() == "true")
            {
                Impl.Emails receiveemail = new Impl.Emails(repository, config, loggermanager);
                RecurringJob.AddOrUpdate(() => receiveemail.GetMail(), ScheduleForGetMail);
            }

            //string ScheduleForCreateTicketApplyTicketRules = Convert.ToString(Configuration["ScheduleForCreateTicketApplyTicketRules:Schedule"]);
            //Impl.TicketCreationFromCustomerEmail createticket = new Impl.TicketCreationFromCustomerEmail(repository, config, loggermanager);
            ////RecurringJob.AddOrUpdate(() => createticket.CreateTicketApplyTicketRules(), "*/3 * * * *");
            //RecurringJob.AddOrUpdate(() => createticket.CreateTicketApplyTicketRules(), ScheduleForCreateTicketApplyTicketRules);

            string ScheduleForCreateTicketApplyTicketRules = Convert.ToString(Configuration["ScheduleForCreateTicketApplyTicketRules:Schedule"]);
            string ScheduleForCreateTicketApplyTicketRules_IsActive = Convert.ToString(Configuration["ScheduleForCreateTicketApplyTicketRules:Active"]);

            if (ScheduleForCreateTicketApplyTicketRules_IsActive.Trim().ToLower() == "true")
            {
                Impl.TicketCreationFromCustomerEmail createticket = new Impl.TicketCreationFromCustomerEmail(repository, config, loggermanager);
                RecurringJob.AddOrUpdate(() => createticket.CreateTicketApplyTicketRules(), ScheduleForCreateTicketApplyTicketRules);
            }

            //app.UseHangfireDashboard();
            //backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));
            //app.UseHangfireDashboard("/dashboard", new DashboardOptions
            //{
            //    AuthorizationFilters = new[] { new CustomAuthorizeFilter() }
            //});

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapControllers().RequireAuthorization();
                endpoints.MapHangfireDashboard();
            });

            //for mvc not for web api
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //            name: "default",
            //            pattern: "{controller=Home}/{action=Index}/{id?}"
            //        ).RequireAuthorization();

            //    endpoints.MapRazorPages().RequireAuthorization();
            //});

        }
    }
}
