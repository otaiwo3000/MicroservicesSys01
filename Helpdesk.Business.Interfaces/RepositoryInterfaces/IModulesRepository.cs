using Helpdesk.Shared.Entities;
using System.Collections.Generic;

namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{

    public interface IModulesRepository : IRepositoryBase<Modules>
    {
        IEnumerable<Modules> GetAllModules(int organizationID);
        Modules GetModuleById(int organizationID, int Id);
        Modules GetModuleWithDetails(int organizationID, int Id);
        void CreateModule(Modules module);
        void UpdateModule(Modules module);
        void DeleteModule(Modules module);
    }
}
