using Helpdesk.Shared.Entities;
using System.Collections.Generic;


namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{
    public interface IRuleCasesRepository : IRepositoryBase<RuleCases>
    {
        IEnumerable<RuleCases> GetAllRuleCases(int organizationID);
        RuleCases GetRuleCaseById(int organizationID, int Id);
        RuleCases GetRuleCaseWithDetails(int organizationID, int Id);
        void CreateRuleCase(RuleCases rc);
        void UpdateRuleCase(RuleCases rc);
        void DeleteRuleCase(RuleCases rc);
    }
}
