using Helpdesk.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Helpdesk.Service.DtoModels
{
    public abstract class SLAPolicyPriorityAbstract
    {
        public int FirstResponseTime { get; set; }  //eg 1hr
        public int ResolutionDuration { get; set; } = 0;
    }

    public class SLAPolicyPriorityModel : SLAPolicyPriorityAbstract
    {
        public virtual SLAPolicy SLAPolicy { get; set; }
        public virtual SLAPriority SLAPriority { get; set; }
        //public virtual Organizations Organization { get; set; }
    }

    public class SLAPolicyPriorityCreateModel : SLAPolicyPriorityAbstract
    {
        [Required(ErrorMessage = "SLA Policy is required")]
        public int SLAPolicyId { get; set; }
    }

    public class SLAPolicyPriorityUpdateModel : SLAPolicyPriorityAbstract
    {
        [Required(ErrorMessage = "SLA Priority is required")]
        public int SLAPriorityId { get; set; }
    }
}
