using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;


namespace Helpdesk.Service.CustomAttributes
{
    //////Inherit AuthorizeAttribute if you want to use the predefined properties and functions from Authorize Attribute else inherit Attribute
    ////[AttributeUsage(AttributeTargets.Class)]
    ////[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    //public class Custom_BAuthorizeAttribute : Attribute, IAuthorizationFilter
    //{
    //    public string RequiredPrivileges { get; set; }  //Privileges string to get from controller
    //    ////public List<int> RequiredPrivileges { get; set; }  //Privileges string to get from controller
    //    //public string Permissions { get; set; } //Permission string to get from controller

    //    //// <returns></returns>  
    //    public void OnAuthorization(AuthorizationFilterContext context)
    //    {
    //        ////Validate if any permissions are passed when using attribute at controller or action level
    //        if (string.IsNullOrEmpty(RequiredPrivileges))
    //        {
    //            //Validation cannot take place without any permissions so returning unauthorized
    //            context.Result = new UnauthorizedResult();
    //            return;
    //        }


    //        var currentUserRoles = context.HttpContext.User.Claims.Where(x => x.Type == "CustomRoles").Select(x => x.Value).ToList();
    //       ////IList<string> roles =  userManager.GetRolesAsync(user);
    //        var userRoles_long = currentUserRoles.Select(long.Parse).ToList();   //converted into int

    //        //var rolesprivilleges = _repository.rolesprivileges.GetRolesPrivilegesByRoles(userRoles_long).Select(x=>x.PrivilegeId).ToList();
    //        var privsObj = new privs_2();

    //        var rolesprivilleges = privsObj.Rolesprivileges(userRoles_long);  //current user roles is used to get privilegeIDs

    //        //next i m to work on the required privileges which is from the controller or action
    //        //the privilege string below is splited into array of strings
    //        var RequiredPrivilegesArray = RequiredPrivileges.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
    //        var _requiredPrivilegeIDs = privsObj.PrivilegesByNames(RequiredPrivilegesArray);
    //        //var v = _repository.privileges.GetPrivilegesByNames(RequiredPrivilegesArray);

    //        var userName = context.HttpContext.User.Identity.Name;  //not used

    //        ////bool isAuthorized = CheckUserPermission(context.HttpContext.User, Permissions);
    //        bool isAuthorized = CheckUserPermission(rolesprivilleges, _requiredPrivilegeIDs);

    //        if (isAuthorized)
    //        {
    //            return;
    //        }

    //        context.Result = new UnauthorizedResult();
    //        return;
    //    }

    //    //private bool CheckUserPermission(ClaimsPrincipal user, string Permissions)
    //    private bool CheckUserPermission(List<int> priviliges, List<int> RequiredPriviliges)
    //    {

    //        //// Logic for checking the user permission goes here. 
    //        ////var requiredPermissions = Permissions.Split(","); //Multiple permissiosn can be received from controller, delimiter "," is used to get individual values
    //       // var requiredPermissions = Permissions.Split(","); //Multiple permissiosn can be received from controller, delimiter "," is used to get individual values

    //        if (RequiredPriviliges.Any(x => priviliges.Any(y => y == x)))
    //        {
    //            return true;
    //        }
    //        //foreach (var x in requiredPermissions)
    //        //{
    //        //    if (assignedPermissionsForUser.Contains(x))
    //        //        return; //User Authorized. Wihtout setting any result value and just returning is sufficent for authorizing user
    //        //}

    //        return false;
    //    }
    //}


    //public class privs_2
    //{
    //    public List<int> Rolesprivileges(List<long> userRoles_long)
    //    {
    //        //var privilleges = _repository.rolesprivileges.GetRolesPrivilegesByRoles(userRoles_long).Select(x => x.PrivilegeId).ToList();

    //        //var privsObj = new RolesPrivs();
    //        //var privilleges = privsObj.GetRolesPrivilegesByRoles(userRoles_long).Select(x => x.PrivilegeId).ToList();

    //        var privsObj = new RolesAndPrivileges_2();
    //        var privilleges = privsObj.GetRolesPrivilegesByRoles(userRoles_long).Select(x => x.PrivilegeId).ToList();

    //        //List<int> privilleges = new List<int> { 1, 2 };
    //        return privilleges;
    //    }

    //    public List<int> PrivilegesByNames(List<string> prilegenames)
    //    {
    //        ////var prilegenames = new List<string> { "createuser", "viewusers" };
    //        //var privilleges = _repository.privileges.GetPrivilegesByNames(prilegenames);
    //        //List<int> intPrivileges = privilleges.Select(x => x.PrivilegeId).ToList();

    //        var privsObj = new RolesAndPrivileges_2();
    //        List<int> intPrivileges = privsObj.GetPrivilegesByNames(prilegenames).Select(x => x.PrivilegeId).ToList();

    //        return intPrivileges;
    //    }

    //}
}

//https://stackoverflow.com/questions/63100180/inject-database-context-into-custom-attribute-net-core