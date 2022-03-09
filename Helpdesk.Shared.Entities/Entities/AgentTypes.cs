using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Shared.Entities
{
    public partial class AgentTypes : EntityBase, IIdentifiableEntity
    {
        public int AgentTypeId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public int OrganizationId { get; set; }
        public virtual Organizations Organization { get; set; }

        public int EntityId
        {
            get
            {
                return AgentTypeId;
            }
        }
    }
}
