using Helpdesk.Shared.Entities;
using System.Collections.Generic;

namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{
    public interface IGroupTypesRepository : IRepositoryBase<GroupTypes>
    {
        IEnumerable<GroupTypes> GetAllGroupTypes(int organizationID);
        //IEnumerable<GroupTypes> GetGroupTypesByOrganizationId(int organizationId);
        GroupTypes GetGroupTypeById(int organizationID, int Id);
        GroupTypes GetGroupTypeWithDetails(int organizationID, int Id);
        void CreateGroupType(GroupTypes grouptype);
        void UpdateGroupType(GroupTypes grouptype);
        void DeleteGroupType(GroupTypes groupype);
    }
}
