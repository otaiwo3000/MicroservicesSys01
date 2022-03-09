using Helpdesk.Service.Misc;
using Helpdesk.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace Helpdesk.Service.Impl
{
    public class TicketsAutomation
    {
        ////public Tickets IncomingTicket(Tickets incomingticket, List<TicketRules> ticketrules)
        //public bool IncomingTicket(Tickets incomingticket, List<TicketRules> ticketrules)
        public Ticketruleresult IncomingTicket(Tickets incomingticket, List<TicketRules> ticketrules)
        {           
            Tickets automatedticket = new Tickets();
            //bool res = false;
            Ticketruleresult res = new Ticketruleresult()
            {
                ReturnedBool = false,   //initialize to avoid obj ref
                                        //ReturnedRuleBatchID = Guid.Empty  //initialize to avoid obj ref
                ReturnedRuleBatchID = ""
            };

            //List<Guid> ticketrulebatchIDs = ticketrules.Select(x => x.RuleBatchId).ToList();
            List<string> ticketrulebatchIDs = ticketrules.Select(x => x.RuleBatchId).Distinct().ToList();

            foreach (var v1 in ticketrulebatchIDs)
            {
                bool subtestresult = false;
               
                //subrule casese ... here
                var TicketsubrulesForABatch = ticketrules.Where(x => x.RuleBatchId == v1);
                int t = TicketsubrulesForABatch.Count();
                int i = 0;

                bool check_1 = false;
                foreach(var v2 in TicketsubrulesForABatch)
                {
                    i = i + 1;
                    //string ruleitemlogicaloperator = !string.IsNullOrEmpty(v2.RuleItemLogicalOperator) ? v2.RuleItemLogicalOperator : "";
                    ////var g = this.GetType().GetProperty(v2.RuleCondition).GetValue(this, null) as string;
                    var incomingticketpropertyValue = incomingticket.GetType().GetProperty(v2.RuleCondition).GetValue(incomingticket, null) as string;
                    //var g = incomingticket.GetType().GetProperty("Subject").GetValue(incomingticket, null) as string;

                    bool check_2 = false;
                    if (v2.RuleOperator.Trim().ToLower() == "contains")
                    {
                        if (incomingticketpropertyValue.Contains(v2.RuleConditionValue))
                        {
                            check_2 = true;
                            check_1 = i==1 ? true : check_1;

                            //if v2.RuleItemLogicalOperator is null or empty, dont call LogicalOperatorRes() method. it means, it ends there.
                            if (!string.IsNullOrEmpty(v2.RuleItemLogicalOperator))
                            {
                                subtestresult = LogicalOperatorRes(v2.RuleItemLogicalOperator, check_1, check_2);
                                check_1 = subtestresult;
                            }
                        }
                        else
                        {
                            subtestresult = false;
                            check_1 = subtestresult;
                        }
                    }
                    else if (v2.RuleOperator.Trim().ToLower() == "is")
                    {
                        if (incomingticketpropertyValue == v2.RuleConditionValue)
                        {
                            check_2 = true;
                            check_1 = i == 1 ? true : check_1;

                            //if v2.RuleItemLogicalOperator is null or empty, dont call LogicalOperatorRes() method. it means, it ends there.
                            if (!string.IsNullOrEmpty(v2.RuleItemLogicalOperator))
                            {
                                subtestresult = LogicalOperatorRes(v2.RuleItemLogicalOperator, check_1, check_2);
                                check_1 = subtestresult;
                            }                           
                        }
                        else
                        {
                            subtestresult = false;
                            check_1 = subtestresult;
                        }
                    }
                }

                res.ReturnedBool = subtestresult;
                res.ReturnedRuleBatchID = v1;



                //return only when subtestresult is true otherwise, continue iteration
                if (subtestresult == true)
                {
                    return res;
                }
            }

            //return automatedticket;
            return res;
        }

        public List<string> TicketProperties()
        {
            var ticketproperties = new Tickets();
            PropertyInfo[] properties = ticketproperties.GetType().GetProperties();
            var ticketpropertyNames = new List<string>();

            foreach (var v in properties)
            {
                ticketpropertyNames.Add(v.Name);
            }

            return ticketpropertyNames;
        }

        ////Extension method
        //public static object GetPropertyValue(this object tickettt, string propertyName)
        //{
        //    return tickettt.GetType().GetProperties()
        //       .Single(pi => pi.Name == propertyName)
        //       .GetValue(tickettt, null);
        //}

        public bool LogicalOperatorRes(string LogicalOperator, bool connector1, bool connector2)
        {
            bool res = false;

            if (LogicalOperator.Trim().ToUpper() == "AND")
            {
                if (connector1 == true && connector2 == true)
                    return true;
            }
            else if (LogicalOperator.Trim().ToUpper() == "OR")
            {
                if (connector1 == true || connector2 == true)
                    return true;
            }

            return res;
        }

    }
}


//https://www.tutorialspoint.com/how-to-set-a-property-value-by-reflection-in-chash

//https://stackoverflow.com/questions/5508050/how-to-get-a-property-value-based-on-the-name