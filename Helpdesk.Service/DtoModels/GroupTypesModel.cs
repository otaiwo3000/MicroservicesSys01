using System;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Service.DtoModels
{
    public abstract class GroupTypesAbstract
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }


    public class GroupTypesModel : GroupTypesAbstract
    {
        public int GroupTypeId { get; set; }
        public OrganizationModel Organization { get; set; }
    }

    public class GroupTypesCreateModel : GroupTypesAbstract
    {

    }

    public class GroupTypesUpdateModel : GroupTypesAbstract
    {

    }
}
