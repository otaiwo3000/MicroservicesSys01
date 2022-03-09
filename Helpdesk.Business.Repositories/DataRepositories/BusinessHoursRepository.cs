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
    public class BusinessHoursRepository : RepositoryBase<BusinessHours>, IBusinessHoursRepository
    {
        public BusinessHoursRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<BusinessHours> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<BusinessHours> _repo;


        public  IEnumerable<BusinessHours> GetAllBusinessHours(int organizationID)
        {
            var res = FindAll().Where(x => x.OrganizationId == organizationID).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            return res;
        }

        public BusinessHours GetBusinessHourById(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.BusinessHourId==Id).FirstOrDefault();
        }

        public BusinessHours GetBusinessHourWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.BusinessHourId==Id)
                .Include(y => y.Organization).FirstOrDefault();
        }

        public void CreateBusinessHour(BusinessHours cbdh)
        {
            Create(cbdh);
        }

        public void UpdateBusinessHour(BusinessHours cbdh)
        {
            Update(cbdh);
        }

        public void DeleteBusinessHour(BusinessHours cbdh)
        {
            Delete(cbdh);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override


    }
}
