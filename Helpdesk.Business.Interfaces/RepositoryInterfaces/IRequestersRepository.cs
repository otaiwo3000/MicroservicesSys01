using Helpdesk.Shared.Entities;
using System.Collections.Generic;

namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{

    public interface IRequestersRepository : IRepositoryBase<Requesters>
    {
        IEnumerable<Requesters> GetAllRequesters(int organizationID);
        Requesters GetRequesterById(int organizationID, int Id);
        Requesters GetRequesterWithDetails(int organizationID, int Id);
        void CreateRequester(Requesters requester);
        void UpdateRequester(Requesters requester);
        void DeleteRequester(Requesters requester);
    }
}
