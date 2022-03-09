using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Shared.Common.Utilities
{
    public enum UserScope
    {
        GlobalAccess = 1,       //Can access all recordas
        GroupAccess = 2,        //Can view and edit records in their group(s) and records partaining to them
        RestrictedAccess = 3   //Can only access the records partaining to them
    }


}
