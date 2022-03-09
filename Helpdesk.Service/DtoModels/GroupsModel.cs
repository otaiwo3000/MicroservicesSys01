using Helpdesk.Shared.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Service.DtoModels
{
    public abstract class GroupsAbstract
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Group Type is required")]
        public int GroupTypeId { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Group Email is required")]
        public string GroupEmail { get; set; }
    }


    public class GroupsModel : GroupsAbstract
    {
        public int GroupId { get; set; }
        public virtual GroupTypesModel GroupType { get; set; }
        public virtual Users GroupLead { get; set; }
        public virtual OrganizationModel Organization { get; set; }
    }

    public class GroupsCreateModel : GroupsAbstract
    {
        //[Required(ErrorMessage = "Group Lead is required")]
        //public int GroupLeadId { get; set; }
    }

    public class GroupsUpdateModel : GroupsAbstract
    {
        //[Required(ErrorMessage = "Group Lead is required")]
        //public int GroupLeadId { get; set; }
    }
}
