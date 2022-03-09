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
    public class TicketRulesRepository : RepositoryBase<TicketRules>, ITicketRulesRepository
    {
        public TicketRulesRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<TicketRules> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<TicketRules> _repo;


        public  IEnumerable<TicketRules> GetAllTicketRules(int organizationID)
        {
            var res = FindAll().Where(x => x.OrganizationId == organizationID).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            return res;
        }

        public TicketRules GetTicketRuleById(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.RuleId == Id).FirstOrDefault();
        }

        public TicketRules GetTicketRuleWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.RuleId == Id)
              .Include(y => y.Organization).FirstOrDefault();
        }

        public void CreateTicketRule(TicketRules ticketrule)
        {
            Create(ticketrule);
        }

        public void UpdateTicketRule(TicketRules ticketrule)
        {
            Update(ticketrule);
        }

        public void DeleteTicketRule(TicketRules ticketrule)
        {
            Delete(ticketrule);
        }

        public void CreateRangeTicketRules(IEnumerable<TicketRules> ticketrules)
        {
            CreateRange(ticketrules);
        }

        public void UpdateRangeTicketRulesIsActiveByRuleBatchId(int organizationID, string rulebatchId, bool isactive)
        {
            IEnumerable<TicketRules> ticketrulesbyrulebatchID = GetTicketRulesByRuleBatchID(organizationID, rulebatchId);
            ticketrulesbyrulebatchID.Select(x => x.IsActive = isactive);  //updating the IsActive column of the list "ticketrulesbyrulebatchID"

            UpdateRange(ticketrulesbyrulebatchID);
        }

        //public IEnumerable<TicketRules> GetTicketRulesByBatchID(int organizationID, string rulebatchID)
        //{
        //    var res = FindAll().Where(x => x.OrganizationId==organizationID && x.RuleBatchId == rulebatchID);
        //    return res;
        //}

        public IEnumerable<TicketRules> GetTicketRulesByRuleName(int organizationID, string rulename)
        {
            return FindAll().Where(x => x.OrganizationId == organizationID && x.RuleName == rulename).ToList();
        }

        public IEnumerable<TicketRules> GetTicketRulesByRuleBatchID(int organizationID, string rulebatchID)
        {
            return FindAll().Where(x => x.OrganizationId == organizationID && x.RuleBatchId == rulebatchID).ToList();
        }

        public void RemoveRangeTicketRules(IEnumerable<TicketRules> ticketrules)
        {
            RemoveRange(ticketrules);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override

    }
}
