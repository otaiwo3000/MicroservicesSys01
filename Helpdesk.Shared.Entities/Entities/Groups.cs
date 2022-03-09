using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Shared.Entities
{
    public partial class Groups : EntityBase, IIdentifiableEntity
    {
        //public Groups()
        //{
        //    Tickets = new HashSet<Tickets>();
        //}

        public int GroupId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Group Email is required")]
        public string GroupEmail { get; set; }

        [Required(ErrorMessage = "Group Type is required")]
        public int GroupTypeId { get; set; }
        public virtual GroupTypes GroupType { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Group Lead is required")]
        public int GroupLeadId { get; set; }
        public virtual Users GroupLead { get; set; }
        
        public int OrganizationId { get; set; }
        public virtual Organizations Organization { get; set; }
        //public virtual ICollection<Tickets> Tickets { get; set; }

        public int EntityId
        {
            get
            {
                return GroupId;
            }
        }
    }
}
