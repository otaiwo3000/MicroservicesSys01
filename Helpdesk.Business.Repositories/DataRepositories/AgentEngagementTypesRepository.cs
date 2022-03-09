using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Helpdesk.Shared.Entities;
using Helpdesk.Shared.DataAccess.DBContext;
using Helpdesk.Business.Interfaces;
using Helpdesk.Business.Interfaces.RepositoryInterfaces;


namespace Helpdesk.Business.Repositories.DataRepositories
{
    public class AgentEngagementTypesRepository : RepositoryBase<AgentEngagementTypes>, IAgentEngagementTypesRepository
    {
        public AgentEngagementTypesRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<AgentEngagementTypes> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<AgentEngagementTypes> _repo;


        public  IEnumerable<AgentEngagementTypes> GetAllAgentEngagementTypes(int organizationID)
        {
            var res = FindAll().Where(x=>x.OrganizationId==organizationID).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            return res;
        }

        //public IEnumerable<AgentEngagementTypes> GetAgentEngagementTypesByOrganizationId(int organizationId)
        //{
        //    var res = FindAll().Where(x=>x.OrganizationId==organizationId).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
        //    return res;
        //}

        public AgentEngagementTypes GetAgentEngagementTypeById(int organizationID, int Id)
        {
            return FindByCondition(x => x. OrganizationId== organizationID && x.AgentEngagementTypeId==Id).Include(x=>x.Organization).FirstOrDefault();
        }

        public AgentEngagementTypes GetAgentEngagementTypeWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.AgentEngagementTypeId==Id)
                .Include(y => y.Organization)
                .FirstOrDefault();
        }

        public void CreateAgentEngagementType(AgentEngagementTypes aet)
        {
            Create(aet);       
        }

        public void UpdateAgentEngagementType(AgentEngagementTypes aet)
        {
            Update(aet);
        }

        public void DeleteAgentEngagementType(AgentEngagementTypes aet)
        {
            Delete(aet);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override


    }
}
