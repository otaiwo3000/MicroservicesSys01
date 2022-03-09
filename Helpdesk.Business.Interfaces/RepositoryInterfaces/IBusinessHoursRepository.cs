using Helpdesk.Shared.Entities;
using System.Collections.Generic;

namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{

    public interface IBusinessHoursRepository : IRepositoryBase<BusinessHours>
    {
        IEnumerable<BusinessHours> GetAllBusinessHours(int organizationID);
        BusinessHours GetBusinessHourById(int organizationID, int Id);
        BusinessHours GetBusinessHourWithDetails(int organizationID, int Id);
        void CreateBusinessHour(BusinessHours cbdh);
        void UpdateBusinessHour(BusinessHours cbdh);
        void DeleteBusinessHour(BusinessHours cbdh);
    }
}
