using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;

namespace Helpdesk.Shared.Entities
{
    public partial class ProductModules : EntityBase, IIdentifiableEntity
    {
        //public ProductsModules()
        //{
        //    Tickets = new HashSet<Tickets>();
        //}

        public int ProductModuleId { get; set; }
        public int ProductId { get; set; }
        public int ModuleId { get; set; }

        public virtual Modules Module { get; set; }
        public virtual Products Product { get; set; }
        //public virtual ICollection<Tickets> Tickets { get; set; }
        //public virtual ICollection<Modules> Module { get; set; }

        public int EntityId
        {
            get
            {
                return ProductModuleId;
            }
        }
    }

}
