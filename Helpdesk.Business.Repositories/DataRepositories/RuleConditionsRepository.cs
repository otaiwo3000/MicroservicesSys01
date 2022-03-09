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
    public class RuleConditionsRepository : RepositoryBase<RuleConditions>, IRuleConditionsRepository
    {
        public RuleConditionsRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<RuleConditions> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<RuleConditions> _repo;


        public  IEnumerable<RuleConditions> GetAllRuleConditions(int organizationID)
        {
            var res = FindAll().Where(x => x.OrganizationId == organizationID).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            return res;
        }

        public RuleConditions GetRuleConditionById(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.Id == Id).FirstOrDefault();
        }

        public RuleConditions GetRuleConditionWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.Id == Id)
               .Include(y => y.Organization).FirstOrDefault();
        }

        public void CreateRuleCondition(RuleConditions rulecondition)
        {
            Create(rulecondition);
        }

        public void UpdateRuleCondition(RuleConditions rulecondition)
        {
            Update(rulecondition);
        }

        public void DeleteRuleCondition(RuleConditions rulecondition)
        {
            Delete(rulecondition);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override

    }
}
