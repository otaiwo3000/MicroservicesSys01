using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;

namespace Helpdesk.Shared.Entities
{
    public partial class Requesters : EntityBase, IIdentifiableEntity
    {
        public int RequesterId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Phones { get; set; }

        public int OrganizationId { get; set; }
        public virtual Organizations Organization { get; set; }

        public int EntityId
        {
            get
            {
                return RequesterId;
            }
        }
    }
}
