using System;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Service.DtoModels
{
    public abstract class CustomBusinessDayHoursAbstract
    {
        [Required(ErrorMessage = "Work day is required")]
        public int WorkDay { get; set; }

        [Required(ErrorMessage = "Start hour  is required")]
        public TimeSpan StartHour { get; set; }

        [Required(ErrorMessage = "End hour  is required")]
        public TimeSpan EndHour { get; set; }

    }


    public class CustomBusinessDayHoursModel : CustomBusinessDayHoursAbstract
    {
        public int CustomBusinessDayHourId { get; set; }
        public OrganizationModel Organization { get; set; }
    }

    public class CustomBusinessDayHoursCreateModel : CustomBusinessDayHoursAbstract
    {

    }

    public class CustomBusinessDayHoursUpdateModel : CustomBusinessDayHoursAbstract
    {

    }

    public class CustomBusinessDayHoursJoinCreateModel
    {
        [Required(ErrorMessage = "Work day is required")]
        public int WorkDay { get; set; }

        [Required(ErrorMessage = "Start hour  is required")]
        public DateTime StartHour { get; set; }

        [Required(ErrorMessage = "End hour  is required")]
        public DateTime EndHour { get; set; }
    }
}
