using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;

namespace Helpdesk.Shared.Entities
{
    public partial class RuleAction_2 : EntityBase, IIdentifiableEntity
    {
        public int Id { get; set; }
        //public Guid RuleBatchId { get; set; }
        public string RuleBatchId { get; set; }
        public string ActionProperty { get; set; }
        public int ActionValue { get; set; }
        //public int OrganizationId { get; set; }

       //public virtual Organizations Organization { get; set; }

        public int EntityId
        {
            get
            {
                return Id;
            }
        }
    }
}
