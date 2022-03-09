using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;

namespace Helpdesk.Shared.Entities
{
    public partial class SLAPriority : EntityBase, IIdentifiableEntity
    {
        //public Slapriority()
        //{
        //    Tickets = new HashSet<Tickets>();
        //}

        public int SLAPriorityId { get; set; }
        public string PriorityName { get; set; }
        public decimal Duration { get; set; }
        public int SendReminderWhen { get; set; } = 2;
        public int OrganizationId { get; set; }

        public virtual Organizations Organization { get; set; }
        //public virtual ICollection<Tickets> Tickets { get; set; }

        public int EntityId
        {
            get
            {
                return SLAPriorityId;
            }
        }
    }
}
