using Helpdesk.Shared.Entities;
using System.Collections.Generic;


namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{

    public interface ISLAPriorityRepository : IRepositoryBase<SLAPriority>
    {
        IEnumerable<SLAPriority> GetAllSLAPriorities(int organizationID);
        SLAPriority GetSLAPriorityById(int organizationID, int Id);
        SLAPriority GetSLAPriorityWithDetails(int organizationID, int Id);
        void CreateSLAPriority(SLAPriority entity);
        void UpdateSLAPriority(SLAPriority entity);
        void DeleteSLAPriority(SLAPriority entity);
    }
}
