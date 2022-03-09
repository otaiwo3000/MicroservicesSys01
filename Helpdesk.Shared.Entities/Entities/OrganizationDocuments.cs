using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Shared.Entities
{
    public partial class OrganizationDocuments : EntityBase, IIdentifiableEntity
    {
        //public Groups()
        //{
        //    Tickets = new HashSet<Tickets>();
        //}

        public int OrganizationDocumentId { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Group is required")]
        public int GroupId { get; set; }
        public virtual Groups Group { get; set; }

        [Required(ErrorMessage = "File Name is required")]
        public string FileName { get; set; }

        [Required(ErrorMessage = "Alias is required")]
        public string Alias { get; set; }

        ////[Required(ErrorMessage = "File Name is required")]
        //public string ParameterKey { get; set; }

        [Required(ErrorMessage = "FileT ype is required")]
        public string FileType { get; set; }

        public int Position { get; set; }

        public bool Visible { get; set; }

        [Required(ErrorMessage = "View Right is required")]
        public string ViewRights { get; set; }

        public int OrganizationId { get; set; }
        public virtual Organizations Organization { get; set; }
        //public virtual ICollection<Tickets> Tickets { get; set; }

        public int EntityId
        {
            get
            {
                return OrganizationDocumentId;
            }
        }
    }
}
