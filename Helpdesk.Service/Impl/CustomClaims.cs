
using Helpdesk.Business.Interfaces;
using Helpdesk.Shared.Entities;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;


namespace Helpdesk.Service.Impl
{
    public class CustomClaims
    {
        private IRepositoryWrapper _repository;

        public CustomClaims(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public List<Claim> UserRolesGroupsClaims(string username)
        //public List<Claim> UserRolesGroupsClaims(Users user)
        {
            Users user = _repository.users.GetUserByUserName(username);

            //List<UserRole> currentUser_Roles = new List<UserRole>();
            List<UserRole>  currentUser_Roles =  _repository.userrole.GetUserRolesByUserId(user.UserId).ToList();

            //List<UserGroup>  currentUser_Groups = _repository.usergroup.GetUserGroupsByUserId(user.UserId).ToList();

            ////int[] currentUser_Roles = new int[] { 1, 2, 3 };
            //List<Roles> currentUser_Roles = new List<Roles>
            //{
            //    new Roles { RoleId = 1, Name = "Admin"},
            //    new Roles { RoleId = 2, Name = "UserAdmin"}
            //};

            //List<Groups> currentUser_Groups = new List<Groups>
            //{
            //    new Groups { GroupId = 1, Name = "MIS"},
            //    new Groups { GroupId = 2, Name = "IFRS"}
            //};

            var authClaims = new List<Claim>
                {
                    new Claim("username", user.Email),
                    new Claim("displaynames", user.FirstName + " " + user.LastName),
                    new Claim("organization", user.OrganizationId.ToString()),
                   // new Claim("agentscope", user.AgentScope.ToString()),               
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

        ////[AllowAnonymous]
        ////[HttpPost]
        ////[Route("authenticate")]
        //public object Authenticate(AuthenticateRequestModel loginUser)
        //{
        //    // My application logic to validate the user
        //    // returns a user entity with Roles collection
        //    var bus = new AccountBusiness();
        //    var user = bus.AuthenticateUser(loginUser.Username, loginUser.Password);
        //    if (user == null)
        //        throw new ApiException("Invalid Login Credentials: " + bus.ErrorMessage, 401);

        //    var claims = new List<Claim>();
        //    claims.Add(new Claim("username", loginUser.Username));
        //    claims.Add(new Claim("displayname", loginUser.Name));

        //    // Add roles as multiple claims
        //    foreach (var role in user.Roles)
        //    {
        //        claims.Add(new Claim(ClaimTypes.Role, role.Name));
        //    }
        //    // Optionally add other app specific claims as needed
        //    claims.Add(new Claim("UserState", UserState.ToString()));

        //    // create a new token with token helper and add our claim
        //    // from `Westwind.AspNetCore`  NuGet Package
        //    var token = JwtHelper.GetJwtToken(
        //        loginUser.Username,
        //        Configuration.JwtToken.SigningKey,
        //        Configuration.JwtToken.Issuer,
        //        Configuration.JwtToken.Audience,
        //        TimeSpan.FromMinutes(Configuration.JwtToken.TokenTimeoutMinutes),
        //        claims.ToArray());

        //    return new
        //    {
        //        token = new JwtSecurityTokenHandler().WriteToken(token),
        //        expires = token.ValidTo
        //    };
        //}

    }
}

//401 - Unauthorized
//403 - Forbidden