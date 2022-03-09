using Helpdesk.Shared.Entities;
using System.Collections.Generic;


namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{

    public interface ISLAPolicyRepository : IRepositoryBase<SLAPolicy>
    {
        IEnumerable<SLAPolicy> GetAllSLAPolicies(int organizationID);
        SLAPolicy GetSLAPolicyById(int organizationID, int Id);
        SLAPolicy GetSLAPolicyWithDetails(int organizationID, int Id);
        void CreateSLAPolicy(SLAPolicy entity);
        void UpdateSLAPolicy(SLAPolicy entity);
        void DeleteSLAPolicy(SLAPolicy entity);
    }
}
