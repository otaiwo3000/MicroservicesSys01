using Helpdesk.Shared.Entities;
using System.Collections.Generic;


namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{
    public interface IRuleTicketCreationRepository : IRepositoryBase<RuleTicketCreation>
    {
        IEnumerable<RuleTicketCreation> GetAllRuleTicketCreation();
        RuleTicketCreation GetRuleTicketCreationById(int Id);
        RuleTicketCreation GetRuleTicketCreationWithDetails(int Id);
        void CreateRuleTicketCreation(RuleTicketCreation rtc);
        void UpdateRuleTicketCreation(RuleTicketCreation rtc);
        void DeleteRuleTicketCreation(RuleTicketCreation rtc);
    }
}
