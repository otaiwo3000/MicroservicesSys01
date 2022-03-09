using Helpdesk.Shared.Entities;
using System.Collections.Generic;


namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{
    public interface ITicketStatusRepository : IRepositoryBase<TicketStatus>
    {
        IEnumerable<TicketStatus> GetAllTicketStatus();
        TicketStatus GetTicketStatusById(int Id);
        TicketStatus GetTicketStatusWithDetails(int Id);
        void CreateTicketStatus(TicketStatus ts);
        void UpdateTicketStatus(TicketStatus ts);
        void DeleteTicketStatus(TicketStatus ts);
    }
}
