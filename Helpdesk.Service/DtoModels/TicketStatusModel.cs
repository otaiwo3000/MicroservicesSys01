using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Helpdesk.Service.DtoModels
{
    public abstract class TicketStatusAbstract
    {
        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }
    }


    public class TicketStatusModel : TicketStatusAbstract
    {
        public int TicketStatusId { get; set; }
    }

    public class TicketStatusCreateModel : TicketStatusAbstract
    {

    }

    public class TicketStatusUpdateModel : TicketStatusAbstract
    {

    }
}
