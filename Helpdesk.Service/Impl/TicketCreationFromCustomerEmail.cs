
using Helpdesk.Business.Interfaces;
using Helpdesk.Business.Logger;
using Helpdesk.Service.Misc;
using Helpdesk.Shared.DataAccess.DBContext;
using Helpdesk.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Helpdesk.Service.Impl
{
    public class TicketCreationFromCustomerEmail
    {
        private IRepositoryWrapper _repository;
        private IConfiguration _config;
        private ILoggerManagerRepository _logger;


        public TicketCreationFromCustomerEmail(IRepositoryWrapper repository, IConfiguration config, ILoggerManagerRepository logger)
        {
            _repository = repository;
            _config = config;
            _logger = logger;
        }

        public void CreateTicketApplyTicketRules()
        {
            _logger.LogInformation("Inside CreateTicketApplyTicketRules");

            IEnumerable<ReceivedIssueMails> receivedmails = _repository.receivedissuemails.GetReceivedIssueMailsByStatus(false);

            _logger.LogInformation($"Receivedmail count: {receivedmails.Count()}");

            if (receivedmails.Count() > 0)
            {
                ReceivedIssueMails topreceivedemail = receivedmails.FirstOrDefault();
                ////string emailto = Shared.Common.Utilities.StringExtraction.ExtractASubstringBtwTwoXters(topreceivedemail.EMailTo, "<", ">");
                //string emailto = Shared.Common.Utilities.StringExtraction.ExtractSubstringsEachBtwTwoXters(topreceivedemail.EMailTo, "<", ">");

                string emailto = "";

                //====================================
                var httpContext = new HttpContextAccessor().HttpContext;
                var dbContext = httpContext.RequestServices.GetService(typeof(HelpDeskDBContext)) as HelpDeskDBContext;
                int currentUserOrganization = int.Parse(httpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());

                //it returns the subject but remove "RE:" or "RE-"
                string extractedTicketSubject = Stringmpl.PartOfaString(topreceivedemail.EMailSubject);
                Tickets ticketBySubjectInTheDB = _repository.tickets.GetaTicketByTicketSubjectWithNoDetail(currentUserOrganization, topreceivedemail.EMailSubject, extractedTicketSubject);

                //if the subject is new (ie is not already existed in the DB) then go ahead to create it otherwise, do nothing to it
                if (ticketBySubjectInTheDB == null)
                {
                    //go ahead and use it to create a new ticket;

                    if (topreceivedemail.EMailTo.Contains("<"))
                    emailto = Shared.Common.Utilities.StringExtraction.ExtractSubstringsEachBtwTwoXters(topreceivedemail.EMailTo, "<", ">");
                else
                    emailto = topreceivedemail.EMailTo;
                

                _logger.LogInformation($"Email to: {emailto}");

                if (!string.IsNullOrEmpty(emailto))
                {
                    var groupRecord = _repository.groups.GetAllGroups(topreceivedemail.OrganizationId).Where(x => x.GroupEmail == topreceivedemail.EMailTo).FirstOrDefault();  //.GetGroupById(topreceivedemail.OrganizationId, dtomodel.GroupId);

                    if (groupRecord == null)
                    {
                        Groups grp = new Groups();
                        //set groupID to the organization support groupID
                        grp.GroupId = 1;
                        groupRecord = _repository.groups.GetGroupById(topreceivedemail.OrganizationId, grp.GroupId);
                    }

                    Tickets ticketEntity = new Tickets()
                    {
                        Subject = topreceivedemail.EMailSubject,
                        //Contacts = topreceivedemail.EMailTo,
                        Contacts = emailto,
                        OrganizationId = topreceivedemail.OrganizationId,
                        Description = topreceivedemail.EMailBodyText,
                        GroupId = groupRecord.GroupId,
                        AgentId = groupRecord.GroupLeadId,
                        SupervisorId = groupRecord.GroupLeadId,
                        ProductId = 1,
                        ModuleId = 1,
                        TypeId = 1
                    };

                    _logger.LogInformation($"ticketEntity.Description: {topreceivedemail.EMailBodyText}");
                    _logger.LogInformation($"ticketEntity.OrganizationId: {topreceivedemail.OrganizationId}");

                    var pendingemailObj = new PendingEmail()
                    {
                        RecepientEmails = topreceivedemail.EMailTo,
                        MailSubject = topreceivedemail.EMailSubject,
                        MailContent = topreceivedemail.EMailBodyText
                    };

                    List<TicketRules> ticketrules = _repository.ticketrules.GetAllTicketRules(topreceivedemail.OrganizationId).Where(x => x.IsActive == true).ToList();
                    TicketsAutomation ticketautomation = new TicketsAutomation();
                    _logger.LogInformation($"ticketrules count: {ticketrules.Count()}");

                    Ticketruleresult res = new Ticketruleresult();
                    Ticketruleresult tkruleres = ticketautomation.IncomingTicket(ticketEntity, ticketrules);
                    _logger.LogInformation($"tkruleres obj to string: {tkruleres.ToString()}");

                    if (tkruleres.ReturnedBool)
                    {
                        var expectedactions = _repository.ruleaction_2.GetRuleAction_2ByRulebatchID(tkruleres.ReturnedRuleBatchID);

                        foreach (var v in expectedactions)
                        {
                            ticketEntity.GetType().GetProperty(v.ActionProperty).SetValue(ticketEntity, v.ActionValue, null);
                        }
                    }

                    topreceivedemail.IsTreated = true;

                    _repository.receivedissuemails.UpdateReceivedIssueMail(topreceivedemail);
                    _repository.Save();

                 
                    _repository.tickets.CreateTicket(ticketEntity);                   
                    _repository.pendingemail.CreatePendingEmail(pendingemailObj);
                    _repository.Save();
                }
                }
                //====================================
            }

        }
    }
}
