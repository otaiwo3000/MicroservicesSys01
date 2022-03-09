using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Helpdesk.Service.DtoModels
{
    public abstract class RuleConditionsAbstract
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Description { get; set; }
    }


    public class RuleConditionsModel : RuleConditionsAbstract
    {
        public int Id { get; set; }
        public OrganizationModel Organization { get; set; }
    }

    public class RuleConditionsCreateModel : RuleConditionsAbstract
    {

    }

    public class RuleConditionsUpdateModel : RuleConditionsAbstract
    {

    }
}
