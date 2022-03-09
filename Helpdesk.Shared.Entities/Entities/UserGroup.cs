using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;

namespace Helpdesk.Shared.Entities
{
    public partial class UserGroup : EntityBase, IIdentifiableEntity_2
    {
        public long UserGroupId { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }

        public virtual Users User { get; set; }
        public virtual Groups Group { get; set; }

        public long EntityId
        {
            get
            {
                return UserGroupId;
            }
        }
    }
}
