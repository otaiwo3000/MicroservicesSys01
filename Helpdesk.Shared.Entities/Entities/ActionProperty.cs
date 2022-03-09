
using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Shared.Entities
{
    public partial class ActionProperty : EntityBase, IIdentifiableEntity
    {
        public int ActionPropertyId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public int OrganizationId { get; set; }
        public virtual Organizations Organization { get; set; }

        public int EntityId
        {
            get
            {
                return ActionPropertyId;
            }
        }
    }
}
