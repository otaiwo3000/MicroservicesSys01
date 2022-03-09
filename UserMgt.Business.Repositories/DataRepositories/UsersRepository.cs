
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using UserMgt.Business.Interfaces;
using UserMgt.Business.Interfaces.RepositoryInterfaces;
using UserMgt.Business.Repositories;
using UserMgt.Shared.DataAccess.DBContext;
using UserMgt.Shared.Entities;

namespace UserMgt.Business.Repositories.DataRepositories
{
    public class UsersRepository : RepositoryBase<Users>, IUsersRepository
    {
        public UsersRepository(UserMgtDBContext userMgtDBContext, IRepositoryBase<Users> repo)
           : base(userMgtDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<Users> _repo;


        public IEnumerable<Users> GetAllUsers(int organizationID)
        {
            var res = FindAll().Where(x => x.OrganizationId == organizationID).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            return res;
        }

        //public IEnumerable<Users> GetAllUsers()
        //{
        //    var res = FindAll().ToList();
        //    return res;
        //}

        public Users GetUserById(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.UserId == Id).FirstOrDefault();
        }

        //public Users GetUserById(int Id)
        //{
        //    return FindByCondition(x => x.UserId == Id).FirstOrDefault();
        //}

        public Users GetUserWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.UserId == Id)
                .Include(y => y.Organization)
               // .Include(y => y.AgentEngagementType)
               // .Include(y => y.AgentType)
                .FirstOrDefault();
        }

        //public Users GetUserWithDetails(int Id)
        //{
        //    return FindByCondition(x => x.UserId == Id).FirstOrDefault();
        //}

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
            //return FindByCondition(x => x.Email.Equals(username)).FirstOrDefault();

            var res = FindByCondition(x => x.Email==username).FirstOrDefault();
            return res;
        }

       
        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override

    }
}
