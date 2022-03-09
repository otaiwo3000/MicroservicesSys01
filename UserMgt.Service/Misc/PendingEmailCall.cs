
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserMgt.Business.EmailService;
using UserMgt.Business.Interfaces;
using UserMgt.Business.Logger;
using UserMgt.Shared.Common.Utilities;

namespace UserMgt.Service.Misc
{
    public class PendingEmailCall
    {
        private readonly IEmailSender _emailSender;
        private IRepositoryWrapper _repository;
        private ILoggerManagerRepository _logger;

        public PendingEmailCall(IEmailSender emailSender, IRepositoryWrapper repository, ILoggerManagerRepository logger)
        {
            _emailSender = emailSender;
            _repository = repository;
            _logger = logger;
        }

        public void SendPendingEmail(int topmails)
        {
            try
            {
                _logger.LogInformation($"Inside PendingEmailCall>>SendPendingEmail");

                var pendingemailList = _repository.pendingemail.PendingemailList(topmails).ToList();  //NOTE: ToList() is very necessary otherwise, it will not be able to use this connection session for the next connection to the same table (PendingEmail table)for update. It will sat that existing connection need to be closed first (ie u need to close the existing connection before u can save your data).
                _logger.LogInformation($"pendingemailList count: {pendingemailList.Count()}");

                foreach (var p in pendingemailList)
                {
                    char[] spearator = { ',', ';', '<', '>', '"', '"' };
                    var vv = Splitting.SplitString(p.RecepientEmails, spearator).ToList();
                    var vv2 = vv.Where(x => x.Contains("@"));
                    ////string[] arrayOfRecepientEmailAddresses = Splitting.SplitString(p.RecepientEmails, spearator);
                    //string[] arrayOfRecepientEmailAddresses = vv2.ToArray();

                    //for troubleshooting starts
                    foreach (var a in vv2)
                    {
                        _logger.LogInformation($"{a}");
                    }
                    //ends

                    ////var message = new Message(new string[] { "test@gmail.com" }, "Test email", "This is the content from our email.");
                    //var message = new Message(arrayOfRecepientEmailAddresses, p.MailSubject, p.MailContent);
                    var message = new Message(vv2, p.MailSubject, p.MailContent);
                    _emailSender.SendEmail(message);

                    p.IsSent = true;
                    p.RetryCount = p.RetryCount + 1;

                    _repository.pendingemail.UpdatePendingEmail(p);
                    _repository.Save();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }
    }
}
