using Helpdesk.Shared.Entities;
using Helpdesk.Shared.DataAccess.DBContext;
using Helpdesk.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Helpdesk.Shared.Entities.Enums;
using Helpdesk.Shared.Entities.RelatedModel;
using Helpdesk.Business.Interfaces.RepositoryInterfaces;


namespace Helpdesk.Business.Repositories.DataRepositories
{
    public class TicketsRepository : RepositoryBase<Tickets>, ITicketsRepository
    {
        public TicketsRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<Tickets> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<Tickets> _repo;


        //public IEnumerable<Tickets> GetAllTickets(int organizationID)
        //{
        //    //var res = FindAll().Where(x => x.OrganizationId == organizationID).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
        //    var res = FindAll().Where(x => x.OrganizationId == organizationID)
        //        .Include(x => x.Organization)
        //        .Include(x=>x.Type)
        //        .Include(x=>x.Status)
        //        .Include(x=>x.SLAPriority)
        //        .Include(x=>x.Product)
        //        .Include(x=>x.Group)
        //        .Include(x=>x.Module)
        //        .Include(x=>x.Agent)
        //        .Include(x=>x.Supervisor)

        //        .OrderBy(x=>x.Group.Name).ToList();
        //    return res;
        //}


        public IEnumerable<Tickets> GetAllTickets(int organizationID, AgentScope agentscope, List<int> groupIDs)
        {
            //NOTE: -------- gives last day of the month: -------------
            //var lastDateofTheCurrentMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)
            //var lastDateOfaParticularMonth = DateTime.DaysInMonth(date.Year, date.Month);

            DateTime firstdateoftheMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime lastdateoftheMonth = firstdateoftheMonth.AddMonths(1).AddSeconds(-1);

            List<Tickets> res = new List<Tickets>();

            //RequiredPriviliges.Any(x => priviliges.Any(y => y == x))

            if (agentscope == AgentScope.GlobalAccess) 
            {
                res = FindAll().Where(x => x.OrganizationId == organizationID && (x.CreatedOn >= firstdateoftheMonth && x.CreatedOn<=DateTime.Now))
                .Include(x => x.Organization)
                .Include(x => x.Type)
                .Include(x => x.Status)
                .Include(x => x.SLAPriority)
                .Include(x => x.Product)
                .Include(x => x.Group)
                .Include(x => x.Module)
                .Include(x => x.Agent)
                .Include(x => x.Supervisor)
                //.Include(x=>x.Agent.AgentType)
                //.Include(x=>x.Agent.AgentEngagementType)

                .OrderBy(x => x.Group.Name).ToList();
            }

            else if (agentscope == AgentScope.GroupAccess)
            {
            res = FindAll().Where(x => x.OrganizationId == organizationID && groupIDs.Contains(x.GroupId) && (x.CreatedOn >= firstdateoftheMonth && x.CreatedOn <= DateTime.Now))
            //res = FindAll().Any(x => groupIDs.Any(y => y == x.GroupId))
                .Include(x => x.Organization)
                .Include(x => x.Type)
                .Include(x => x.Status)
                .Include(x => x.SLAPriority)
                .Include(x => x.Product)
                .Include(x => x.Group)
                .Include(x => x.Module)
                .Include(x => x.Agent)
                .Include(x => x.Supervisor)

                .OrderBy(x => x.Group.Name).ToList();
            }

            return res;
        }

        public Tickets GetTicketById(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.TicketId == Id).FirstOrDefault();
        }

        public Tickets GetTicketWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.TicketId == Id)
                 .Include(x => x.Organization)
                .Include(x => x.Type)
                .Include(x => x.Status)
                .Include(x => x.SLAPriority)
                .Include(x => x.Product)
                .Include(x => x.Group)
                .Include(x => x.Module)
                .Include(x => x.Agent)
                .Include(x => x.Supervisor)
                .FirstOrDefault();
            //.Include(y => y.Organization)
            //.FirstOrDefault();
        }

        public Tickets GetaTicketBySysGeneratedTicketIdWithNoDetail(int organizationID, string SysGeneratedTicketId)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.SysGeneratedTicketId == SysGeneratedTicketId).FirstOrDefault();
        }

        public Tickets GetaTicketByTicketSubjectWithNoDetail(int organizationID, string ticketsubject, string extractedticketsubject)
        {
            //RequiredPriviliges.Any(x => priviliges.Any(y => y == x))
            return FindByCondition(x => x.OrganizationId == organizationID && x.Subject == ticketsubject || x.Subject == extractedticketsubject).FirstOrDefault();
        }

        public void CreateTicket(Tickets ticket)
        {
            ticket.SysGeneratedTicketId = Guid.NewGuid().ToString();
            Create(ticket);
        }

        public void UpdateTicket(Tickets ticket)
        {
            Update(ticket);
        }

        public void DeleteTicket(Tickets ticket)
        {
            Delete(ticket);
        }


        //public List<TicketStatusReport> TicketStatusReportResult(int organizationID, AgentScope agentscope, List<int> groupIDs, List<TicketStatus> ticketstatus, string priority, DateTime startdate, DateTime enddate)
        public List<TicketStatusReport> TicketStatusReportResult(int organizationID, AgentScope agentscope, List<int> groupIDs, List<TicketStatus> ticketstatus, string tickettype, DateTime startdate, DateTime enddate)
        {
            List<Tickets> queryRes = new List<Tickets>();

            if (agentscope == AgentScope.GlobalAccess)
            {
                if (tickettype.Trim().ToLower() == "all")
                {
                    queryRes = FindAll().Where(x => x.OrganizationId == organizationID && (x.CreatedOn >= startdate && x.CreatedOn <= enddate)).ToList();
                }

                else
                {
                    int intTickettype = int.Parse(tickettype.Trim());
                    queryRes = FindAll().Where(x => x.OrganizationId == organizationID && x.TypeId == intTickettype && (x.CreatedOn >= startdate && x.CreatedOn <= enddate)).ToList();
                }               
            }

            else if (agentscope == AgentScope.GroupAccess)
            {
                if (tickettype.Trim().ToLower() == "all")
                {
                    queryRes = FindAll().Where(x => x.OrganizationId == organizationID && groupIDs.Contains(x.GroupId) && (x.CreatedOn >= startdate && x.CreatedOn <= enddate)).ToList();
                }
                else
                {
                    int intTickettype = int.Parse(tickettype.Trim());
                    queryRes = FindAll().Where(x => x.OrganizationId == organizationID && groupIDs.Contains(x.GroupId) && x.TypeId == intTickettype && (x.CreatedOn >= startdate && x.CreatedOn <= enddate)).ToList();
                }
            }
            
            List<TicketStatusReport> tsrList = new List<TicketStatusReport>();

            if (queryRes.Count() > 0)
            {
                foreach (var v in ticketstatus)
                {
                    TicketStatusReport tsrObj = new TicketStatusReport();

                    tsrObj.TicketStatusId = v.TicketStatusId;
                    tsrObj.TicketStatusName = v.Status;
                    tsrObj.ReportValue = queryRes.Where(x => x.StatusId == v.TicketStatusId).Count();

                    tsrList.Add(tsrObj);
                }
            }           
            return tsrList;
        }

        ////public List<GroupTicketStatus> TicketStatusReportResultGroupBase(int organizationID, List<TicketStatus> ticketstatus, List<int> AllgroupIDs)
        //public List<GroupTicketStatus> TicketStatusReportResultGroupBase(int organizationID, AgentScope agentscope, List<int> AllgroupIDs, string priority, DateTime startdate, DateTime enddate)
        public List<GroupTicketStatus> TicketStatusReportResultGroupBase(int organizationID, AgentScope agentscope, List<int> AllgroupIDs, string tickettype, DateTime startdate, DateTime enddate)
        {
            List<Tickets> queryRes = new List<Tickets>();

            if (agentscope == AgentScope.GlobalAccess)
            {
                if (tickettype.Trim().ToLower() == "all")
                {
                    queryRes = FindAll()
                        .Include(x=>x.Group)
                        .Where(x => x.OrganizationId == organizationID && (x.CreatedOn >= startdate && x.CreatedOn <= enddate)).ToList();
                }
                else
                {
                    int intTickettype = int.Parse(tickettype.Trim());
                    queryRes = FindAll()
                        .Include(x => x.Group)
                        .Where(x => x.OrganizationId == organizationID && x.TypeId == intTickettype && (x.CreatedOn >= startdate && x.CreatedOn <= enddate)).ToList();
                }
            }          

            List<GroupTicketStatus> GTList = new List<GroupTicketStatus>();

            if (queryRes.Count() > 0)
            {
                //should only be used by user with global access/view. So, non global access/view is not necessary to be implemented
                foreach (var g in AllgroupIDs)
                {
                    GroupTicketStatus GT = new GroupTicketStatus();
                    var queryRes_2 = queryRes.Where(x => x.GroupId == g);
                    string groupname = queryRes_2.Select(x=>x.Group.Name).FirstOrDefault();

                    GT.GroupID = g;
                    GT.GroupName = groupname;
                    GT.TicketStatus1 = queryRes_2.Where(x => x.StatusId == 1).Count();
                    GT.TicketStatus2 = queryRes_2.Where(x => x.StatusId == 2).Count();
                    GT.TicketStatus3 = queryRes_2.Where(x => x.StatusId == 3).Count();
                    GT.TicketStatus4 = queryRes_2.Where(x => x.StatusId == 4).Count();
                    GT.TicketStatus5 = queryRes_2.Where(x => x.StatusId == 5).Count();
                    GT.TicketStatus6 = queryRes_2.Where(x => x.StatusId == 6).Count();
                    GT.TicketStatus7 = queryRes_2.Where(x => x.StatusId == 7).Count();
                    GT.TicketStatus8 = queryRes_2.Where(x => x.StatusId == 8).Count();
                    GT.TicketStatus9 = queryRes_2.Where(x => x.StatusId == 9).Count();
                    GT.TicketStatus10 = queryRes_2.Where(x => x.StatusId == 10).Count();
                    GT.TotalNumberOfTickets = queryRes_2.Count();

                    GTList.Add(GT);
                }
            }
            return GTList;
        }

        //public IEnumerable<Tickets> TicketsFilter(int organizationID, AgentScope agentscope, List<int> CurrentUsergroupIDs, List<int> groupids, List<int> productids, List<int> statusids, List<int> priorityids, DateTime startdate, DateTime enddate)
        public IEnumerable<Tickets> TicketsFilter(int organizationID, AgentScope agentscope, List<int> CurrentUsergroupIDs, List<int> groupids, List<int> productids, List<int> statusids, List<int> tickettypeids, DateTime startdate, DateTime enddate)
        {
            //NOTE:
            //CurrentUsergroupIDs: are groups the current user belong to.
            //groupid: is the group you are searching for.

            List<Tickets> queryRes = new List<Tickets>();
           
            if (agentscope == AgentScope.GlobalAccess)
            {
                //queryRes = FindAll().Where(x => x.OrganizationId == organizationID && (x.CreatedOn >= startdate && x.CreatedOn <= enddate))
                queryRes = FindAll().Where(x => x.OrganizationId == organizationID && productids.Contains(x.ProductId) && statusids.Contains(x.StatusId) && tickettypeids.Contains(x.TypeId) && (x.CreatedOn >= startdate && x.CreatedOn <= enddate))
                    .Include(x => x.Organization)
                    .Include(x => x.Type)
                    .Include(x => x.Status)
                    .Include(x => x.SLAPriority)
                    .Include(x => x.Product)
                    .Include(x => x.Group)
                    .Include(x => x.Module)
                    .Include(x => x.Agent)
                    .Include(x => x.Supervisor)
                     .OrderBy(x => x.Group.Name).ToList();
            }
            else
            {
                //queryRes = FindAll().Where(x => x.OrganizationId == organizationID && CurrentUsergroupIDs.Contains(x.GroupId) && (x.CreatedOn >= startdate && x.CreatedOn <= enddate))
                queryRes = FindAll().Where(x => x.OrganizationId == organizationID && CurrentUsergroupIDs.Contains(x.GroupId) && productids.Contains(x.ProductId) && statusids.Contains(x.StatusId) && tickettypeids.Contains(x.TypeId) && (x.CreatedOn >= startdate && x.CreatedOn <= enddate))
                    .Include(x => x.Organization)
                    .Include(x => x.Type)
                    .Include(x => x.Status)
                    .Include(x => x.SLAPriority)
                    .Include(x => x.Product)
                    .Include(x => x.Group)
                    .Include(x => x.Module)
                    .Include(x => x.Agent)
                    .Include(x => x.Supervisor)
                     .OrderBy(x => x.Group.Name).ToList();
            }

            List<Tickets> ticketList = new List<Tickets>();

            ////ticketList = queryRes.Where(x => x.GroupId == groupid && x.ProductId == productid && x.StatusId == statusid && x.SLAPriorityId == priorityid && (x.CreatedOn >= startdate && x.CreatedOn <= enddate)).ToList();
            //ticketList = queryRes.Where(x => groupids.Contains(x.GroupId) && productids.Contains(x.ProductId) && statusids.Contains(x.StatusId) && priorityids.Contains(x.SLAPriorityId) && (x.CreatedOn >= startdate && x.CreatedOn <= enddate)).ToList();
            ticketList = queryRes.Where(x => groupids.Contains(x.GroupId) && (x.CreatedOn >= startdate && x.CreatedOn <= enddate)).ToList();

            return ticketList;
        }

        //public IEnumerable<Tickets> TicketsFilter_2(int organizationID, AgentScope agentscope, List<int> CurrentUsergroupIDs, string groupid, string productid, string statusid, string priorityid, DateTime startdate, DateTime enddate)
        public IEnumerable<Tickets> TicketsFilter_2(int organizationID, AgentScope agentscope, List<int> CurrentUsergroupIDs, string groupid, string productid, string statusid, string tickettypeid, DateTime startdate, DateTime enddate)
        {
            //NOTE:
            //CurrentUsergroupIDs: are groups the current user belong to.
            //groupid: is the group you are searching for.

            List<Tickets> queryRes = new List<Tickets>();


            int[] groupArray = new int[20];
            int[] groupArray_3 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            if (groupid.Trim().ToLower() == "all")
                groupArray = groupArray_3;
            else
                groupArray[0] = int.Parse(groupid);


            int[] productArray = new int[30];
            int[] productArray_3 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
            if (productid.Trim().ToLower() == "all")
                productArray = productArray_3;
            else
                productArray[0] = int.Parse(productid);


            int[] statusArray = new int[10];
            int[] statusArray_3 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            if (statusid.Trim().ToLower() == "all")
                statusArray = statusArray_3;
            else
                statusArray[0] = int.Parse(statusid);


            int[] tickettypeArray = new int[10];
            int[] tickettypeArray_3 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,11,12,13,14,15,16,17,18,19,20 };
            if (tickettypeid.Trim().ToLower() == "all")
                tickettypeArray = tickettypeArray_3;
            else
                tickettypeArray[0] = int.Parse(tickettypeid);


            if (agentscope == AgentScope.GlobalAccess)
            {
                queryRes = FindAll().Where(x => x.OrganizationId == organizationID && (x.CreatedOn >= startdate && x.CreatedOn <= enddate)).ToList();
            }
            else
            {
                queryRes = FindAll().Where(x => x.OrganizationId == organizationID && CurrentUsergroupIDs.Contains(x.GroupId) && (x.CreatedOn >= startdate && x.CreatedOn <= enddate)).ToList();
            }

            List<Tickets> ticketList = new List<Tickets>();

            //ticketList = queryRes.Where(x => x.GroupId == groupid && x.ProductId == productid && x.StatusId == statusid && x.SLAPriorityId == priorityid && (x.CreatedOn >= startdate && x.CreatedOn <= enddate)).ToList();
            ticketList = queryRes.Where(x => groupArray.Contains(x.GroupId) && productArray.Contains(x.ProductId) && statusArray.Contains(x.StatusId) && tickettypeArray.Contains(x.TypeId) && (x.CreatedOn >= startdate && x.CreatedOn <= enddate)).ToList();

            return ticketList;
        }


        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override

        public void UpdateTicketTicketStatus(Tickets ticket)
        {
            
        }

    }
}
