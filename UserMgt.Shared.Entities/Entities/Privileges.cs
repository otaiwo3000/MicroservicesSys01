
using System;
using System.Collections.Generic;
using UserMgt.Shared.Common.Contracts;
using UserMgt.Shared.Common.Utilities;

namespace UserMgt.Shared.Entities
{
    public class Privileges : EntityBase, IIdentifiableEntity
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
