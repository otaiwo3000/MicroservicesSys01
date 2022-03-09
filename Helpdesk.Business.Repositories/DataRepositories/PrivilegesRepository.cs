using Helpdesk.Shared.Entities;
using Helpdesk.Shared.DataAccess.DBContext;
using Helpdesk.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Helpdesk.Business.Interfaces.RepositoryInterfaces;


namespace Helpdesk.Business.Repositories.DataRepositories
{
    public class PrivilegesRepository : RepositoryBase<Privileges>, IPrivilegesRepository
    {
        public PrivilegesRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<Privileges> repo)
           : base(helpDeskDBContext)
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
