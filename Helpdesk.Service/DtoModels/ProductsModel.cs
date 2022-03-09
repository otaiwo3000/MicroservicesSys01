using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Helpdesk.Service.DtoModels
{
    public abstract class ProductsAbstract
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }


    public class ProductsModel : ProductsAbstract
    {
        public int ProductId { get; set; }
        public UsersModel Owner { get; set; }
        public OrganizationModel Organization { get; set; }
    }

    public class ProductsCreateModel : ProductsAbstract
    {

    }

    public class ProductsUpdateModel : ProductsAbstract
    {

    }
}
