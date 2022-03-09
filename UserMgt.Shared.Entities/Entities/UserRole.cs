
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UserMgt.Shared.Common.Contracts;
using UserMgt.Shared.Common.Utilities;

namespace UserMgt.Shared.Entities
{
    public class UserRole : EntityBase, IIdentifiableEntity_2
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
