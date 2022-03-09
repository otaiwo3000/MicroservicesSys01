using System.Collections.Generic;
using UserMgt.Business.Interfaces;
using UserMgt.Shared.Entities;

namespace UserMgt.Business.Interfaces.RepositoryInterfaces
{

    public interface IPendingEmailRepository : IRepositoryBase<PendingEmail>
    {
        IEnumerable<PendingEmail> PendingemailList(int topmails);
        void CreatePendingEmail(PendingEmail pendingemail);
        void UpdatePendingEmail(PendingEmail pendingemail);
        void DeletePendingEmail(PendingEmail pendingemail);
    }
}
