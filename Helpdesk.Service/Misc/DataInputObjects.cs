using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helpdesk.Service.Misc
{
    public class DataInputObjects
    {

    }

    public class StartAndEndDates
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class IntParamAndStartAndEndDates
    {
        public int IntParam { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class StringParamAndStartAndEndDates
    {
        public string StringParam { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    //public class PriorityAndStartAndEndDates
    public class TypeAndStartAndEndDates
    {
        //public string Priority { get; set; } = "All";
        public string Type { get; set; } = "All";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class TicketFilter
    {
        public List<int> GroupId { get; set; } //= [1, 2, 3, 4];
        public List<int> ProductId { get; set; }
        public List<int> StatusId { get; set; }
        //public List<int> PriorityId { get; set; }
        public List<int> TypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class TicketFilter_2
    {
        public string GroupId { get; set; }
        public string ProductId { get; set; }
        public string StatusId { get; set; }
        //public string PriorityId { get; set; }
        public string TypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class TicketFilter_SLAPosition
    {
        public int SLAPosition { get; set; }
        public List<int> GroupId { get; set; } //= [1, 2, 3, 4];
        public List<int> ProductId { get; set; }
        public List<int> StatusId { get; set; }
        //public List<int> PriorityId { get; set; }
        public List<int> TypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
