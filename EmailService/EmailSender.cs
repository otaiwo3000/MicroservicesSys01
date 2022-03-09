using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        //injecting email configuration into EmailSender class 
        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        //public void SendEmail(Message message)
        public string SendEmail(Message message)
        {
            string res = "failed";
            var emailMessage = CreateEmailMessage(message);
            res = Send(emailMessage);
            return res;
        }

        //this method create an object of type MimeMessage and also used to configure the required properties as below
        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            return emailMessage;
        }

        ////the object above is passed to the below. And this method below uses the SmtpClient class to connect to the email server, authenticate and send the email.
        ////the SmtpClient class comes from the MailKit.Net.Smtp namespace. So, we should use that one instead of System.Net.Mail.
        //private void Send(MimeMessage mailMessage)
        private string Send(MimeMessage mailMessage)
        {
            string res = "failed";
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                    client.Send(mailMessage);
                    res = "successful";
                }
                catch (Exception ex)
                {
                    ////log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
            return res;
        }

    }
}
