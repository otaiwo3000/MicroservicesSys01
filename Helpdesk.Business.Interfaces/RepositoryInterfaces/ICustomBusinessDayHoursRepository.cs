using Helpdesk.Shared.Entities;
using System.Collections.Generic;

namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{

    public interface ICustomBusinessDayHoursRepository : IRepositoryBase<CustomBusinessDayHours>
    {
        IEnumerable<CustomBusinessDayHours> GetAllCustomBusinessDayHours(int organizationID);
        CustomBusinessDayHours GetCustomBusinessDayHourById(int organizationID, int Id);
        CustomBusinessDayHours GetCustomBusinessDayHourWithDetails(int organizationID, int Id);
        void CreateCustomBusinessDayHour(CustomBusinessDayHours cbdh);
        void UpdateCustomBusinessDayHour(CustomBusinessDayHours cbdh);
        void DeleteCustomBusinessDayHour(CustomBusinessDayHours cbdh);
    }
}
