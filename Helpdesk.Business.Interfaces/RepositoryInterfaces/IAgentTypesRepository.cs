using Helpdesk.Shared.Entities;
using System.Collections.Generic;

namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{

    public interface IAgentTypesRepository : IRepositoryBase<AgentTypes>
    {
        IEnumerable<AgentTypes> GetAllAgentTypes(int organizationID);
        //IEnumerable<AgentTypes> GetAgentTypesByOrganizationId(int organizationId);
        AgentTypes GetAgentTypeById(int organizationID, int Id);
        AgentTypes GetAgentTypeWithDetails(int organizationID, int Id);
        void CreateAgentType(AgentTypes at);
        void UpdateAgentType(AgentTypes at);
        void DeleteAgentType(AgentTypes at);
    }
}
