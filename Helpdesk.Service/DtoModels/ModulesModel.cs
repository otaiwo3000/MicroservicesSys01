using System;
using System.ComponentModel.DataAnnotations;


namespace Helpdesk.Service.DtoModels
{
    public abstract class ModulesAbstract
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }


    public class ModulesModel : ModulesAbstract
    {
        public int ModuleId { get; set; }
        public UsersModel Owner { get; set; }
        public OrganizationModel Organization { get; set; }
    }

    public class ModulesCreateModel : ModulesAbstract
    {

    }

    public class ModulesUpdateModel : ModulesAbstract
    {

    }
}
