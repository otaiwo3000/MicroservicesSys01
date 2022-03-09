using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;

namespace Helpdesk.Shared.Entities
{
    public partial class TicketStatus : EntityBase, IIdentifiableEntity
    {
        //public TicketStatus()
        //{
        //    Tickets = new HashSet<Tickets>();
        //}

        public int TicketStatusId { get; set; }
        public string Status { get; set; }

        //public virtual ICollection<Tickets> Tickets { get; set; }

        public int EntityId
        {
            get
            {
                return TicketStatusId;
            }
        }
    }
}
