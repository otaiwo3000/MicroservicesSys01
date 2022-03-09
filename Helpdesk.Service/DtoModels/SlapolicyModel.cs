using Helpdesk.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Helpdesk.Service.DtoModels
{
    public abstract class SLAPolicyAbstract
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Description { get; set; }

        //[Required(ErrorMessage = "First Response Time is required")]
        //public DateTime FirstResponseTime { get; set; }

        //[Required(ErrorMessage = "Resolution Time is required")]
        //public DateTime ResolutionTime { get; set; }

        //[Required(ErrorMessage = "Resolution Time is required")]
        //public TimeSpan OperationHour { get; set; }
    }


    public class SLAPolicyModel : SLAPolicyAbstract
    {
        public int SLAPolicyId { get; set; }
        public virtual Organizations Organization { get; set; }
    }

    public class SLAPolicyCreateModel : SLAPolicyAbstract
    {

    }

    public class SLAPolicyUpdateModel : SLAPolicyAbstract
    {

    }
}
