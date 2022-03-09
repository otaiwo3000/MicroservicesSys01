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
    public class UserGroupRepository : RepositoryBase<UserGroup>, IUserGroupRepository
    {
        public UserGroupRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<UserGroup> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<UserGroup> _repo;


        public  IEnumerable<UserGroup> GetAllUserGroup()
        {
            //var res = FindAll().Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            var res = FindAll().OrderBy(y => y.UserId).ToList();
            return res;
        }

        public UserGroup GetUserGroupById(int Id)
        {
            return FindByCondition(x => x.UserGroupId.Equals(Id)).FirstOrDefault();
        }

        public UserGroup GetUserGroupWithDetails(int Id)
        {
            return FindByCondition(x => x.UserGroupId.Equals(Id))
                //.Include(y => y.Organization)
                .FirstOrDefault();
        }

        public void CreateUserGroup(UserGroup usergroup)
        {
            Create(usergroup);
        }

        public void UpdateUserGroup(UserGroup usergroup)
        {
            Update(usergroup);
        }

        public void DeleteUserGroup(UserGroup usergroup)
        {
            Delete(usergroup);
        }

        public void CreateRangeUserGroups(IEnumerable<UserGroup> usergroups)
        {
            CreateRange(usergroups);
        }

        public IEnumerable<UserGroup> GetUserGroupsByUserId(long userid)
        {
            return FindByCondition(x => x.UserId == userid)
                 .Include(x => x.Group)
                .Include(x => x.User);
        }

        public IEnumerable<UserGroup> GetUserGroupByGroupId(int OrganizationId,int groupID)
        {
            //var res = FindAll().Where(x=>x.GroupId==groupID && x.Group.OrganizationId== OrganizationId)
            var res = FindByCondition(x => x.GroupId == groupID && x.Group.OrganizationId == OrganizationId)
                .Include(x=>x.Group)
                .Include(x=>x.User)
                .ToList();
            return res;
        }

        public void UpdateRangeUserGroups(IEnumerable<UserGroup> usergroups)
        {
            UpdateRange(usergroups);
        }

        public void RemoveRangeUserGroups(IEnumerable<UserGroup> usergroups)
        {
            RemoveRange(usergroups);
        }


        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override
    }
}
