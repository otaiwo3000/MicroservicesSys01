using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserMgt.Service.Impl;
using Microsoft.Extensions.Configuration;
using UserMgt.Business.Interfaces;
using UserMgt.Business.Logger;
using UserMgt.Shared.Entities.AspNetEntities;
using UserMgt.Shared.DataAccess.DBContext;
using UserMgt.Service.DtoModels;
using UserMgt.Business.Repositories.Misc;
using UserMgt.Shared.Entities;
using UserMgt.Service.DtoModelsFromJoints;
using UserMgt.Service.Misc;
using UserMgt.Business.EmailService;
using UserMgt.Shared.Common.Utilities;

namespace UserMgt.Service.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/users")]
    // [Authorize]
    public class UsersController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        //private readonly ILogger<UsersController> _logger;
        private readonly ILoggerManagerRepository _logger;
        private readonly UserManager<Aspnetusers> _userManager;
        private readonly AspnetIdentityDBContext _aspnetidentitycontext;
        private IConfiguration _config;


        public UsersController(ILoggerManagerRepository logger, IRepositoryWrapper repository, IMapper mapper, IConfiguration config, 
            IRepositoryWrapper usergrouprepository, UserManager<Aspnetusers> userManager, AspnetIdentityDBContext aspnetidentitycontext)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _config = config;

            _userManager = userManager;
            _aspnetidentitycontext = aspnetidentitycontext;
        }


        ////[HttpGet]
        ////[Route("getallusers")]
        [HttpGet("getallusers")]
        //[CustomAuthorizeFilter(RequiredPrivileges = "viewusers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                //int currentUserOrganization = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());
                int currentUserOrganization = 1;

                var aet = _repository.users.GetAllUsers(currentUserOrganization);
                _logger.LogInformation($"Returned all users from database.");

                var res = _mapper.Map<IEnumerable<UsersModel>>(aet);

                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllUsers action: {ex.Message}");
                _logger.LogError($"Something went wrong inside GetAllUsers action: {ex.InnerException}");
                return StatusCode(500, "Internal server error");
            }
        }

        //[HttpGet]
        //[Route("getusersbyorganizationid")]
        //public IActionResult UsersByOrganizationId()
        //{
        //    try
        //    {
        //        int currentUserOrganization = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());

        //        var users = _repository.users.GetUsersByOrganizationId(currentUserOrganization);
        //        _logger.LogInformation($"Returned the organization users from database.");

        //        var res = _mapper.Map<IEnumerable<UsersModel>>(users);
        //        return Ok(res);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Something went wrong inside UsersByOrganizationId action: {ex.Message}");
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

        [HttpGet("getuser/{id}")]
        //[CustomAuthorizeFilter(RequiredPrivileges = "getuser")]
        public IActionResult UserById(int id)
        {
            try
            {
                int currentUserOrganization = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());

                var user = _repository.users.GetUserById(currentUserOrganization, id);
                if (user == null)
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned user with id: {id}");

                    var res = _mapper.Map<UsersModel>(user);
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UserById action: {ex.Message}");
                _logger.LogError($"Something went wrong inside UserById action: {ex.InnerException}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("getuserwithdetails/{id}")]
        //[CustomAuthorizeFilter(RequiredPrivileges = "getuserwithdetails")]
        public IActionResult UserByIdWithDetails(int id)
        {
            try
            {
                int currentUserOrganization = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());

                var user = _repository.users.GetUserWithDetails(currentUserOrganization, id);
                if (user == null)
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {                 
                    _logger.LogInformation($"Returned user with id: {id}");

                    var res = _mapper.Map<UsersModel>(user);

                    ////var usergroupList = _repository.usergroup.GetUserGroupsByUserId(user.UserId).AsQueryable().Include(y => y.Group).Select(x=> new GroupIDandName { GroupID = x.GroupId, Name = x.Group.Name});
                    //var usergroupList = _repository.usergroup.GetUserGroupsByUserId(user.UserId).AsQueryable().Include(y => y.Group);
                    var userroleList = _repository.userrole.GetUserRolesByUserId(user.UserId).AsQueryable().Include(y => y.Role);

                    //res.UserGroupList = usergroupList.ToList();
                    res.UserRoleList = userroleList.ToList();

                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UserByIdWithDetails action: {ex.Message}");
                _logger.LogError($"Something went wrong inside UserByIdWithDetails action: {ex.InnerException}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("createnewuser")]
        //[CustomAuthorizeFilter(RequiredPrivileges = "createnewuser")]
        public IActionResult CreateUser([FromBody] UserRoleGroupJoinsCreateModel userrolegroupjoinModel)
        //public IActionResult CreateUsers([FromBody]UsersCreateModel dtomodel)
        {
            try
            {
                ////var dtomodel = new UsersCreateModel();
                ////dtomodel.Email = "tes03@g.c";
                ////dtomodel.FirstName = "t03";
                ////dtomodel.LastName = "L03";
                ////dtomodel.Gender = "Male";
                ////dtomodel.AgentTypeId = 1;
                ////dtomodel.AgentEngagementTypeId = 1;
                ////dtomodel.AgentScope = 2;
                ////dtomodel.Status = 1;

                //var dtomodel = new UsersCreateModel();
                //dtomodel.Email = userrolejoinModel.Email;
                //dtomodel.FirstName = userrolejoinModel.FirstName;
                //dtomodel.LastName = userrolejoinModel.LastName;
                //dtomodel.Gender = userrolejoinModel.Gender;
                //dtomodel.AgentTypeId = userrolejoinModel.AgentTypeId;
                //dtomodel.AgentEngagementTypeId = 1;
                //dtomodel.AgentScope = 2;
                //dtomodel.Status = 1;

                var dtomodel = new UsersCreateModel();
                var getdto = new GetDto();
                dtomodel = getdto.GetUserCreateDtoModel(userrolegroupjoinModel);

                if (dtomodel == null)
                {
                    _logger.LogError("Users object sent from client is null.");
                    return BadRequest("Users object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Users object sent from client.");
                    return BadRequest("Invalid model object");
                }

                int currentUserOrganization = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());
                var modelEntity = _mapper.Map<Users>(dtomodel);
                modelEntity.OrganizationId = currentUserOrganization;   //////////// to be got from the login user

                _repository.users.CreateUser(modelEntity);
               _repository.Save();
                _logger.LogInformation($"A new user: {modelEntity.Email} is created.");

                ////var user = _mapper.Map<Aspnetusers>(dtomodel);    //////
                var user = new Aspnetusers()
                {
                    Email = modelEntity.Email,
                    UserName = modelEntity.Email
                };
                ////var pwdObj = new PasswordGeneratorHelper();
                //var result = _userManager.CreateAsync(user, PasswordGeneratorHelper.GeneratePassword(_userManager));
                string pwd = PasswordGeneratorHelper.GeneratePassword(_userManager);
                var result = _userManager.CreateAsync(user, pwd);
                ////var result = _userManager.CreateAsync(user, "A@1xxpass");

                //////var result = await _userManager.CreateAsync(user, userModel.Password);               
                ////var result = _userManager.CreateAsync(new IdentityUser(model.Username), model.Password);
                ////var result = _userManager.AddPasswordAsync(user, "A@1xxpass");
                ////var result =  _signInManager.PasswordSignInAsync(user, model.Password, true, false);


                //var modelEntity_2List = new List<UserGroup>();
                //string[] stringArrayGroupIDs2 = Splitting.SplitString(userrolegroupjoinModel.StringgroupIDs);
                //List<int> groupIDs2 = stringArrayGroupIDs2.Select(int.Parse).ToList();  //convert a list of string to a list of int

                //foreach (var v in groupIDs2)
                //{
                //    var modelEntity_2 = new UserGroup();
                //    modelEntity_2.UserId = modelEntity.UserId;
                //    modelEntity_2.GroupId = v;

                //    modelEntity_2List.Add(modelEntity_2);
                //}

                //_repository.usergroup.CreateRangeUserGroups(modelEntity_2List);
                _repository.Save();
                _logger.LogInformation($"Mapping of the user: {modelEntity.Email} to group(s) upon creation is successful.");


                var modelEntity_3List = new List<UserRole>();
                string[] stringArrayRoleIDs3 = Splitting.SplitString(userrolegroupjoinModel.StringroleIDs);
                List<int> roleIDs3 = stringArrayRoleIDs3.Select(int.Parse).ToList();

                foreach (var v in roleIDs3)
                {
                    var modelEntity_3 = new UserRole();
                    modelEntity_3.UserId = modelEntity.UserId;
                    modelEntity_3.RoleId = v;

                    modelEntity_3List.Add(modelEntity_3);
                }

                _repository.userrole.CreateRangeUserRoles(modelEntity_3List);
                _repository.Save();
                _logger.LogInformation($"Mapping of the user: {modelEntity.Email} to role(s) upon creation is successful.");

                var pendingemailObj = new PendingEmail()
                {
                    RecepientEmails = dtomodel.Email,
                    MailSubject = "USER PROFILE ON ' '  APPLICATION",
                    MailContent = "You have been profiled on ' ' application with your username as: " + user.UserName + " and password as: " + pwd
                };

                _repository.pendingemail.CreatePendingEmail(pendingemailObj);
                _repository.Save();

                var createdUser = _mapper.Map<UsersModel>(modelEntity);
                return Ok(createdUser);
                //return CreatedAtRoute("UserById", new { id = createdUser.UserId }, createdUser);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateUsers action: {ex.Message}");
                _logger.LogError($"Something went wrong inside CreateUsers action: {ex.InnerException}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("updateuser/{id}")]
        //[CustomAuthorizeFilter(RequiredPrivileges = "updateuser")]
        public IActionResult UpdateUser(int id, [FromBody] UserRoleGroupJoinsCreateModel userrolegroupjoinModel)
        {
            try
            {
                ////string userRecordUpdateRes = "User update is not successful. You may try again.";
                //string userGroupRecordsUpdateRes = "User-Group Mapping update is not successful. You may try again.";
                //string userRoleRecordsUpdateRes = "User-Role Mapping update is not successful. You may try again.";

                var dtomodel = new UsersCreateModel();
                var getdto = new GetDto();
                dtomodel = getdto.GetUserCreateDtoModel(userrolegroupjoinModel);

                if (dtomodel == null)
                {
                    _logger.LogError("Users object sent from client is null.");
                    return BadRequest("Users object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Users object sent from client.");
                    return BadRequest("Invalid model object");
                }

                int currentUserOrganization = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());
                var userEntity = _repository.users.GetUserWithDetails(currentUserOrganization, id);
                _logger.LogInformation($"Target User object in the DB has Username: {userEntity.Email}, UserId: {userEntity.UserId}");

                if (userEntity == null)
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(dtomodel, userEntity);

                _repository.users.UpdateUser(userEntity);
                _repository.Save();
                _logger.LogInformation($"The user object for: {userEntity.Email} is successfully updated");

                //var modelEntity_2List = new List<UserGroup>();
                //string[] stringArrayGroupIDs2 = Splitting.SplitString(userrolegroupjoinModel.StringgroupIDs);
                //List<int> groupIDs2 = stringArrayGroupIDs2.Select(int.Parse).ToList();  //convert a list of string to a list of int

                //foreach (var v in groupIDs2)
                //{
                //    var modelEntity_2 = new UserGroup();
                //    modelEntity_2.UserId = userEntity.UserId;
                //    modelEntity_2.GroupId = v;

                //    modelEntity_2List.Add(modelEntity_2);
                //}

                ////to remove range of usergroups
                ////getting corresponding usergroups from the DB using userID
                //IEnumerable<UserGroup> usergroupsDB = _repository.usergroup.GetUserGroupsByUserId(userEntity.UserId);

                //if (usergroupsDB.Count() != 0)
                //{
                //    _repository.usergroup.RemoveRangeUserGroups(usergroupsDB);
                //    _repository.Save();
                //    _logger.LogInformation($"UserGroup records of the user: {userEntity.Email} are deleted successfully.");

                //} 

                //_repository.usergroup.CreateRangeUserGroups(modelEntity_2List);
                //_repository.Save();
                //_logger.LogInformation($"Mapping of the user: {userEntity.Email} to group(s) is successful.");
                

                ////next modify list of userrole,

                //////modify list of user group if requires, 
               
                var modelEntity_3List = new List<UserRole>();
                string[] stringArrayRoleIDs3 = Splitting.SplitString(userrolegroupjoinModel.StringroleIDs);
                List<int> roleIDs3 = stringArrayRoleIDs3.Select(int.Parse).ToList();

                foreach (var v in roleIDs3)
                {
                    var modelEntity_3 = new UserRole();
                    modelEntity_3.UserId = userEntity.UserId;
                    modelEntity_3.RoleId = v;

                    modelEntity_3List.Add(modelEntity_3);
                }

                //to remove range of userroles
                //getting corresponding userroles from the DB using userID
                IEnumerable<UserRole> userrolesDB = _repository.userrole.GetUserRolesByUserId(userEntity.UserId);

                if (userrolesDB.Count() != 0)
                {
                    _repository.userrole.RemoveRangeUserRoles(userrolesDB);
                    _repository.Save();
                    _logger.LogInformation($"UserRole records of the user: {userEntity.Email} are deleted successfully.");
                }

                _repository.userrole.CreateRangeUserRoles(modelEntity_3List);
                _repository.Save();
                _logger.LogInformation($"Mapping of the user: {userEntity.Email} to role(s) is successful.");
              

                var pendingemailObj = new PendingEmail()
                {
                    RecepientEmails = dtomodel.Email,
                    MailSubject = "USER PROFILE ON ' ' APPLICATION",
                    MailContent = "You profile has been modified on ' ' application"
                };

                //return NoContent();

                var updatedUser = _mapper.Map<UsersModel>(userEntity);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateUser action: {ex.Message}");
                _logger.LogError($"Something went wrong inside UpdateUser action: {ex.InnerException}");
                //return StatusCode(500, "Internal server error");
                return StatusCode(500, "User update is not successful or complete.");
            }
        }


        ////[HttpGet("{id}", Name = "OrganizationById")]
        [HttpGet("getuserbyusername/{username}")]
        //[CustomAuthorizeFilter(RequiredPrivileges = "getuserbyusername")]
        public IActionResult GetUserByUserName(string username)
        {
            try
            {            
                var user = _repository.users.GetUserByUserName(username);

                //var curUser = HttpContext.User.Identity;
                //var claimss = HttpContext.User.Claims;
                //var userRoles = HttpContext.User.Claims.Where(x => x.Type == "CustomRoles").Select(x => x.Value).ToList();

                if (user == null)
                {
                    _logger.LogError($"User with username: {username}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned user with username: {username}");

                    var res = _mapper.Map<UsersModel>(user);
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUserByUserName action: {ex.Message}");
                _logger.LogError($"Something went wrong inside GetUserByUserName action: {ex.InnerException}");
                return StatusCode(500, "Internal server error");
            }
        }

        //[HttpGet("getusersbygroup/{groupid}")]
        ////[CustomAuthorizeFilter(RequiredPrivileges = "getusersbygroup")]
        //public IActionResult UserGroupByGroup(int groupid)
        //{
        //    try
        //    {
        //        int currentUserOrganization = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());

        //        var usergroup = _repository.usergroup.GetUserGroupByGroupId(currentUserOrganization, groupid);

        //        //var curUser = HttpContext.User.Identity;
        //        //var claimss = HttpContext.User.Claims;
        //        //var userRoles = HttpContext.User.Claims.Where(x => x.Type == "CustomRoles").Select(x => x.Value).ToList();

        //        if (usergroup == null)
        //        {
        //            _logger.LogError($"User with usergroup hasn't been found in db.");
        //            return NotFound();
        //        }
        //        else
        //        {
        //            _logger.LogInformation($"Returned users");

        //            var res = _mapper.Map<IEnumerable<UserGroupModel>>(usergroup);
        //            return Ok(res);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Something went wrong inside UserGroupByGroup action: {ex.Message}");
        //        _logger.LogError($"Something went wrong inside UserGroupByGroup action: {ex.InnerException}");
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

        [HttpPost]
        [Route("forgotpassword")]
        //[CustomAuthorizeFilter(RequiredPrivileges = "forgotpassword")]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordModel forgotpasswordModel)
        {
            try
            {
                string username = forgotpasswordModel.UserEmail;
                //var user = _userManager.FindByIdAsync(id);
                var aspnetuser_async = _userManager.FindByNameAsync(username);

                if (aspnetuser_async == null)
                {
                    _logger.LogInformation($"email not found");
                    return Ok();    //for security reason, dont let the user know the email is not found
                    //return NotFound("email not found");
                }


                var aspnetuser = (Aspnetusers)aspnetuser_async.GetType().GetProperty("Result").GetValue(aspnetuser_async);

                var passwordresettoken_async = _userManager.GeneratePasswordResetTokenAsync(aspnetuser);
                string passwordresettoken = passwordresettoken_async.GetType().GetProperty("Result").GetValue(passwordresettoken_async).ToString();

                var callback = Url.Action(nameof(ResetPassword), "Account", new { passwordresettoken, email = username }, Request.Scheme);



                string pwd = PasswordGeneratorHelper.GeneratePassword(_userManager);

                var result = _userManager.ResetPasswordAsync(aspnetuser, passwordresettoken, pwd);

                if (result.Result.Succeeded)
                    return Ok($"You have successfully reset password for {username}.");

                else
                    return StatusCode(500, "Operation not successful");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception Message in ForgotPassword Action: {ex.Message}");
                _logger.LogError($"Inner Exception Message in ForgotPassword Action: {ex.InnerException.Message}");
                _logger.LogError($"Inner Exception Message in ForgotPassword Action: {ex.InnerException}");
                _logger.LogError($"Inner Stacktrace from ForgotPassword Action: {ex.StackTrace}");

                return StatusCode(500, "Internal server error. Check log file for detail");
            }
        }

        [HttpPost]
        [Route("forgotpassword_2")]
        //[CustomAuthorizeFilter(RequiredPrivileges = "forgotpassword")]
        [AllowAnonymous]
        public IActionResult ForgotPassword_2([FromBody] ForgotPasswordModel forgotpasswordModel)
        {
            //this action is the flow to generate password reset token, create password reset link, and send email.
            try
            {
                string username = forgotpasswordModel.UserEmail;
                //var user = _userManager.FindByIdAsync(id);
                //var aspnetuser_async = _userManager.FindByNameAsync(username);
                var aspnetuser_async = _userManager.FindByEmailAsync(username);
                var aspnetuser = (Aspnetusers)aspnetuser_async.GetType().GetProperty("Result").GetValue(aspnetuser_async);

                if (aspnetuser_async == null)
                {
                    _logger.LogInformation($"email not found");
                    return Ok();    //for security reason, dont let the user know the email is not found
                    //return NotFound("email not found");
                }

                //var aspnetemail_async = _userManager.FindByEmailAsync(username);
                //var aspnetemail = (Aspnetusers)aspnetemail_async.GetType().GetProperty("Result").GetValue(aspnetemail_async);
                //var isemailconfirmed_async = _userManager.IsEmailConfirmedAsync(aspnetemail);
                //var isemailconfirmed = isemailconfirmed_async.GetType().GetProperty("Result").GetValue(isemailconfirmed_async).ToString();

                //if (isemailconfirmed.ToLower() != "true")
                //{
                ////for security purpose, we dont want to review to the enduser that email doesnt exist or the email is not confirmed, we just 
                ////need to return to forgot password page.
                //    //return to forgot password page
                //}


                //generate password reset token in the below:
                var passwordresettoken_async = _userManager.GeneratePasswordResetTokenAsync(aspnetuser);
                string passwordresettoken = passwordresettoken_async.GetType().GetProperty("Result").GetValue(passwordresettoken_async).ToString();

                ////I will be building password reset link in the below.
                ////In the below, the callback is the password reset link:
                //var callback = Url.Action(nameof(ResetPassword), "Users", new { email = username, passwordresettoken  }, Request.Scheme);
                var callback = Url.Action(nameof(ResetPassword_2), "Users", new { Token = passwordresettoken, UserEmail = username },  Request.Scheme);


                //NOTE:
                //var callback = Url.Action("TheNameOfTheActionThatwillResetThePassword", "Account", new { passwordresettoken, email = username }, Request.Scheme);
                //var callback = Url.Action("ChangePassword_2", "Users", new { passwordresettoken, email = username }, Request.Scheme);
                //Request.Scheme is used to generate absolute Url (with http or https) otherwise, it will generate relative Url.

                var message = new UserMgt.Business.EmailService.Message(new string[] { username }, "Reset password token", callback, null);
                //_emailSender.SendEmailAsync(message);
                NetMailImpl nmi = new NetMailImpl(_repository, _config, _logger);

                return Ok($"A link has been sent, please check your email to reset your password.");
                //return to forgot password confirmation page with message such as:
                //"A link has been sent, please check your email to reset your password."
                //OR
                //"If you have an account with us, we have sent an email with the instruction to reset your password."
                //OR
                //"Check your email for confirmation"
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception Message in ForgotPassword Action: {ex.Message}");
                _logger.LogError($"Inner Exception Message in ForgotPassword Action: {ex.InnerException.Message}");
                _logger.LogError($"Inner Exception Message in ForgotPassword Action: {ex.InnerException}");
                _logger.LogError($"Inner Stacktrace from ForgotPassword Action: {ex.StackTrace}");

                return StatusCode(500, "Internal server error. Check log file for detail");
            }
        }


        [HttpPost]
        [Route("resetpassword")]
        //[CustomAuthorizeFilter(RequiredPrivileges = "resetpassword")]
        public IActionResult ResetPassword([FromBody] ResetPasswordModel resetpasswordModel)
        {
            try
            {
                string username = resetpasswordModel.UserEmail;
                //var user = _userManager.FindByIdAsync(id);
                var aspnetuser_async = _userManager.FindByNameAsync(username);
                var aspnetuser = (Aspnetusers)aspnetuser_async.GetType().GetProperty("Result").GetValue(aspnetuser_async);

                var passwordresettoken_async = _userManager.GeneratePasswordResetTokenAsync(aspnetuser);
                string passwordresettoken = passwordresettoken_async.GetType().GetProperty("Result").GetValue(passwordresettoken_async).ToString();

                string pwd = PasswordGeneratorHelper.GeneratePassword(_userManager);

                var result = _userManager.ResetPasswordAsync(aspnetuser, passwordresettoken, pwd);

                if (result.Result.Succeeded)
                    return Ok($"You have successfully reset password for {username}.");

                else
                    return StatusCode(500, "Operation not successful");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception Message in ResetPassword Action: {ex.Message}");
                _logger.LogError($"Inner Exception Message in ResetPassword Action: {ex.InnerException.Message}");
                _logger.LogError($"Inner Exception Message in ResetPassword Action: {ex.InnerException}");
                _logger.LogError($"Inner Stacktrace from ResetPassword Action: {ex.StackTrace}");

                return StatusCode(500, "Internal server error. Check log file for detail");
            }
        }

        //below will be used to change password from forgot password call by the end-user. The 
        [HttpPost]
        [Route("resetpassword_2")]
        //[CustomAuthorizeFilter(RequiredPrivileges = "resetpassword")]
        [AllowAnonymous]    //end-user is not expected to login before getting access/privilege to this url/endpoint/page
        public IActionResult ResetPassword_2([FromBody] ResetPassword_2Model resetpasswordModel)
        {
            try
            {
                string username = resetpasswordModel.UserEmail;
                ////var user = _userManager.FindByIdAsync(id);
                //var aspnetuser_async = _userManager.FindByNameAsync(username);
                var aspnetuser_async = _userManager.FindByEmailAsync(username);
                var aspnetuser = (Aspnetusers)aspnetuser_async.GetType().GetProperty("Result").GetValue(aspnetuser_async);

                //var passwordresettoken_async = _userManager.GeneratePasswordResetTokenAsync(aspnetuser);
                //string passwordresettoken = passwordresettoken_async.GetType().GetProperty("Result").GetValue(passwordresettoken_async).ToString();

                string passwordresettoken = resetpasswordModel.Token;

                if (string.IsNullOrEmpty(resetpasswordModel.Token))
                    return StatusCode(500, "Invalid password reset Token");

                if (string.IsNullOrEmpty(resetpasswordModel.UserEmail))
                    return StatusCode(500, "Invalid password reset Token");

                if (string.IsNullOrEmpty(resetpasswordModel.NewPassword))
                    return StatusCode(500, "Invalid password reset Token");

                if (string.IsNullOrEmpty(resetpasswordModel.NewPassword))
                    return StatusCode(500, "New Password is not supplied");

                if (!CheckingPassword(resetpasswordModel.NewPassword))
                    return StatusCode(500, "Password should be a minimum of 8 characters and maximum of 40 characters. " +
                        "Must contain at least one digit. " +
                        "Must contain at least one lowercase letter. " +
                        "Must contain at least one uppercase letter. " +
                        "Must contain at least one special character");

                //var result = _userManager.ChangePasswordAsync(aspnetuser, changepasswordModel.CurrentPassword, changepasswordModel.NewPassword);
                var result = _userManager.ResetPasswordAsync(aspnetuser, passwordresettoken, resetpasswordModel.NewPassword);

                if (result.Result.Succeeded)
                    //return Ok("You have successfully reset your password.");
                    return Ok("Your password is reset");
                //return Ok("Your password is reset. Click HERE to login");

                else
                    //return StatusCode(500, "Operation not successful");
                    return StatusCode(((int)result.Status), "Operation not successful or Invalid Password reset token");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception Message in ResetPassword Action: {ex.Message}");
                _logger.LogError($"Inner Exception Message in ResetPassword Action: {ex.InnerException.Message}");
                _logger.LogError($"Inner Exception Message in ResetPassword Action: {ex.InnerException}");
                _logger.LogError($"Inner Stacktrace from ResetPassword Action: {ex.StackTrace}");

                return StatusCode(500, "Internal server error. Check log file for detail");
            }
        }

        [HttpPost]
        [Route("changepassword")]
        //[CustomAuthorizeFilter(RequiredPrivileges = "changepassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordModel changepasswordModel)
        {
            try
            {
                if (string.IsNullOrEmpty(changepasswordModel.CurrentPassword))
                    return StatusCode(500, "Current Password is not supplied");

                if (string.IsNullOrEmpty(changepasswordModel.NewPassword))
                    return StatusCode(500, "New Password is not supplied");

                if (changepasswordModel.CurrentPassword == changepasswordModel.NewPassword)
                    return StatusCode(500, "You cannot supplied the same value for both Current Password and New Password");

                if (!CheckingPassword(changepasswordModel.NewPassword))
                    return StatusCode(500, "Password should be a minimum of 8 characters and maximum of 40 characters. " +
                        "Must contain at least one digit. " +
                        "Must contain at least one lowercase letter. " +
                        "Must contain at least one uppercase letter. " +
                        "Must contain at least one special character");

                string username = HttpContext.User.Claims.Where(x => x.Type == "username").Select(x => x.Value).FirstOrDefault();
                var aspnetuser_async = _userManager.FindByNameAsync(username);

                //var task = aspnetuser_async;
                //var aspnetuser = (Aspnetusers) task.GetType().GetProperty("Result").GetValue(task);
                var aspnetuser = (Aspnetusers)aspnetuser_async.GetType().GetProperty("Result").GetValue(aspnetuser_async);

                var result = _userManager.ChangePasswordAsync(aspnetuser, changepasswordModel.CurrentPassword, changepasswordModel.NewPassword);

                if(result.Result.Succeeded)
                    return Ok("You have successfully changed your password.");

                else
                    return StatusCode(500, "Operation not successful");

            }
            catch(Exception ex)
            {
                _logger.LogError($"Exception Message in ChangePassword Action: {ex.Message}");
                _logger.LogError($"Inner Exception Message in ChangePassword Action: {ex.InnerException.Message}");
                _logger.LogError($"Inner Exception Message in ChangePassword Action: {ex.InnerException}");
                _logger.LogError($"Inner Stacktrace from ChangePassword Action: {ex.StackTrace}");

                return StatusCode(500, "Internal server error. Check log file for detail");
            }           
        }

        public bool CheckingPassword(string password)
        {
            //((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{8,40})

            bool result = System.Text.RegularExpressions.Regex.IsMatch(password, @"(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{8,400}");

            return result;

            //string email = "support@javatpoint.com";
            //var result = Regex.IsMatch(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

            //string email = txtemail.Text;
            //Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            //Match match = regex.Match(email);
            //if (match.Success)
            //    Response.Write(email + " is correct");
        }

        [HttpGet("currentuser")]
        //[CustomAuthorizeFilter(RequiredPrivileges = "currentuser")]
        public IActionResult CurrentUser()
        {
            try
            {
                CurrentUser currentuser = new CurrentUser();

                //int currentUserOrganization = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());
                currentuser.UserName = Convert.ToString(HttpContext.User.Claims.Where(x => x.Type == "username").Select(x => x.Value).FirstOrDefault());
                currentuser.FullName = Convert.ToString(HttpContext.User.Claims.Where(x => x.Type == "displaynames").Select(x => x.Value).FirstOrDefault());

                if (currentuser == null)
                {
                    _logger.LogError($"User not found");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned current user username and fullname");

                    var res = currentuser;
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CurrentUser action: {ex.Message}");
                _logger.LogError($"Something went wrong inside CurrentUser action: {ex.InnerException}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
