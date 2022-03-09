
using Helpdesk.Shared.Entities;
using System.Collections.Generic;


namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{
    public interface ITicketRulesRepository : IRepositoryBase<TicketRules>
    {
        IEnumerable<TicketRules> GetAllTicketRules(int organizationID);
        IEnumerable<TicketRules> GetTicketRulesByRuleName(int organizationID, string rulename);
        IEnumerable<TicketRules> GetTicketRulesByRuleBatchID(int organizationID, string rulebatchID);
        TicketRules GetTicketRuleById(int organizationID, int Id);
        TicketRules GetTicketRuleWithDetails(int organizationID, int Id);
        void CreateTicketRule(TicketRules tr);
        void UpdateTicketRule(TicketRules tr);
        void DeleteTicketRule(TicketRules tr);

        void CreateRangeTicketRules(IEnumerable<TicketRules> ticketrules);
        void UpdateRangeTicketRulesIsActiveByRuleBatchId(int organizationID, string rulebatchId, bool isactive);
        void RemoveRangeTicketRules(IEnumerable<TicketRules> ticketrules);
    }
}
