using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserMgt.Business.Interfaces;
using UserMgt.Shared.Entities;

namespace UserMgt.Service.Impl
{
    public class CustomClaims
    {
        private IRepositoryWrapper _repository;

        public CustomClaims(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public List<Claim> UserRolesGroupsClaims(string username)
        ////public List<Claim> UserRolesGroupsClaims(Users user)
        {
            Users user = _repository.users.GetUserByUserName(username);

            ////List<UserRole> currentUser_Roles = new List<UserRole>();
            List<UserRole> currentUser_Roles = _repository.userrole.GetUserRolesByUserId(user.UserId).ToList();

            //List<UserGroup> currentUser_Groups = _repository.usergroup.GetUserGroupsByUserId(user.UserId).ToList();

            //////int[] currentUser_Roles = new int[] { 1, 2, 3 };
            ////List<Roles> currentUser_Roles = new List<Roles>
            ////{
            ////    new Roles { RoleId = 1, Name = "Admin"},
            ////    new Roles { RoleId = 2, Name = "UserAdmin"}
            ////};

            ////List<Groups> currentUser_Groups = new List<Groups>
            ////{
            ////    new Groups { GroupId = 1, Name = "MIS"},
            ////    new Groups { GroupId = 2, Name = "IFRS"}
            //};

            var authClaims = new List<Claim>
                {
                    new Claim("username", user.Email),
                    new Claim("displaynames", user.FirstName + " " + user.LastName),
                    new Claim("organization", user.OrganizationId.ToString()),
                    //new Claim("agentscope", user.AgentScope.ToString()),
                    ////new Claim("agentengagementtype", user.AgentEngagementType.ToString()),
                    ////new Claim("agenttype", user.AgentType.ToString()),
                    //new Claim("agentengagementtype", user.AgentEngagementTypeId.ToString()),
                    //new Claim("agenttype", user.AgentTypeId.ToString()),
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var uRole in currentUser_Roles)
            {
                //authClaims.Add(new Claim(ClaimTypes.Role, Convert.ToString(uRole.RoleId)));
                authClaims.Add(new Claim("CustomRoles", Convert.ToString(uRole.RoleId)));
            }

            //foreach (var uGroup in currentUser_Groups)
            //{
            //    authClaims.Add(new Claim("Groups", Convert.ToString(uGroup.GroupId)));
            //}

            return authClaims;
        }

    }
}
