using Helpdesk.Shared.Entities;
using System.Collections.Generic;


namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{
    public interface IRuleActionRepository : IRepositoryBase<RuleAction>
    {
        IEnumerable<RuleAction> GetAllRuleAction(int organizationID);
        RuleAction GetRuleActionById(int organizationID, int Id);
        RuleAction GetRuleActionWithDetails(int organizationID, int Id);
        //RuleAction GetRuleActionByRuleBatchIDWithDetails(int organizationID, Guid RuleBatchId);
        RuleAction GetRuleActionByRuleBatchIDWithDetails(int organizationID, string RuleBatchId);
        void CreateRuleAction(RuleAction ra);
        void UpdateRuleAction(RuleAction ra);
        void DeleteRuleAction(RuleAction ra);
    }
}
