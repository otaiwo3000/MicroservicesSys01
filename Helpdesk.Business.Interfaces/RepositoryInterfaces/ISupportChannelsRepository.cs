using Helpdesk.Shared.Entities;
using System.Collections.Generic;


namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{

    public interface ISupportChannelsRepository : IRepositoryBase<SupportChannels>
    {
        IEnumerable<SupportChannels> GetAllSupportChannels();
        SupportChannels GetSupportChannelById(int Id);
        SupportChannels GetSupportChannelWithDetails(int Id);
        void CreateSupportChannel(SupportChannels sc);
        void UpdateSupportChannel(SupportChannels sc);
        void DeleteSupportChannel(SupportChannels sc);
    }
}
