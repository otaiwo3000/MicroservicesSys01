using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Shared.Entities.Enums
{
    public enum AgentScope
    {
        GlobalAccess = 1,       //Can access all tickets in Helpdesk
        GroupAccess = 2,        //Can view and edit tickets in their group(s) and tickets assigned to them
        RestrictedAccess = 3   //Can only access the tickets assigned to them
    }

    public enum AgentStatus
    {
        Active = 1,
        Inactive = 2,       //eg on leave, not currently/presently available
        Deleted = 3,        //eg when the agent is marked as deleted on the table
        Left = 4,           //eg when the agent has left the organization
    }
}
