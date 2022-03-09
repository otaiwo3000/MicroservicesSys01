using Helpdesk.Shared.Entities;
using System.Collections.Generic;


namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{
    public interface IRuleAction_2Repository : IRepositoryBase<RuleAction_2>
    {
        IEnumerable<RuleAction_2> GetAllRuleAction_2();
        //IEnumerable<RuleAction_2> GetRuleAction_2ByRulebatchID(Guid RuleBatchId);
        IEnumerable<RuleAction_2> GetRuleAction_2ByRulebatchID(string RuleBatchId);
        RuleAction_2 GetRuleAction_2ById(int Id);
        RuleAction_2 GetRuleAction_2WithDetails(int Id);
        void CreateRuleAction_2(RuleAction_2 ra);
        void UpdateRuleAction_2(RuleAction_2 ra);
        void DeleteRuleAction_2(RuleAction_2 ra);
        void CreateRangeRuleAction_2(IEnumerable<RuleAction_2> ruleactions);

        void RemoveRangeRuleAction_2(IEnumerable<RuleAction_2> ruleactions);
    }
}
