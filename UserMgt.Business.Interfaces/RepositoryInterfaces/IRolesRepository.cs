using UserMgt.Business.Interfaces;
using UserMgt.Shared.Entities;
using System.Collections.Generic;

namespace UserMgt.Business.Interfaces.RepositoryInterfaces
{

    public interface IRolesRepository : IRepositoryBase<Roles>
    {
        IEnumerable<Roles> GetAllRoles(int organizationID);
        Roles GetRoleById(int organizationID, long roleId);
        Roles GetRoleWithDetails(int organizationID, long roleId);
        //IEnumerable<Roles> GetAllRoles();
        //Roles GetRoleById(long roleId);
        //Roles GetRoleWithDetails(long roleId);
        void CreateRole(Roles role);
        void UpdateRole(Roles role);
        void DeleteRole(Roles role);
    }
}
