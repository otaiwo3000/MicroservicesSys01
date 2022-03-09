using Helpdesk.Shared.Entities;
using Helpdesk.Shared.DataAccess.DBContext;
using Helpdesk.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Helpdesk.Business.Interfaces.RepositoryInterfaces;


namespace Helpdesk.Business.Repositories.DataRepositories
{
    public class RolesPrivilegesRepository : RepositoryBase<RolesPrivileges>, IRolesPrivilegesRepository
    {
        public RolesPrivilegesRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<RolesPrivileges> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<RolesPrivileges> _repo;


        public  IEnumerable<RolesPrivileges> GetAllRolesPrivileges()
        {
            // var res = FindAll().Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            var res = FindAll().OrderBy(y => y.Role.Name).ToList();
            return res;
        }

        public RolesPrivileges GetRolePrivilegeById(long roleprivilegeId)
        {
            return FindByCondition(x => x.RolePrivilegeId.Equals(roleprivilegeId)).FirstOrDefault();
        }

        public RolesPrivileges GetRolePrivilegeWithDetails(long roleprivilegeId)
        {
            return FindByCondition(x => x.RolePrivilegeId.Equals(roleprivilegeId))
                //.Include(y => y.Organization)
                .FirstOrDefault();
        }

        public void CreateRolePrivilege(RolesPrivileges roleprivilege)
        {
            Create(roleprivilege);
        }

        public void UpdateRolePrivilege(RolesPrivileges roleprivilege)
        {
            Update(roleprivilege);
        }

        public void DeleteRolePrivilege(RolesPrivileges roleprivilege)
        {
            Delete(roleprivilege);
        }

        public IEnumerable<RolesPrivileges> GetRolesPrivilegesByRoles(List<long> roleIDs)
        {
            var res = FindByCondition(x => roleIDs.Contains(x.RoleId));
            //var res = FindAll().OrderBy(y => y.Role.Name).ToList();
            return res;
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override

    }
    //public class RolesPrivs
    //{
    //    public IEnumerable<RolesPrivileges> GetRolesPrivilegesByRoles(List<long> roleIDs)
    //    {
        
    //        var connectionstring = "Connection string";

    //        var optionsBuilder = new DbContextOptionsBuilder<HelpDeskDBContext>();
    //        optionsBuilder.UseSqlServer(connectionstring);

    //        using (HelpDeskDBContext dbContext = new HelpDeskDBContext(optionsBuilder.Options))
    //        {
    //            var res = dbContext.RolesPrivilegesSet.Where(x => roleIDs.Contains(x.RoleId));
    //            return res;
    //        }

    //        //List<long> roleIDs2 = new List<long> { 1,2};
    //        //var res = GetRolesPrivilegesByRoles(roleIDs2);

    //        //var res = helpDeskDBContext.RolesPrivilegesSet.Where(x => roleIDs.Contains(x.RoleId));

    //        //var v = new RolesPrivilegesRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase < RolesPrivileges > repo);
    //        //var res = GetRolesPrivilegesByRoles_2(roleIDs2);

    //        //return res;
    //    }
    //}

}


//var connectionstring = "Connection string";

//var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
//optionsBuilder.UseSqlServer(connectionstring);


//ApplicationDbContext dbContext = new ApplicationDbContext(optionsBuilder.Options);

//// Or you can also instantiate inside using

//using (ApplicationDbContext dbContext = new ApplicationDbContext(optionsBuilder.Options))
//{
//    //...do stuff
//}