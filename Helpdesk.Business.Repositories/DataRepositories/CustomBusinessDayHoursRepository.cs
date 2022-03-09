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
    public class CustomBusinessDayHoursRepository : RepositoryBase<CustomBusinessDayHours>, ICustomBusinessDayHoursRepository
    {
        public CustomBusinessDayHoursRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<CustomBusinessDayHours> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<CustomBusinessDayHours> _repo;


        public  IEnumerable<CustomBusinessDayHours> GetAllCustomBusinessDayHours(int organizationID)
        {
            var res = FindAll().Where(x => x.OrganizationId == organizationID).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            return res;
        }

        public CustomBusinessDayHours GetCustomBusinessDayHourById(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.CustomBusinessDayHourId==Id).FirstOrDefault();
        }

        public CustomBusinessDayHours GetCustomBusinessDayHourWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.CustomBusinessDayHourId==Id)
                .Include(y => y.Organization).FirstOrDefault();
        }

        public void CreateCustomBusinessDayHour(CustomBusinessDayHours cbdh)
        {
            Create(cbdh);
        }

        public void UpdateCustomBusinessDayHour(CustomBusinessDayHours cbdh)
        {
            Update(cbdh);
        }

        public void DeleteCustomBusinessDayHour(CustomBusinessDayHours cbdh)
        {
            Delete(cbdh);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override


    }
}
