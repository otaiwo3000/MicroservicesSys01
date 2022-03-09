
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net.Mail;
using UserMgt.Business.Interfaces;
using UserMgt.Business.Logger;
using UserMgt.Shared.Common.Utilities;

namespace UserMgt.Service.Impl
{
    public class NetMailImpl
    {
        private IRepositoryWrapper _repository;
        private IConfiguration _config;
        private readonly ILoggerManagerRepository _logger;


        public NetMailImpl(IRepositoryWrapper repository, IConfiguration config, ILoggerManagerRepository logger)
        {
            _repository = repository;
            _config = config;
            _logger = logger;
        }

        public bool SendPendingEmails()
        {
            bool res = false;

            string Sender = Convert.ToString(_config["EmailConfiguration:From"]);
            bool EnableSsl = bool.Parse(_config["EmailConfiguration:EnableSsl"]);
            bool SmtpUseDefaultCredentials = bool.Parse(_config["EmailConfiguration:SmtpUseDefaultCredentials"]);
            string SmtpServer = Convert.ToString(_config["EmailConfiguration:SmtpServer"]);
            int Port = int.Parse(_config["EmailConfiguration:Port"]);
            string Username = Convert.ToString(_config["EmailConfiguration:Username"]);
            string Password = Convert.ToString(_config["EmailConfiguration:Password"]);

            int toppendingemails = int.Parse(_config["toppendingemails"]);

            var emails = _repository.pendingemail.PendingemailList(toppendingemails).ToList();

            foreach(var p in emails)
            {
                char[] spearator = { ',', ';', '<', '>', '"', '"' };
                var vv = Splitting.SplitString(p.RecepientEmails, spearator).ToList();
                var vv2 = vv.Where(x => x.Contains("@"));

                //for troubleshooting starts
                foreach (var a in vv2)
                {
                    _logger.LogInformation($"{a}");
                }

                _logger.LogInformation($"About to call ConnectAndSendMail");

                var bb = ConnectAndSendMail(Sender, vv2.ToArray(), p.MailSubject, p.MailContent, SmtpServer, Port, Username, Password, EnableSsl);

                _logger.LogInformation($"This loop is successful");

                p.IsSent = true;
                p.RetryCount = p.RetryCount + 1;

                _repository.pendingemail.UpdatePendingEmail(p);
                _repository.Save();
            }

            _logger.LogInformation($"Loop completed");
            return res;
        }

        public bool ConnectAndSendMail(string sender, string[] toEmails, string mailSubj, string mailcontent, string SmtpServer, int SmtpPort, string UserName, string Password, bool EnableSsl)
        {
            bool res = false;
            try
            {
                using (MailMessage message = new MailMessage())
                {
                    message.From = new MailAddress(sender);

                    ////message.To.Add("taiwo.oyewunmi@fintraksoftware.com");
                    ////message.To.Add("otaiwo3001@gmail.com");
                    //string[] toEmails = recepient.Split(',');
                    foreach (string email in toEmails)
                    {
                        message.To.Add(email);
                    }

                    // Include some non-ASCII characters in body and subject.
                    //string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
                    //message.Body += Environment.NewLine + someArrows; 
                    message.Body = mailcontent;
                    message.BodyEncoding = System.Text.Encoding.UTF8;
                    //message.Subject = "test message 1" + someArrows;
                    message.Subject = mailSubj;
                    message.SubjectEncoding = System.Text.Encoding.UTF8;

                    //SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
                    SmtpClient smtpClient = new SmtpClient(SmtpServer, SmtpPort);
                    //System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("otaiwo3000@gmail.com", "cisco@cisco@cisco@cisco");
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(UserName, Password);
                    smtpClient.Credentials = credentials;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //smtpClient.EnableSsl = true;
                    smtpClient.EnableSsl = EnableSsl;
                    smtpClient.Send(message);
                    res = true;
                    _logger.LogInformation("Mail are sent successfully;");

                    message.Dispose();
                }
                return res;
            }
            catch(Exception ex)
            {
                _logger.LogInformation("Mail is not sent;");
                _logger.LogError($"Exception message: {ex.Message}");
                _logger.LogError($"Inner Exception: {ex.InnerException}");
            }
            return res;
        }

        public void SendEmails(string[] mailrecepients, string mailsubject, string mailcontent)
        {
            string Sender = Convert.ToString(_config["EmailConfiguration:From"]);
            bool EnableSsl = bool.Parse(_config["EmailConfiguration:EnableSsl"]);
            bool SmtpUseDefaultCredentials = bool.Parse(_config["EmailConfiguration:SmtpUseDefaultCredentials"]);
            string SmtpServer = Convert.ToString(_config["EmailConfiguration:SmtpServer"]);
            int Port = int.Parse(_config["EmailConfiguration:Port"]);
            string Username = Convert.ToString(_config["EmailConfiguration:Username"]);
            string Password = Convert.ToString(_config["EmailConfiguration:Password"]);

            //var mailrecepients = new string[] { "otaiwo3001@gmail.com" };

            NetMailImpl nm = new NetMailImpl(_repository, _config, _logger);
            //nm.ConnectAndSendMail(Sender, mailArray, messagecontent.Subject, custommessagebody, SmtpServer, Port, Username, Password, EnableSsl);
            nm.ConnectAndSendMail(Sender, mailrecepients, mailsubject, mailcontent, SmtpServer, Port, Username, Password, EnableSsl);
        }
    }

    public class EmailFields
    {
        public string Recepient { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }

}
