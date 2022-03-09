using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;

namespace Helpdesk.Shared.Entities
{
    public partial class TicketTypes : EntityBase, IIdentifiableEntity
    {
        //public TicketTypes()
        //{
        //    Tickets = new HashSet<Tickets>();
        //}

        public int TicketTypeId { get; set; }
        public string Type { get; set; }
        public int OwnerId { get; set; }
        public int OrganizationId { get; set; }

        public virtual Organizations Organization { get; set; }
        public virtual Users Owner { get; set; }
        //public virtual ICollection<Tickets> Tickets { get; set; }

        public int EntityId
        {
            get
            {
                return TicketTypeId;
            }
        }
    }
}
