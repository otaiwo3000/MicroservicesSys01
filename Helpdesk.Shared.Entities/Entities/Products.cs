using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;

namespace Helpdesk.Shared.Entities
{
    public partial class Products : EntityBase, IIdentifiableEntity
    {
        //public Products()
        //{
        //    ProductsModules = new HashSet<ProductsModules>();
        //}

        public int ProductId { get; set; }
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
                return ProductId;
            }
        }
    }
}
