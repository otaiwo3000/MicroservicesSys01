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
    public class ProductModulesRepository : RepositoryBase<ProductModules>, IProductModulesRepository
    {
        public ProductModulesRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<ProductModules> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<ProductModules> _repo;


        //public IEnumerable<ProductModules> GetAllProductModules(int productId)
        //{
        //    var res = FindAll().Where(x => x.OrganizationId == organizationID).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
        //    return res;
        //}

        public IEnumerable<ProductModules> GetProductModulesByProductId(int organizationID, int Id)
        {
            var res = FindAll().Where(x=>x.Product.OrganizationId == organizationID && x.ProductId==Id)
                .Include(x => x.Product.Owner)
                .Include(x => x.Module.Owner)
                .ToList();

            return res;
        }

        public IEnumerable<ProductModules> GetProductModulesWithDetails(int organizationID, int productId)
        {
            return FindByCondition(x => x.Product.OrganizationId==organizationID && x.ProductId == productId)
                .Include(x => x.Product.Owner)
                .Include(x => x.Module.Owner);
        }

        public void CreateRangeProductModules(IEnumerable<ProductModules> productmodules)
        {
            CreateRange(productmodules);
        }

        public void UpdateRangeProductModules(IEnumerable<ProductModules> productmodules)
        {
            UpdateRange(productmodules);
        }

        public void CreateProductModule(ProductModules productmodule)
        {
            Create(productmodule);
        }

        public void UpdateProductModule(ProductModules productmodule)
        {
            Update(productmodule);
        }

        public void DeleteProductModule(ProductModules productmodule)
        {
            Delete(productmodule);
        }

        public void RemoveRangeProductModules(IEnumerable<ProductModules> productmodules)
        {
            RemoveRange(productmodules);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override


    }
}
