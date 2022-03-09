using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Helpdesk.Shared.Entities
{
    public partial class ReceivedIssueMails : EntityBase, IIdentifiableEntity
    {
        public int Id { get; set; }

        public string EMailSubject { get; set; }
        public string EMailFrom { get; set; }
        public string EMailTo { get; set; }
        public string EMailCC { get; set; }
        public string EMailBodyText { get; set; }
        public DateTime EMailDateTime { get; set; }
        public bool IsTreated { get; set; }

        public int OrganizationId { get; set; }
        public virtual Organizations Organization { get; set; }

        public int EntityId
        {
            get
            {
                return Id;
            }
        }
    }
}
