using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserMgt.Shared.Entities;

namespace UserMgt.Business.Repositories.Misc
{
    //public class ReportingUsers //UsersHierarchy
    //{
    //    public List<int> Children(int LogedInUserId, List<Users> users)
    //    {
    //        ////List<ParentChildUser> pcu = new List<ParentChildUser>();
    //        List<int> pcu = new List<int>();
    //        pcu.Add(LogedInUserId);

    //        ////var users = db.Users.Select(x => new { x.UserID, x.TeamLeadID, x.CompanyID }).Where(x => x.CompanyID == LoginCompany);
    //        //var users = _repository.users.GetAllUsers(currentUserOrganization);

    //        List<int> userList = new List<int>() { LogedInUserId };

    //        int i = 1;
    //        while (i != 0)
    //        {
    //            ////searching for who is/are reporting to the users in the userList
    //            //var reportingUsers = users.Select(x => new { UserID = x.UserId, SupervisorId = x.SupervisorId }).Where(x => userList.Contains(x.SupervisorId)).Select(x => new ParentChildUser { Child = x.UserID, Parent = x.SupervisorId });
    //            var reportingUsers = users.Select(x => new { UserID = x.UserId, ReportingTo = x.ReportToId }).Where(x => userList.Contains(x.ReportingTo)).Select(x => new ParentChildUser { Child = x.UserID, Parent = x.ReportingTo });

    //            if (reportingUsers.Count() > 0)
    //            {
    //                i = i + 1;
    //                pcu.AddRange(reportingUsers.Select(x => x.Child));
    //                userList.Clear(); //clear it and make it empty for new set
    //                userList = reportingUsers.Select(x => x.Child).ToList(); //these are the one reporting to the LogedInUserId. next iteration will tell if we have some users reporting to these ones
    //            }
    //            else
    //            {
    //                i = 0;
    //            }
    //        }
    //        return pcu.Distinct().ToList();
    //    }
    //}

    public class ParentChildUser
    {
        public int Child { get; set; }
        public int Parent { get; set; }
    }
}
