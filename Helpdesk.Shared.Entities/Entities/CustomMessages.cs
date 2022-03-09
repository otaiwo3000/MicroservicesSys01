using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Shared.Entities
{
    public class CustomMessages : EntityBase, IIdentifiableEntity
    {
        public int CustomMessageId { get; set; }
        public string Subject { get; set; }
        public string MessageContent { get; set; }
        public string Comment { get; set; }
        public int GroupId { get; set; }
        public Groups Group { get; set; }
        public int OrganizationId { get; set; }
        public virtual Organizations Organization { get; set; }

        public int EntityId
        {
            get
            {
                return CustomMessageId;
            }
        }
    }
}
