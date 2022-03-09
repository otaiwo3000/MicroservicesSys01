using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;

namespace Helpdesk.Shared.Entities
{
    public partial class Modules : EntityBase, IIdentifiableEntity
    {
        //public Modules()
        //{
        //    ProductsModules = new HashSet<ProductsModules>();
        //}

        public int ModuleId { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public int OrganizationId { get; set; }

        public virtual Organizations Organization { get; set; }
        public virtual Users Owner { get; set; }
        //public virtual ICollection<ProductsModules> ProductsModules { get; set; }

        public int EntityId
        {
            get
            {
                return ModuleId;
            }
        }
    }
}
