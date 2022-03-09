using System.Collections.Generic;
using UserMgt.Business.Interfaces;
using UserMgt.Shared.Entities;

namespace UserMgt.Business.Interfaces.RepositoryInterfaces
{

    public interface IOrganizationsRepository : IRepositoryBase<Organizations>
    {
        IEnumerable<Organizations> GetAllOrganizations();
        Organizations GetOrganizationById(int organizationId);
        Organizations GetOrganizationWithDetails(int organizationId);
        void CreateOrganization(Organizations organization);
        void UpdateOrganization(Organizations organization);
        void DeleteOrganization(Organizations organization);
    }
}
