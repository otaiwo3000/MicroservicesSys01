using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UserMgt.Shared.Entities.AspNetEntities;

namespace UserMgt.Shared.DataAccess.DBContext
{
    public partial class AspnetIdentityDBContext : IdentityDbContext<Aspnetusers>
    //public abstract class AspnetIdentityDBContext : IdentityDbContext
    {

        public AspnetIdentityDBContext(DbContextOptions options)
            : base(options)
        {

        }



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Filename=MyDatabase.db");
        //}     

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "MyDb.db" };
        //    var connectionString = connectionStringBuilder.ToString();
        //    var connection = new SqliteConnection(connectionString);

        //    optionsBuilder.UseSqlite(connection);
        //}
    }
}
