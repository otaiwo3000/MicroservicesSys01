using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using Helpdesk.Shared.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Shared.Entities
{
    public partial class Users : EntityBase, IIdentifiableEntity
    {
        public Users()
        {
            //Modules = new HashSet<Modules>();
            //Products = new HashSet<Products>();
            //TicketTypes = new HashSet<TicketTypes>();
            //TicketsOwnerNavigation = new HashSet<Tickets>();
            //TicketsUser = new HashSet<Tickets>();
        }
    
        public int UserId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        //public string MiddleName { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        //public string Photo { get; set; }
        //public string Signature { get; set; }

        //[Required(ErrorMessage = "Agent Type is required")]
        //public int AgentTypeId { get; set; }
        //public virtual AgentTypes AgentType { get; set; }

        //[Required(ErrorMessage = "Agent Engagement Type is required")]
        //public int AgentEngagementTypeId { get; set; }
        //public virtual AgentEngagementTypes AgentEngagementType { get; set; }

        //public AgentScope AgentScope { get; set; } = AgentScope.RestrictedAccess; //(AgentScope)3;
        //public AgentStatus Status { get; set; } = AgentStatus.Active;  //(AgentStatus)1;
        public int SupervisorId { get; set; }   //= loginId
        public int OrganizationId { get; set; }
        //public virtual Organizations Organization { get; set; }

        public int EntityId
        {
            get
            {
                return UserId;
            }
        }

        //public virtual ICollection<Modules> Modules { get; set; }
        //public virtual ICollection<Products> Products { get; set; }
        //public virtual ICollection<TicketTypes> TicketTypes { get; set; }
        //public virtual ICollection<Tickets> TicketsOwnerNavigation { get; set; }
        //public virtual ICollection<Tickets> TicketsUser { get; set; }
    }


    //class A
    //{
    //    public void test()
    //    {
            
    //    }
    //}

    //class B : A
    //{
       
    //    class C : A
    //    {
          
    //    }
    //}

}
