using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Helpdesk.Service.DtoModels
{
    public abstract class RuleCasesAbstract
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }


    public class RuleCasesModel : RuleCasesAbstract
    {
        public int Id { get; set; }
        //public UsersModel Owner { get; set; }
        public OrganizationModel Organization { get; set; }
    }

    public class RuleCasesCreateModel : RuleCasesAbstract
    {

    }

    public class RuleCasesUpdateModel : RuleCasesAbstract
    {

    }
}
