using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Helpdesk.Service.DtoModels
{
    public abstract class TicketTypesAbstract
    {
        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }
    }


    public class TicketTypesModel : TicketTypesAbstract
    {
        public int TicketTypeId { get; set; }
    }

    public class TicketTypesCreateModel : TicketTypesAbstract
    {

    }

    public class TicketTypesUpdateModel : TicketTypesAbstract
    {

    }
}
