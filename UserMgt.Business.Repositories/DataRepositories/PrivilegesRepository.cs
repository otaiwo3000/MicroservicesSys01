
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
    public class PrivilegesRepository : RepositoryBase<Privileges>, IPrivilegesRepository
    {
        public PrivilegesRepository(UserMgtDBContext userMgtDBContext, IRepositoryBase<Privileges> repo)
           : base(userMgtDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<Privileges> _repo;


        public  IEnumerable<Privileges> GetAllPrivileges()
        {
            var res = FindAll().OrderBy(y => y.Name).ToList();
            return res;
        }

        public Privileges GetPrivilegeById(int Id)
        {
            return FindByCondition(x => x.PrivilegeId.Equals(Id)).FirstOrDefault();
        }

        public Privileges GetPrivilegeWithDetails(int Id)
        {
            return FindByCondition(x => x.PrivilegeId.Equals(Id))
                .FirstOrDefault();
        }

        public void CreatePrivilege(Privileges privilege)
        {
            Create(privilege);
        }

        public void UpdatePrivilege(Privileges privilege)
        {
            Update(privilege);
        }

        public void DeletePrivilege(Privileges privilege)
        {
            Delete(privilege);
        }

        public IEnumerable<Privileges> GetPrivilegesByNames(List<string> privilegenames)
        {
            var res = FindByCondition(x => privilegenames.Contains(x.Name));
            return res;
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override
    }

    //public class Privs
    //{
    //    public IEnumerable<Privileges> GetPrivilegesByNames(List<string> privilegenames)
    //    {
    //        var res = GetPrivilegesByNames(privilegenames);
    //        return res;
    //    }
    //}
}
