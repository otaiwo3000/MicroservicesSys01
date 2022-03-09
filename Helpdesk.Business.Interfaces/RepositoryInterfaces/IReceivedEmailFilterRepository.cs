using Helpdesk.Shared.Entities;
using System.Collections.Generic;


namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{

    public interface IReceivedEmailFilterRepository : IRepositoryBase<ReceivedEmailFilter>
    {
        IEnumerable<ReceivedEmailFilter> GetAllReceivedEmailFilter(int organizationID);
        ReceivedEmailFilter GetReceivedEmailFilterById(int organizationID, int Id);
        ReceivedEmailFilter GetReceivedEmailFilterWithDetails(int organizationID, int Id);
        void CreateReceivedEmailFilter(ReceivedEmailFilter re);
        void UpdateReceivedEmailFilter(ReceivedEmailFilter re);
        void DeleteReceivedEmailFilter(ReceivedEmailFilter re);
    }
}
