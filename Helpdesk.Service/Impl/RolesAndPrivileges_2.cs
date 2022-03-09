

namespace Helpdesk.Service.Impl
{
    //public class RolesAndPrivileges_2
    //{
    //    ////private readonly IConfiguration config;
    //     readonly string constring = "Data Source=DESKTOP-ENE48CR\\SQLSERVER2014;Initial Catalog=FintrakHelpDeskDB;User =sa;Password=password@1;Integrated Security=True";
    //    //private readonly string constring = "Data Source=HELPDESKAPP2\\MSSQLSERVER2019;Initial Catalog=FintrakHelpDeskDB;User =sa;Password=sqluser10$;Integrated Security=false;";


    //    //private readonly Microsoft.Data.SqlClient.SqlConnection constring = Misc.SqlHelper.GetConnection();


    //    public IEnumerable<RolesPrivileges> GetRolesPrivilegesByRoles(List<long> roleIDs)
    //    {
    //   //     var builder = new ConfigurationBuilder()
    //   //.SetBasePath(System.IO.Directory.GetCurrentDirectory())
    //   //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    //   //.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
    //   //.AddEnvironmentVariables();
    //   //     IConfiguration _configStartup = builder.Build();

    //   //     var connectionStringstartup = _configStartup.GetConnectionString("HelpDeskDBConnection:connectionString");

    //        IConfigurationRoot configuration = new ConfigurationBuilder()
    //        .AddJsonFile("appsettings.json.config", optional: true)
    //        .Build();

    //        string connectionString = configuration["HelpDeskDBConnection:connectionString"];

    //        var optionsBuilder = new DbContextOptionsBuilder<HelpDeskDBContext>();
    //        //optionsBuilder.UseSqlServer("Data Source=DESKTOP-ENE48CR\\SQLSERVER2014;Initial Catalog=FintrakHelpDeskDB;User =sa;Password=password@1;Integrated Security=True");
    //        optionsBuilder.UseSqlServer(constring);

    //        //using (HelpDeskDBContext dbContext = new HelpDeskDBContext(optionsBuilder.Options))
    //        //{
    //        //    //GetRolesPrivilegesByRoles
    //        //    var res = dbContext.RolesPrivilegesSet.Where(x => roleIDs.Contains(x.RoleId));
    //        //    return res;
    //        //}

    //        HelpDeskDBContext dbContext = new HelpDeskDBContext(optionsBuilder.Options);
    //        //GetRolesPrivilegesByRoles
    //        var res = dbContext.RolesPrivilegesSet.Where(x => roleIDs.Contains(x.RoleId));
    //        return res;
    //    }

    //    public IEnumerable<Privileges> GetPrivilegesByNames(List<string> prilegenames)
    //    {
    //        IConfigurationRoot configuration = new ConfigurationBuilder()
    //        .AddJsonFile("appsettings.json.config", optional: true)
    //        .Build();

    //        string connectionString = configuration["HelpDeskDBConnection:connectionString"];

    //        var optionsBuilder = new DbContextOptionsBuilder<HelpDeskDBContext>();
    //        optionsBuilder.UseSqlServer(constring);

    //        //using (HelpDeskDBContext dbContext = new HelpDeskDBContext(optionsBuilder.Options))
    //        //{
    //        //    var res = dbContext.PrivilegesSet.Where(x => prilegenames.Contains(x.Name));
    //        //    return res;
    //        //}

    //        HelpDeskDBContext dbContext = new HelpDeskDBContext(optionsBuilder.Options);
    //        var res = dbContext.PrivilegesSet.Where(x => prilegenames.Contains(x.Name));
    //        return res;
    //    }

    //}
}
