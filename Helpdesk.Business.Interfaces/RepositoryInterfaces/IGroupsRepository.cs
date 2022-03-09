using Helpdesk.Shared.Entities;
using System.Collections.Generic;

namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{

    public interface IGroupsRepository : IRepositoryBase<Groups>
    {
        IEnumerable<Groups> GetAllGroups(int organizationID);
        //IEnumerable<Groups> GetGroupsByOrganizationId(int organizationId);
        Groups GetGroupById(int organizationID, int Id);
        Groups GetGroupWithDetails(int organizationID, int Id);
        void CreateGroup(Groups group);
        void UpdateGroup(Groups group);
        void DeleteGroup(Groups group);
    }
}
