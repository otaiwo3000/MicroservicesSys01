using Helpdesk.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Helpdesk.Service.DtoModels
{
    public class TicketRules_2     //this will be mapped to TicketRules
    {
        //[Required(ErrorMessage = "Rule Name is required")]
        //public string RuleName { get; set; }
        public string RuleCase { get; set; }
        public string RuleCondition { get; set; }
        public string RuleOperator { get; set; }
        public string RuleConditionValue { get; set; }
        ////public int RuleItemOrder { get; set; }  //position of individual item in a particular rule
        public string RuleItemLogicalOperator { get; set; }
    }

    public class TicketRules_3      //this is created so that TicketRulesCreateModel can be mapped to it bcos TicketRules cannot be mapped to TicketRulesCreateModel due to different in their structures
    {
        public string RuleName { get; set; }
        public List<TicketRules_2> TicketRuleEntries { get; set; }
        public List<RuleActions_B> RuleActionEntries { get; set; }
    }

    ////// working fine
    //public abstract class TicketRulesAbstract
    //{
    //    [Required(ErrorMessage = "Rule Name is required")]
    //    public string RuleName { get; set; }
    //    public List<TicketRules_2> TicketRuleEntries { get; set; }
    //}

    public abstract class TicketRulesAbstract
    {
        [Required(ErrorMessage = "Rule Name is required")]
        public string RuleName { get; set; }
        public List<TicketRules_2> TicketRuleEntries { get; set; }
        public List<RuleActions_B> RuleActionEntries { get; set; }
    }


    public class TicketRulesModel : TicketRulesAbstract
    {
        public virtual Organizations Organization { get; set; }

    }

    public class TicketRulesCreateModel : TicketRulesAbstract
    {

    }

    public class TicketRulesUpdateModel : TicketRulesAbstract
    {

    }

    //public class TicketRulesAndActionsCreateModel : TicketRulesAbstract
    //{
    //    public List<TicketActions> TicketActionList { get; set; }
    //}

    public class RuleActions_B
    {
        public string ActionProperty { get; set; }
        public int ActionValue { get; set; }
    }

}
