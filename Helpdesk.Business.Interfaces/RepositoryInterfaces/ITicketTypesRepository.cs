using Helpdesk.Business.Interfaces;
using Helpdesk.Shared.Entities;
using System.Collections.Generic;


namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{
    public interface ITicketTypesRepository : IRepositoryBase<TicketTypes>
    {
        //IEnumerable<TicketTypes> GetAllTicketTypes();
        //TicketTypes GetTicketTypeById(int Id);
        //TicketTypes GetTicketTypeWithDetails(int Id);

        IEnumerable<TicketTypes> GetAllTicketTypes(int organizationID);
        TicketTypes GetTicketTypeById(int organizationID, int Id);
        TicketTypes GetTicketTypeWithDetails(int organizationID, int Id);
        void CreateTicketType(TicketTypes tt);
        void UpdateTicketType(TicketTypes tt);
        void DeleteTicketType(TicketTypes tt);
    }
}
