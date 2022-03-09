
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UserMgt.Shared.Common.Contracts;
using UserMgt.Shared.Common.Utilities;

namespace UserMgt.Shared.Entities
{
    public class OrganizationContacts : EntityBase, IIdentifiableEntity
    {
        public int OrganizationContactId { get; set; }
        public string ContactType { get; set; }

        [Required(ErrorMessage = "Contact is required")]
        public string ContactValue { get; set; }
        public bool? PreferredContact { get; set; } = true;
        public bool IsSupportContact { get; set; } = true;
        
        public int OrganizationId { get; set; }
        public virtual Organizations Organization { get; set; }

        public int EntityId
        {
            get
            {
                return OrganizationContactId;
            }
        }
    }
}
