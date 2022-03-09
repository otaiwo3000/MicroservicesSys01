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
    public class RequestersRepository : RepositoryBase<Requesters>, IRequestersRepository
    {
        public RequestersRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<Requesters> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<Requesters> _repo;


        public  IEnumerable<Requesters> GetAllRequesters(int organizationID)
        {
            var res = FindAll().Where(x => x.OrganizationId == organizationID).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            return res;
        }

        public Requesters GetRequesterById(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.RequesterId == Id).FirstOrDefault();
        }

        public Requesters GetRequesterWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.RequesterId == Id)
                .Include(y => y.Organization).FirstOrDefault();
        }

        public void CreateRequester(Requesters requester)
        {
            Create(requester);
        }

        public void UpdateRequester(Requesters requester)
        {
            Update(requester);
        }

        public void DeleteRequester(Requesters requester)
        {
            Delete(requester);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override


    }
}
