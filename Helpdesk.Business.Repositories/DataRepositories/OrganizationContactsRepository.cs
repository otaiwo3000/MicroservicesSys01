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
    public class OrganizationContactsRepository : RepositoryBase<OrganizationContacts>, IOrganizationContactsRepository
    {
        public OrganizationContactsRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<OrganizationContacts> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<OrganizationContacts> _repo;


        public  IEnumerable<OrganizationContacts> GetAllOrganizationContacts(int organizationID)
        {
            //var res = FindAll().Select(a=> new { a.Organization.Name});
            var res = FindAll().Where(x => x.OrganizationId == organizationID).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            return res;
        }

        //public IEnumerable<OrganizationContacts> GetOrganizationContactsByOrganizationId(int organizationId)
        //{
        //    var res = FindAll().Where(x => x.OrganizationId == organizationId).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
        //    return res;
        //}

        public OrganizationContacts GetOrganizationContactById(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.OrganizationContactId==Id).FirstOrDefault();
        }

        public OrganizationContacts GetOrganizationContactWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.OrganizationContactId==Id)
                .Include(y => y.Organization).FirstOrDefault();
        }

        public void CreateOrganizationContact(OrganizationContacts orgcontact)
        {
            Create(orgcontact);
        }

        public void UpdateOrganizationContact(OrganizationContacts orgcontact)
        {
            Update(orgcontact);
        }

        public void DeleteOrganizationContact(OrganizationContacts orgcontact)
        {
            Delete(orgcontact);
        }

        public IEnumerable<OrganizationContacts> RequesterToGetOrganizationContacts(int organizationID)
        {
            return FindAll().Where(x => x.OrganizationId == organizationID);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override

    }
}
