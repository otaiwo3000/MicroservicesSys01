using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Helpdesk.Service.Impl
{
    public class MailTemplateImpl
    {
        public string MessageBody(string mailbody)
        {         
            string mailtemplate = "";
            ////Read template file from the App_Data folder
            ////using (var sr = new StreamReader(Server.MapPath("\\App_Data\\Templates\") + "WelcomeEmail.txt"))
            string File_baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            //string file_path = File_baseDirectory + "TemplateFiles/EmailTemplates/pi360mpr_usercreation_emailtemplate.txt";
            //string file_path = File_baseDirectory + "TemplateFiles/EmailTemplates/pi360mpr_usercreation_emailtemplate.txt";
            //using (StreamReader SourceReader = System.IO.File.OpenText(file_path))
            //{
            //    mailtemplate = SourceReader.ReadToEnd();
            //}

            mailtemplate = mailbody;
            string messageBody = string.Format(mailtemplate, "FintrakLogin", "appURLHere", "CMP", "FIntrak", "mypassword");


            return messageBody;
            //try
            //{
            //    SendNotification.sendemail(accountModel.UserSetup.Email, "User Creation", messageBody);
            //}
            //catch (Exception ex)
            //{
            //    SendNotification.sendemail(accountModel.UserSetup.Email, "User Creation", messageBody);
            //    //SendNotification.SaveEmail(accountModel.UserSetup.Email, "User Creation", mailcontent);
            //}
        }
    }
}
