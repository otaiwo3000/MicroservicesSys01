using Helpdesk.Shared.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Service.DtoModels
{
    public abstract class OrganizationDocumentsAbstract
    {
        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "File Name is required")]
        public string FileName { get; set; }

        [Required(ErrorMessage = "Alias is required")]
        public string Alias { get; set; }

        public string ParameterKey { get; set; }

        [Required(ErrorMessage = "FileT ype is required")]
        public string FileType { get; set; }

        public int Position { get; set; }

        public bool Visible { get; set; } = false;

        [Required(ErrorMessage = "View Right is required")]
        public int ViewRights { get; set; }
    }


    public class OrganizationDocumentsModel : OrganizationDocumentsAbstract
    {
        public int OrganizationDocumentId { get; set; }
        public virtual Groups Group { get; set; }
    }

    public class OrganizationDocumentsCreateModel : OrganizationDocumentsAbstract
    {
        [Required(ErrorMessage = "Group is required")]
        public int GroupId { get; set; }
    }

    public class OrganizationDocumentsUpdateModel : OrganizationDocumentsAbstract
    {
        [Required(ErrorMessage = "Group is required")]
        public int GroupId { get; set; }
    }
}
