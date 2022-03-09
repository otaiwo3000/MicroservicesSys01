using Helpdesk.Shared.Entities;
using System.Collections.Generic;


namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{
    public interface IRolesPrivilegesRepository : IRepositoryBase<RolesPrivileges>
    {
        IEnumerable<RolesPrivileges> GetAllRolesPrivileges();
        RolesPrivileges GetRolePrivilegeById(long Id);
        RolesPrivileges GetRolePrivilegeWithDetails(long Id);
        void CreateRolePrivilege(RolesPrivileges roleprivilege);
        void UpdateRolePrivilege(RolesPrivileges roleprivilege);
        void DeleteRolePrivilege(RolesPrivileges roleprivilege);

        IEnumerable<RolesPrivileges> GetRolesPrivilegesByRoles(List<long> roles);
    }
}
