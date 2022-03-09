using Helpdesk.Shared.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Service.DtoModels
{
    public abstract class CustomMessagesAbstract
    {
        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }
        public string MessageContent { get; set; }
        public string Comment { get; set; }       
    }


    public class CustomMessagesModel : CustomMessagesAbstract
    {
        public Groups Group { get; set; }
        public virtual Organizations Organization { get; set; }
    }

    public class CustomMessagesCreateModel : CustomMessagesAbstract
    {

    }

    public class CustomMessagesUpdateModel : CustomMessagesAbstract
    {
        
    }
}
