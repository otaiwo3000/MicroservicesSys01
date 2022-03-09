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
    public class AgentTypesRepository : RepositoryBase<AgentTypes>, IAgentTypesRepository
    {
        public AgentTypesRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<AgentTypes> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<AgentTypes> _repo;


        public  IEnumerable<AgentTypes> GetAllAgentTypes(int organizationID)
        {
            var res = FindAll().Where(x => x.OrganizationId == organizationID).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            return res;
        }

        //public IEnumerable<AgentTypes> GetAgentTypesByOrganizationId(int organizationId)
        //{
        //    var res = FindAll().Where(x => x.OrganizationId == organizationId).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
        //    return res;
        //}

        public AgentTypes GetAgentTypeById(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.AgentTypeId==Id).FirstOrDefault();
        }

        public AgentTypes GetAgentTypeWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.AgentTypeId==Id)
                .Include(y => y.Organization).FirstOrDefault();
        }

        public void CreateAgentType(AgentTypes aet)
        {
            Create(aet);
        }

        public void UpdateAgentType(AgentTypes aet)
        {
            Update(aet);
        }

        public void DeleteAgentType(AgentTypes aet)
        {
            Delete(aet);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override


    }
}
