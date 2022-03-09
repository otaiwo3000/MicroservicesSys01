using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Shared.Entities.Enums
{
    public enum SLAPosition
    {
        ResolvedWithinSLA = 1,
        ResolvedOutsideSLA = 2,
        ClosedWithinSLA = 3,
        ClosedOutsideSLA = 4,
        NotResolved = 5
    }
}
