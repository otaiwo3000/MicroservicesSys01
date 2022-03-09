using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Shared.Entities.RelatedModel
{
    public class GroupTicketStatus
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }

        public int TicketStatus1 { get; set; }
        public int TicketStatus2 { get; set; }
        public int TicketStatus3 { get; set; }
        public int TicketStatus4 { get; set; }
        public int TicketStatus5 { get; set; }
        public int TicketStatus6 { get; set; }
        public int TicketStatus7 { get; set; }
        public int TicketStatus8 { get; set; }
        public int TicketStatus9 { get; set; }
        public int TicketStatus10 { get; set; }
        public int TotalNumberOfTickets { get; set; }
        //public TicketStatusDefined GlobalTicketStatusList { get; set; }
    }

    public class TicketStatusDefined
    {
        public int TicketStatus1 { get; set; }
        public int TicketStatus2 { get; set; }
        public int TicketStatus3 { get; set; }
        public int TicketStatus4 { get; set; }
        public int TicketStatus5 { get; set; }
        public int TicketStatus6 { get; set; }
        public int TicketStatus7 { get; set; }
        public int TicketStatus8 { get; set; }
        public int TicketStatus9 { get; set; }
        public int TicketStatus10 { get; set; }
    }

    public class Report
    {
        //public TicketStatus GlobalTicketStatusList { get; set; }

        public int Open { get; set; }
        public int Pending { get; set; }
        public int Resolved { get; set; }
        public int Closed { get; set; }
        public int WaitingOnCustomer { get; set; }
        public int WaitingOnThirdParty { get; set; }
    }

    public class TicketStatusReport
    {
        public int TicketStatusId { get; set; }
        public string TicketStatusName { get; set; }
        public int ReportValue { get; set; }
    }

    //public class GroupAndTicketStatus
    //{
    //    public int GroupId { get; set; }
    //    public int TicketStatusId { get; set; }
    //}
}
