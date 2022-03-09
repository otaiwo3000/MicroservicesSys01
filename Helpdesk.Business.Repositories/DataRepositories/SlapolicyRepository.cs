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
    public class SLAPolicyRepository : RepositoryBase<SLAPolicy>, ISLAPolicyRepository
    {
        public SLAPolicyRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<SLAPolicy> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<SLAPolicy> _repo;


        public  IEnumerable<SLAPolicy> GetAllSLAPolicies(int organizationID)
        {
            var res = FindAll().Where(x => x.OrganizationId == organizationID).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            return res;
        }

        public SLAPolicy GetSLAPolicyById(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.SLAPolicyId == Id).FirstOrDefault();
        }

        public SLAPolicy GetSLAPolicyWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.SLAPolicyId == Id)
                .Include(y => y.Organization).FirstOrDefault();
        }

        public void CreateSLAPolicy(SLAPolicy slapolicy)
        {
            Create(slapolicy);
        }

        public void UpdateSLAPolicy(SLAPolicy slapolicy)
        {
            Update(slapolicy);
        }

        public void DeleteSLAPolicy(SLAPolicy slapolicy)
        {
            Delete(slapolicy);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override


    }
}
