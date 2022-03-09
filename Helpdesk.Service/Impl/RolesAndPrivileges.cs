
using Helpdesk.Shared.DataAccess.DBContext;
using Helpdesk.Shared.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;


namespace Helpdesk.Service.Impl
{
    public class RolesAndPrivileges
    {             
        public IEnumerable<RolesPrivileges> GetRolesPrivilegesByRoles(AuthorizationFilterContext context, List<long> roleIDs)
        {
            //applying RequestServices feature provided by .net core
            var dbContext = context.HttpContext
                                .RequestServices
                                .GetService(typeof(HelpDeskDBContext)) as HelpDeskDBContext;

            var res = dbContext.RolesPrivilegesSet.Where(x => roleIDs.Contains(x.RoleId));

            return res;
        }

        public IEnumerable<Privileges> GetPrivilegesByNames(AuthorizationFilterContext context, List<string> prilegenames)
        {
            //applying RequestServices feature provided by .net core
            var dbContext = context.HttpContext
                                .RequestServices
                                .GetService(typeof(HelpDeskDBContext)) as HelpDeskDBContext;
            
            var res = dbContext.PrivilegesSet.Where(x => prilegenames.Contains(x.Name));

            return res;
        }
    }
}
