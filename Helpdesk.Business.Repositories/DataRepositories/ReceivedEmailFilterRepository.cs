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
    public class ReceivedEmailFilterRepository : RepositoryBase<ReceivedEmailFilter>, IReceivedEmailFilterRepository
    {
        public ReceivedEmailFilterRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<ReceivedEmailFilter> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<ReceivedEmailFilter> _repo;


        public  IEnumerable<ReceivedEmailFilter> GetAllReceivedEmailFilter(int organizationID)
        {
            var res = FindAll().Where(x => x.OrganizationId == organizationID).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            return res;
        }

        //public IEnumerable<ActionProperty> GetActionPropertyByOrganizationId(int organizationId)
        //{
        //    var res = FindAll().Where(x => x.OrganizationId == organizationId).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
        //    return res;
        //}

        public ReceivedEmailFilter GetReceivedEmailFilterById(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.ReceivedEmailFilterId == Id).FirstOrDefault();
        }

        public ReceivedEmailFilter GetReceivedEmailFilterWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.ReceivedEmailFilterId == Id)
                .Include(y => y.Organization).FirstOrDefault();
        }

        public void CreateReceivedEmailFilter(ReceivedEmailFilter re)
        {
            Create(re);
        }

        public void UpdateReceivedEmailFilter(ReceivedEmailFilter re)
        {
            Update(re);
        }

        public void DeleteReceivedEmailFilter(ReceivedEmailFilter re)
        {
            Delete(re);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override


    }
}
