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
    }
}
