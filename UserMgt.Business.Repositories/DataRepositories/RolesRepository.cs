
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using UserMgt.Business.Interfaces;
using UserMgt.Business.Interfaces.RepositoryInterfaces;
using UserMgt.Business.Repositories;
using UserMgt.Shared.DataAccess.DBContext;
using UserMgt.Shared.Entities;


namespace UserMgt.Business.Repositories.DataRepositories
{
    public class RolesRepository : RepositoryBase<Roles>, IRolesRepository
    {
        public RolesRepository(UserMgtDBContext userMgtDBContext, IRepositoryBase<Roles> repo)
           : base(userMgtDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<Roles> _repo;


        public IEnumerable<Roles> GetAllRoles(int organizationID)
        {
            var res = FindAll().Where(x => x.OrganizationId == organizationID).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            return res;
        }

        //public IEnumerable<Roles> GetAllRoles()
        //{
        //    var res = FindAll().ToList();
        //    return res;
        //}

        public Roles GetRoleById(int organizationID, long roleId)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.RoleId == roleId).FirstOrDefault();
        }

        //public Roles GetRoleById(long roleId)
        //{
        //    return FindByCondition(x => x.RoleId == roleId).FirstOrDefault();
        //}

        public Roles GetRoleWithDetails(int organizationID, long roleId)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.RoleId == roleId)
                .Include(y => y.Organization).FirstOrDefault();
        }

        //public Roles GetRoleWithDetails(long roleId)
        //{
        //    return FindByCondition(x => x.RoleId == roleId).FirstOrDefault();
        //}

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
