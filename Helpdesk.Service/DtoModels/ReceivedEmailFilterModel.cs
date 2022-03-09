using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Helpdesk.Service.DtoModels
{
    public abstract class ReceivedEmailFilterAbstract
    {
        [Required(ErrorMessage = "Word is required")]
        public string Word { get; set; }

        [Required(ErrorMessage = "IsEnabled is required")]
        public bool IsEnabled { get; set; }
    }


    public class ReceivedEmailFilterModel : ReceivedEmailFilterAbstract
    {
        public int ReceivedEmailFilterId { get; set; }
        public OrganizationModel Organization { get; set; }
    }

    public class ReceivedEmailFilterCreateModel : ReceivedEmailFilterAbstract
    {
       
    }

    public class ReceivedEmailFilterUpdateModel : ReceivedEmailFilterAbstract
    {
        
    }
}
