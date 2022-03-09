using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;

namespace Helpdesk.Shared.Entities
{
    public partial class Tickets : EntityBase, IIdentifiableEntity
    {
        public int TicketId { get; set; }
        public string SysGeneratedTicketId { get; set; }
        public string Contacts { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public DateTime ResolutionStartDate { get; set; } = DateTime.Now;
        public DateTime ResolutionEndDate { get; set; } = DateTime.Now;
        public DateTime ClosedDate { get; set; } = DateTime.Now;
        public bool ReminderHasBeenSentToday { get; set; }

        public int TypeId { get; set; }
        public int StatusId { get; set; } = 1;
        public int SLAPriorityId { get; set; } = 1;
        public int GroupId { get; set; }
        public int ModuleId { get; set; }
        public int ProductId { get; set; }
        public int AgentId { get; set; }
        public int SupervisorId { get; set; }
        public int OrganizationId { get; set; }

        public virtual TicketTypes Type { get; set; }
        public virtual TicketStatus Status { get; set; }
        public virtual SLAPriority SLAPriority { get; set; }
        public virtual Groups Group { get; set; }
        public virtual Modules Module { get; set; }
        public virtual Products Product { get; set; }
        public virtual Users Agent { get; set; }
        public virtual Users Supervisor { get; set; }
        public virtual Organizations Organization { get; set; }

        public int EntityId
        {
            get
            {
                return TicketId;
            }
        }
    }
}
