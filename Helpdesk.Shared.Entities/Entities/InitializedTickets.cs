using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;

namespace Helpdesk.Shared.Entities
{
    public partial class InitializedTickets : EntityBase, IIdentifiableEntity
    {
        public int InitializedTicketId { get; set; }
        public string Requester { get; set; }       //requester/client email
        public string Receipients { get; set; }    // Agent email/group email/support email 
        public string Subject { get; set; }
        public string Description { get; set; }
       
        public int OrganizationId { get; set; }
        public virtual Organizations Organization { get; set; }

        public int EntityId
        {
            get
            {
                return InitializedTicketId;
            }
        }
    }
}
