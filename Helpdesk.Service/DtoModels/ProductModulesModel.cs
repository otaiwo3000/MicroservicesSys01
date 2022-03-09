using Helpdesk.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Helpdesk.Service.DtoModels
{
    public class ModuleIDs
    {
        [Required(ErrorMessage = "Module is required")]
        public int ModuleId { get; set; }
    }

    public class ProductModues_List
    {
        [Required(ErrorMessage = "Product is required")]
        public int ProductId { get; set; }

        public List<ModuleIDs> ModuleIds { get; set; }
    }


    public abstract class ProductModulesAbstract
    {
        [Required(ErrorMessage = "Product is required")]
        public int ProductId { get; set; }

        public List<ModuleIDs> ModuleIds { get; set; }
    }


    public class ProductModulesModel : ProductModulesAbstract
    {
        //public int ProductModuleId { get; set; }
        public virtual Products Product { get; set; }
        public virtual Modules Module { get; set; }
        public UsersModel Owner { get; set; }
        public OrganizationModel Organization { get; set; }
    }

    public class ProductModulesCreateModel : ProductModulesAbstract
    {

    }

    public class ProductModulesUpdateModel : ProductModulesAbstract
    {

    }

    public class ProModModel
    {
        public int ProductModuleId { get; set; }
        public int ProductId { get; set; }
        public int ModuleId { get; set; }
    }
}
