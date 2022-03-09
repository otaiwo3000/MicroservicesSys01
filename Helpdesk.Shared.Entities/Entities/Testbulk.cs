using Helpdesk.Shared.Common.Contracts;
using Helpdesk.Shared.Common.Utilities;
using System;
using System.Collections.Generic;

namespace Helpdesk.Shared.Entities
{
    public partial class TestBulk : EntityBase, IIdentifiableEntity
    {
        public int TestBulkId { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }

        public int EntityId
        {
            get
            {
                return TestBulkId;
            }
        }
    }
}
