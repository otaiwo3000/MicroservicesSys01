using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;

namespace Helpdesk.Shared.Entities
{
    public partial class TicketRules : EntityBase, IIdentifiableEntity
    {
        public int RuleId { get; set; }
        public string RuleName { get; set; }
        //public Guid RuleBatchId { get; set; }
        public string RuleBatchId { get; set; }
        public string RuleCase { get; set; }
        public string RuleCondition { get; set; }
        public string RuleOperator { get; set; }
        public string RuleConditionValue { get; set; }
        public int RuleItemOrder { get; set; }
        public string RuleItemLogicalOperator { get; set; }
        public bool IsActive { get; set; }
        public int OrganizationId { get; set; }

        public virtual Organizations Organization { get; set; }

        public int EntityId
        {
            get
            {
                return RuleId;
            }
        }
    }
}
