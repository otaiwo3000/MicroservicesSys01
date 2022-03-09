using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Helpdesk.Service.DtoModels
{
    public abstract class RequestersAbstract
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; }

        public string Phones { get; set; }
    }


    public class RequestersModel : RequestersAbstract
    {
        public int RequesterId { get; set; }
        public OrganizationModel Organization { get; set; }
    }

    public class RequestersCreateModel : RequestersAbstract
    {

    }

    public class RequestersUpdateModel : RequestersAbstract
    {

    }
}
