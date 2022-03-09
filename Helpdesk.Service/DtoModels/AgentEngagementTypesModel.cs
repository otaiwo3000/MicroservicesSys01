using System;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Service.DtoModels
{
    public abstract class AgentEngagementTypesAbstract
    {
        [Required(ErrorMessage = "Engagement Type is required")]
        public string EngagementType { get; set; }
    }


    public class AgentEngagementTypesModel : AgentEngagementTypesAbstract
    {
        public int AgentEngagementTypeId { get; set; }
        public OrganizationModel Organization { get; set; }
    }

    public class AgentEngagementTypesCreateModel : AgentEngagementTypesAbstract
    {
       
    }

    public class AgentEngagementTypesUpdateModel : AgentEngagementTypesAbstract
    {
        
    }
}
