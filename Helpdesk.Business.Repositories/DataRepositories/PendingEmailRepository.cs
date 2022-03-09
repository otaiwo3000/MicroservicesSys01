using Helpdesk.Shared.Entities;
using Helpdesk.Shared.DataAccess.DBContext;
using Helpdesk.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Helpdesk.Business.Interfaces.RepositoryInterfaces;


namespace Helpdesk.Business.Repositories.DataRepositories
{
    public class PendingEmailRepository : RepositoryBase<PendingEmail>, IPendingEmailRepository
    {
        public PendingEmailRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<PendingEmail> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<PendingEmail> _repo;


        public IEnumerable<PendingEmail> PendingemailList(int topmails)
        {
            return FindAll().Where(x => x.IsSent == false && x.RetryCount < 3).Take(topmails);
        }

        public void CreatePendingEmail(PendingEmail pendingemail)
        {
            Create(pendingemail);
        }

        public void UpdatePendingEmail(PendingEmail pendingemail)
        {
            Update(pendingemail);
        }

        public void DeletePendingEmail(PendingEmail pendingemail)
        {
            Delete(pendingemail);
        }

    }
}
