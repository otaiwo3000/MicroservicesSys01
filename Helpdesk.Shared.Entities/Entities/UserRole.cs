using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;

namespace Helpdesk.Shared.Entities
{
    public partial class UserRole : EntityBase, IIdentifiableEntity_2
    {
        public long UserRoleId { get; set; }
        public int UserId { get; set; }
        public long RoleId { get; set; }

        public virtual Users User { get; set; }
        public virtual Roles Role { get; set; }

        public long EntityId
        {
            get
            {
                return UserRoleId;
            }
        }
    }
}
