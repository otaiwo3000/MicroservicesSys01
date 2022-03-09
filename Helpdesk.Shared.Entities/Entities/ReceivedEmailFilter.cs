using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Shared.Entities
{
    public partial class ReceivedEmailFilter : EntityBase, IIdentifiableEntity
    {
        public int ReceivedEmailFilterId { get; set; }

        [Required(ErrorMessage = "Word is required")]
        public string Word { get; set; }

        [Required(ErrorMessage = "IsEnabled is required")]
        public bool IsEnabled { get; set; }

        public int OrganizationId { get; set; }
        public virtual Organizations Organization { get; set; }

        public int EntityId
        {
            get
            {
                return ReceivedEmailFilterId;
            }
        }
    }
}
