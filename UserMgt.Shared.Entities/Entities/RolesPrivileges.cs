
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UserMgt.Shared.Common.Contracts;
using UserMgt.Shared.Common.Utilities;

namespace UserMgt.Shared.Entities
{
    public class RolesPrivileges : EntityBase, IIdentifiableEntity_2
    {
        public long RolePrivilegeId { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public long RoleId { get; set; }
        public virtual Roles Role { get; set; }

        [Required(ErrorMessage = "Privilege is required")]
        public int PrivilegeId { get; set; }
        public virtual Privileges Privilege { get; set; }
        

        public long EntityId
        {
            get
            {
                return RolePrivilegeId;
            }
        }
    }
}
