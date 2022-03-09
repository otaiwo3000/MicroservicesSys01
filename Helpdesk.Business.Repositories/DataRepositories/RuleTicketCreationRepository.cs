using Helpdesk.Shared.Entities;
using Helpdesk.Shared.DataAccess.DBContext;
using Helpdesk.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Helpdesk.Business.Interfaces.RepositoryInterfaces;

namespace Helpdesk.Business.Repositories.DataRepositories
{
    public class RuleTicketCreationRepository : RepositoryBase<RuleTicketCreation>, IRuleTicketCreationRepository
    {
        public RuleTicketCreationRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<RuleTicketCreation> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<RuleTicketCreation> _repo;


        public  IEnumerable<RuleTicketCreation> GetAllRuleTicketCreation()
        {
            var res = FindAll().ToList();
            return res;
        }

        public RuleTicketCreation GetRuleTicketCreationById(int Id)
        {
            return FindByCondition(x => x.RuleId == Id).FirstOrDefault();
        }

        public RuleTicketCreation GetRuleTicketCreationWithDetails(int Id)
        {
            return FindByCondition(x => x.RuleId == Id).FirstOrDefault();
        }

        public void CreateRuleTicketCreation(RuleTicketCreation ruleticketcreation)
        {
            Create(ruleticketcreation);
        }

        public void UpdateRuleTicketCreation(RuleTicketCreation ruleticketcreation)
        {
            Update(ruleticketcreation);
        }

        public void DeleteRuleTicketCreation(RuleTicketCreation ruleticketcreation)
        {
            Delete(ruleticketcreation);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override

    }
}
