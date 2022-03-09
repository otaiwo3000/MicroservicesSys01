using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;

namespace Helpdesk.Shared.Entities
{
    public partial class SLAPolicy : EntityBase, IIdentifiableEntity
    {
        public int SLAPolicyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public DateTime FirstResponseTime { get; set; }
        //public DateTime ResolutionTime { get; set; }
        //public TimeSpan OperationHour { get; set; }
        public int OrganizationId { get; set; }

        public virtual Organizations Organization { get; set; }

        public int EntityId
        {
            get
            {
                return SLAPolicyId;
            }
        }
    }
}
