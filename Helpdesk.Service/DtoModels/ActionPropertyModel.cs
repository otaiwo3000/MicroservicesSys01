
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Service.DtoModels
{
    public abstract class ActionPropertyAbstract
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }


    public class ActionPropertyModel : ActionPropertyAbstract
    {
        public int ActionPropertyId { get; set; }
        public OrganizationModel Organization { get; set; }
    }

    public class ActionPropertyCreateModel : ActionPropertyAbstract
    {
       
    }

    public class ActionPropertyUpdateModel : ActionPropertyAbstract
    {
        
    }
}
