using Helpdesk.Shared.Entities;
using Helpdesk.Shared.DataAccess.DBContext;
using Helpdesk.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Helpdesk.Business.Interfaces.RepositoryInterfaces;

namespace Helpdesk.Business.Repositories.DataRepositories
{
    public class UsersRepository : RepositoryBase<Users>, IUsersRepository
    {
        public UsersRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<Users> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<Users> _repo;


        public  IEnumerable<Users> GetAllUsers(int organizationID)
        {
            //var res = FindAll().Where(x => x.OrganizationId == organizationID).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            var res = FindAll().Where(x => x.OrganizationId == organizationID).ToList();
            return res;
        }

        //public IEnumerable<Users> GetUsersByOrganizationId(int organizationId)
        //{
        //    var res = FindAll().Where(x => x.OrganizationId == organizationId).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
        //    return res;
        //}

        public Users GetUserById(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.UserId==Id).FirstOrDefault();
        }

        public Users GetUserWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.UserId == Id).FirstOrDefault();
            //.Include(y => y.Organization)
            //.Include(y => y.AgentEngagementType)
            //.Include(y => y.AgentType)
            //.FirstOrDefault();
        }

        public void CreateUser(Users user)
        {
            Create(user);
        }

        public void UpdateUser(Users user)
        {
            Update(user);
        }

        public void DeleteUser(Users user)
        {
            Delete(user);
        }

        public Users GetUserByUserName(string username)
        {
            return FindByCondition(x => x.Email.Equals(username)).FirstOrDefault();
            //return FindByCondition(x => x.Email.Equals(username)).FirstOrDefault();
        }

       
        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override

    }
}
