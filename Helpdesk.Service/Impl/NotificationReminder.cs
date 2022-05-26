using Helpdesk.Business.Interfaces;
using Helpdesk.Business.Logger;
using Helpdesk.Service.DtoModels;
using Helpdesk.Service.Misc;
using Helpdesk.Shared.Common.Utilities;
using Helpdesk.Shared.DataAccess.DBContext;
using Helpdesk.Shared.Entities;
using Helpdesk.Shared.Entities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

using Helpdesk.Service.Controllers;


namespace Helpdesk.Service.Impl
{
    public class NotificationReminder
    {
        private IRepositoryWrapper _repository;
        private IConfiguration _config;
        private ILoggerManagerRepository _logger;

       

        public void ResetTicketReminderHasBeenSentTodayColumn()
        {
            HttpContext httpContext = new HttpContextAccessor().HttpContext;
          
            //applying RequestServices feature provided by .net core
            using (var dbContext = httpContext
                             .RequestServices
                             .GetService(typeof(HelpDeskDBContext)) as HelpDeskDBContext)
            {
                if (DateTime.Now == DateTime.Now.Date)
                {
                    
                    //////var ReminderHasBeenSentTodayColumn = dbContext.TicketsSet.Select(x=> new Tickets { TicketId = x.TicketId, ReminderHasBeenSentToday = false}).ToList();
                    //////dbContext.AttachRange(ReminderHasBeenSentTodayColumn);
                    //////dbContext.Entry(ReminderHasBeenSentTodayColumn).Property(r => r.Select(x=>x.ReminderHasBeenSentToday)).IsModified = true;
                    ////////dbContext.Entry(ReminderHasBeenSentTodayColumn).Property(r => r.ReminderHasBeenSentToday).IsModified = true;
                    //////dbContext.SaveChanges();

                     var ReminderHasBeenSentTodayColumn = dbContext.TicketsSet.ToList();
                    ReminderHasBeenSentTodayColumn.ForEach(a =>
                            {
                                a.ReminderHasBeenSentToday = false;                        
                                dbContext.Entry(a).State = EntityState.Modified;  
                                dbContext.SaveChanges();
                            });
                }
            }
        }

        //public void TicketSLAResolveDateTimeReminder(IRepositoryWrapper repository, IConfiguration config, ILoggerManagerRepository logger)
        //{
        //    _repository = repository;
        //    _config = config;
        //    _logger = logger;


        //    _logger.LogInformation($"Inside Tickets>>TicketFilter_SLAResolutionTime");

        //    HttpContext httpContext = new HttpContextAccessor().HttpContext;

        //    List<SLAPriority> slapriorityList = _repository.slapriority.GetAllSLAPriorities(1).ToList();

        //    //int currentUserOrganization = int.Parse(HttpContext.User.Claims.Where(x => x.Type == "organization").Select(x => x.Value).FirstOrDefault());

        //    ////int[] Arr01 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 } };
        //    ////int[] intArray01 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        //    List<int> intList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

        //    TicketFilter ticketFilter = new TicketFilter()
        //    {
        //        GroupId = intList,
        //        ProductId = intList,
        //        StatusId = intList,
        //        TypeId = intList,
        //        StartDate = DateTime.Now.Date,
        //        EndDate = DateTime.Now.Date.AddDays(1)
        //    };

        //    List<TicketStatus> ticketstatusList = new List<TicketStatus>();
        //    ticketstatusList = _repository.ticketstatus.GetAllTicketStatus().ToList();

        //    //string agentscope = HttpContext.User.Claims.Where(x => x.Type == "agentscope").Select(x => x.Value).FirstOrDefault();
        //    //_logger.LogInformation($"agentscope: {agentscope}");
        //    ////convert strin to Enum eg below:
        //    ////EnumName VariableName= (EnumName)Enum.Parse(typeof(EnumName), StringValue);
        //    AgentScope agentscope2 = (AgentScope)Enum.Parse(typeof(AgentScope), "1");

        //    ////string groupsString = Convert.ToString(HttpContext.User.Claims.Where(x => x.Type == "Groups").Select(x => x.Value).FirstOrDefault());
        //    ////_logger.LogInformation($"agent groups: {groupsString}");
        //    ////var groups = groupsString.Split(',').ToList();
        //    ////var CurrentUsergroupIDs = groups.Select(x => Int32.Parse(x)).ToList();
        //    //var CurrentUsergroupIDs = intList.ConvertAll(y=>y.ToString()).Select(x => Int32.Parse(x)).ToList();


        //    //NOTE:
        //    //CurrentUsergroupIDs: are groups the current user belong to.
        //    //groupid: is the group you are searching for.

        //    _logger.LogInformation($"About to call TicketsFilter");
        //    ////var filteredTickets = _repository.tickets.TicketsFilter(currentUserOrganization, agentscope2, groups2, ticketFilter.GroupId, ticketFilter.ProductId, ticketFilter.StatusId, ticketFilter.PriorityId, ticketFilter.StartDate, ticketFilter.EndDate);
        //    //var filteredTickets = _repository.tickets.TicketsFilter(1, agentscope2, CurrentUsergroupIDs, ticketFilter.GroupId, ticketFilter.ProductId, ticketFilter.StatusId, ticketFilter.TypeId, ticketFilter.StartDate, ticketFilter.EndDate);
        //    var filteredTickets = _repository.tickets.TicketsFilter(1, agentscope2, intList, ticketFilter.GroupId, ticketFilter.ProductId, ticketFilter.StatusId, ticketFilter.TypeId, ticketFilter.StartDate, ticketFilter.EndDate);

        //    if (filteredTickets.Count() == 0)
        //        //return NoContent();
        //        _logger.LogInformation($"Returned filtered tickets from database.");

        //    ////I need to get only one record for the business work period mapped to this organization
        //    //var businessperiod = _repository.businesshours.GetAllBusinessHours(currentUserOrganization).FirstOrDefault();
        //    var businessperiod = _repository.businesshours.GetAllBusinessHours(1).FirstOrDefault();
        //    if (businessperiod == null)
        //        //return Ok("Business hours have not been setup");
        //        _logger.LogInformation($"Business hours have not been setup.");

        //    ////Here, i need to get SLA issue resolution duration as setup in the SLAPolicyPriority table for the org
        //    //var sla_policypriority_durationList = _repository.slapolicypriority.GetAllSLAPolicyPriorities(currentUserOrganization);
        //    var sla_policypriority_durationList = _repository.slapolicypriority.GetAllSLAPolicyPriorities(1);
        //    if (sla_policypriority_durationList.Count() == 0)
        //        //return Ok("No priority has been mapped to a Policy");
        //        _logger.LogInformation($"No priority has been mapped to a Policy.");

        //    var tickets_sla = filteredTickets.Select(x => new Tickets_SLA
        //    {

        //        TicketId = x.TicketId,
        //        SysGeneratedTicketId = x.SysGeneratedTicketId,
        //        Contacts = x.Contacts,
        //        Subject = x.Subject,
        //        Description = x.Description,
        //        Tag = x.Tag,
        //        ResolutionStartDate = x.ResolutionStartDate,
        //        ResolutionEndDate = x.ResolutionEndDate,
        //        ClosedDate = x.ClosedDate,
        //        ReminderHasBeenSentToday = x.ReminderHasBeenSentToday,

        //        TypeId = x.TypeId,
        //        StatusId = x.StatusId,
        //        SLAPriorityId = x.SLAPriorityId,
        //        GroupId = x.GroupId,
        //        ModuleId = x.ModuleId,
        //        ProductId = x.ProductId,
        //        AgentId = x.AgentId,
        //        SupervisorId = x.SupervisorId,
        //        OrganizationId = x.OrganizationId,


        //        SLAPolicyPriorityDuration = sla_policypriority_durationList.FirstOrDefault(y => y.SLAPriorityId == y.SLAPriorityId).ResolutionDuration,
        //        WorkStartHour = businessperiod.StartHour,
        //        WorkEndHour = businessperiod.EndHour,
        //        SLAResolveDateTime = TicketsController.SLAExpectedResolutionTimeline(x.ResolutionStartDate, sla_policypriority_durationList.FirstOrDefault(y => y.SLAPriorityId == y.SLAPriorityId).ResolutionDuration, businessperiod.StartHour, businessperiod.EndHour),          //SLA Expected Timeline

        //    });

        //    //tickets_sla = tickets_sla.Select(x => {
        //    //    x.SLAPosition = x.StatusId == 4 && x.ClosedDate <= x.SLAResolveDateTime ? SLAPosition.ClosedWithinSLA
        //    //                    : x.StatusId == 4 && x.ClosedDate > x.SLAResolveDateTime ? SLAPosition.ClosedOutsideSLA
        //    //                    : x.StatusId == 3 && x.ResolutionEndDate <= x.SLAResolveDateTime ? SLAPosition.ResolvedWithinSLA
        //    //                    : x.StatusId == 3 && x.ResolutionEndDate > x.SLAResolveDateTime ? SLAPosition.ResolvedOutsideSLA
        //    //                    : SLAPosition.NotResolved;
        //    //    return x;
        //    //});

        //    tickets_sla = tickets_sla.Select(x => {
        //        x.SLAPosition = x.StatusId == 4 && x.ClosedDate <= x.SLAResolveDateTime ? SLAPosition.ClosedWithinSLA.ToString()
        //                        : x.StatusId == 4 && x.ClosedDate > x.SLAResolveDateTime ? SLAPosition.ClosedOutsideSLA.ToString()
        //                        : x.StatusId == 3 && x.ResolutionEndDate <= x.SLAResolveDateTime ? SLAPosition.ResolvedWithinSLA.ToString()
        //                        : x.StatusId == 3 && x.ResolutionEndDate > x.SLAResolveDateTime ? SLAPosition.ResolvedOutsideSLA.ToString()
        //                        : SLAPosition.NotResolved.ToString();
        //        return x;
        //    });

        //    List<Tickets_SLA> tSLA = new List<Tickets_SLA>();

        //    foreach (var v in tickets_sla)
        //    {
        //        if ((v.StatusId == 1 || v.StatusId == 2) && !v.ReminderHasBeenSentToday) //status: open=1, pending=2   and reminder has not been sent today
        //        {
        //            if (v.SLAPriorityId == 1)  //priority: low=1
        //            {
        //                SLAPriority slapr = slapriorityList.Where(x => x.SLAPriorityId == 1).FirstOrDefault();

        //                if (DateTime.Now.Subtract(v.SLAResolveDateTime).TotalHours <= slapr.SendReminderWhen)
        //                    tSLA.Add(v);
        //            }
        //            else if (v.SLAPriorityId == 2)  //priority: Medium=2
        //            {
        //                SLAPriority slapr = slapriorityList.Where(x => x.SLAPriorityId == 2).FirstOrDefault();

        //                if (DateTime.Now.Subtract(v.SLAResolveDateTime).TotalHours <= slapr.SendReminderWhen)
        //                    tSLA.Add(v);
        //            }
        //            else if (v.SLAPriorityId == 3)  //priority: High=3
        //            {
        //                SLAPriority slapr = slapriorityList.Where(x => x.SLAPriorityId == 3).FirstOrDefault();

        //                if (DateTime.Now.Subtract(v.SLAResolveDateTime).TotalHours <= slapr.SendReminderWhen)
        //                    tSLA.Add(v);
        //            }
        //            else if (v.SLAPriorityId == 4)  //priority: Urgent=4
        //            {
        //                SLAPriority slapr = slapriorityList.Where(x => x.SLAPriorityId == 4).FirstOrDefault();

        //                if (DateTime.Now.Subtract(v.SLAResolveDateTime).TotalHours <= slapr.SendReminderWhen)
        //                    tSLA.Add(v);
        //            }
        //        }

        //    }

        //    List<int> ticketIDs = new List<int>();

        //    foreach (var v in tSLA)
        //    {
        //        char[] spearator = { ',', ';', '<', '>', '"', '"' };
        //        var vv = Splitting.SplitString(v.Contacts, spearator).ToList();
        //        var vv2 = vv.Where(x => x.Contains("@"));

        //        NetMailImpl nmi = new NetMailImpl(_repository, _config, _logger);
        //        nmi.SendEmails(vv2.ToArray(), v.Subject, "");

        //        ticketIDs.Add(v.TicketId);
        //    }

        //    using (var dbContext = httpContext.RequestServices.GetService(typeof(HelpDeskDBContext)) as HelpDeskDBContext)
        //    {
        //        var ReminderHasBeenSentTodayColumn = dbContext.TicketsSet.Where(x => ticketIDs.Contains(x.TicketId)).ToList();
        //        ReminderHasBeenSentTodayColumn.ForEach(a =>
        //        {
        //            a.ReminderHasBeenSentToday = true;
        //            dbContext.Entry(a).State = EntityState.Modified;
        //            dbContext.SaveChanges();
        //        });
        //    }
        //}


        ////https://entityframeworkcore.com/knowledge-base/56918249/how-to-update-a-list-of-multiple-records-same-as-normal-sql-update-query-using-entity-framework-core-

        ////update list
        ////var idList=new int[]{1, 2, 3, 4};
        ////using (var db=new SomeDatabaseContext())
        ////{
        ////    var friends = db.Friends.Where(f => idList.Contains(f.ID)).ToList();
        ////    friends.ForEach(a=>a.msgSentBy='1234');
        ////    db.SaveChanges();
        ////}


        ////List<Users> usersList = _context.Users.Where(u => u.age > 18).ToList();
        ////usersList.ForEach(a =>
        ////        {
        ////            a.isAdult = 1;
        ////            _context.Entry(a).State = EntityState.Modified;
        ////             _context.SaveChanges();
        ////        });

    }
}
