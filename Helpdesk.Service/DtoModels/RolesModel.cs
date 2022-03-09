using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Helpdesk.Service.DtoModels
{
    public abstract class RolesAbstract
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Description { get; set; }
    }


    public class RolesModel : RolesAbstract
    {
        public long RoleId { get; set; }
        public OrganizationModel Organization { get; set; }    
    }

    public class RolesCreateModel : RolesAbstract
    {
       
    }

    public class RolesUpdateModel : RolesAbstract
    {
        
    }
}
