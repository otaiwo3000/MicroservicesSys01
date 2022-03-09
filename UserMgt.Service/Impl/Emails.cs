
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMgt.Business.Interfaces;
using UserMgt.Business.Logger;

namespace UserMgt.Service.Impl
{
    public class Emails
    {
        private IRepositoryWrapper _repository;
        private IConfiguration _config;
        private readonly ILoggerManagerRepository _logger;

        public Emails(IRepositoryWrapper repository, IConfiguration config, ILoggerManagerRepository logger)
        {
            _repository = repository;
            _config = config;
            _logger = logger;
        }

        ////public void GetMail(IRepositoryWrapper repository, IConfiguration config)
        //public void GetMail()
        //{
        //    _logger.LogInformation($"Inside Emails>>GetMail method");
        //    //_repository = repository;
        //    //_config = config;

        //    //string connectionString = configuration["HelpDeskDBConnection:connectionString"];
        //    string host = Convert.ToString(_config["ReceivingEmailConfiguration:Host"]);
        //    string username = Convert.ToString(_config["ReceivingEmailConfiguration:Username"]);
        //    string password = Convert.ToString(_config["ReceivingEmailConfiguration:Password"]);
        //    int port = int.Parse(_config["ReceivingEmailConfiguration:Port"]);
        //    bool enablessl = bool.Parse(_config["ReceivingEmailConfiguration:EnableSsl"]);

        //    _logger.LogInformation($"about to call GetUserByUserName method");
        //    var organization = _repository.users.GetUserByUserName(username);

        //    _logger.LogInformation($"abount to call ReceiveEmailImpl method");
        //    var emailres = ReceiveEmails.ReceiveEmailImpl(host, username, password, port, enablessl);

        //    _logger.LogInformation($"about to declare rim object");
        //    ReceivedIssueMails rim = new ReceivedIssueMails()
        //    {
        //        EMailSubject = emailres.EMailSubject,
        //        EMailFrom = emailres.EMailFrom,
        //        EMailTo = emailres.EMailTo,
        //        EMailCC = emailres.EMailCC,
        //        EMailBodyText = emailres.EMailBodyText,
        //        EMailDateTime = emailres.EMailDateTime,
        //        OrganizationId = organization.OrganizationId,
        //        IsTreated = false
        //    };

        //    _logger.LogInformation($"about to call GetReceivedIssueMailBySubject method");

        //    ReceivedIssueMails emailbysubject =_repository.receivedissuemails.GetReceivedIssueMailBySubject(organization.OrganizationId, emailres.EMailSubject, emailres.EMailFrom);
        //    if (emailbysubject == null)
        //    {
        //        List<string> exceptionWords = _repository.receivedemailfilter.GetAllReceivedEmailFilter(organization.OrganizationId).Where(x => x.IsEnabled == true).Select(x => x.Word.Trim().ToLower()).ToList();

        //        //if exception words are listed in the DB/table, use the words to filter the email as done below:
        //        if(exceptionWords.Count > 0)
        //        {
        //            //char[] spearator = { ',', ';' };
        //            char[] spearator = { ' ', ',', ';', ':', '-' };
        //            if (!exceptionWords.Any(x => emailbysubject.EMailSubject.Split(spearator, StringSplitOptions.RemoveEmptyEntries).Any(y => y == x)))
        //            {
        //                _repository.receivedissuemails.CreateReceivedIssueMail(rim);
        //                _repository.Save();

        //                SupportAutoResponseMail(emailres.EMailFrom, emailres.EMailSubject);
        //            }
        //        }
        //        //if exception words returns empty, just go ahead and do the below:
        //        else
        //        {
        //            _repository.receivedissuemails.CreateReceivedIssueMail(rim);
        //            _repository.Save();

        //            SupportAutoResponseMail(emailres.EMailFrom, emailres.EMailSubject);
        //        }
        //    }
        //    _logger.LogInformation($"Emails>>GetMail call is successful");
        //}

        //public void SupportAutoResponseMail(string mailrecepient, string mailsubject)
        //{
        //    var recepientArray = new string[] { mailrecepient };

        //    string mailcontent = string.Format($"Hi {mailrecepient}" + Environment.NewLine + Environment.NewLine + "We would like to acknowledge that we have received your request and a ticket has been created." + Environment.NewLine + Environment.NewLine + "A support representative will be reviewing your request and will send you a personal response.(usually within 24 hours)."
        //          + Environment.NewLine + Environment.NewLine + "Thank you for your patience."
        //          + Environment.NewLine + Environment.NewLine + "Sincerely,"
        //          + Environment.NewLine + "FinTrak Software Support Team");

        //    NetMailImpl nm = new NetMailImpl(_repository, _config, _logger);
        //    nm.SendEmails(recepientArray, mailsubject, mailcontent);
        //}
    }
}
