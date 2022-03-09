using Helpdesk.Shared.Entities;
using System.Collections.Generic;

namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{

    public interface IPendingEmailRepository : IRepositoryBase<PendingEmail>
    {
        IEnumerable<PendingEmail> PendingemailList(int topmails);
        void CreatePendingEmail(PendingEmail pendingemail);
        void UpdatePendingEmail(PendingEmail pendingemail);
        void DeletePendingEmail(PendingEmail pendingemail);
    }
}
