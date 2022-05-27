using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMgt.Business.EmailService;
using UserMgt.Business.Interfaces;
using UserMgt.Business.Repositories;
using UserMgt.Shared.DataAccess.DBContext;
using UserMgt.Shared.Entities.AspNetEntities;

namespace UserMgt.Service.Extensions
{
    //NOTE
    //All of my configuration code could be written inside the ConfigureServices method, but large applications could potentially contain many services. As a result, it could make this method unreadable and hard to maintain. Therefore I will create extension methods for each configuration and place the configuration code inside those methods.

    //Methods inside this class below will be known/treated/regarded as extension methods.An extension method is inherently the static method. They play a great role in .NET Core configuration because they increase the readability of our code for sure. What makes it different from the other static methods is that it accepts “this” as the first parameter, and “this” represents the data type of the object which uses that extension method. An extension method must be inside a static class. This kind of method extends the behavior of the types in .NET.
    public static class ServiceExtensions
    {
        //First, I need to configure CORS in our application. CORS (Cross-Origin Resource Sharing) is a mechanism that gives rights to the user to access resources from the server on a different domain. Because I will use Angular as a client-side on a different domain than the server’s domain, configuring CORS is mandatory. 
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            //I do not initialize any of the properties inside the options because I am just fine with the default values. 
            services.Configure<IISOptions>(options =>
            {

            });
        }

        ////public static void ConfigureLoggerService(this IServiceCollection services)
        ////{
        ////    services.AddSingleton<ILoggerManager, LoggerManager>();
        ////}

        //public static void ConfigureSqlServerContext(this IServiceCollection services, IConfiguration config)
        //{
        //    var connectionString = config["ConnectionStrings:UserMgtDBConnection"];
        //    services.AddDbContext<UserMgtDBContext>(o => o.UseSqlServer(connectionString));
        //    //services.AddDbContextPool<HelpDeskDBContext>(o => o.UseSqlServer(connectionString));
        //}

        public static void ConfigureSqlServerContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["ConnectionStrings:UserMgtDBConnection2"];
            services.AddDbContext<UserMgtDBContext>(o => o.UseSqlite(connectionString));
            //services.AddDbContextPool<HelpDeskDBContext>(o => o.UseSqlServer(connectionString));
        }


        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureSqlServerContextForIdentity(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["ConnectionStrings:UserMgtDBConnection2"];
            //services.AddDbContext<AspnetIdentityDBContext>(o => o.UseSqlServer(connectionString));
            services.AddDbContext<AspnetIdentityDBContext>(o => o.UseSqlite(connectionString));
        }

        public static void ConfigureIdentityWrapper(this IServiceCollection services)
        {
            services.AddIdentity<Aspnetusers, IdentityRole>()
                //.AddEntityFrameworkSqlite().AddDbContext<AspnetIdentityDBContext>()
               .AddEntityFrameworkStores<AspnetIdentityDBContext>()
               .AddDefaultTokenProviders();     //NOTE: registering the token provider for the application. This will enable password reset token generation (ie help you to be able to create password reset token during password reset operation), otherwise, your password reset implementation will not work.

            services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromHours(2));   //password reset token will be valid for a limited time of 2hrs

            //services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<AspnetIdentityDBContext>();

            //services.AddIdentity<Aspnetusers, IdentityRole>(opt =>
            //{
            //    opt.Password.RequiredLength = 7;
            //    opt.Password.RequireDigit = false;
            //    opt.Password.RequireUppercase = false;
            //    opt.User.RequireUniqueEmail = true;
            //})
            // .AddEntityFrameworkStores<AspnetIdentityDBContext>()
            // .AddDefaultTokenProviders();
        }

        public static void ConfigureEmailWrapper(this IServiceCollection services, IConfiguration config)
        {
            var emailConfig = config.GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);     // registering EmailConfiguration as a singleton
        }

    }
}
