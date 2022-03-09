using Helpdesk.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Helpdesk.Service.DtoModels
{
    public abstract class TicketsAbstract
    {
        [Required(ErrorMessage = "Contact is required")]
        public string Contacts { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        public string Description { get; set; }
        public string Tag { get; set; }
    }


    public class TicketsModel : TicketsAbstract
    {
        public int TicketId { get; set; }
        public string SysGeneratedTicketId { get; set; }
        public virtual TicketTypes Type { get; set; }
        public virtual TicketStatus Status { get; set; }
        public virtual SLAPriority SLAPriority { get; set; }
        public virtual Groups Group { get; set; }
        public virtual Modules Module { get; set; }
        public virtual Products Product { get; set; }
        public virtual Users Agent { get; set; }
        public virtual Users Supervisor { get; set; }
        public virtual Organizations Organization { get; set; }

        public DateTime ResolutionStartDate { get; set; }
        public DateTime ResolutionEndDate { get; set; }
        public DateTime ClosedDate { get; set; }
        //public SLAPosition SLAPosition { get; set; }
        public string SLAPosition { get; set; }

        //you might not necessarily return the below properties
        public int SLAPolicyPriorityDuration { get; set; }
        public TimeSpan WorkStartHour { get; set; }
        public TimeSpan WorkEndHour { get; set; }
        public DateTime SLAResolveDateTime { get; set; }
        
    }

    public class TicketsCreateModel : TicketsAbstract
    {
        [Required(ErrorMessage = "Ticket Type is required")]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "Ticket Status is required")]
        public int StatusId { get; set; }

        [Required(ErrorMessage = "SLA Priority is required")]
        public int SlapriorityId { get; set; }

        [Required(ErrorMessage = "Group is required")]
        public int GroupId { get; set; }

        [Required(ErrorMessage = "Module is required")]
        public int ModuleId { get; set; }

        [Required(ErrorMessage = "Product is required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Agent is required")]
        public int AgentId { get; set; }
    }

    public class TicketsUpdateModel : TicketsAbstract
    {
        [Required(ErrorMessage = "Ticket Type is required")]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "Ticket Status is required")]
        public int StatusId { get; set; }

        [Required(ErrorMessage = "SLA Priority is required")]
        public int SlapriorityId { get; set; }

        [Required(ErrorMessage = "Group is required")]
        public int GroupId { get; set; }

        [Required(ErrorMessage = "Module is required")]
        public int ModuleId { get; set; }

        [Required(ErrorMessage = "Product is required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Agent is required")]
        public int AgentId { get; set; }
    }

    public class ClientTicketsCreateModel : TicketsAbstract
    {
        [Required(ErrorMessage = "Ticket Type is required")]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "Group is required")]
        public int GroupId { get; set; }

        [Required(ErrorMessage = "Product is required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Module is required")]
        public int ModuleId { get; set; }
    }

    public class ClientTicketsUpdateModel : TicketsAbstract
    {
        [Required(ErrorMessage = "Ticket Type is required")]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "Group is required")]
        public int GroupId { get; set; }

        [Required(ErrorMessage = "Product is required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Module is required")]
        public int ModuleId { get; set; }

        [Required(ErrorMessage = "Ticket Status is required")]
        public int StatusId { get; set; }
    }

    public class Tickets_SLA
    {
        public int TicketId { get; set; }
        public string SysGeneratedTicketId { get; set; }
        public string Contacts { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public DateTime ResolutionStartDate { get; set; } = DateTime.Now;
        public DateTime ResolutionEndDate { get; set; } = DateTime.Now;
        public DateTime ClosedDate { get; set; } = DateTime.Now;
        public bool ReminderHasBeenSentToday { get; set; }

        public int TypeId { get; set; }
        public virtual TicketTypes Type { get; set; }
        public int StatusId { get; set; } = 1;
        public virtual TicketStatus Status { get; set; }
        public int SLAPriorityId { get; set; } = 1;
        public virtual SLAPriority SLAPriority { get; set; }
        public int GroupId { get; set; }
        public virtual Groups Group { get; set; }
        public int ModuleId { get; set; }
        public virtual Modules Module { get; set; }
        public int ProductId { get; set; }
        public virtual Products Product { get; set; }
        public int AgentId { get; set; }
        public virtual Users Agent { get; set; }
        public int SupervisorId { get; set; }
        public virtual Users Supervisor { get; set; }
        public int OrganizationId { get; set; }
        public virtual Organizations Organization { get; set; }

        public TimeSpan WorkStartHour { get; set; }
        public TimeSpan WorkEndHour { get; set; }
        public int SLAPolicyPriorityDuration { get; set; }
        public DateTime SLAResolveDateTime { get; set; }

        //public SLAPosition SLAPosition { get; set; }
        public string SLAPosition { get; set; }
    }

    public class TicketTicketStatusUpdateModel
    {
        public int TicketId { get; set; }

        [Required(ErrorMessage = "Ticket Status is required")]
        public int StatusId { get; set; }

    }

}
