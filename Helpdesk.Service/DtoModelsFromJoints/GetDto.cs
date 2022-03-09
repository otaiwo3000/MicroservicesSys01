using Helpdesk.Service.DtoModels;


namespace Helpdesk.Service.DtoModelsFromJoints
{
    public class GetDto
    {
        public UsersCreateModel GetUserCreateDtoModel(UserRoleGroupJoinsCreateModel userrolejoinModel)
        {
            var dtomodel = new UsersCreateModel();
            dtomodel.Email = userrolejoinModel.Email;
            dtomodel.FirstName = userrolejoinModel.FirstName;
            dtomodel.LastName = userrolejoinModel.LastName;
            dtomodel.Gender = userrolejoinModel.Gender;
            dtomodel.AgentTypeId = userrolejoinModel.AgentTypeId;
            dtomodel.AgentEngagementTypeId = userrolejoinModel.AgentEngagementTypeId;
            dtomodel.AgentScope = userrolejoinModel.AgentScope;
            dtomodel.Status = userrolejoinModel.Status;
            //dtomodel.AgentEngagementTypeId = 1;
            //dtomodel.AgentScope = 2;
            //dtomodel.Status = 1;

            return dtomodel;
        }

        public CustomBusinessDayHoursCreateModel GetCustomBusinessDayHoursCreateModel(CustomBusinessDayHoursJoinCreateModel custombizdayhrjoinModel)
        {
            var dtomodel = new CustomBusinessDayHoursCreateModel();
            dtomodel.WorkDay = custombizdayhrjoinModel.WorkDay;
            dtomodel.StartHour = custombizdayhrjoinModel.StartHour.TimeOfDay;
            dtomodel.EndHour = custombizdayhrjoinModel.EndHour.TimeOfDay;

            return dtomodel;
        }

        public BusinessHoursCreateModel GetBusinessHoursCreateModel(BusinessHoursJoinCreateModel bizhrjoinModel)
        {
            var dtomodel = new BusinessHoursCreateModel();
            dtomodel.StartHour = bizhrjoinModel.StartHour.TimeOfDay;
            dtomodel.EndHour = bizhrjoinModel.EndHour.TimeOfDay;

            return dtomodel;
        }

    }

}
