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
    public class GroupTypesRepository : RepositoryBase<GroupTypes>, IGroupTypesRepository
    {
        public GroupTypesRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<GroupTypes> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<GroupTypes> _repo;


        public  IEnumerable<GroupTypes> GetAllGroupTypes(int organizationID)
        {
            var res = FindAll().Where(x => x.OrganizationId == organizationID).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            return res;
        }

        //public IEnumerable<GroupTypes> GetGroupTypesByOrganizationId(int organizationId)
        //{
        //    var res = FindAll().Where(x => x.OrganizationId == organizationId).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
        //    return res;
        //}

        public GroupTypes GetGroupTypeById(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.GroupTypeId==Id).FirstOrDefault();
        }

        public GroupTypes GetGroupTypeWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.GroupTypeId==Id)
                .Include(y => y.Organization).FirstOrDefault();
        }

        public void CreateGroupType(GroupTypes grouptype)
        {
            Create(grouptype);
        }

        public void UpdateGroupType(GroupTypes grouptype)
        {
            Update(grouptype);
        }

        public void DeleteGroupType(GroupTypes grouptype)
        {
            Delete(grouptype);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override


    }
}
