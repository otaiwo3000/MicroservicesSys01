using Helpdesk.Business.Interfaces;
using Helpdesk.Shared.Entities;
using System.Collections.Generic;


namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{
    public interface IUserGroupRepository : IRepositoryBase<UserGroup>
    {
        IEnumerable<UserGroup> GetAllUserGroup();
        UserGroup GetUserGroupById(int Id);
        UserGroup GetUserGroupWithDetails(int Id);
        void CreateUserGroup(UserGroup usergroup);
        void UpdateUserGroup(UserGroup usergroup);
        void DeleteUserGroup(UserGroup usergroup);

        IEnumerable<UserGroup> GetUserGroupsByUserId(long userId);
        IEnumerable<UserGroup> GetUserGroupByGroupId(int OrganizationId, int groupID);

        void CreateRangeUserGroups(IEnumerable<UserGroup> usergroups);
        void UpdateRangeUserGroups(IEnumerable<UserGroup> usergroups);
        void RemoveRangeUserGroups(IEnumerable<UserGroup> usergroups);
    }
}
