using System;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Service.DtoModels
{
    public abstract class OrganizationAbstract
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Organization Code is required")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Domain is required")]
        public string Domain { get; set; }

        public string Address { get; set; }
        public string Industry { get; set; }
    }


    public class OrganizationModel : OrganizationAbstract
    {
        public int OrganizationId { get; set; }
    }

    public class OrganizationCreateModel : OrganizationAbstract
    {

    }

    public class OrganizationUpdateModel : OrganizationAbstract
    {

    }
}
