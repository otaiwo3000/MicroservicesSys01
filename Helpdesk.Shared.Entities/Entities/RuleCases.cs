using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;

namespace Helpdesk.Shared.Entities
{
    public partial class RuleCases : EntityBase, IIdentifiableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
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
