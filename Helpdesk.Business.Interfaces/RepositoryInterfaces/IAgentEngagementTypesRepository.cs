using Helpdesk.Shared.Entities;
using System.Collections.Generic;

namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{

    public interface IAgentEngagementTypesRepository : IRepositoryBase<AgentEngagementTypes>
    {
        IEnumerable<AgentEngagementTypes> GetAllAgentEngagementTypes(int organizationID);
        //IEnumerable<AgentEngagementTypes> GetAgentEngagementTypesByOrganizationId(int organizationId);
        AgentEngagementTypes GetAgentEngagementTypeById(int organizationID, int Id);
        AgentEngagementTypes GetAgentEngagementTypeWithDetails(int organizationID, int Id);
        void CreateAgentEngagementType(AgentEngagementTypes aet);
        void UpdateAgentEngagementType(AgentEngagementTypes aet);
        void DeleteAgentEngagementType(AgentEngagementTypes aet);

    }
}
