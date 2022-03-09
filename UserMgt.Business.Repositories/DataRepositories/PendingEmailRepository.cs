
using System.Collections.Generic;
using System.Linq;
using UserMgt.Business.Interfaces;
using UserMgt.Business.Interfaces.RepositoryInterfaces;
using UserMgt.Business.Repositories;
using UserMgt.Shared.DataAccess.DBContext;
using UserMgt.Shared.Entities;

namespace UserMgt.Business.Repositories.DataRepositories
{
    public class PendingEmailRepository : RepositoryBase<PendingEmail>, IPendingEmailRepository
    {
        public PendingEmailRepository(UserMgtDBContext userMgtDBContext, IRepositoryBase<PendingEmail> repo)
           : base(userMgtDBContext)
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
