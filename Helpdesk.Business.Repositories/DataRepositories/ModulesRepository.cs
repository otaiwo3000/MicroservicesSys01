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
    public class ModulesRepository : RepositoryBase<Modules>, IModulesRepository
    {
        public ModulesRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<Modules> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<Modules> _repo;


        public  IEnumerable<Modules> GetAllModules(int organizationID)
        {
            var res = FindAll().Where(x => x.OrganizationId == organizationID).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            return res;
        }

        public Modules GetModuleById(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.ModuleId == Id).FirstOrDefault();
        }

        public Modules GetModuleWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.ModuleId == Id)
                .Include(y => y.Organization).FirstOrDefault();
        }

        public void CreateModule(Modules module)
        {
            Create(module);
        }

        public void UpdateModule(Modules module)
        {
            Update(module);
        }

        public void DeleteModule(Modules module)
        {
            Delete(module);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override


    }
}
