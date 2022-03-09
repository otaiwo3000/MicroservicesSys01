using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;

namespace Helpdesk.Shared.Entities 
{
    public partial class Days : EntityBase, IIdentifiableEntity
    {
        public int DayId { get; set; }
        public string Name { get; set; }

        public int EntityId
        {
            get
            {
                return DayId;
            }
        }
    }
}
