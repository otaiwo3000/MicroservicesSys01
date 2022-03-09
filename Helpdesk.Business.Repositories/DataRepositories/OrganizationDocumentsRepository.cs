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
    public class OrganizationDocumentsRepository : RepositoryBase<OrganizationDocuments>, IOrganizationDocumentsRepository
    {
        public OrganizationDocumentsRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<OrganizationDocuments> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<OrganizationDocuments> _repo;


        public  IEnumerable<OrganizationDocuments> GetAllOrganizationDocuments(int organizationID)
        {
            var res = FindAll().Where(x => x.OrganizationId == organizationID)
                .Include(x => x.Organization)
                .Include(x => x.Group)
                .OrderBy(y => y.Organization.Name).ToList();
            return res;
        }

        //public IEnumerable<OrganizationDocuments> GetOrganizationDocumentsByOrganizationId(int organizationID, int organizationId)
        //{
        //    var res = FindAll().Where(x => x.OrganizationId == organizationId).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
        //    return res;
        //}

        public OrganizationDocuments GetOrganizationDocumentById(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID).FirstOrDefault();
        }

        public OrganizationDocuments GetOrganizationDocumentWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.GroupId==Id)
                .Include(y => y.Organization)
                .Include(x => x.Group).FirstOrDefault();
        }

        public void CreateOrganizationDocument(OrganizationDocuments entity)
        {
            Create(entity);
        }

        public void UpdateOrganizationDocument(OrganizationDocuments entity)
        {
            Update(entity);
        }

        public void DeleteOrganizationDocument(OrganizationDocuments entity)
        {
            Delete(entity);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override


    }
}
