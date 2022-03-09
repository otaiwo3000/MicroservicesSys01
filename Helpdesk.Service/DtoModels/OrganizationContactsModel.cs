using System;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Service.DtoModels
{
    public abstract class OrganizationContactsAbstract
    {
        public string ContactType { get; set; }

        [Required(ErrorMessage = "Contact is required")]
        public string ContactValue { get; set; }
        public bool PreferredContact { get; set; } //IsPreferredContact
        public bool IsSupportContact { get; set; } //IsPreferredContact
    }


    public class OrganizationContactsModel : OrganizationContactsAbstract
    {
        public int OrganizationContactId { get; set; }
        public OrganizationModel Organization { get; set; }
     
    }

    public class OrganizationContactsCreateModel : OrganizationContactsAbstract
    {
       
    }

    public class OrganizationContactsUpdateModel : OrganizationContactsAbstract
    {

    }
}
