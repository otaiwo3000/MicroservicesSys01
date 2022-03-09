
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UserMgt.Shared.Common.Contracts;
using UserMgt.Shared.Common.Utilities;

namespace UserMgt.Shared.Entities
{
    public class Organizations : EntityBase, IIdentifiableEntity
    {
        
        public int OrganizationId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Organization Code is required")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Domain is required")]
        public string Domain { get; set; }

        public string Address { get; set; }
        public string Industry { get; set; }

        public int EntityId
        {
            get
            {
                return OrganizationId;
            }
        }

    }
}
