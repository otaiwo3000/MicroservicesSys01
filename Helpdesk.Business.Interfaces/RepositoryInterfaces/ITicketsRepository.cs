using Helpdesk.Shared.Entities;
using System;
using System.Collections.Generic;
using Helpdesk.Shared.Entities.Enums;
using Helpdesk.Shared.Entities.RelatedModel;


namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{
    public interface ITicketsRepository : IRepositoryBase<Tickets>
    {
        //IEnumerable<Tickets> GetAllTickets(int organizationID);
        IEnumerable<Tickets> GetAllTickets(int organizationID, AgentScope agentscope, List<int> groupIDs);
        Tickets GetTicketById(int organizationID, int Id);
        Tickets GetTicketWithDetails(int organizationID, int Id);
        void CreateTicket(Tickets ts);
        void UpdateTicket(Tickets ts);
        void DeleteTicket(Tickets ts);
        //List<TicketStatusReport> TicketStatusReportResult(int organizationID, AgentScope agentscope, List<int> groupIDs, List<TicketStatus> ticketstatus, string priority, DateTime startdate, DateTime enddate);
        //List<GroupTicketStatus> TicketStatusReportResultGroupBase(int organizationID, AgentScope agentscope, List<int> AllgroupIDs, string priority, DateTime startdate, DateTime enddate);
        //IEnumerable<Tickets> TicketsFilter(int organizationID, AgentScope agentscope, List<int> CurrentUsergroupIDs, List<int> groupids, List<int> productids, List<int> statusids, List<int> priorityids, DateTime startdate, DateTime enddate);
        //IEnumerable<Tickets> TicketsFilter_2(int organizationID, AgentScope agentscope, List<int> CurrentUsergroupIDs, string groupid, string productid, string statusid, string priorityid, DateTime startdate, DateTime enddate);

        Tickets GetaTicketBySysGeneratedTicketIdWithNoDetail(int organizationID, string SysGeneratedTicketId);
        Tickets GetaTicketByTicketSubjectWithNoDetail(int organizationID, string ticketsubject, string extractedticketsubject);
        //Tickets GetaTicketByTicketSubjectWithNoDetail(int organizationID, List<string> ticketsubject);

        void UpdateTicketTicketStatus(Tickets ts);

        //NOTE:
        //CurrentUsergroupIDs: are groups the current user belong to.
        //groupid: is the group you are searching for.
    }
}
