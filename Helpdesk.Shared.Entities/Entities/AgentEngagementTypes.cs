using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Shared.Entities
{
    public partial class AgentEngagementTypes : EntityBase, IIdentifiableEntity
    {
        public int AgentEngagementTypeId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string EngagementType { get; set; }

        public int OrganizationId { get; set; }
        public virtual Organizations Organization { get; set; }

        public int EntityId
        {
            get
            {
                return AgentEngagementTypeId;
            }
        }
    }
}
