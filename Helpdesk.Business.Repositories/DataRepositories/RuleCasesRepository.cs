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
    public class RuleCasesRepository : RepositoryBase<RuleCases>, IRuleCasesRepository
    {
        public RuleCasesRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<RuleCases> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<RuleCases> _repo;


        public  IEnumerable<RuleCases> GetAllRuleCases(int organizationID)
        {
            var res = FindAll().Where(x => x.OrganizationId == organizationID).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            return res;
        }

        public RuleCases GetRuleCaseById(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.Id == Id).FirstOrDefault();
        }

        public RuleCases GetRuleCaseWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.Id == Id)
               .Include(y => y.Organization).FirstOrDefault();
        }

        public void CreateRuleCase(RuleCases rulecases)
        {
            Create(rulecases);
        }

        public void UpdateRuleCase(RuleCases rulecases)
        {
            Update(rulecases);
        }

        public void DeleteRuleCase(RuleCases rulecases)
        {
            Delete(rulecases);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override

    }
}
