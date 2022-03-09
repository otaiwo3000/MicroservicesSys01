using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Shared.Entities
{
    public partial class SLAPolicyPriority : EntityBase, IIdentifiableEntity
    {
        public int SLAPolicyPriorityId { get; set; }

        [Required(ErrorMessage = "SLA Policy is required")]
        public int SLAPolicyId { get; set; }
        public virtual SLAPolicy SLAPolicy { get; set; }

        [Required(ErrorMessage = "SLA Priority is required")]
        public int SLAPriorityId { get; set; }
        public virtual SLAPriority SLAPriority { get; set; }

        //public int OrganizationId { get; set; }
        //public virtual Organizations Organization { get; set; }

        public int FirstResponseTime { get; set; }  //eg 1hr
        public int ResolutionDuration { get; set; } = 0;
        
        public int EntityId
        {
            get
            {
                return SLAPolicyPriorityId;
            }
        }
    }
}
