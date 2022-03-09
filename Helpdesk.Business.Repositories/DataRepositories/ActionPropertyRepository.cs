using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Helpdesk.Shared.Entities;
using Helpdesk.Shared.DataAccess.DBContext;
using Helpdesk.Business.Interfaces;
using Helpdesk.Business.Interfaces.RepositoryInterfaces;


namespace Helpdesk.Business.Repositories.DataRepositories
{
    public class ActionPropertyRepository : RepositoryBase<ActionProperty>, IActionPropertyRepository
    {
        public ActionPropertyRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<ActionProperty> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<ActionProperty> _repo;


        public  IEnumerable<ActionProperty> GetAllActionProperty(int organizationID)
        {
            var res = FindAll().Where(x => x.OrganizationId == organizationID).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            return res;
        }

        //public IEnumerable<ActionProperty> GetActionPropertyByOrganizationId(int organizationId)
        //{
        //    var res = FindAll().Where(x => x.OrganizationId == organizationId).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
        //    return res;
        //}

        public ActionProperty GetActionPropertyById(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.ActionPropertyId == Id).FirstOrDefault();
        }

        public ActionProperty GetActionPropertyWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.ActionPropertyId == Id)
                .Include(y => y.Organization).FirstOrDefault();
        }

        public void CreateActionProperty(ActionProperty aet)
        {
            Create(aet);
        }

        public void UpdateActionProperty(ActionProperty aet)
        {
            Update(aet);
        }

        public void DeleteActionProperty(ActionProperty aet)
        {
            Delete(aet);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override


    }
}
