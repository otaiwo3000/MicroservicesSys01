using Helpdesk.Shared.Entities;
using Helpdesk.Shared.DataAccess.DBContext;
using Helpdesk.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Helpdesk.Business.Interfaces.RepositoryInterfaces;


namespace Helpdesk.Business.Repositories.DataRepositories
{
    public class RolesRepository : RepositoryBase<Roles>, IRolesRepository
    {
        public RolesRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<Roles> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<Roles> _repo;


        public  IEnumerable<Roles> GetAllRoles(int organizationID)
        {
            //var res = FindAll().Select(a=> new { a.Organization.Name});
            var res = FindAll().Where(x => x.OrganizationId == organizationID).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            return res;
        }

        //public IEnumerable<Roles> GetRolesByOrganizationId(int organizationId)
        //{
        //    var res = FindAll().Where(x => x.OrganizationId == organizationId).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
        //    return res;
        //}

        public Roles GetRoleById(int organizationID, long roleId)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.RoleId==roleId).FirstOrDefault();
        }

        public Roles GetRoleWithDetails(int organizationID, long roleId)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.RoleId==roleId)
                .Include(y => y.Organization).FirstOrDefault();
        }

        public void CreateRole(Roles role)
        {
            Create(role);
        }

        public void UpdateRole(Roles role)
        {
            Update(role);
        }

        public void DeleteRole(Roles role)
        {
            Delete(role);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override


    }
}
