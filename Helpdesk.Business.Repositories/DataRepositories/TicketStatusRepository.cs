using Helpdesk.Shared.Entities;
using Helpdesk.Shared.DataAccess.DBContext;
using Helpdesk.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Helpdesk.Business.Interfaces.RepositoryInterfaces;

namespace Helpdesk.Business.Repositories.DataRepositories
{
    public class TicketStatusRepository : RepositoryBase<TicketStatus>, ITicketStatusRepository
    {
        public TicketStatusRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<TicketStatus> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<TicketStatus> _repo;


        public  IEnumerable<TicketStatus> GetAllTicketStatus()
        {
            var res = FindAll().ToList();
            return res;
        }

        public TicketStatus GetTicketStatusById(int Id)
        {
            return FindByCondition(x => x.TicketStatusId == Id).FirstOrDefault();
        }

        public TicketStatus GetTicketStatusWithDetails(int Id)
        {
            return FindByCondition(x => x.TicketStatusId == Id).FirstOrDefault();
        }

        public void CreateTicketStatus(TicketStatus ticketstatus)
        {
            Create(ticketstatus);
        }

        public void UpdateTicketStatus(TicketStatus ticketstatus)
        {
            Update(ticketstatus);
        }

        public void DeleteTicketStatus(TicketStatus ticketstatus)
        {
            Delete(ticketstatus);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override

    }
}
