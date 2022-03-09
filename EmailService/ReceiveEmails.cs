using Spire.Email;
using Spire.Email.Pop3;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EmailService
{
    public static class ReceiveEmails
    {
        public static ReceivedEmail ReceiveEmailImpl(string host, string username, string password, int port, bool enablessl)
        {
            try
            {
                Pop3Client client = new Pop3Client();

                client.Host = host;
                client.Username = username;
                client.Password = password;
                //client.Host = "pop-mail.outlook.com";
                //client.Username = "taiwo.oyewunmi@fintraksoftware.com";
                //client.Password = "@123";
                client.Port = port;
                client.EnableSsl = enablessl;
                client.Connect();

                var count = client.GetMessageCount();

                MailMessage message = client.GetMessage(count);

                ReceivedEmail emailResult = new ReceivedEmail()
                {
                    EMailSubject = message.Subject,
                    EMailFrom = Convert.ToString(message.From),
                    EMailTo = Convert.ToString(message.To),
                    EMailCC = Convert.ToString(message.Cc),
                    EMailBodyText = message.BodyText,
                    EMailDateTime = DateTime.Parse(message.Date.ToString(CultureInfo.InvariantCulture))
                };

                return emailResult;
            }
            catch(Exception ex)
            {
                ReceivedEmail emailResult = new ReceivedEmail()
                {
                    EMailSubject = "DEFAULT",
                    EMailFrom = "DEFAULT",
                    EMailTo = "DEFAULT",
                    EMailDateTime = DateTime.Now
                };

                return emailResult;
            }
        }
    }
  

    public class ReceivedEmail
    {
        public string EMailSubject { get; set; }
        public string EMailFrom { get; set; }
        public string EMailTo { get; set; }
        public string EMailCC { get; set; }
        public string EMailBodyText { get; set; }
        public DateTime EMailDateTime { get; set; }
    }
}
