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
    public class ProductsRepository : RepositoryBase<Products>, IProductsRepository
    {
        public ProductsRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<Products> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<Products> _repo;


        public  IEnumerable<Products> GetAllProducts(int organizationID)
        {
            var res = FindAll().Where(x => x.OrganizationId == organizationID).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            return res;
        }

        public Products GetProductById(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.ProductId == Id).FirstOrDefault();
        }

        public Products GetProductWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.ProductId == Id)
                .Include(y => y.Organization).FirstOrDefault();
        }

        public void CreateProduct(Products product)
        {
            Create(product);
        }

        public void UpdateProduct(Products product)
        {
            Update(product);
        }

        public void DeleteProduct(Products product)
        {
            Delete(product);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override


    }
}
