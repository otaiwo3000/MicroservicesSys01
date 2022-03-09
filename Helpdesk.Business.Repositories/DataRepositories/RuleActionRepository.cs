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
    public class RuleActionRepository : RepositoryBase<RuleAction>, IRuleActionRepository
    {
        public RuleActionRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<RuleAction> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<RuleAction> _repo;


        public  IEnumerable<RuleAction> GetAllRuleAction(int organizationID)
        {
            var res = FindAll().Where(x => x.OrganizationId == organizationID).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            return res;
        }

        public RuleAction GetRuleActionById(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.RuleActionId == Id).FirstOrDefault();

        }

        public RuleAction GetRuleActionWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.RuleActionId == Id)
               .Include(y => y.Organization)
               .Include(x=>x.Type)
               .Include(x=>x.Status)
               .Include(x=>x.Slapriority)
               .Include(x=>x.Group)
               .Include(x=>x.Module)
               .Include(x=>x.Product)
               .Include(x=>x.Agent)
               .FirstOrDefault();
        }

        public RuleAction GetRuleActionByRuleBatchIDWithDetails(int organizationID, string RuleBatchId)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.RuleBatchId == RuleBatchId)
               .Include(y => y.Organization)
               .Include(x => x.Type)
               .Include(x => x.Status)
               .Include(x => x.Slapriority)
               .Include(x => x.Group)
               .Include(x => x.Module)
               .Include(x => x.Product)
               .Include(x => x.Agent)
               .FirstOrDefault();
        }

        public void CreateRuleAction(RuleAction ruleaction)
        {
            Create(ruleaction);
        }

        public void UpdateRuleAction(RuleAction ruleaction)
        {
            Update(ruleaction);
        }

        public void DeleteRuleAction(RuleAction ruleaction)
        {
            Delete(ruleaction);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override

    }
}
