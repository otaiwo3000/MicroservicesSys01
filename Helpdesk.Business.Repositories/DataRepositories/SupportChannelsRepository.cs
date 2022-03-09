using Helpdesk.Shared.Entities;
using Helpdesk.Shared.DataAccess.DBContext;
using Helpdesk.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Helpdesk.Business.Interfaces.RepositoryInterfaces;


namespace Helpdesk.Business.Repositories.DataRepositories
{
    public class SupportChannelsRepository : RepositoryBase<SupportChannels>, ISupportChannelsRepository
    {
        public SupportChannelsRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<SupportChannels> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<SupportChannels> _repo;


        public  IEnumerable<SupportChannels> GetAllSupportChannels()
        {
            var res = FindAll().ToList();
            return res;
        }

        public SupportChannels GetSupportChannelById(int Id)
        {
            return FindByCondition(x => x.SupportChannelId == Id).FirstOrDefault();
        }

        public SupportChannels GetSupportChannelWithDetails(int Id)
        {
            return FindByCondition(x => x.SupportChannelId == Id).FirstOrDefault();
        }

        public void CreateSupportChannel(SupportChannels supportchannel)
        {
            Create(supportchannel);
        }

        public void UpdateSupportChannel(SupportChannels supportchannel)
        {
            Update(supportchannel);
        }

        public void DeleteSupportChannel(SupportChannels supportchannel)
        {
            Delete(supportchannel);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override


    }
}
