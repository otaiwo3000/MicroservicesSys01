using Helpdesk.Shared.Entities;
using System.Collections.Generic;

namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
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
