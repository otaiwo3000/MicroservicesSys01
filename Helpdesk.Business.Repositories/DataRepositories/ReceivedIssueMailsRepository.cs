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
    public class ReceivedIssueMailsRepository : RepositoryBase<ReceivedIssueMails>, IReceivedIssueMailsRepository
    {
        public ReceivedIssueMailsRepository(HelpDeskDBContext helpDeskDBContext, IRepositoryBase<ReceivedIssueMails> repo)
           : base(helpDeskDBContext)
        {
            _repo = repo;
        }
        IRepositoryBase<ReceivedIssueMails> _repo;


        public  IEnumerable<ReceivedIssueMails> GetAllReceivedIssueMails(int organizationID)
        {
            var res = FindAll().Where(x => x.OrganizationId == organizationID).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            return res;
        }

        public IEnumerable<ReceivedIssueMails> GetReceivedIssueMails(int organizationID, bool IsTreated)
        {
            var res = FindAll().Where(x => x.OrganizationId == organizationID && x.IsTreated==IsTreated).Include(x => x.Organization).OrderBy(y => y.Organization.Name).ToList();
            return res;
        }

        public IEnumerable<ReceivedIssueMails> GetReceivedIssueMailsByStatus(bool IsTreated)
        {
            var res = FindAll().Where(x=>x.IsTreated == IsTreated).ToList();
            return res;
        }

        public ReceivedIssueMails GetReceivedIssueMailById(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.Id==Id).FirstOrDefault();
        }

        public ReceivedIssueMails GetReceivedIssueMailWithDetails(int organizationID, int Id)
        {
            return FindByCondition(x => x.OrganizationId == organizationID && x.Id==Id)
                .Include(y => y.Organization).FirstOrDefault();
        }

        public ReceivedIssueMails GetReceivedIssueMailBySubject(int organizationID, string subject, string mailfrom)
        {
            return FindByCondition(x => x.OrganizationId == organizationID &&
                x.EMailSubject.Trim().ToLower() == subject.Trim().ToLower() && x.EMailFrom.Trim().ToLower() == mailfrom.Trim().ToLower())
                    .Include(y => y.Organization).FirstOrDefault();

            //return FindByCondition(x => x.OrganizationId == organizationID &&
            //x.EMailSubject.Trim().ToLower() == subject.Trim().ToLower() &&
            //Helpdesk.Shared.Common.Utilities.StringExtraction.ExtractASubstringBtwTwoXters(x.EMailFrom.Trim().ToLower(), "<", ">") ==
            //Helpdesk.Shared.Common.Utilities.StringExtraction.ExtractASubstringBtwTwoXters(mailfrom.Trim().ToLower(), "<", ">"))
            //    .Include(y => y.Organization).FirstOrDefault();
        }
       
        public void CreateReceivedIssueMail(ReceivedIssueMails aet)
        {
            Create(aet);
        }

        public void UpdateReceivedIssueMail(ReceivedIssueMails aet)
        {
            Update(aet);
        }

        public void DeleteReceivedIssueMail(ReceivedIssueMails aet)
        {
            Delete(aet);
        }

        //NOTE NOTE: you cannot override method that is not marked virtual, abstract, override


    }
}
