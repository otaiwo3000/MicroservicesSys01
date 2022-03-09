using Helpdesk.Shared.Entities;
using System.Collections.Generic;

namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{

    public interface IProductModulesRepository : IRepositoryBase<ProductModules>
    {
        //IEnumerable<ProductModules> GetAllProductModules();
        //ProductModules GetProductModulesById(int Id);
        //ProductModules GetProductModulesWithDetails(int Id);
        IEnumerable<ProductModules> GetProductModulesByProductId(int organizationID, int productId);
        IEnumerable<ProductModules> GetProductModulesWithDetails(int organizationID, int productId);
        void CreateRangeProductModules(IEnumerable<ProductModules> productmodules);
        void UpdateRangeProductModules(IEnumerable<ProductModules> productmodules);
        void CreateProductModule(ProductModules productmodule);
        void UpdateProductModule(ProductModules productmodule);
        void DeleteProductModule(ProductModules productmodule);
        void RemoveRangeProductModules(IEnumerable<ProductModules> productmodules);
    }
}
