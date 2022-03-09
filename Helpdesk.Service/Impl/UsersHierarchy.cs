
using Helpdesk.Business.Interfaces;
using Helpdesk.Business.Logger;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Helpdesk.Service.Impl
{
    public class UsersHierarchy
    {
        private IRepositoryWrapper _repository;
        private readonly ILoggerManagerRepository _logger;
        private IConfiguration _config;

        public UsersHierarchy(ILoggerManagerRepository logger, IRepositoryWrapper repository, IConfiguration config) 
        {
            _logger = logger;
            _repository = repository;
            _config = config;
        }

        //DBContext db = new DBContext();


        //public List<int> Children(int LogedInUserId)
        public List<int> Children(int currentUserOrganization, int LogedInUserId)
        {
             //public int LoginCompany = SessionWrapper.Current.CompanyID;     

            //List<ParentChildUser> pcu = new List<ParentChildUser>();
            List<int> pcu = new List<int>();
            pcu.Add(LogedInUserId);

            //var users = db.Users.Select(x => new { x.UserID, x.TeamLeadID, x.CompanyID }).Where(x => x.CompanyID == LoginCompany);
            var users = _repository.users.GetAllUsers(currentUserOrganization);
          
            List<int> userList = new List<int>() { LogedInUserId };

            int i = 1;
            while (i != 0)
            {
                ////searching for who is/are reporting to the users in the userList
                var reportingUsers = users.Select(x => new { UserID = x.UserId, SupervisorId = x.SupervisorId }).Where(x => userList.Contains(x.SupervisorId)).Select(x => new ParentChildUser { Child = x.UserID, Parent = x.SupervisorId });

                if (reportingUsers.Count() > 0)
                {
                    i = i + 1;
                    pcu.AddRange(reportingUsers.Select(x => x.Child));
                    userList.Clear(); //clear it and make it empty for new set
                    userList = reportingUsers.Select(x => x.Child).ToList(); //these are the one reporting to the LogedInUserId. next iteration will tell if we have some users reporting to these ones
                }
                else
                {
                    i = 0;
                }
            }
            return pcu.Distinct().ToList();
        }

    }

    public class ParentChildUser
    {
        public int Child { get; set; }
        public int Parent { get; set; }
    }
}
