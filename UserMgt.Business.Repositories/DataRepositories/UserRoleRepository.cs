
using System.Collections.Generic;
using System.Linq;
using System.Data;
using UserMgt.Business.Interfaces;
using UserMgt.Business.Interfaces.RepositoryInterfaces;
using UserMgt.Business.Repositories;
using UserMgt.Shared.DataAccess.DBContext;
using UserMgt.Shared.Entities;

namespace UserMgt.Business.Repositories.DataRepositories
{
    public class UserRoleRepository : RepositoryBase<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(UserMgtDBContext userMgtDBContext, IRepositoryBase<UserRole> repo)
           : base(userMgtDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<UserRole> _repo;


        public  IEnumerable<UserRole> GetAllUserRole()
        {
            //var res = FindAll().Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            var res = FindAll().OrderBy(y => y.UserId).ToList();
            return res;
        }

        public UserRole GetUserRoleById(int Id)
        {
            //return FindByCondition(x => x.UserRoleId.Equals(Id)).FirstOrDefault();
            return FindByCondition(x => x.UserRoleId==Id).FirstOrDefault();
        }

        public UserRole GetUserRoleWithDetails(int Id)
        {
            return FindByCondition(x => x.UserRoleId==Id)
                //.Include(y => y.Organization)
                .FirstOrDefault();
        }

        public void CreateUserRole(UserRole userrole)
        {
            Create(userrole);
        }

        public void UpdateUserRole(UserRole userrole)
        {
            Update(userrole);
        }

        public void DeleteUserRole(UserRole userrole)
        {
            Delete(userrole);
        }

        public void CreateRangeUserRoles(IEnumerable<UserRole> userroles)
        {
            CreateRange(userroles);
        }

        public void UpdateRangeUserRoles(IEnumerable<UserRole> userroles)
        {
            UpdateRange(userroles);
        }

        public void RemoveRangeUserRoles(IEnumerable<UserRole> userroles)
        {
            RemoveRange(userroles);
        }

        public IEnumerable<UserRole> GetUserRolesByUserId(long userid)
        {
            return FindByCondition(x => x.UserId==userid);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override
    }
}
