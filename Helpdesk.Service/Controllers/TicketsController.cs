using AutoMapper;
using EmailService;
using Helpdesk.Business.Interfaces;
using Helpdesk.Business.Logger;
using Helpdesk.Service.CustomAttributes;
using Helpdesk.Service.DtoModels;
using Helpdesk.Service.Impl;
using Helpdesk.Service.Misc;
using Helpdesk.Shared.Entities;
using Helpdesk.Shared.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helpdesk.Service.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/tickets")]
    // [Authorize]
    public class TicketsController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private readonly IEmailSender _emailSender;

        private readonly ILoggerManagerRepository _logger;
        private IConfiguration _config;

        public TicketsController(ILoggerManagerRepository logger, IRepositoryWrapper repository, IMapper mapper, IEmailSender emailSender, IConfiguration config)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _emailSender = emailSender;
            _config = config;
        }


        [HttpGet("getalltickets")]
        ////[Route("getalltickets")]
        [CustomAuthorizeFilter(RequiredPrivileges = "getalltickets")]
        public IActionResult AllTickets()
        {
            try
            {
                _logger.LogInformation($"Inside Tickets>>AllTickets");

                int currentUserOrganization = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());
                //string username = HttpContext.User.Claims.Where(x => x.Type == "username").Select(x => x.Value).FirstOrDefault();
                //Users user = _repository.users.GetUserByUserName(username);
                //int LogedInUserId = user.UserId;

                //UsersHierarchy uh = new UsersHierarchy(_logger, _repository, _config);
                //var uhList = uh.Children(currentUserOrganization, LogedInUserId);

                //////string[] test = new string[] { "2", "7" };
                ////var convertedToStringList = uhList.ConvertAll(obj => obj.ToString());

                string agentscope = HttpContext.User.Claims.Where(x => x.Type == "agentscope").Select(x => x.Value).FirstOrDefault();
                _logger.LogInformation($"agentscope: {agentscope}");
                //convert strin to Enum eg below:
                //EnumName VariableName= (EnumName)Enum.Parse(typeof(EnumName), StringValue);
                AgentScope agentscope2 = (AgentScope)Enum.Parse(typeof(AgentScope), agentscope);

                string groupsString= Convert.ToString(HttpContext.User.Claims.Where(x => x.Type == "Groups").Select(x => x.Value).FirstOrDefault());
                _logger.LogInformation($"agent groups: {groupsString}");
                var groups = groupsString.Split(',').ToList();
                var groups2 = groups.Select(x => Int32.Parse(x)).ToList();

                _logger.LogInformation($"About to get tickets");
                //var tickets = _repository.tickets.GetAllTickets(currentUserOrganization).Where(x => uhList.Contains(x.AgentId));
                var tickets = _repository.tickets.GetAllTickets(currentUserOrganization, agentscope2, groups2);

                _logger.LogInformation($"Returned all tickets from database.");

                var res = _mapper.Map<IEnumerable<TicketsModel>>(tickets);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside AllTickets action: {ex.Message}");
                _logger.LogError($"Something went wrong inside AllTickets action: {ex.InnerException}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("getticket/{id}")]
        [CustomAuthorizeFilter(RequiredPrivileges = "getticket")]
        public IActionResult TicketById(int id)
        {
            try
            {
                _logger.LogInformation($"Inside Tickets>>TicketById");

                int currentUserOrganization = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());

                var ticket = _repository.tickets.GetTicketById(currentUserOrganization, id);
                if (ticket == null)
                {
                    _logger.LogError($"Ticket with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned ticket with id: {id}");

                    var res = _mapper.Map<TicketsModel>(ticket);
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside TicketById action: {ex.Message}");
                _logger.LogError($"Something went wrong inside TicketById action: {ex.InnerException}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("getticketwithdetails/{id}")]
        [CustomAuthorizeFilter(RequiredPrivileges = "getticketwithdetails")]
        public IActionResult TicketByIdWithDetails(int id)
        {
            try
            {
                _logger.LogInformation($"Inside Tickets>>TicketByIdWithDetails");

                int currentUserOrganization = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());

                var ticket = _repository.tickets.GetTicketWithDetails(currentUserOrganization, id);
                if (ticket == null)
                {
                    _logger.LogError($"Ticket with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation($"Returned ticket with id: {id}");

                    var res = _mapper.Map<TicketsModel>(ticket);
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside TicketByIdWithDetails action: {ex.Message}");
                _logger.LogError($"Something went wrong inside TicketByIdWithDetails action: {ex.InnerException}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("createnewticket")]
        //[Route("createnewticket")]
        [CustomAuthorizeFilter(RequiredPrivileges = "createnewticket")]
        public IActionResult CreateTicket([FromBody] TicketsCreateModel dtomodel)
        {
            try
            {
                _logger.LogInformation($"Inside Tickets>>CreateTicket");

                if (dtomodel == null)
                {
                    _logger.LogError("Tickets object sent from client is null.");
                    return BadRequest("Tickets object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid tickets object sent from client.");
                    return BadRequest("Invalid model object");
                }

                string groupsString = Convert.ToString(HttpContext.User.Claims.Where(x => x.Type == "Groups").Select(x => x.Value).FirstOrDefault());
                _logger.LogInformation($"agent groups: {groupsString}");
                var groups = groupsString.Split(',').ToList();
                var groups2 = groups.Select(x => Int32.Parse(x)).ToList();

                if(!groups2.Contains(1))
                    return Ok("You are not in Customer Experiance Team!!! Hence, you cannot create ticket.");

                int currentUserOrganization = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());
                var modelEntity = _mapper.Map<Tickets>(dtomodel);
                modelEntity.OrganizationId = currentUserOrganization;   //////////// to be got from the login user
                modelEntity.AgentId = dtomodel.AgentId;
                _logger.LogError($"OrganizationID is: { modelEntity.OrganizationId}; AgentID is: { modelEntity.AgentId}");
                modelEntity.SupervisorId = _repository.users.GetUserById(modelEntity.OrganizationId, dtomodel.AgentId).SupervisorId;
                _logger.LogError($"SupervisorID is: { modelEntity.SupervisorId}");


                var pendingemailObj = new PendingEmail()
                {
                    RecepientEmails = dtomodel.Contacts,
                    MailSubject = dtomodel.Subject,
                    MailContent = dtomodel.Description
                };

                ////
                //List<TicketRules> ticketrules = _repository.ticketrules.GetAllTicketRules(currentUserOrganization).Where(x=>x.IsActive==true).ToList();
                //TicketsAutomation ticketautomation = new TicketsAutomation();

                //Ticketruleresult res = new Ticketruleresult();
                //Ticketruleresult tkruleres = ticketautomation.IncomingTicket(modelEntity, ticketrules);
                //if (tkruleres.ReturnedBool)
                //{
                //    //var expectedactions = _repository.ruleaction.GetRuleActionByRuleBatchIDWithDetails(currentUserOrganization, tkruleres.ReturnedRuleBatchID);
                //    var expectedactions = _repository.ruleaction_2.GetRuleAction_2ByRulebatchID(tkruleres.ReturnedRuleBatchID);

                //    foreach(var v in expectedactions)
                //    {
                //        //////Tickets ticket = new Tickets();
                //        ////Type type = modelEntity.GetType();
                //        ////System.Reflection.PropertyInfo prop = type.GetProperty(v.ActionProperty);
                //        ////prop.SetValue(modelEntity, v.ActionValue, null);

                //        //modelEntity.GetType().GetProperty(v.ActionProperty).SetValue(modelEntity, int.Parse(v.ActionValue.Trim()), null);
                //        modelEntity.GetType().GetProperty(v.ActionProperty).SetValue(modelEntity, v.ActionValue, null);
                //    }

                //    //modelEntity.
                //}

                _repository.tickets.CreateTicket(modelEntity);
                _repository.pendingemail.CreatePendingEmail(pendingemailObj);
                _repository.Save();

                //PendingEmailCall pec = new PendingEmailCall(_emailSender, _repository);
                //pec.SendPendingEmail();

                var createdTickets = _mapper.Map<TicketsModel>(modelEntity);
                return Ok(createdTickets);
                //return CreatedAtRoute("GroupById", new { id = createdGroup.GroupId }, createdGroup);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateTicket action: {ex.Message}");
                _logger.LogError($"Something went wrong inside CreateTicket action: {ex.InnerException}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("updateticket/{id}")]
        //[Route("updateticket/{id}")]
        [CustomAuthorizeFilter(RequiredPrivileges = "updateticket")]
        public IActionResult UpdateTicket(int id, [FromBody] TicketsUpdateModel dtomodel)
        {
            try
            {
                _logger.LogInformation($"Inside Tickets>>UpdateTicket");
                string fintraksupportemailaddress = _config["fintraksupportemailaddress"];

                if (dtomodel == null)
                {
                    _logger.LogError("Ticket object sent from client is null.");
                    return BadRequest("Ticket object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Ticket object sent from client.");
                    return BadRequest("Invalid model object");
                }

                int currentUserOrganization = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());
                //string rolesString = Convert.ToString(HttpContext.User.Claims.Where(x => x.Type == "CustomRoles").Select(x => x.Value).FirstOrDefault());
                //_logger.LogInformation($"user roles: {rolesString}");
                //var roles = rolesString.Split(',').ToList();
                // var roles2 = roles.Select(x => Int32.Parse(x)).ToList();

                var currentUserRoles = HttpContext.User.Claims.Where(x => x.Type == "CustomRoles").Select(x => x.Value).ToList();
                var currentUserRoles_int = currentUserRoles.Select(long.Parse).ToList();   //converted into int


                //var ticketEntity = _repository.tickets.GetTicketById(currentUserOrganization, id);
                var ticketEntity = _repository.tickets.GetTicketWithDetails(currentUserOrganization, id);
                if (ticketEntity == null)
                {
                    _logger.LogError($"Ticket with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                var pendingemailObj = new PendingEmail()
                {
                    RecepientEmails = fintraksupportemailaddress + ", " + ticketEntity.Agent.Email + "," + ticketEntity.Supervisor.Email,
                    MailSubject = dtomodel.Subject,
                    MailContent = dtomodel.Description
                };

                
                //ResolutionStartDate and ResolutionEndDate are set when ticket is assigned to an agent as in the case below
                //When agent is changed in the ticket page, it means the ticket is assigned to the new agent
                //The immediate below is meant for admin/superadmin
                if (dtomodel.AgentId != ticketEntity.AgentId)
                {                  
                    ticketEntity.ResolutionStartDate = DateTime.Now;
                    ticketEntity.ResolutionEndDate = DateTime.Now;

                    pendingemailObj.RecepientEmails = fintraksupportemailaddress + ", " + ticketEntity.Agent.Email + "," + ticketEntity.Supervisor.Email;
                }


                /* if(dtomodel.StatusId != ticketEntity.StatusId && dtomodel.StatusId == 3)
                 {
                     TicketTicketStatusUpdateModel ttsUpdateModel = new TicketTicketStatusUpdateModel();
                     ttsUpdateModel.TicketId = id;
                     ttsUpdateModel.StatusId = dtomodel.StatusId;

                     ticketEntity.ResolutionEndDate = DateTime.Now;

                     pendingemailObj.MailSubject = dtomodel.Subject + " " + "The Ticket has been resolved.";

                     _mapper.Map(ttsUpdateModel, ticketEntity);
                     _repository.tickets.UpdateTicketTicketStatus(ticketEntity);
                     _repository.pendingemail.CreatePendingEmail(pendingemailObj);
                     _repository.Save();

                     var updatedTicket_0 = _mapper.Map<TicketsModel>(ticketEntity);
                     return Ok(updatedTicket_0);
                 }

                 else if(dtomodel.StatusId != ticketEntity.StatusId && dtomodel.StatusId == 4)
                 {
                     TicketTicketStatusUpdateModel ttsUpdateModel = new TicketTicketStatusUpdateModel();
                     ttsUpdateModel.TicketId = id;
                     ttsUpdateModel.StatusId = dtomodel.StatusId;

                     ticketEntity.ClosedDate = DateTime.Now;

                     pendingemailObj.MailSubject = dtomodel.Subject + " " + "The Ticket has been closed.";

                 }
                */

                int[] agentOrSupervisor = { 4, 5 };
                //check if the login user is a supervisor or an agent
                if ((currentUserRoles_int.Any(x => agentOrSupervisor.Any(y => y == x))))
                {                    
                    TicketTicketStatusUpdateModel ttsUpdateModel = new TicketTicketStatusUpdateModel();
                    ttsUpdateModel.TicketId = id;
                    ttsUpdateModel.StatusId = dtomodel.StatusId;

                    ticketEntity.ResolutionEndDate = DateTime.Now;

                    pendingemailObj.MailSubject = dtomodel.Subject + " " + "Ticket Resolved!!";
                    pendingemailObj.RecepientEmails = dtomodel.Contacts + ", " + fintraksupportemailaddress + ", " + ticketEntity.Agent.Email + "," + ticketEntity.Supervisor.Email;

                    _mapper.Map(ttsUpdateModel, ticketEntity);
                    _repository.tickets.UpdateTicketTicketStatus(ticketEntity);
                    _repository.pendingemail.CreatePendingEmail(pendingemailObj);
                    _repository.Save();

                    var updatedTicket_0 = _mapper.Map<TicketsModel>(ticketEntity);
                    return Ok(updatedTicket_0);
                }

                if (dtomodel.StatusId != ticketEntity.StatusId && dtomodel.StatusId == 4)
                {
                    //TicketTicketStatusUpdateModel ttsUpdateModel = new TicketTicketStatusUpdateModel();
                    //ttsUpdateModel.TicketId = id;
                    //ttsUpdateModel.StatusId = dtomodel.StatusId;

                    ticketEntity.ClosedDate = DateTime.Now;
                    pendingemailObj.MailSubject = dtomodel.Subject + " " + "Ticket Closed!!!";
                    pendingemailObj.RecepientEmails = dtomodel.Contacts + ", " + fintraksupportemailaddress + ", " + ticketEntity.Agent.Email + "," + ticketEntity.Supervisor.Email;
                }

                _mapper.Map(dtomodel, ticketEntity);

                ticketEntity.AgentId = dtomodel.AgentId;
                ticketEntity.SupervisorId = _repository.users.GetUserById(ticketEntity.OrganizationId, dtomodel.AgentId).SupervisorId;

                _repository.tickets.UpdateTicket(ticketEntity);
                _repository.pendingemail.CreatePendingEmail(pendingemailObj);
                _repository.Save();

                //return NoContent();

                var updatedTicket = _mapper.Map<TicketsModel>(ticketEntity);
                return Ok(updatedTicket);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateTicket action: {ex.Message}");
                _logger.LogError($"Something went wrong inside UpdateTicket action: {ex.InnerException}");
                return StatusCode(500, "Internal server error");
            }
        }

        //[HttpDelete("{id}")]
        //[Route("deleteorganization")]
        //public IActionResult DeleteOrganization(int id)
        //{
        //    try
        //    {
        //        var organization = _repository.organizations.GetOrganizationById(id);
        //        if (organization == null)
        //        {
        //            _logger.LogError($"Organization with id: {id}, hasn't been found in db.");
        //            return NotFound();
        //        }

        //        //if (_repository.Account.AccountsByOwner(id).Any())
        //        //{
        //        //    _logger.LogError($"Cannot delete owner with id: {id}. It has related accounts. Delete those accounts first");
        //        //    return BadRequest("Cannot delete owner. It has related accounts. Delete those accounts first");
        //        //}

        //        _repository.organizations.DeleteOrganization(organization);
        //        _repository.Save();

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Something went wrong inside DeleteOrganization action: {ex.Message}");
        //        return StatusCode(500, "Internal server error");
        //    }
        //}


        //==================== Customer face ticket creation/Issue log starts here =====================================

        [HttpPost("createnewticketbycustomer")]
        //[CustomAuthorizeFilter(RequiredPrivileges = "createnewticketbycustomer")]
        [AllowAnonymous]
        public IActionResult CreateTicketByCustomer([FromBody] ClientTicketsCreateModel dtomodel)
        {
            try
            {
                _logger.LogInformation($"Inside Tickets>>CreateTicketByCustomer");

                if (dtomodel == null)
                {
                    _logger.LogError("Tickets object sent from client is null.");
                    return BadRequest("Tickets object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid tickets object sent from client.");
                    return BadRequest("Invalid model object");
                }

                //int currentUserOrganization = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());
                int currentUserOrganization = 1;
                var modelEntity = _mapper.Map<Tickets>(dtomodel);
                modelEntity.OrganizationId = currentUserOrganization;   //////////// to be got from the login user
                var groupRecord = _repository.groups.GetGroupById(currentUserOrganization, dtomodel.GroupId);
                modelEntity.AgentId = groupRecord.GroupLeadId;           //agent will be the group head or support staff
                //modelEntity.SupervisorId = _repository.users.GetUserById(modelEntity.OrganizationId, dtomodel.AgentId).SupervisorId;
                modelEntity.SupervisorId = groupRecord.GroupLeadId;

                var pendingemailObj = new PendingEmail()
                {
                    RecepientEmails = dtomodel.Contacts,
                    MailSubject = dtomodel.Subject,
                    MailContent = dtomodel.Description
                };

                //
                List<TicketRules> ticketrules = _repository.ticketrules.GetAllTicketRules(currentUserOrganization).Where(x => x.IsActive == true).ToList();
                TicketsAutomation ticketautomation = new TicketsAutomation();

                Ticketruleresult res = new Ticketruleresult();
                Ticketruleresult tkruleres = ticketautomation.IncomingTicket(modelEntity, ticketrules);
                if (tkruleres.ReturnedBool)
                {
                    //var expectedactions = _repository.ruleaction.GetRuleActionByRuleBatchIDWithDetails(currentUserOrganization, tkruleres.ReturnedRuleBatchID);
                    var expectedactions = _repository.ruleaction_2.GetRuleAction_2ByRulebatchID(tkruleres.ReturnedRuleBatchID);

                    foreach (var v in expectedactions)
                    {
                        //////Tickets ticket = new Tickets();
                        ////Type type = modelEntity.GetType();
                        ////System.Reflection.PropertyInfo prop = type.GetProperty(v.ActionProperty);
                        ////prop.SetValue(modelEntity, v.ActionValue, null);

                        //modelEntity.GetType().GetProperty(v.ActionProperty).SetValue(modelEntity, int.Parse(v.ActionValue.Trim()), null);
                        modelEntity.GetType().GetProperty(v.ActionProperty).SetValue(modelEntity, v.ActionValue, null);
                    }

                    //modelEntity.
                }

                _repository.tickets.CreateTicket(modelEntity);
                _repository.pendingemail.CreatePendingEmail(pendingemailObj);
                _repository.Save();

                //PendingEmailCall pec = new PendingEmailCall(_emailSender, _repository);
                //pec.SendPendingEmail();

                var createdTickets = _mapper.Map<TicketsModel>(modelEntity);
                return Ok(createdTickets);
                //return CreatedAtRoute("GroupById", new { id = createdGroup.GroupId }, createdGroup);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateTicketByCustomer action: {ex.Message}");
                _logger.LogError($"Something went wrong inside CreateTicketByCustomer action: {ex.InnerException}");
                return StatusCode(500, "Internal server error");
            }
        }


        //============ Ticket Reports =========================

        [HttpPost("ticketstatusreport")]
        [CustomAuthorizeFilter(RequiredPrivileges = "ticketstatusreport")]
        //public IActionResult GetTicketStatusReport([FromBody] PriorityAndStartAndEndDates priorityANDstartDateANDendDate)
        public IActionResult GetTicketStatusReport([FromBody] TypeAndStartAndEndDates typeANDstartDateANDendDate)
        {
            try
            {
                _logger.LogInformation($"Inside Tickets>>GetTicketStatusReport");

                int currentUserOrganization = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());
                List<TicketStatus> ticketstatusList = new List<TicketStatus>();
                ticketstatusList = _repository.ticketstatus.GetAllTicketStatus().ToList();

                string agentscope = HttpContext.User.Claims.Where(x => x.Type == "agentscope").Select(x => x.Value).FirstOrDefault();
                _logger.LogInformation($"agentscope: {agentscope}");
                //convert strin to Enum eg below:
                //EnumName VariableName= (EnumName)Enum.Parse(typeof(EnumName), StringValue);
                AgentScope agentscope2 = (AgentScope)Enum.Parse(typeof(AgentScope), agentscope);

                string groupsString = Convert.ToString(HttpContext.User.Claims.Where(x => x.Type == "Groups").Select(x => x.Value).FirstOrDefault());
                _logger.LogInformation($"agent groups: {groupsString}");
                var groups = groupsString.Split(',').ToList();
                var CurrentUsergroupIDs = groups.Select(x => Int32.Parse(x)).ToList();

                _logger.LogInformation($"About to call TicketStatusReportResult");
                var ticketstatusreport = _repository.tickets.TicketStatusReportResult(currentUserOrganization, agentscope2, CurrentUsergroupIDs, ticketstatusList, typeANDstartDateANDendDate.Type, typeANDstartDateANDendDate.StartDate, typeANDstartDateANDendDate.EndDate);

                if (ticketstatusreport.Count() == 0)
                {
                    _logger.LogInformation($"No record is found.");

                    return NotFound();
                }

                _logger.LogInformation($"Returned ticket status report from database.");

                //var res = _mapper.Map<IEnumerable<TicketsModel>>(tickets);
                return Ok(ticketstatusreport);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetTicketStatusReport action: {ex.Message}");
                _logger.LogError($"Something went wrong inside GetTicketStatusReport action: {ex.InnerException}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("ticketstatusreportgroupbase")]
        [CustomAuthorizeFilter(RequiredPrivileges = "ticketstatusreportgroupbase")]
        //public IActionResult GetTicketStatusReportGroupBase([FromBody] PriorityAndStartAndEndDates priorityANDstartDateANDendDate)
        public IActionResult GetTicketStatusReportGroupBase([FromBody] TypeAndStartAndEndDates typeANDstartDateANDendDate)
        {
            try
            {
                _logger.LogInformation($"Inside Tickets>>GetTicketStatusReportGroupBase");

                int currentUserOrganization = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());
                //List<TicketStatus> ticketstatusList = new List<TicketStatus>();
                //ticketstatusList = _repository.ticketstatus.GetAllTicketStatus().ToList();

                string agentscope = HttpContext.User.Claims.Where(x => x.Type == "agentscope").Select(x => x.Value).FirstOrDefault();
                _logger.LogInformation($"agentscope: {agentscope}");
                //convert strin to Enum eg below:
                //EnumName VariableName= (EnumName)Enum.Parse(typeof(EnumName), StringValue);
                AgentScope agentscope2 = (AgentScope)Enum.Parse(typeof(AgentScope), agentscope);

                var allgroups = _repository.groups.GetAllGroups(currentUserOrganization);
                List<int> allgroupsIDs = allgroups.Select(x => x.GroupId).ToList();

                _logger.LogInformation($"About to call TicketStatusReportResultGroupBase");
                var ticketstatusreportgroupbase = _repository.tickets.TicketStatusReportResultGroupBase(currentUserOrganization, agentscope2, allgroupsIDs, typeANDstartDateANDendDate.Type, typeANDstartDateANDendDate.StartDate, typeANDstartDateANDendDate.EndDate);

                _logger.LogInformation($"Returned tickets status report (group base) from database.");

                //var res = _mapper.Map<IEnumerable<TicketsModel>>(tickets);
                return Ok(ticketstatusreportgroupbase);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetTicketStatusReportGroupBase action: {ex.Message}");
                _logger.LogError($"Something went wrong inside GetTicketStatusReportGroupBase action: {ex.InnerException}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("ticketfilter")]
        [CustomAuthorizeFilter(RequiredPrivileges = "ticketfilter")]
        public IActionResult TicketFilter([FromBody] TicketFilter ticketFilter)
        {
            try
            {
                _logger.LogInformation($"Inside Tickets>>TicketFilter");

                int currentUserOrganization = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());
                //List<TicketStatus> ticketstatusList = new List<TicketStatus>();
                //ticketstatusList = _repository.ticketstatus.GetAllTicketStatus().ToList();

                string agentscope = HttpContext.User.Claims.Where(x => x.Type == "agentscope").Select(x => x.Value).FirstOrDefault();
                _logger.LogInformation($"agentscope: {agentscope}");
                //convert strin to Enum eg below:
                //EnumName VariableName= (EnumName)Enum.Parse(typeof(EnumName), StringValue);
                AgentScope agentscope2 = (AgentScope)Enum.Parse(typeof(AgentScope), agentscope);

                string groupsString = Convert.ToString(HttpContext.User.Claims.Where(x => x.Type == "Groups").Select(x => x.Value).FirstOrDefault());
                _logger.LogInformation($"agent groups: {groupsString}");
                var groups = groupsString.Split(',').ToList();
                var CurrentUsergroupIDs = groups.Select(x => Int32.Parse(x)).ToList();

                //NOTE:
                //CurrentUsergroupIDs: are groups the current user belong to.
                //groupid: is the group you are searching for.

                _logger.LogInformation($"About to call TicketsFilter");
                ////var filteredTickets = _repository.tickets.TicketsFilter(currentUserOrganization, agentscope2, groups2, ticketFilter.GroupId, ticketFilter.ProductId, ticketFilter.StatusId, ticketFilter.PriorityId, ticketFilter.StartDate, ticketFilter.EndDate);
                var filteredTickets = _repository.tickets.TicketsFilter(currentUserOrganization, agentscope2, CurrentUsergroupIDs, ticketFilter.GroupId, ticketFilter.ProductId, ticketFilter.StatusId, ticketFilter.TypeId, ticketFilter.StartDate, ticketFilter.EndDate);

                _logger.LogInformation($"Returned filtered tickets from database.");

                //var res = _mapper.Map<IEnumerable<TicketsModel>>(tickets);
                return Ok(filteredTickets);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Tickets>>TicketFilter action: {ex.Message}");
                _logger.LogError($"Something went wrong inside Tickets>>TicketFilter action: {ex.InnerException}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("ticketfilter_2")]
        [CustomAuthorizeFilter(RequiredPrivileges = "ticketfilter")]
        public IActionResult TicketFilter_2([FromBody] TicketFilter ticketFilter)
        {
            try
            {
                _logger.LogInformation($"Inside Tickets>>TicketFilter_2");

                int currentUserOrganization = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());
                List<TicketStatus> ticketstatusList = new List<TicketStatus>();
                ticketstatusList = _repository.ticketstatus.GetAllTicketStatus().ToList();

                string agentscope = HttpContext.User.Claims.Where(x => x.Type == "agentscope").Select(x => x.Value).FirstOrDefault();
                _logger.LogInformation($"agentscope: {agentscope}");
                //convert strin to Enum eg below:
                //EnumName VariableName= (EnumName)Enum.Parse(typeof(EnumName), StringValue);
                AgentScope agentscope2 = (AgentScope)Enum.Parse(typeof(AgentScope), agentscope);

                string groupsString = Convert.ToString(HttpContext.User.Claims.Where(x => x.Type == "Groups").Select(x => x.Value).FirstOrDefault());
                _logger.LogInformation($"agent groups: {groupsString}");
                var groups = groupsString.Split(',').ToList();
                var CurrentUsergroupIDs = groups.Select(x => Int32.Parse(x)).ToList();

                //NOTE:
                //CurrentUsergroupIDs: are groups the current user belong to.
                //groupid: is the group you are searching for.

                _logger.LogInformation($"About to call TicketsFilter");
                //var filteredTickets = _repository.tickets.TicketsFilter(currentUserOrganization, agentscope2, groups2, ticketFilter.GroupId, ticketFilter.ProductId, ticketFilter.StatusId, ticketFilter.PriorityId, ticketFilter.StartDate, ticketFilter.EndDate);
                var filteredTickets = _repository.tickets.TicketsFilter(currentUserOrganization, agentscope2, CurrentUsergroupIDs, ticketFilter.GroupId, ticketFilter.ProductId, ticketFilter.StatusId, ticketFilter.TypeId, ticketFilter.StartDate, ticketFilter.EndDate);

                _logger.LogInformation($"Returned filtered tickets from database.");

                //var res = _mapper.Map<IEnumerable<TicketsModel>>(tickets);
                return Ok(filteredTickets);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Tickets>>TicketFilter_2 action: {ex.Message}");
                _logger.LogError($"Something went wrong inside Tickets>>TicketFilter_2 action: {ex.InnerException}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("ticketvolumetrend")]
        [CustomAuthorizeFilter(RequiredPrivileges = "ticketvolumetrend")]
        public IActionResult TicketVolumeTrend([FromBody] StartAndEndDates StartdateEnddate)
        {
            try
            {
                _logger.LogInformation($"Inside Tickets>>TicketVolumeTrend");


                int[] intArray01 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

                TicketFilter ticketFilter = new TicketFilter()
                {
                    GroupId = intArray01.ToList(),
                    StatusId = intArray01.ToList(),
                    //PriorityId = intArray01.ToList(),
                    TypeId = intArray01.ToList(),
                    ProductId = intArray01.ToList(),
                    StartDate = StartdateEnddate.StartDate,
                    EndDate = StartdateEnddate.EndDate
                };

                int currentUserOrganization = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());
                //List<TicketStatus> ticketstatusList = new List<TicketStatus>();
                //ticketstatusList = _repository.ticketstatus.GetAllTicketStatus().ToList();

                string agentscope = HttpContext.User.Claims.Where(x => x.Type == "agentscope").Select(x => x.Value).FirstOrDefault();
                _logger.LogInformation($"agentscope: {agentscope}");
                //convert strin to Enum eg below:
                //EnumName VariableName= (EnumName)Enum.Parse(typeof(EnumName), StringValue);
                AgentScope agentscope2 = (AgentScope)Enum.Parse(typeof(AgentScope), agentscope);

                string groupsString = Convert.ToString(HttpContext.User.Claims.Where(x => x.Type == "Groups").Select(x => x.Value).FirstOrDefault());
                _logger.LogInformation($"agent groups: {groupsString}");
                var groups = groupsString.Split(',').ToList();
                var CurrentUsergroupIDs = groups.Select(x => Int32.Parse(x)).ToList();

                //NOTE:
                //CurrentUsergroupIDs: are groups the current user belong to.
                //groupid: is the group you are searching for.

                _logger.LogInformation($"About to call TicketsFilter");
                ////var filteredTickets = _repository.tickets.TicketsFilter(currentUserOrganization, agentscope2, groups2, ticketFilter.GroupId, ticketFilter.ProductId, ticketFilter.StatusId, ticketFilter.PriorityId, ticketFilter.StartDate, ticketFilter.EndDate);
                var filteredTickets = _repository.tickets.TicketsFilter(currentUserOrganization, agentscope2, CurrentUsergroupIDs, ticketFilter.GroupId, ticketFilter.ProductId, ticketFilter.StatusId, ticketFilter.TypeId, ticketFilter.StartDate, ticketFilter.EndDate);

                _logger.LogInformation($"Returned filtered tickets from database.");

                foreach (var mc in filteredTickets.Where(x => x.StatusId != 3)) mc.StatusId = 0;

                List<TicketStatusYearMonth> tsList = filteredTickets.Select(x => new TicketStatusYearMonth { StatusId = x.StatusId, Month = x.CreatedOn.Month, Year = x.CreatedOn.Year }).ToList();

                List<int> formedStatusIDList = new List<int>() { 0, 3};  //0=unresolved, 3=resolved
                List<int> yearList = tsList.Select(x => x.Year).Distinct().ToList();
                List<int> monthList = tsList.Select(x => x.Month).Distinct().ToList();
                List<TicketStatusYearMonth> statusyearmonthCountList = new List<TicketStatusYearMonth>();


                foreach (var y in yearList)
                {
                    foreach (var m in monthList)
                    {
                        //foreach (var s in formedStatusIDList)
                        //{
                        //if (s == 0)
                        //{
                        //    TicketStatusYearMonth t = new TicketStatusYearMonth();
                        //    //unresolved
                        //    t.StatusId = 0;
                        //    t.Year = y;
                        //    t.Month = m;
                        //    int unresolvedcount = tsList.Where(x => x.Year == y && x.Month == m && x.StatusId == 0).Count();
                        //    t.Count = unresolvedcount;
                        //    statusyearmonthCountList.Add(t);
                        //}
                        //if (s == 3)
                        //{
                        //    TicketStatusYearMonth t = new TicketStatusYearMonth();
                        //    //resolved
                        //    t.StatusId = 3;
                        //    t.Year = y;
                        //    t.Month = m;
                        //    int resolvedcount = tsList.Where(x => x.Year == y && x.Month == m && x.StatusId == 3).Count();
                        //    t.Count = resolvedcount;
                        //    statusyearmonthCountList.Add(t);
                        //}
                        //}

                        int TotalNumberOfTickets = tsList.Where(x => x.Year == y && x.Month == m).Count();

                        TicketStatusYearMonth t1 = new TicketStatusYearMonth();
                        ////unresolved
                        t1.StatusId = 0;
                        t1.StatusName = "Unresolved Tickets";
                        t1.Year = y;
                        t1.Month = m;
                        t1.TotalNumberOfTickets = TotalNumberOfTickets;
                        int unresolvedcount = tsList.Where(x => x.Year == y && x.Month == m && x.StatusId == 0).Count();
                        t1.Count = unresolvedcount;
                        t1.Percentage = TotalNumberOfTickets!=0 ? ((decimal)unresolvedcount / t1.TotalNumberOfTickets) * 100:0;
                        //t1.Percentage = ((decimal)3/4) * 100;
                        statusyearmonthCountList.Add(t1);

                        TicketStatusYearMonth t2 = new TicketStatusYearMonth();
                        ////resolved
                        t2.StatusId = 3;
                        t2.StatusName = "Resolved Tickets";
                        t2.TotalNumberOfTickets = TotalNumberOfTickets;
                        t2.Year = y;
                        t2.Month = m;
                        int resolvedcount = tsList.Where(x => x.Year == y && x.Month == m && x.StatusId == 3).Count();
                        t2.Count = resolvedcount;
                        t2.Percentage = TotalNumberOfTickets != 0 ? ((decimal)resolvedcount / t2.TotalNumberOfTickets) * 100:0;
                        statusyearmonthCountList.Add(t2);
                    }
                }

                ////var res = _mapper.Map<IEnumerable<TicketsModel>>(tickets);
                return Ok(statusyearmonthCountList.OrderBy(x=> x.Year).ThenBy(x=>x.Month));
                //return Ok(statusyearmonthCountList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Tickets>>TicketVolumeTrend action: {ex.Message}");
                _logger.LogError($"Something went wrong inside Tickets>>TicketVolumeTrend action: {ex.InnerException}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("ticketvolumetrend_2")]
        [CustomAuthorizeFilter(RequiredPrivileges = "ticketvolumetrend")]
        public IActionResult TicketVolumeTrend_2([FromBody] StartAndEndDates StartdateEnddate)
        {
            try
            {
                _logger.LogInformation($"Inside Tickets>>ticketvolumetrend_2");

                int[] intArray01 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

                TicketFilter ticketFilter = new TicketFilter()
                {
                    GroupId = intArray01.ToList(),
                    StatusId = intArray01.ToList(),
                    //PriorityId = intArray01.ToList(),
                    TypeId = intArray01.ToList(),
                    ProductId = intArray01.ToList(),
                    StartDate = StartdateEnddate.StartDate,
                    EndDate = StartdateEnddate.EndDate
                };

                int currentUserOrganization = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());
                //List<TicketStatus> ticketstatusList = new List<TicketStatus>();
                //ticketstatusList = _repository.ticketstatus.GetAllTicketStatus().ToList();

                string agentscope = HttpContext.User.Claims.Where(x => x.Type == "agentscope").Select(x => x.Value).FirstOrDefault();
                _logger.LogInformation($"agentscope: {agentscope}");
                //convert strin to Enum eg below:
                //EnumName VariableName= (EnumName)Enum.Parse(typeof(EnumName), StringValue);
                AgentScope agentscope2 = (AgentScope)Enum.Parse(typeof(AgentScope), agentscope);

                string groupsString = Convert.ToString(HttpContext.User.Claims.Where(x => x.Type == "Groups").Select(x => x.Value).FirstOrDefault());
                _logger.LogInformation($"agent groups: {groupsString}");
                var groups = groupsString.Split(',').ToList();
                var CurrentUsergroupIDs = groups.Select(x => Int32.Parse(x)).ToList();

                //NOTE:
                //CurrentUsergroupIDs: are groups the current user belong to.
                //groupid: is the group you are searching for.

                _logger.LogInformation($"About to call TicketsFilter");
                ////var filteredTickets = _repository.tickets.TicketsFilter(currentUserOrganization, agentscope2, groups2, ticketFilter.GroupId, ticketFilter.ProductId, ticketFilter.StatusId, ticketFilter.PriorityId, ticketFilter.StartDate, ticketFilter.EndDate);
                var filteredTickets = _repository.tickets.TicketsFilter(currentUserOrganization, agentscope2, CurrentUsergroupIDs, ticketFilter.GroupId, ticketFilter.ProductId, ticketFilter.StatusId, ticketFilter.TypeId, ticketFilter.StartDate, ticketFilter.EndDate);

                _logger.LogInformation($"Returned filtered tickets from database.");

                foreach (var mc in filteredTickets.Where(x => x.StatusId != 3)) mc.StatusId = 0;

                List<TicketStatusYearMonth_B> tsList = filteredTickets.Select(x => new TicketStatusYearMonth_B { Ticket="", StatusId = x.StatusId, NumberOfTickets=0, Month = x.CreatedOn.Month, Year = x.CreatedOn.Year }).ToList();

                List<int> formedStatusIDList = new List<int>() { 0, 3 };  //0=unresolved, 3=resolved
                List<int> yearList = tsList.Select(x => x.Year).Distinct().ToList();
                List<int> monthList = tsList.Select(x => x.Month).Distinct().ToList();
                List<TicketStatusYearMonth_B> statusyearmonthCountList = new List<TicketStatusYearMonth_B>();


                foreach (var y in yearList)
                {
                    foreach (var m in monthList)
                    {
                        //foreach (var s in formedStatusIDList)
                        //{
                        //if (s == 0)
                        //{
                        //    TicketStatusYearMonth t = new TicketStatusYearMonth();
                        //    //unresolved
                        //    t.StatusId = 0;
                        //    t.Year = y;
                        //    t.Month = m;
                        //    int unresolvedcount = tsList.Where(x => x.Year == y && x.Month == m && x.StatusId == 0).Count();
                        //    t.Count = unresolvedcount;
                        //    statusyearmonthCountList.Add(t);
                        //}
                        //if (s == 3)
                        //{
                        //    TicketStatusYearMonth t = new TicketStatusYearMonth();
                        //    //resolved
                        //    t.StatusId = 3;
                        //    t.Year = y;
                        //    t.Month = m;
                        //    int resolvedcount = tsList.Where(x => x.Year == y && x.Month == m && x.StatusId == 3).Count();
                        //    t.Count = resolvedcount;
                        //    statusyearmonthCountList.Add(t);
                        //}
                        //}

                        int TotalNumberOfTicket = tsList.Where(x => x.Year == y && x.Month == m).Count();

                        TicketStatusYearMonth_B t1 = new TicketStatusYearMonth_B();
                        ////unresolved
                        t1.StatusId = 0;
                        t1.Ticket = "Unresolved Tickets";
                        t1.Year = y;
                        t1.Month = m;
                        t1.NumberOfTickets = tsList.Where(x => x.Year == y && x.Month == m && x.StatusId == 0).Count();
                        t1.Percentage = TotalNumberOfTicket!=0 ? ((decimal)t1.NumberOfTickets / TotalNumberOfTicket) * 100:0;
                        statusyearmonthCountList.Add(t1);

                        TicketStatusYearMonth_B t2 = new TicketStatusYearMonth_B();
                        ////resolved
                        t2.StatusId = 3;
                        t2.Ticket = "Resolved Tickets";
                        t2.Year = y;
                        t2.Month = m;
                        t2.NumberOfTickets = tsList.Where(x => x.Year == y && x.Month == m && x.StatusId == 3).Count();
                        t2.Percentage = TotalNumberOfTicket != 0 ? ((decimal)t2.NumberOfTickets / TotalNumberOfTicket) * 100:0;
                        statusyearmonthCountList.Add(t2);

                        //overall ticket for the range
                        TicketStatusYearMonth_B t3 = new TicketStatusYearMonth_B();
                        t3.StatusId = 100;
                        t3.Ticket = "Received Tickets";
                        t3.Year = y;
                        t3.Month = m;
                        t3.NumberOfTickets = TotalNumberOfTicket;
                        t3.Percentage = TotalNumberOfTicket != 0 ? ((decimal)t3.NumberOfTickets / TotalNumberOfTicket) * 100:0;
                        statusyearmonthCountList.Add(t3);
                    }
                }
                //var res = _mapper.Map<IEnumerable<TicketsModel>>(tickets);
                return Ok(statusyearmonthCountList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Tickets>>ticketvolumetrend_2 action: {ex.Message}");
                _logger.LogError($"Something went wrong inside Tickets>>ticketvolumetrend_2 action: {ex.InnerException}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("ticketvolumetrend_dayofweek")]
        [CustomAuthorizeFilter(RequiredPrivileges = "ticketvolumetrend")]
        public IActionResult TicketVolumeTrend_Dayofweek([FromBody] StartAndEndDates StartdateEnddate)
        {
            try
            {
                _logger.LogInformation($"Inside Tickets>>ticketvolumetrend_dayofweek");

                int[] intArray01 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

                TicketFilter ticketFilter = new TicketFilter()
                {
                    GroupId = intArray01.ToList(),
                    StatusId = intArray01.ToList(),
                    //PriorityId = intArray01.ToList(),
                    TypeId = intArray01.ToList(),
                    ProductId = intArray01.ToList(),
                    StartDate = StartdateEnddate.StartDate,
                    EndDate = StartdateEnddate.EndDate
                };

                int currentUserOrganization = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());
                //List<TicketStatus> ticketstatusList = new List<TicketStatus>();
                //ticketstatusList = _repository.ticketstatus.GetAllTicketStatus().ToList();

                string agentscope = HttpContext.User.Claims.Where(x => x.Type == "agentscope").Select(x => x.Value).FirstOrDefault();
                _logger.LogInformation($"agentscope: {agentscope}");
                //convert strin to Enum eg below:
                //EnumName VariableName= (EnumName)Enum.Parse(typeof(EnumName), StringValue);
                AgentScope agentscope2 = (AgentScope)Enum.Parse(typeof(AgentScope), agentscope);

                string groupsString = Convert.ToString(HttpContext.User.Claims.Where(x => x.Type == "Groups").Select(x => x.Value).FirstOrDefault());
                _logger.LogInformation($"agent groups: {groupsString}");
                var groups = groupsString.Split(',').ToList();
                var CurrentUsergroupIDs = groups.Select(x => Int32.Parse(x)).ToList();

                //NOTE:
                //CurrentUsergroupIDs: are groups the current user belong to.
                //groupid: is the group you are searching for.

                _logger.LogInformation($"About to call TicketsFilter");
                //var filteredTickets = _repository.tickets.TicketsFilter(currentUserOrganization, agentscope2, groups2, ticketFilter.GroupId, ticketFilter.ProductId, ticketFilter.StatusId, ticketFilter.PriorityId, ticketFilter.StartDate, ticketFilter.EndDate);
                var filteredTickets = _repository.tickets.TicketsFilter(currentUserOrganization, agentscope2, CurrentUsergroupIDs, ticketFilter.GroupId, ticketFilter.ProductId, ticketFilter.StatusId, ticketFilter.TypeId, ticketFilter.StartDate, ticketFilter.EndDate);

                _logger.LogInformation($"Returned filtered tickets from database.");

                foreach (var mc in filteredTickets.Where(x => x.StatusId != 3)) mc.StatusId = 0;

                List<TicketStatusYearMonth_DayOfWeek> tsList = filteredTickets.Select(x => new TicketStatusYearMonth_DayOfWeek { Ticket = "", StatusId = x.StatusId, NumberOfTickets = 0, Month = x.CreatedOn.Month, Year = x.CreatedOn.Year, Dayofweek = x.CreatedOn.DayOfWeek }).ToList();

                List<int> formedStatusIDList = new List<int>() { 0, 3 };  //0=unresolved, 3=resolved
                List<string> dayList = new List<string>() { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
                List<int> yearList = tsList.Select(x => x.Year).Distinct().ToList();
                List<int> monthList = tsList.Select(x => x.Month).Distinct().ToList();
                List<TicketStatusYearMonth_DayOfWeek> statusyearmonthCountList = new List<TicketStatusYearMonth_DayOfWeek>();


                foreach (var y in yearList)
                {
                    foreach (var m in monthList)
                    {                      
                        foreach(var day in dayList)
                        {
                            TicketStatusYearMonth_DayOfWeek t1 = new TicketStatusYearMonth_DayOfWeek();

                            //total number of tickets for the day
                            int TotalNumberOfTicket = tsList.Where(x => x.Year == y && x.Month == m && x.Dayofweek.ToString() == day).Count();

                            ////unresolved
                            t1.StatusId = 0;
                            t1.Ticket = "Unresolved Tickets";
                            t1.Year = y;
                            t1.Month = m;
                            //t1.Dayofweek = lll;
                            t1.DayofweekString = day;
                            t1.NumberOfTickets = tsList.Where(x => x.Year == y && x.Month == m && x.StatusId == 0 && x.Dayofweek.ToString() == day).Count();
                            t1.Percentage = TotalNumberOfTicket!=0 ? ((decimal)t1.NumberOfTickets / TotalNumberOfTicket) * 100 : 0;
                            statusyearmonthCountList.Add(t1);
                        }

                        foreach (var day in dayList)
                        {
                            TicketStatusYearMonth_DayOfWeek t2 = new TicketStatusYearMonth_DayOfWeek();

                            //total number of tickets for the day
                            int TotalNumberOfTicket = tsList.Where(x => x.Year == y && x.Month == m && x.Dayofweek.ToString() == day).Count();

                            ////resolved
                            t2.StatusId = 3;
                            t2.Ticket = "Resolved Tickets";
                            t2.Year = y;
                            t2.Month = m;
                            //t2.Dayofweek = lll;
                            t2.DayofweekString = day;
                            t2.NumberOfTickets = tsList.Where(x => x.Year == y && x.Month == m && x.StatusId == 3 && x.Dayofweek.ToString() == day).Count();
                            t2.Percentage = TotalNumberOfTicket!= 0 ? ((decimal)t2.NumberOfTickets / TotalNumberOfTicket) * 100 : 0;
                            statusyearmonthCountList.Add(t2);
                        }

                        foreach (var day in dayList)
                        {
                            //overall ticket for the range
                            TicketStatusYearMonth_DayOfWeek t3 = new TicketStatusYearMonth_DayOfWeek();

                            //total number of tickets for the day
                            int TotalNumberOfTicket = tsList.Where(x => x.Year == y && x.Month == m && x.Dayofweek.ToString() == day).Count();

                            t3.StatusId = 100;
                            t3.Ticket = "Received Tickets";
                            t3.Year = y;
                            t3.Month = m;
                            //t3.Dayofweek = lll;
                            t3.DayofweekString = day;
                            t3.NumberOfTickets = TotalNumberOfTicket;
                            t3.Percentage = TotalNumberOfTicket!=0 ? ((decimal)t3.NumberOfTickets / TotalNumberOfTicket) * 100:0;
                            statusyearmonthCountList.Add(t3);
                        }
                    }
                }
                //var res = _mapper.Map<IEnumerable<TicketsModel>>(tickets);
                return Ok(statusyearmonthCountList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Tickets>>ticketvolumetrend_dayofweek action: {ex.Message}");
                _logger.LogError($"Something went wrong inside Tickets>>ticketvolumetrend_dayofweek action: {ex.InnerException}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("ticketvolumetrend_param")]
        [CustomAuthorizeFilter(RequiredPrivileges = "ticketvolumetrend")]
        public IActionResult TicketVolumeTrend_Param([FromBody] IntParamAndStartAndEndDates intParamStartdateEnddate)
        {
            try
            {
                _logger.LogInformation($"Inside Tickets>>TicketVolumeTrend_Param");

                //NOTE NOTE NOTE
                //unresolved=0, resolved=3, received=100

                int[] intArray01 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

                TicketFilter ticketFilter = new TicketFilter()
                {
                    GroupId = intArray01.ToList(),
                    StatusId = intArray01.ToList(),
                    //PriorityId = intArray01.ToList(),
                    TypeId = intArray01.ToList(),
                    ProductId = intArray01.ToList(),
                    StartDate = intParamStartdateEnddate.StartDate,
                    EndDate = intParamStartdateEnddate.EndDate
                };

                int currentUserOrganization = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());
                //List<TicketStatus> ticketstatusList = new List<TicketStatus>();
                //ticketstatusList = _repository.ticketstatus.GetAllTicketStatus().ToList();

                string agentscope = HttpContext.User.Claims.Where(x => x.Type == "agentscope").Select(x => x.Value).FirstOrDefault();
                _logger.LogInformation($"agentscope: {agentscope}");
                //convert strin to Enum eg below:
                //EnumName VariableName= (EnumName)Enum.Parse(typeof(EnumName), StringValue);
                AgentScope agentscope2 = (AgentScope)Enum.Parse(typeof(AgentScope), agentscope);

                string groupsString = Convert.ToString(HttpContext.User.Claims.Where(x => x.Type == "Groups").Select(x => x.Value).FirstOrDefault());
                _logger.LogInformation($"agent groups: {groupsString}");
                var groups = groupsString.Split(',').ToList();
                var CurrentUsergroupIDs = groups.Select(x => Int32.Parse(x)).ToList();

                //NOTE:
                //CurrentUsergroupIDs: are groups the current user belong to.
                //groupid: is the group you are searching for.

                _logger.LogInformation($"About to call TicketsFilter");
                ////var filteredTickets = _repository.tickets.TicketsFilter(currentUserOrganization, agentscope2, groups2, ticketFilter.GroupId, ticketFilter.ProductId, ticketFilter.StatusId, ticketFilter.PriorityId, ticketFilter.StartDate, ticketFilter.EndDate);
                //var filteredTickets = _repository.tickets.TicketsFilter(currentUserOrganization, agentscope2, CurrentUsergroupIDs, ticketFilter.GroupId, ticketFilter.ProductId, ticketFilter.StatusId, ticketFilter.PriorityId, ticketFilter.StartDate, ticketFilter.EndDate);
                var filteredTickets = _repository.tickets.TicketsFilter(currentUserOrganization, agentscope2, CurrentUsergroupIDs, ticketFilter.GroupId, ticketFilter.ProductId, ticketFilter.StatusId, ticketFilter.TypeId, ticketFilter.StartDate, ticketFilter.EndDate);

                _logger.LogInformation($"Returned filtered tickets from database.");

                foreach (var mc in filteredTickets.Where(x => x.StatusId != 3)) mc.StatusId = 0;

                List<TicketStatusYearMonth_Day> tsList = filteredTickets.Select(x => new TicketStatusYearMonth_Day { Ticket = "", StatusId = x.StatusId, NumberOfTickets = 0, Month = x.CreatedOn.Month, Year = x.CreatedOn.Year, Day=x.CreatedOn.Day }).ToList();

                List<int> formedStatusIDList = new List<int>() { 0, 3 };  //0=unresolved, 3=resolved
                List<int> yearList = tsList.Select(x => x.Year).Distinct().ToList();
                List<int> monthList = tsList.Select(x => x.Month).Distinct().ToList();
                List<int> dayList = tsList.Select(x => x.Day).Distinct().ToList();
                List<TicketStatusYearMonth_Day> statusyearmonthCountList = new List<TicketStatusYearMonth_Day>();


                foreach (var y in yearList)
                {
                    foreach (var m in monthList)
                    {
                        foreach(var d in dayList)
                        {
                            //total number of tickets for the day
                            int TotalNumberOfTicket = tsList.Where(x => x.Year == y && x.Month == m && x.Day == d ).Count();

                            TicketStatusYearMonth_Day t1 = new TicketStatusYearMonth_Day();
                            ////unresolved          //NOTE NOTE NOTE: Possible values for  intParamStartdateEnddate.IntParam are 0, 3, 100  where 0=unresolved, 3=resolved and 100 is both resolved & unresolved (ie all the received tickets for the day)
                            t1.StatusId = intParamStartdateEnddate.IntParam == 0 ? 0 : intParamStartdateEnddate.IntParam == 3 ? 3 : 100;
                            t1.Ticket = intParamStartdateEnddate.IntParam==0 ? "Unresolved Tickets" : intParamStartdateEnddate.IntParam == 3 ? "Resolved Tickets" : "Received Tivkets";
                            t1.Day = d;
                            t1.Year = y;
                            t1.Month = m;
                            t1.NumberOfTickets = tsList.Where(x => x.Year == y && x.Month == m && x.StatusId == intParamStartdateEnddate.IntParam).Count();
                            t1.Percentage = TotalNumberOfTicket != 0 ? ((decimal)t1.NumberOfTickets / TotalNumberOfTicket) * 100 : 0;
                            statusyearmonthCountList.Add(t1);
                        }
                    }
                }
                //var res = _mapper.Map<IEnumerable<TicketsModel>>(tickets);
                return Ok(statusyearmonthCountList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Tickets>>TicketVolumeTrend_Param action: {ex.Message}");
                _logger.LogError($"Something went wrong inside Tickets>>TicketVolumeTrend_Param action: {ex.InnerException}");
                return StatusCode(500, "Internal server error");
            }
        }

        //SLA Resolution Time Ticket Report: Ticket report on Expected Resolve Date, SLA Duration, Work Period/hours, Resolution Start Date, etc:
        [HttpPost("ticketfilter_slaresolutiontime")]
        [CustomAuthorizeFilter(RequiredPrivileges = "ticketfilter")]
        public IActionResult TicketFilter_SLAResolutionTime([FromBody] TicketFilter ticketFilter)
        {
            try
            {
                _logger.LogInformation($"Inside Tickets>>TicketFilter_SLAResolutionTime");

                int currentUserOrganization = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());
                List<TicketStatus> ticketstatusList = new List<TicketStatus>();
                ticketstatusList = _repository.ticketstatus.GetAllTicketStatus().ToList();

                string agentscope = HttpContext.User.Claims.Where(x => x.Type == "agentscope").Select(x => x.Value).FirstOrDefault();
                _logger.LogInformation($"agentscope: {agentscope}");
                //convert strin to Enum eg below:
                //EnumName VariableName= (EnumName)Enum.Parse(typeof(EnumName), StringValue);
                AgentScope agentscope2 = (AgentScope)Enum.Parse(typeof(AgentScope), agentscope);

                string groupsString = Convert.ToString(HttpContext.User.Claims.Where(x => x.Type == "Groups").Select(x => x.Value).FirstOrDefault());
                _logger.LogInformation($"agent groups: {groupsString}");
                var groups = groupsString.Split(',').ToList();
                var CurrentUsergroupIDs = groups.Select(x => Int32.Parse(x)).ToList();

                //NOTE:
                //CurrentUsergroupIDs: are groups the current user belong to.
                //groupid: is the group you are searching for.

                _logger.LogInformation($"About to call TicketsFilter");
                //var filteredTickets = _repository.tickets.TicketsFilter(currentUserOrganization, agentscope2, groups2, ticketFilter.GroupId, ticketFilter.ProductId, ticketFilter.StatusId, ticketFilter.PriorityId, ticketFilter.StartDate, ticketFilter.EndDate);
                var filteredTickets = _repository.tickets.TicketsFilter(currentUserOrganization, agentscope2, CurrentUsergroupIDs, ticketFilter.GroupId, ticketFilter.ProductId, ticketFilter.StatusId, ticketFilter.TypeId, ticketFilter.StartDate, ticketFilter.EndDate);
                if (filteredTickets.Count() == 0)
                    return NoContent();

                _logger.LogInformation($"Returned filtered tickets from database.");

                //I need to get only one record for the business work period mapped to this organization
                var businessperiod = _repository.businesshours.GetAllBusinessHours(currentUserOrganization).FirstOrDefault();
                if (businessperiod == null)
                    return Ok("Business hours have not been setup");

                //Here, i need to get SLA issue resolution duration as setup in the SLAPolicyPriority table for the org
                var sla_policypriority_durationList = _repository.slapolicypriority.GetAllSLAPolicyPriorities(currentUserOrganization);
                if (sla_policypriority_durationList.Count() == 0)
                    return Ok ("No priority has been mapped to a Policy");

                var tickets_sla = filteredTickets.Select(x => new Tickets_SLA { 

                    TicketId = x.TicketId,
                    SysGeneratedTicketId =x.SysGeneratedTicketId,
                    Contacts=x.Contacts,
                    Subject=x.Subject,
                    Description=x.Description,
                    Tag=x.Tag,
                    ResolutionStartDate=x.ResolutionStartDate,
                    ResolutionEndDate=x.ResolutionEndDate,
                    ClosedDate=x.ClosedDate,

                    TypeId=x.TypeId,
                    StatusId=x.StatusId,
                    SLAPriorityId=x.SLAPriorityId,
                    GroupId=x.GroupId,
                    ModuleId=x.ModuleId,
                    ProductId=x.ProductId,
                    AgentId=x.AgentId,
                    SupervisorId=x.SupervisorId,
                    OrganizationId=x.OrganizationId,

                    SLAPolicyPriorityDuration =  sla_policypriority_durationList.FirstOrDefault(y=>y.SLAPriorityId==y.SLAPriorityId).ResolutionDuration,
                    WorkStartHour= businessperiod.StartHour,
                    WorkEndHour= businessperiod.EndHour,
                    SLAResolveDateTime = SLAExpectedResolutionTimeline(x.ResolutionStartDate, sla_policypriority_durationList.FirstOrDefault(y => y.SLAPriorityId == y.SLAPriorityId).ResolutionDuration, businessperiod.StartHour, businessperiod.EndHour),          //SLA Expected Timeline
                    
                });

                //tickets_sla = tickets_sla.Select(x => {
                //    x.SLAPosition = x.StatusId == 4 && x.ClosedDate <= x.SLAResolveDateTime ? SLAPosition.ClosedWithinSLA
                //                    : x.StatusId == 4 && x.ClosedDate > x.SLAResolveDateTime ? SLAPosition.ClosedOutsideSLA
                //                    : x.StatusId == 3 && x.ResolutionEndDate <= x.SLAResolveDateTime ? SLAPosition.ResolvedWithinSLA
                //                    : x.StatusId == 3 && x.ResolutionEndDate > x.SLAResolveDateTime ? SLAPosition.ResolvedOutsideSLA
                //                    : SLAPosition.NotResolved;
                //    return x;
                //});

                tickets_sla = tickets_sla.Select(x =>
                {
                    x.SLAPosition = x.StatusId == 4 && x.ClosedDate <= x.SLAResolveDateTime ? SLAPosition.ClosedWithinSLA.ToString()
                                    : x.StatusId == 4 && x.ClosedDate > x.SLAResolveDateTime ? SLAPosition.ClosedOutsideSLA.ToString()
                                    : x.StatusId == 3 && x.ResolutionEndDate <= x.SLAResolveDateTime ? SLAPosition.ResolvedWithinSLA.ToString()
                                    : x.StatusId == 3 && x.ResolutionEndDate > x.SLAResolveDateTime ? SLAPosition.ResolvedOutsideSLA.ToString()
                                    : SLAPosition.NotResolved.ToString();
                    return x;
                });

                //var res = _mapper.Map<IEnumerable<TicketsModel>>(tickets);
                return Ok(tickets_sla);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Tickets>>TicketFilter_SLAResolutionTime action: {ex.Message}");
                _logger.LogError($"Something went wrong inside Tickets>>TicketFilter_SLAResolutionTime action: {ex.InnerException}");
                return StatusCode(500, "Internal server error");
            }
        }

        public static DateTime SLAExpectedResolutionTimeline(DateTime resolutionStartDate, int slaPolicyPriorityDuration, TimeSpan workStartHour, TimeSpan workEndHour)
        {
            //NOTE: resolutionStartDate is the time the ticket was assigned to the resource/agent. So, resolution start from that moment (it could be 8am or 9am or 10am . . . 5pm)
            DateTime slaExpectedResolveDateTime = DateTime.Now;
            double officialWorkDurationFortheDay = Convert.ToDouble(workEndHour.Subtract(workStartHour).TotalHours);

            int dayC = 0;  //day counter

            if(resolutionStartDate.TimeOfDay > workEndHour)
            {
                resolutionStartDate = DateTime.Parse(resolutionStartDate.ToString("MM/dd/yyyy 17:00:00"));
                //var resolutionStartDatex = new DateTime(resolutionStartDate.Year, resolutionStartDate.Month, resolutionStartDate.Day, 17, 0, 0);
                ////DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 4, 5, 6);
            }
            else if (resolutionStartDate.TimeOfDay < workStartHour)
            {
                resolutionStartDate = DateTime.Parse(resolutionStartDate.ToString("MM/dd/yyyy 08:00:00"));
            }

            //var rs = resolutionStartDate;
            var rsd = resolutionStartDate.Date;
            var rst = resolutionStartDate.TimeOfDay;
            ///TimeSpan slaPolicyPriorityDuration_2 = new TimeSpan(slaPolicyPriorityDuration/9, slaPolicyPriorityDuration % 9, 0, 0);

            //https://www.c-sharpcorner.com/blogs/date-and-time-format-in-c-sharp-programming1
            //DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss")
            //DateTime.Now.ToString("MM/dd/yyyy HH:mm")
            double slaPolicyPriorityDuration_2 = double.Parse(slaPolicyPriorityDuration.ToString());

            while (dayC >= 0)
            {
                var worktimedifference = workEndHour - rst;
                //convert timespan to double
                var worktimedifference_2 = Convert.ToDouble(worktimedifference.TotalHours);
                //var worktimedifference_2= Convert.ToDouble(worktimedifference.TotalHours).ToString("#.00");

                // if (slaPolicyPriorityDuration_2 > worktimedifference_2 && slaPolicyPriorityDuration_2 > officialWorkDurationFortheDay)  //means you have more SLA duration time to resolve the issue
                if (slaPolicyPriorityDuration_2 > worktimedifference_2 )  //means you have more SLA duration time to resolve the issue
                {
                    //rsd = rsd.AddDays(1);   //add 1 day to it
                    rsd = rsd.AddDays(dayC);   //add 1 day to it
                    rst = workStartHour;        //next rst will now be the hour business/worh starts (resumption time). so, we continue counting time spent from the time staff resumes on the following day eg 8am
                    resolutionStartDate = rsd + rst;
                    slaPolicyPriorityDuration_2 = slaPolicyPriorityDuration_2 - worktimedifference_2;
                    //dayC++;
                    dayC = 1;
                }
                else
                {
                    if (dayC > 0)
                    {
                        rsd = rsd.AddDays(1);
                        rst = workStartHour;
                        resolutionStartDate = rsd + rst;
                    }
                    ////TimeSpan interval = TimeSpan.FromSeconds(seconds);
                    //convert double to timespan
                    TimeSpan slaPolicyPriorityDuration_3 = TimeSpan.FromHours(slaPolicyPriorityDuration_2);
                    slaExpectedResolveDateTime = resolutionStartDate + slaPolicyPriorityDuration_3;
                    dayC = -1;
                }
            }

            return slaExpectedResolveDateTime;
        }

        //To be loaded on ticket page load.
        //SLA Resolution Time Ticket Report: Ticket report on Expected Resolve Date, SLA Duration, Work Period/hours, Resolution Start Date, etc:
        [HttpGet("ticketfilter_slaresolutiontime_slaposition_oninit")]
        [CustomAuthorizeFilter(RequiredPrivileges = "ticketfilter")]
        //public IActionResult TicketFilter_SLAResolutionTime_SLAPosition_OnInit([FromBody] TicketFilter_SLAPosition ticketFilter)
        public IActionResult TicketFilter_SLAResolutionTime_SLAPosition_OnInit()
        {
            try
            {
                _logger.LogInformation($"Inside Tickets>>TicketFilter_SLAResolutionTime");

                int currentUserOrganization = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());

                //int[] Arr01 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 } };
                //int[] intArray01 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
                List<int> intList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

                TicketFilter ticketFilter = new TicketFilter()
                {
                    GroupId = intList,
                    ProductId = intList,
                    StatusId = intList,
                    TypeId = intList,
                    StartDate = DateTime.Now.Date,
                    EndDate = DateTime.Now.Date.AddDays(1)
                };


                List<TicketStatus> ticketstatusList = new List<TicketStatus>();
                ticketstatusList = _repository.ticketstatus.GetAllTicketStatus().ToList();

                string agentscope = HttpContext.User.Claims.Where(x => x.Type == "agentscope").Select(x => x.Value).FirstOrDefault();
                _logger.LogInformation($"agentscope: {agentscope}");
                //convert strin to Enum eg below:
                //EnumName VariableName= (EnumName)Enum.Parse(typeof(EnumName), StringValue);
                AgentScope agentscope2 = (AgentScope)Enum.Parse(typeof(AgentScope), agentscope);

                string groupsString = Convert.ToString(HttpContext.User.Claims.Where(x => x.Type == "Groups").Select(x => x.Value).FirstOrDefault());
                _logger.LogInformation($"agent groups: {groupsString}");
                var groups = groupsString.Split(',').ToList();
                var CurrentUsergroupIDs = groups.Select(x => Int32.Parse(x)).ToList();

                //NOTE:
                //CurrentUsergroupIDs: are groups the current user belong to.
                //groupid: is the group you are searching for.

                _logger.LogInformation($"About to call TicketsFilter");
                //var filteredTickets = _repository.tickets.TicketsFilter(currentUserOrganization, agentscope2, groups2, ticketFilter.GroupId, ticketFilter.ProductId, ticketFilter.StatusId, ticketFilter.PriorityId, ticketFilter.StartDate, ticketFilter.EndDate);
                var filteredTickets = _repository.tickets.TicketsFilter(currentUserOrganization, agentscope2, CurrentUsergroupIDs, ticketFilter.GroupId, ticketFilter.ProductId, ticketFilter.StatusId, ticketFilter.TypeId, ticketFilter.StartDate, ticketFilter.EndDate);
                if (filteredTickets.Count() == 0)
                    return NoContent();

                _logger.LogInformation($"Returned filtered tickets from database.");

                //I need to get only one record for the business work period mapped to this organization
                var businessperiod = _repository.businesshours.GetAllBusinessHours(currentUserOrganization).FirstOrDefault();
                if (businessperiod == null)
                    return Ok("Business hours have not been setup");

                //Here, i need to get SLA issue resolution duration as setup in the SLAPolicyPriority table for the org
                var sla_policypriority_durationList = _repository.slapolicypriority.GetAllSLAPolicyPriorities(currentUserOrganization);
                if (sla_policypriority_durationList.Count() == 0)
                    return Ok("No priority has been mapped to a Policy");

                var tickets_sla = filteredTickets.Select(x => new Tickets_SLA
                {

                    TicketId = x.TicketId,
                    SysGeneratedTicketId = x.SysGeneratedTicketId,
                    Contacts = x.Contacts,
                    Subject = x.Subject,
                    Description = x.Description,
                    Tag = x.Tag,
                    ResolutionStartDate = x.ResolutionStartDate,
                    ResolutionEndDate = x.ResolutionEndDate,
                    ClosedDate = x.ClosedDate,

                    TypeId = x.TypeId,
                    Type = x.Type,
                    StatusId = x.StatusId,
                    Status = x.Status,
                    SLAPriorityId = x.SLAPriorityId,
                    SLAPriority = x.SLAPriority,
                    GroupId = x.GroupId,
                    Group = x.Group,
                    ModuleId = x.ModuleId,
                    Module = x.Module,
                    ProductId = x.ProductId,
                    Product = x.Product,
                    AgentId = x.AgentId,
                    Agent = x.Agent,
                    SupervisorId = x.SupervisorId,
                    Supervisor = x.Supervisor,
                    OrganizationId = x.OrganizationId,
                    Organization = x.Organization,

                    SLAPolicyPriorityDuration = sla_policypriority_durationList.FirstOrDefault(y => y.SLAPriorityId == y.SLAPriorityId).ResolutionDuration,
                    WorkStartHour = businessperiod.StartHour,
                    WorkEndHour = businessperiod.EndHour,
                    SLAResolveDateTime = SLAExpectedResolutionTimeline(x.ResolutionStartDate, sla_policypriority_durationList.FirstOrDefault(y => y.SLAPriorityId == y.SLAPriorityId).ResolutionDuration, businessperiod.StartHour, businessperiod.EndHour),          //SLA Expected Timeline
                    //SLAPosition = (SLAPosition)5,
                     SLAPosition = SLAPosition.NotResolved.ToString(),
                });


                //tickets_sla = tickets_sla.Select(x =>
                //{
                //    x.SLAPosition = x.StatusId == 4 && x.ClosedDate <= x.SLAResolveDateTime ? SLAPosition.ClosedWithinSLA
                //                    : x.StatusId == 4 && x.ClosedDate > x.SLAResolveDateTime ? SLAPosition.ClosedOutsideSLA
                //                    : x.StatusId == 3 && x.ResolutionEndDate <= x.SLAResolveDateTime ? SLAPosition.ResolvedWithinSLA
                //                    : x.StatusId == 3 && x.ResolutionEndDate > x.SLAResolveDateTime ? SLAPosition.ResolvedOutsideSLA
                //                    : SLAPosition.NotResolved;
                //    return x;
                //});

                tickets_sla = tickets_sla.Select(x =>
                {
                    x.SLAPosition = x.StatusId == 4 && x.ClosedDate <= x.SLAResolveDateTime ? SLAPosition.ClosedWithinSLA.ToString()
                                    : x.StatusId == 4 && x.ClosedDate > x.SLAResolveDateTime ? SLAPosition.ClosedOutsideSLA.ToString()
                                    : x.StatusId == 3 && x.ResolutionEndDate <= x.SLAResolveDateTime ? SLAPosition.ResolvedWithinSLA.ToString()
                                    : x.StatusId == 3 && x.ResolutionEndDate > x.SLAResolveDateTime ? SLAPosition.ResolvedOutsideSLA.ToString()
                                    : SLAPosition.NotResolved.ToString();
                    return x;
                });

                //tickets_sla = tickets_sla.Where(x => x.SLAPosition == (SLAPosition)ticketFilter.SLAPosition);

                //var res = _mapper.Map<IEnumerable<TicketsModel>>(tickets);
                var res = _mapper.Map<IEnumerable<TicketsModel>>(tickets_sla);
                //return Ok(tickets_sla);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Tickets>>TicketFilter_SLAResolutionTime action: {ex.Message}");
                _logger.LogError($"Something went wrong inside Tickets>>TicketFilter_SLAResolutionTime action: {ex.InnerException}");
                return StatusCode(500, "Internal server error");
            }
        }

        //SLA Resolution Time Ticket Report: Ticket report on Expected Resolve Date, SLA Duration, Work Period/hours, Resolution Start Date, etc:
        [HttpPost("ticketfilter_slaresolutiontime_slaposition")]
        [CustomAuthorizeFilter(RequiredPrivileges = "ticketfilter")]
        public IActionResult TicketFilter_SLAResolutionTime_SLAPosition([FromBody] TicketFilter_SLAPosition ticketFilter)
        {
            try
            {
                _logger.LogInformation($"Inside Tickets>>TicketFilter_SLAResolutionTime");

                int currentUserOrganization = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());
                List<TicketStatus> ticketstatusList = new List<TicketStatus>();
                ticketstatusList = _repository.ticketstatus.GetAllTicketStatus().ToList();

                string agentscope = HttpContext.User.Claims.Where(x => x.Type == "agentscope").Select(x => x.Value).FirstOrDefault();
                _logger.LogInformation($"agentscope: {agentscope}");
                //convert strin to Enum eg below:
                //EnumName VariableName= (EnumName)Enum.Parse(typeof(EnumName), StringValue);
                AgentScope agentscope2 = (AgentScope)Enum.Parse(typeof(AgentScope), agentscope);

                string groupsString = Convert.ToString(HttpContext.User.Claims.Where(x => x.Type == "Groups").Select(x => x.Value).FirstOrDefault());
                _logger.LogInformation($"agent groups: {groupsString}");
                var groups = groupsString.Split(',').ToList();
                var CurrentUsergroupIDs = groups.Select(x => Int32.Parse(x)).ToList();

                //NOTE:
                //CurrentUsergroupIDs: are groups the current user belong to.
                //groupid: is the group you are searching for.

                _logger.LogInformation($"About to call TicketsFilter");
                //var filteredTickets = _repository.tickets.TicketsFilter(currentUserOrganization, agentscope2, groups2, ticketFilter.GroupId, ticketFilter.ProductId, ticketFilter.StatusId, ticketFilter.PriorityId, ticketFilter.StartDate, ticketFilter.EndDate);
                var filteredTickets = _repository.tickets.TicketsFilter(currentUserOrganization, agentscope2, CurrentUsergroupIDs, ticketFilter.GroupId, ticketFilter.ProductId, ticketFilter.StatusId, ticketFilter.TypeId, ticketFilter.StartDate, ticketFilter.EndDate);
                if (filteredTickets.Count() == 0)
                    return NoContent();

                _logger.LogInformation($"Returned filtered tickets from database.");

                //I need to get only one record for the business work period mapped to this organization
                var businessperiod = _repository.businesshours.GetAllBusinessHours(currentUserOrganization).FirstOrDefault();
                if (businessperiod == null)
                    return Ok("Business hours have not been setup");

                //Here, i need to get SLA issue resolution duration as setup in the SLAPolicyPriority table for the org
                var sla_policypriority_durationList = _repository.slapolicypriority.GetAllSLAPolicyPriorities(currentUserOrganization);
                if (sla_policypriority_durationList.Count() == 0)
                    return Ok("No priority has been mapped to a Policy");

                var tickets_sla = filteredTickets.Select(x => new Tickets_SLA
                {

                    TicketId = x.TicketId,
                    SysGeneratedTicketId = x.SysGeneratedTicketId,
                    Contacts = x.Contacts,
                    Subject = x.Subject,
                    Description = x.Description,
                    Tag = x.Tag,
                    ResolutionStartDate = x.ResolutionStartDate,
                    ResolutionEndDate = x.ResolutionEndDate,
                    ClosedDate = x.ClosedDate,

                    TypeId = x.TypeId,
                    Type = x.Type,
                    StatusId = x.StatusId,
                    Status = x.Status,
                    SLAPriorityId = x.SLAPriorityId,
                    SLAPriority = x.SLAPriority,
                    GroupId = x.GroupId,
                    Group = x.Group,
                    ModuleId = x.ModuleId,
                    Module = x.Module,
                    ProductId = x.ProductId,
                    Product = x.Product,
                    AgentId = x.AgentId,
                    Agent = x.Agent,
                    SupervisorId = x.SupervisorId,
                    Supervisor = x.Supervisor,
                    OrganizationId = x.OrganizationId,
                    Organization = x.Organization,


                    SLAPolicyPriorityDuration = sla_policypriority_durationList.FirstOrDefault(y => y.SLAPriorityId == y.SLAPriorityId).ResolutionDuration,
                    WorkStartHour = businessperiod.StartHour,
                    WorkEndHour = businessperiod.EndHour,
                    SLAResolveDateTime = SLAExpectedResolutionTimeline(x.ResolutionStartDate, sla_policypriority_durationList.FirstOrDefault(y => y.SLAPriorityId == y.SLAPriorityId).ResolutionDuration, businessperiod.StartHour, businessperiod.EndHour),          //SLA Expected Timeline
                    SLAPosition = SLAPosition.NotResolved.ToString(),   //just put default for SLAPosition now. In the next code block, we will pass the right SLAPosition on the rule/condition base
                });

                //tickets_sla = tickets_sla.Select(x => {
                //    x.SLAPosition = x.StatusId == 4 && x.ClosedDate <= x.SLAResolveDateTime ? SLAPosition.ClosedWithinSLA
                //                    : x.StatusId == 4 && x.ClosedDate > x.SLAResolveDateTime ? SLAPosition.ClosedOutsideSLA
                //                    : x.StatusId == 3 && x.ResolutionEndDate <= x.SLAResolveDateTime ? SLAPosition.ResolvedWithinSLA
                //                    : x.StatusId == 3 && x.ResolutionEndDate > x.SLAResolveDateTime ? SLAPosition.ResolvedOutsideSLA
                //                    : SLAPosition.NotResolved;
                //    return x;
                //});

                tickets_sla = tickets_sla.Select(x =>
                {
                    x.SLAPosition = x.StatusId == 4 && x.ClosedDate <= x.SLAResolveDateTime ? SLAPosition.ClosedWithinSLA.ToString()
                                    : x.StatusId == 4 && x.ClosedDate > x.SLAResolveDateTime ? SLAPosition.ClosedOutsideSLA.ToString()
                                    : x.StatusId == 3 && x.ResolutionEndDate <= x.SLAResolveDateTime ? SLAPosition.ResolvedWithinSLA.ToString()
                                    : x.StatusId == 3 && x.ResolutionEndDate > x.SLAResolveDateTime ? SLAPosition.ResolvedOutsideSLA.ToString()
                                    : SLAPosition.NotResolved.ToString();
                    return x;
                });

                //tickets_sla = tickets_sla.Where(x => x.SLAPosition == (SLAPosition)ticketFilter.SLAPosition);
                tickets_sla = tickets_sla.Where(x => x.SLAPosition == ((SLAPosition)ticketFilter.SLAPosition).ToString());

                ////var res = _mapper.Map<IEnumerable<TicketsModel>>(tickets);
                var res = _mapper.Map<IEnumerable<TicketsModel>>(tickets_sla);
                //return Ok(tickets_sla);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Tickets>>TicketFilter_SLAResolutionTime action: {ex.Message}");
                _logger.LogError($"Something went wrong inside Tickets>>TicketFilter_SLAResolutionTime action: {ex.InnerException}");
                return StatusCode(500, "Internal server error");
            }
        }

        [AllowAnonymous]
        [HttpGet("testreminder")]
        //[CustomAuthorizeFilter(RequiredPrivileges = "ticketfilter")]
        public IActionResult TestReminder()
        {
            NotificationReminder n = new NotificationReminder();
            n.ResetTicketReminderHasBeenSentTodayColumn();

            return Ok("Test for reminder reset values is successful");
        }

    }
}


/*
 ------------ I used the below in sql to see how the output will look like ------------------------
------------- I extract month, year from Datetime values in the sql DB table, then, I group them by month, then by year and then, take a count of each group

    select count(*) Count, month(CreatedOn) Period, year(CreatedOn) Year from [FintrakHelpDeskDB].[dbo].[Tickets]
    group by month(CreatedOn), year(CreatedOn)


    select count(*) Count, Day(createdon) Day, month(CreatedOn) Period, year(CreatedOn) Year from [FintrakHelpDeskDB].[dbo].[Tickets]
    group by DAY(createdon),  month(CreatedOn), year(CreatedOn)

    select count(*) Count, statusid, month(CreatedOn) Period, year(CreatedOn) Year from [FintrakHelpDeskDB].[dbo].[Tickets]
    group by statusid, month(CreatedOn), year(CreatedOn)


    select year(CreatedOn) from [FintrakHelpDeskDB].[dbo].[Tickets]
    group by year(CreatedOn)


    select getdate()
    select year(getdate())
   select month(getdate())
 */


//------------- modify a list values -----------------
//list.Where(w => w.Name == "height").ToList().ForEach(s => s.Value = 30);
//OR
//foreach (var mc in list.Where(x => x.Name == "height"))  mc.Value = 30;