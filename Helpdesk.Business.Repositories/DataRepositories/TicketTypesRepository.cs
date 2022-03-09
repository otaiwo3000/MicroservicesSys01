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
    public class TicketTypesRepository : RepositoryBase<TicketTypes>, ITicketTypesRepository
    {
        public TicketTypesRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<TicketTypes> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<TicketTypes> _repo;


        public IEnumerable<TicketTypes> GetAllTicketTypes(int organizationID)
        {
            var res = FindAll().Where(x => x.OrganizationId == organizationID).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            return res;
        }

        public TicketTypes GetTicketTypeById(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.TicketTypeId == Id).FirstOrDefault();
        }

        public TicketTypes GetTicketTypeWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.TicketTypeId == Id)
                .Include(y => y.Organization).FirstOrDefault();
        }
      
        public void CreateTicketType(TicketTypes tickettype)
        {
            Create(tickettype);
        }

        public void UpdateTicketType(TicketTypes tickettype)
        {
            Update(tickettype);
        }

        public void DeleteTicketType(TicketTypes tickettype)
        {
            Delete(tickettype);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override

    }
}
