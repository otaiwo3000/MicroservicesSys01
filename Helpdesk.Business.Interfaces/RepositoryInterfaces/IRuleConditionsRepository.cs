using Helpdesk.Shared.Entities;
using System.Collections.Generic;


namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{
    public interface IRuleConditionsRepository : IRepositoryBase<RuleConditions>
    {
        IEnumerable<RuleConditions> GetAllRuleConditions(int organizationID);
        RuleConditions GetRuleConditionById(int organizationID, int Id);
        RuleConditions GetRuleConditionWithDetails(int organizationID, int Id);
        void CreateRuleCondition(RuleConditions rc);
        void UpdateRuleCondition(RuleConditions rc);
        void DeleteRuleCondition(RuleConditions rc);
    }
}
