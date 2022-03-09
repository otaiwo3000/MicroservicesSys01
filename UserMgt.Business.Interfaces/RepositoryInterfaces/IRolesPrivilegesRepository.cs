using UserMgt.Business.Interfaces;
using UserMgt.Shared.Entities;
using System.Collections.Generic;


namespace UserMgt.Business.Interfaces.RepositoryInterfaces
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
