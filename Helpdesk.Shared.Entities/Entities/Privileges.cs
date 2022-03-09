using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;

namespace Helpdesk.Shared.Entities
{
    public partial class Privileges : EntityBase, IIdentifiableEntity
    {
        public int PrivilegeId { get; set; }
        public string Name { get; set; }

        public int EntityId
        {
            get
            {
                return PrivilegeId;
            }
        }
    }
}
