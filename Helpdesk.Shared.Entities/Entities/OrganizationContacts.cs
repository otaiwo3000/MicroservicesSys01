using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Shared.Entities
{
    public partial class OrganizationContacts : EntityBase, IIdentifiableEntity
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
