using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Shared.Entities
{
    public partial class Organizations : EntityBase, IIdentifiableEntity
    {
        //public Organizations()
        //{
        //    AgentEngagementTypes = new HashSet<AgentEngagementTypes>();
        //    BusinessHours = new HashSet<BusinessHours>();
        //    CustomBusinessDayHours = new HashSet<CustomBusinessDayHours>();
        //    Groups = new HashSet<Groups>();
        //    Modules = new HashSet<Modules>();
        //    OrganizationContacts = new HashSet<OrganizationContacts>();
        //    Products = new HashSet<Products>();
        //    Roles = new HashSet<Roles>();
        //    Slapolicy = new HashSet<Slapolicy>();
        //    Slapriority = new HashSet<Slapriority>();
        //    TicketTypes = new HashSet<TicketTypes>();
        //    Tickets = new HashSet<Tickets>();
        //    Users = new HashSet<Users>();
        //}

        public int OrganizationId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Organization Code is required")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Domain is required")]
        public string Domain { get; set; }

        public string Address { get; set; }
        public string Industry { get; set; }

        public int EntityId
        {
            get
            {
                return OrganizationId;
            }
        }

        //public virtual ICollection<AgentEngagementTypes> AgentEngagementTypes { get; set; }
        //public virtual ICollection<BusinessHours> BusinessHours { get; set; }
        //public virtual ICollection<CustomBusinessDayHours> CustomBusinessDayHours { get; set; }
        //public virtual ICollection<Groups> Groups { get; set; }
        //public virtual ICollection<Modules> Modules { get; set; }
        //public virtual ICollection<OrganizationContacts> OrganizationContacts { get; set; }
        //public virtual ICollection<Products> Products { get; set; }
        //public virtual ICollection<Roles> Roles { get; set; }
        //public virtual ICollection<Slapolicy> Slapolicy { get; set; }
        //public virtual ICollection<Slapriority> Slapriority { get; set; }
        //public virtual ICollection<TicketTypes> TicketTypes { get; set; }
        //public virtual ICollection<Tickets> Tickets { get; set; }
        //public virtual ICollection<Users> Users { get; set; }
    }
}
