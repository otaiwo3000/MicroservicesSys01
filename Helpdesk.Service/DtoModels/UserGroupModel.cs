using Helpdesk.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Helpdesk.Service.DtoModels
{
    public abstract class UserGroupAbstract
    {
        [Required(ErrorMessage = "User is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Group is required")]
        public int GroupId { get; set; }

    }


    public class UserGroupModel : UserGroupAbstract
    {
       public virtual Users user { get; set; }
       public virtual Groups group { get; set; }
    }

    public class UserGroupCreateModel : UserGroupAbstract
    {
      
    }

    public class UserGroupUpdateModel : UserGroupAbstract
    {
      
    }
}
