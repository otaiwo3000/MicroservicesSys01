using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Shared.Entities
{
    public partial class Roles : EntityBase, IIdentifiableEntity_2
    {
        public long RoleId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Description { get; set; }

        public int OrganizationId { get; set; }
        public virtual Organizations Organization { get; set; }
        //public virtual ICollection<RolesPrivileges> RolesPrivileges { get; set; }

        public long EntityId
        {
            get
            {
                return RoleId;
            }
        }
    }
}
