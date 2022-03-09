using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UserMgt.Shared.Entities;

namespace UserMgt.Service.DtoModels
{
    public abstract class UsersAbstract
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        //public string Photo { get; set; }
        //public string Signature { get; set; }

        //[Required(ErrorMessage = "Supervisor is required")]
        //public int SupervisorId { get; set; }

    }


    public class UsersModel : UsersAbstract
    {
        public int UserId { get; set; }

        //public AgentTypesModel AgentType { get; set; }
        //public AgentEngagementTypes AgentEngagementType { get; set; }

        //public AgentScope AgentScope { get; set; }
        //public AgentStatus Status { get; set; }
        ////public int SupervisorId { get; set; }   //= loginId
        //public int OrganizationId { get; set; }
        //public OrganizationModel Organization { get; set; }

        //public List<UserGroup> UserGroupList { get; set; }
        public List<UserRole> UserRoleList { get; set; }
    }

    public class UsersCreateModel : UsersAbstract
    {
        //[Required(ErrorMessage = "Agent Type is required")]
        //public int AgentTypeId { get; set; }

        //[Required(ErrorMessage = "Agent Engagement Type is required")]
        //public int AgentEngagementTypeId { get; set; }

        //public int AgentScope { get; set; }
        //public int Status { get; set; }
    }

    public class UsersUpdateModel : UsersAbstract
    {
        //[Required(ErrorMessage = "Agent Type is required")]
        //public int AgentTypeId { get; set; }

        //[Required(ErrorMessage = "Agent Engagement Type is required")]
        //public int AgentEngagementTypeId { get; set; }

        //public int AgentScope { get; set; }
        //public int Status { get; set; }
    }

    public class UserRoleGroupJoinsCreateModel : UsersAbstract
    {
        //[Required(ErrorMessage = "Agent Type is required")]
        //public int AgentTypeId { get; set; }

        //[Required(ErrorMessage = "Agent Engagement Type is required")]
        //public int AgentEngagementTypeId { get; set; }

        //public int AgentScope { get; set; }
        //public int Status { get; set; }

        public string StringroleIDs { get; set; }
        //public string StringgroupIDs { get; set; }
    }

    public class GroupIDandName
    {
        public int GroupID { get; set; }
        public string Name { get; set; }
    }

    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }
    }

    public class ResetPasswordModel
    {
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }
    }

    public class ChangePasswordModel
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class ResetPassword_2Model
    {


        [Required(ErrorMessage = "Invalid password reset Token")]
        public string Token { get; set; }

        [Required(ErrorMessage = "Invalid password reset Token")]
        [EmailAddress]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
