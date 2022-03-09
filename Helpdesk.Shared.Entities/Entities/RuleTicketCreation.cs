using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;

namespace Helpdesk.Shared.Entities
{
    public partial class RuleTicketCreation : EntityBase, IIdentifiableEntity
    {
        public int Id { get; set; }
        public int RuleId { get; set; }
        public bool IsTicketCreated { get; set; }
        public int OrganizationId { get; set; }

        public virtual Organizations Organization { get; set; }

        public int EntityId
        {
            get
            {
                return Id;
            }
        }
    }
}
