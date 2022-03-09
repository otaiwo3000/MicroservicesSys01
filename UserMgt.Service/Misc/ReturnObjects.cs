using System;


namespace UserMgt.Service.Misc
{
    public class ReturnObjects
    {

    }

    public class Ticketruleresult
    {
        public bool ReturnedBool { get; set; }
        public string ReturnedRuleBatchID { get; set; } 
    }

    public class TicketVolumeTrend
    {
        public int Resolved { get; set; }
        public int UnResolved { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }

    //public class TicketStatusVolumeTrend
    //{
    //    public int StatusId { get; set; }
    //    public int Month { get; set; }
    //    public int Year { get; set; }
    //    public int Count { get; set; }
    //}

    public class TicketStatusYearMonth
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        //public TicketStatus Status { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Count { get; set; }
        public int TotalNumberOfTickets { get; set; }
        //public decimal Percentage { get; set; }
        private decimal _Percentage;
        public decimal Percentage
        {
            get { return decimal.Round(_Percentage, 2); }
            set { _Percentage = value; }
        }
    }

    public class TicketStatusYearMonth_B
    {
        public string Ticket { get; set; }
        public int StatusId { get; set; }
        public int NumberOfTickets { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        //public decimal Percentage { get; set; }
        private decimal _Percentage;
        public decimal Percentage
        {
            get { return decimal.Round(_Percentage, 2); }
            set { _Percentage = value; }
        }
    }

    public class TicketStatusYearMonth_DayOfWeek
    {
        public string Ticket { get; set; }
        public int StatusId { get; set; }
        public int NumberOfTickets { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        //public decimal Percentage { get; set; }
        private decimal _Percentage;
        public decimal Percentage
        {
            get { return decimal.Round(_Percentage, 2); }
            set { _Percentage = value; }
        }
        public DayOfWeek Dayofweek { get; set; }
        public string DayofweekString { get; set; }

        //private string _test;
        //public string Test
        //{
        //    get { return _test; }
        //    set { _test = value; }
        //}
    }

    public class TicketStatusYearMonth_Day
    {
        public string Ticket { get; set; }
        public int StatusId { get; set; }
        public int NumberOfTickets { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        private decimal _Percentage;
        public decimal Percentage
        {
            get { return decimal.Round(_Percentage, 2); }
            set { _Percentage = value; }
        }
    }

    public class CurrentUser
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
    }

    public class IdANDName
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SLAPositionEnum
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

}
