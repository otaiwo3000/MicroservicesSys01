using Helpdesk.Shared.Entities;
using System.Collections.Generic;

namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{
    public interface IInitializedTicketsRepository : IRepositoryBase<InitializedTickets>
    {
        IEnumerable<InitializedTickets> GetAllInitializedTickets(int organizationID);
        InitializedTickets GetInitializedTicketById(int organizationID, int Id);
        InitializedTickets GetInitializedTicketWithDetails(int organizationID, int Id);
        void CreateInitializedTicket(InitializedTickets ts);
        void UpdateInitializedTicket(InitializedTickets ts);
        void DeleteInitializedTicket(InitializedTickets ts);
    }
}
