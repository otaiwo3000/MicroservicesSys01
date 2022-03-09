using Helpdesk.Shared.Entities;
using System.Collections.Generic;


namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{
    public interface IUserRoleRepository : IRepositoryBase<UserRole>
    {
        IEnumerable<UserRole> GetAllUserRole();
        UserRole GetUserRoleById(int Id);
        UserRole GetUserRoleWithDetails(int Id);
        void CreateUserRole(UserRole userrole);
        void UpdateUserRole(UserRole userrole);
        void DeleteUserRole(UserRole userrole);

        void CreateRangeUserRoles(IEnumerable<UserRole> userroles);
        void UpdateRangeUserRoles(IEnumerable<UserRole> userroles);
        void RemoveRangeUserRoles(IEnumerable<UserRole> userroles);
        IEnumerable<UserRole> GetUserRolesByUserId(long userId);
    }
}
