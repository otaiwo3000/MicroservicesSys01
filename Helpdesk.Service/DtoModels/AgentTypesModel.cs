using System;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Service.DtoModels
{
    public abstract class AgentTypesAbstract
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }


    public class AgentTypesModel : AgentTypesAbstract
    {
        public int AgentTypeId { get; set; }
        public OrganizationModel Organization { get; set; }
    }

    public class AgentTypesCreateModel : AgentTypesAbstract
    {
       
    }

    public class AgentTypesUpdateModel : AgentTypesAbstract
    {
        
    }
}
