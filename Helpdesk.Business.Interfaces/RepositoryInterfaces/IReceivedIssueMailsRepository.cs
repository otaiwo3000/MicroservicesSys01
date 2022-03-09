using Helpdesk.Shared.Entities;
using System.Collections.Generic;

namespace Helpdesk.Business.Interfaces.RepositoryInterfaces
{

    public interface IReceivedIssueMailsRepository : IRepositoryBase<ReceivedIssueMails>
    {
        IEnumerable<ReceivedIssueMails> GetAllReceivedIssueMails(int organizationID);
        IEnumerable<ReceivedIssueMails> GetReceivedIssueMails(int organizationID, bool IsTreated);
        IEnumerable<ReceivedIssueMails> GetReceivedIssueMailsByStatus(bool IsTreated);
        ReceivedIssueMails GetReceivedIssueMailById(int organizationID, int Id);
        ReceivedIssueMails GetReceivedIssueMailWithDetails(int organizationID, int Id);
        ReceivedIssueMails GetReceivedIssueMailBySubject(int organizationID, string subject, string mailfrom);
        void CreateReceivedIssueMail(ReceivedIssueMails at);
        void UpdateReceivedIssueMail(ReceivedIssueMails at);
        void DeleteReceivedIssueMail(ReceivedIssueMails at);
    }
}
