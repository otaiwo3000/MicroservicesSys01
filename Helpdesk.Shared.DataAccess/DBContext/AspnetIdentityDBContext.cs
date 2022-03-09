using Helpdesk.Shared.Entities.AspNetEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Shared.DataAccess.DBContext
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
