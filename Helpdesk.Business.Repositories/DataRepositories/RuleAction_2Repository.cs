using Helpdesk.Shared.Entities;
using Helpdesk.Shared.DataAccess.DBContext;
using Helpdesk.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Helpdesk.Business.Interfaces.RepositoryInterfaces;


namespace Helpdesk.Business.Repositories.DataRepositories
{
    public class RuleAction_2Repository : RepositoryBase<RuleAction_2>, IRuleAction_2Repository
    {
        public RuleAction_2Repository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<RuleAction_2> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<RuleAction_2> _repo;


        public  IEnumerable<RuleAction_2> GetAllRuleAction_2()
        {
            var res = FindAll().ToList();
            return res;
        }

        public IEnumerable<RuleAction_2> GetRuleAction_2ByRulebatchID(string RuleBatchId)
        {
            var res = FindAll().Where(x=>x.RuleBatchId==RuleBatchId).ToList();
            return res;
        }

        public RuleAction_2 GetRuleAction_2ById(int Id)
        {
            return FindByCondition(x => x.Id == Id).FirstOrDefault();
        }

        public RuleAction_2 GetRuleAction_2WithDetails(int Id)
        {
            return FindByCondition(x => x.Id == Id).FirstOrDefault();
        }

        public void CreateRuleAction_2(RuleAction_2 ruleaction)
        {
            Create(ruleaction);
        }

        public void CreateRangeRuleAction_2(IEnumerable<RuleAction_2> ruleactions)
        {
            CreateRange(ruleactions);
        }

        public void UpdateRuleAction_2(RuleAction_2 ruleaction)
        {
            Update(ruleaction);
        }

        public void DeleteRuleAction_2(RuleAction_2 ruleaction)
        {
            Delete(ruleaction);
        }

        public void RemoveRangeRuleAction_2(IEnumerable<RuleAction_2> ruleaction)
        {
            RemoveRange(ruleaction);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override

    }
}
