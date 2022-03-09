using Helpdesk.Shared.Entities;
using Helpdesk.Shared.DataAccess.DBContext;
using Helpdesk.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Helpdesk.Business.Interfaces.RepositoryInterfaces;


namespace Helpdesk.Business.Repositories.DataRepositories
{
    public class CustomMessagesRepository : RepositoryBase<CustomMessages>, ICustomMessagesRepository
    {
        public CustomMessagesRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<CustomMessages> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<CustomMessages> _repo;


        public  IEnumerable<CustomMessages> GetAllCustomMessages(int organizationID)
        {
            var res = FindAll().Where(x => x.OrganizationId == organizationID)
                .Include(x => x.Organization)
                .Include(x=>x.Group)
                .OrderBy(y => y.Organization.Name).ToList();
            return res;
        }

        public CustomMessages GetCustomMessageById(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.CustomMessageId == Id).FirstOrDefault();
        }

        public CustomMessages GetCustomMessageWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.CustomMessageId == Id)
                .Include(y => y.Organization).FirstOrDefault();
        }

        public void CreateCustomMessage(CustomMessages cm)
        {
            Create(cm);
        }

        public void UpdateCustomMessage(CustomMessages cm)
        {
            Update(cm);
        }

        public void DeleteCustomMessage(CustomMessages cm)
        {
            Delete(cm);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override


    }
}
