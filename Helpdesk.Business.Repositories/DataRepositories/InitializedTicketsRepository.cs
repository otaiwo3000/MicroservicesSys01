using Helpdesk.Shared.Entities;
using Helpdesk.Shared.DataAccess.DBContext;
using Helpdesk.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Helpdesk.Business.Interfaces.RepositoryInterfaces;


namespace Helpdesk.Business.Repositories.DataRepositories
{
    public class InitializedTicketsRepository : RepositoryBase<InitializedTickets>, IInitializedTicketsRepository
    {
        public InitializedTicketsRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<InitializedTickets> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<InitializedTickets> _repo;


        public IEnumerable<InitializedTickets> GetAllInitializedTickets(int organizationID)
        {
            //var res = FindAll().Where(x => x.OrganizationId == organizationID).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            var res = FindAll().Where(x => x.OrganizationId == organizationID).Include(x => x.Organization).ToList();
            return res;
        }

        public InitializedTickets GetInitializedTicketById(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.InitializedTicketId == Id).FirstOrDefault();
        }

        public InitializedTickets GetInitializedTicketWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.InitializedTicketId == Id).Include(y => y.Organization).FirstOrDefault();
        }

        public void CreateInitializedTicket(InitializedTickets ticket)
        {
            Create(ticket);
        }

        public void UpdateInitializedTicket(InitializedTickets ticket)
        {
            Update(ticket);
        }

        public void DeleteInitializedTicket(InitializedTickets ticket)
        {
            Delete(ticket);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override

    }
}
