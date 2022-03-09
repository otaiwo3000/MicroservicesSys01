using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;

namespace Helpdesk.Shared.Entities
{
    public partial class RuleAction : EntityBase, IIdentifiableEntity
    {
        public int RuleActionId { get; set; }
        //public Guid RuleBatchId { get; set; }
        public string RuleBatchId { get; set; }
        //public string Contacts { get; set; }
        //public string Subject { get; set; }
        //public string Description { get; set; }
        //public string Tag { get; set; }

        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public int SlapriorityId { get; set; }
        public int GroupId { get; set; }
        public int ModuleId { get; set; }
        public int ProductId { get; set; }
        public int AgentId { get; set; }
        //public int SupervisorId { get; set; }
        public int OrganizationId { get; set; }

        public virtual TicketTypes Type { get; set; }
        public virtual TicketStatus Status { get; set; }
        public virtual SLAPriority Slapriority { get; set; }
        public virtual Groups Group { get; set; }
        public virtual Modules Module { get; set; }
        public virtual Products Product { get; set; }
        public virtual Users Agent { get; set; }
        //public virtual Users Supervisor { get; set; }
        public virtual Organizations Organization { get; set; }

        public int EntityId
        {
            get
            {
                return RuleActionId;
            }
        }
    }
}
