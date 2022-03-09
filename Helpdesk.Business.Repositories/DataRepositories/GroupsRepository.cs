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
    public class GroupsRepository : RepositoryBase<Groups>, IGroupsRepository
    {
        public GroupsRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<Groups> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<Groups> _repo;


        public  IEnumerable<Groups> GetAllGroups(int organizationID)
        {
            var res = FindAll().Where(x => x.OrganizationId == organizationID)
                .Include(x => x.Organization)
                .Include(x=>x.GroupType)
                .Include(x=>x.GroupLead)
                .OrderBy(y => y.Organization.Name).ToList();
            return res;
        }

        //public IEnumerable<Groups> GetGroupsByOrganizationId(int organizationID, int organizationId)
        //{
        //    var res = FindAll().Where(x => x.OrganizationId == organizationId).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
        //    return res;
        //}

        public Groups GetGroupById(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.GroupId==Id).FirstOrDefault();
        }

        public Groups GetGroupWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.GroupId==Id)
                .Include(x => x.Organization)
                .Include(x => x.GroupType)
                .Include(x => x.GroupLead)
                .FirstOrDefault();
        }

        public void CreateGroup(Groups group)
        {
            Create(group);
        }

        public void UpdateGroup(Groups group)
        {
            Update(group);
        }

        public void DeleteGroup(Groups group)
        {
            Delete(group);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override


    }
}
