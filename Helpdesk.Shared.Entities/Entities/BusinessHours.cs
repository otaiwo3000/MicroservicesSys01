using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Shared.Entities
{
    public partial class BusinessHours : EntityBase, IIdentifiableEntity
    {
        public int BusinessHourId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Timezone { get; set; }
        public string BusinessType { get; set; }
        public int OrganizationId { get; set; }

        [Required(ErrorMessage = "Business Start Time is required")]
        public TimeSpan StartHour { get; set; }

        [Required(ErrorMessage = "Business End/Close Time is required")]
        public TimeSpan EndHour { get; set; }

        public virtual Organizations Organization { get; set; }

        public int EntityId
        {
            get
            {
                return BusinessHourId;
            }
        }
    }
}
