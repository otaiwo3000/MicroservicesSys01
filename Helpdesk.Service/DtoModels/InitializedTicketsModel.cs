using Helpdesk.Shared.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Service.DtoModels
{
    public abstract class InitializedTicketsAbstract
    {
        public int InitializedTicketId { get; set; }          //for now. This organization input will always come from the frontend so the client will login with the organization CODE along the username and password

        //[Required(ErrorMessage = "Contact is required")]
        //public string Contacts { get; set; }

        [Required(ErrorMessage = "Receipient is required")]
        public string Receipients { get; set; }    // Agent email/group email/support email 

        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        public string Description { get; set; }
    }


    public class InitializedTicketsModel : InitializedTicketsAbstract
    {       
        public virtual Organizations Organization { get; set; }
    }

    public class InitializedTicketsCreateModel : InitializedTicketsAbstract
    {

    }

    public class InitializedTicketsUpdateModel : InitializedTicketsAbstract
    {

    }
}
