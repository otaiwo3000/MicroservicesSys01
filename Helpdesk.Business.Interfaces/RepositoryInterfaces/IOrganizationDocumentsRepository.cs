using Helpdesk.Shared.Entities;
using System.Collections.Generic;

namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{

    public interface IOrganizationDocumentsRepository : IRepositoryBase<OrganizationDocuments>
    {
        IEnumerable<OrganizationDocuments> GetAllOrganizationDocuments(int organizationID);
        OrganizationDocuments GetOrganizationDocumentById(int organizationID, int Id);
        OrganizationDocuments GetOrganizationDocumentWithDetails(int organizationID, int Id);
        void CreateOrganizationDocument(OrganizationDocuments entity);
        void UpdateOrganizationDocument(OrganizationDocuments entity);
        void DeleteOrganizationDocument(OrganizationDocuments entity);
    }
}
