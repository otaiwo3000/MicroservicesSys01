using Helpdesk.Shared.Entities;
using System.Collections.Generic;

namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{

    public interface IRolesRepository : IRepositoryBase<Roles>
    {
        IEnumerable<Roles> GetAllRoles(int organizationID);
        //IEnumerable<Roles> GetRolesByOrganizationId(int organizationId);
        Roles GetRoleById(int organizationID, long roleId);
        Roles GetRoleWithDetails(int organizationID, long roleId);
        void CreateRole(Roles role);
        void UpdateRole(Roles role);
        void DeleteRole(Roles role);
    }
}
