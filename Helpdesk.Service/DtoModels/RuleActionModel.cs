using Helpdesk.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Helpdesk.Service.DtoModels
{
    public abstract class RuleActionAbstract
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        
        public string Action { get; set; }
    }


    public class RuleActionModel : RuleActionAbstract
    {
        public int Id { get; set; }
        //public UsersModel Owner { get; set; }
        public virtual TicketRules RuleName { get; set; }
        public OrganizationModel Organization { get; set; }
    }

    public class RuleActionCreateModel : RuleActionAbstract
    {
        public int RuleId { get; set; }
    }

    public class RuleActionUpdateModel : RuleActionAbstract
    {
        public int RuleId { get; set; }
    }
}
