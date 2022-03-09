using Helpdesk.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Helpdesk.Service.DtoModels
{
    public abstract class SLAPriorityAbstract
    {
        //[Required(ErrorMessage = "Priority is required")]
        //public int Priority { get; set; }

        [Required(ErrorMessage = "Priority Name is required")]
        public string PriorityName { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        public decimal Duration { get; set; }
    }


    public class SLAPriorityModel : SLAPriorityAbstract
    {
        public int SLAPriorityId { get; set; }
        public virtual Organizations Organization { get; set; }
    }

    public class SLAPriorityCreateModel : SLAPriorityAbstract
    {

    }

    public class SLAPriorityUpdateModel : SLAPriorityAbstract
    {

    }
}
