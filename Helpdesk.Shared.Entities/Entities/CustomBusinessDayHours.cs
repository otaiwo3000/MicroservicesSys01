using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;

namespace Helpdesk.Shared.Entities
{
    public partial class CustomBusinessDayHours : EntityBase, IIdentifiableEntity
    {
        public int CustomBusinessDayHourId { get; set; }
        public int WorkDay { get; set; }
        public TimeSpan StartHour { get; set; }
        public TimeSpan EndHour { get; set; }
        public int OrganizationId { get; set; }

        public virtual Organizations Organization { get; set; }

        public int EntityId
        {
            get
            {
                return CustomBusinessDayHourId;
            }
        }
    }
}
