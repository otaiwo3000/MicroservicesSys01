using Helpdesk.Shared.Entities;
using System.Collections.Generic;

namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{

    public interface IOrganizationContactsRepository : IRepositoryBase<OrganizationContacts>
    {
        IEnumerable<OrganizationContacts> GetAllOrganizationContacts(int organizationID);
        //IEnumerable<OrganizationContacts> GetOrganizationContactsByOrganizationId(int organizationId);
        OrganizationContacts GetOrganizationContactById(int organizationID, int organizationcontactId);
        OrganizationContacts GetOrganizationContactWithDetails(int organizationID, int organizationcontactId);
        void CreateOrganizationContact(OrganizationContacts organizationcontact);
        void UpdateOrganizationContact(OrganizationContacts organizationcontact);
        void DeleteOrganizationContact(OrganizationContacts organizationcontact);
        IEnumerable<OrganizationContacts> RequesterToGetOrganizationContacts(int organizationID);
    }
}
