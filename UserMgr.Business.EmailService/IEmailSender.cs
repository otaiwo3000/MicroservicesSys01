using System;
using System.Collections.Generic;
using System.Text;

namespace UserMgt.Business.EmailService
{
    public interface IEmailSender
    {
        //void SendEmail(Message message);
        string SendEmail(Message message);
    }
}
