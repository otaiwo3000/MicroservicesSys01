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
    public class SLAPolicyPriorityRepository : RepositoryBase<SLAPolicyPriority>, ISLAPolicyPriorityRepository
    {
        public SLAPolicyPriorityRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<SLAPolicyPriority> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<SLAPolicyPriority> _repo;


        public  IEnumerable<SLAPolicyPriority> GetAllSLAPolicyPriorities(int organizationID)
        {
            var res = FindAll().Where(x => x.SLAPolicy.OrganizationId == organizationID)
                .Include(x => x.SLAPolicy)
                .Include(x => x.SLAPriority)
                .Include(x => x.SLAPolicy.Organization)
                .ToList();
            return res;
        }

        public SLAPolicyPriority GetSLAPolicyPriorityById(int organizationID, int Id)
        {
            return FindByCondition(x => x.SLAPolicy.OrganizationId == organizationID && x.SLAPolicyPriorityId == Id).FirstOrDefault();
        }

        public SLAPolicyPriority GetSLAPolicyPriorityWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.SLAPolicy.OrganizationId == organizationID && x.SLAPolicyPriorityId == Id)
                 .Include(x => x.SLAPolicy)
                .Include(x => x.SLAPriority)
                .Include(x => x.SLAPolicy.Organization)
                .FirstOrDefault();
        }

        public void CreateSLAPolicyPriority(SLAPolicyPriority entity)
        {
            Create(entity);
        }

        public void UpdateSLAPolicyPriority(SLAPolicyPriority entity)
        {
            Update(entity);
        }

        public void DeleteSLAPolicyPriority(SLAPolicyPriority entity)
        {
            Delete(entity);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override


    }
}
