using Helpdesk.Shared.Entities;
using System.Collections.Generic;


namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{

    public interface ISLAPolicyPriorityRepository : IRepositoryBase<SLAPolicyPriority>
    {
        IEnumerable<SLAPolicyPriority> GetAllSLAPolicyPriorities(int organizationID);
        SLAPolicyPriority GetSLAPolicyPriorityById(int organizationID, int Id);
        SLAPolicyPriority GetSLAPolicyPriorityWithDetails(int organizationID, int Id);
        void CreateSLAPolicyPriority(SLAPolicyPriority entity);
        void UpdateSLAPolicyPriority(SLAPolicyPriority entity);
        void DeleteSLAPolicyPriority(SLAPolicyPriority entity);
    }
}
