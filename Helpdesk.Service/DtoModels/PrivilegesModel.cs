using System;
using System.ComponentModel.DataAnnotations;


namespace Helpdesk.Service.DtoModels
{
    public abstract class PrivilegesAbstract
    {

    }


    public class PrivilegesModel : PrivilegesAbstract
    {
        public int PrivilegeId { get; set; }
        public string Name { get; set; }
    }

    public class PrivilegesCreateModel : PrivilegesAbstract
    {
       
    }

    public class PrivilegesUpdateModel : PrivilegesAbstract
    {
       
    }
}
