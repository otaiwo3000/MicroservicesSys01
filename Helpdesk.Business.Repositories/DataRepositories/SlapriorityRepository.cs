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
    public class SLAPriorityRepository : RepositoryBase<SLAPriority>, ISLAPriorityRepository
    {
        public SLAPriorityRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<SLAPriority> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<SLAPriority> _repo;


        public  IEnumerable<SLAPriority> GetAllSLAPriorities(int organizationID)
        {
            var res = FindAll().Where(x => x.OrganizationId == organizationID).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            return res;
        }

        public SLAPriority GetSLAPriorityById(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.SLAPriorityId == Id).FirstOrDefault();
        }

        public SLAPriority GetSLAPriorityWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.SLAPriorityId == Id)
                .Include(y => y.Organization).FirstOrDefault();
        }

        public void CreateSLAPriority(SLAPriority slapriority)
        {
            Create(slapriority);
        }

        public void UpdateSLAPriority(SLAPriority slapriority)
        {
            Update(slapriority);
        }

        public void DeleteSLAPriority(SLAPriority slapriority)
        {
            Delete(slapriority);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override


    }
}
