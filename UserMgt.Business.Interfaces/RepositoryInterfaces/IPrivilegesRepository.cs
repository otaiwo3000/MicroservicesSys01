using UserMgt.Business.Interfaces;
using UserMgt.Shared.Entities;
using System.Collections.Generic;

namespace UserMgt.Business.Interfaces.RepositoryInterfaces
{
    public interface IPrivilegesRepository : IRepositoryBase<Privileges>
    {
        IEnumerable<Privileges> GetAllPrivileges();
        Privileges GetPrivilegeById(int Id);
        Privileges GetPrivilegeWithDetails(int Id);
        void CreatePrivilege(Privileges privilege);
        void UpdatePrivilege(Privileges privilege);
        void DeletePrivilege(Privileges privilege);

        IEnumerable<Privileges> GetPrivilegesByNames(List<string> privilegenames);
    }
}
