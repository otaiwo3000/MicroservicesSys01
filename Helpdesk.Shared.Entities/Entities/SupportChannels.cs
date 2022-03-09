using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;

namespace Helpdesk.Shared.Entities
{
    public partial class SupportChannels : EntityBase, IIdentifiableEntity
    {
        public int SupportChannelId { get; set; }
        public string Name { get; set; }

        public int EntityId
        {
            get
            {
                return SupportChannelId;
            }
        }
    }
}
