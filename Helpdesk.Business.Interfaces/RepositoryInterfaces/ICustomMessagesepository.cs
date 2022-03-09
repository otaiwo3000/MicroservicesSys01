using Helpdesk.Shared.Entities;
using System.Collections.Generic;

namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{

    public interface ICustomMessagesRepository : IRepositoryBase<CustomMessages>
    {
        IEnumerable<CustomMessages> GetAllCustomMessages(int organizationID);
        CustomMessages GetCustomMessageById(int organizationID, int Id);
        CustomMessages GetCustomMessageWithDetails(int organizationID, int Id);
        void CreateCustomMessage(CustomMessages cm);
        void UpdateCustomMessage(CustomMessages cm);
        void DeleteCustomMessage(CustomMessages cm);
    }
}
