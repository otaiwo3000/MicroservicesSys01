using Helpdesk.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Helpdesk.Service.DtoModels
{
    public abstract class RolesPrivilegesAbstract
    {
      
    }


    public class RolesPrivilegesModel : RolesPrivilegesAbstract
    {
        public int RolePrivilegeId { get; set; }
        public virtual Roles Role { get; set; }
        public virtual Privileges Privilege { get; set; }
    }

    public class RolesPrivilegesCreateModel : RolesPrivilegesAbstract
    {
        [Required(ErrorMessage = "Role is required")]
        public long RoleId { get; set; }

        [Required(ErrorMessage = "Privilege is required")]
        public int PrivilegeId { get; set; }
    }

    public class RolesPrivilegesUpdateModel : RolesPrivilegesAbstract
    {
        [Required(ErrorMessage = "Role is required")]
        public long RoleId { get; set; }

        [Required(ErrorMessage = "Privilege is required")]
        public int PrivilegeId { get; set; }
    }
}
