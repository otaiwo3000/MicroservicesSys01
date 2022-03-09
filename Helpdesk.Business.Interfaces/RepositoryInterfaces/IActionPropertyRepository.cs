using Helpdesk.Shared.Entities;
using System.Collections.Generic;

namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{

    public interface IActionPropertyRepository : IRepositoryBase<ActionProperty>
    {
        IEnumerable<ActionProperty> GetAllActionProperty(int organizationID);
        ActionProperty GetActionPropertyById(int organizationID, int Id);
        ActionProperty GetActionPropertyWithDetails(int organizationID, int Id);
        void CreateActionProperty(ActionProperty at);
        void UpdateActionProperty(ActionProperty at);
        void DeleteActionProperty(ActionProperty at);
    }
}
