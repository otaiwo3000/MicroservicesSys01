using System;

using System.ComponentModel.DataAnnotations;


namespace Helpdesk.Service.DtoModels
{
    public abstract class BusinessHoursAbstract
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Timezone is required")]
        public DateTime? Timezone { get; set; }

        [Required(ErrorMessage = "BusinessType is required")]
        public string BusinessType { get; set; }

        [Required(ErrorMessage = "Business Start Time is required")]
        public TimeSpan StartHour { get; set; }

        [Required(ErrorMessage = "Business End/Close Time is required")]
        public TimeSpan EndHour { get; set; }
    }


    public class BusinessHoursModel : BusinessHoursAbstract
    {
        public int BusinessHourId { get; set; }
        public OrganizationModel Organization { get; set; }
    }

    public class BusinessHoursCreateModel : BusinessHoursAbstract
    {

    }

    public class BusinessHoursUpdateModel : BusinessHoursAbstract
    {

    }

    public class BusinessHoursJoinCreateModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Timezone is required")]
        public DateTime? Timezone { get; set; }

        [Required(ErrorMessage = "BusinessType is required")]
        public string BusinessType { get; set; }

        [Required(ErrorMessage = "Business Start time  is required")]
        public DateTime StartHour { get; set; }

        [Required(ErrorMessage = "Business End time  is required")]
        public DateTime EndHour { get; set; }
    }

}
