using System;
using UserMgt.Service.DtoModels;

namespace UserMgt.Service.DtoModelsFromJoints
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
            //dtomodel.AgentTypeId = userrolejoinModel.AgentTypeId;
            //dtomodel.AgentEngagementTypeId = userrolejoinModel.AgentEngagementTypeId;
            //dtomodel.AgentScope = userrolejoinModel.AgentScope;
            //dtomodel.Status = userrolejoinModel.Status;
            ////dtomodel.AgentEngagementTypeId = 1;
            ////dtomodel.AgentScope = 2;
            ////dtomodel.Status = 1;

            return dtomodel;
        }        
    }
}
