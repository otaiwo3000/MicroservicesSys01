using Helpdesk.Shared.Entities;
using System.Collections.Generic;

namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{

    public interface IProductsRepository : IRepositoryBase<Products>
    {
        IEnumerable<Products> GetAllProducts(int organizationID);
        Products GetProductById(int organizationID, int Id);
        Products GetProductWithDetails(int organizationID, int Id);
        void CreateProduct(Products product);
        void UpdateProduct(Products product);
        void DeleteProduct(Products product);
    }
}
