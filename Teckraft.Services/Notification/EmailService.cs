using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Teckraft.Core.Domian.Loging;
using Teckraft.Core.Domian.Transactions;
using Teckraft.Core.Workflow;

namespace Teckraft.Services.Notification
{
    public class EmailService:IEmailService
    {

        private readonly IService<EventLog> logService;
        public EmailService(IService<EventLog> logService) {
            this.logService = logService;
        }
        //public void SendEmail(EmailTemplate emailTemplate, string FromEmail, String ToEmail, string CCEmail, CR cr, Task task) {
        //    if (string.IsNullOrEmpty(FromEmail)) FromEmail = "crsystem@pidilite.com";
        //AsyncCallback callback=new AsyncCallback(it=>{
        //    try
        //    {
        //        var obj = it as Teckraft.Core.Workflow.EmailWriter.SendMailAsyncResult;
        //        if (obj.Error != null)
        //        {
        //            var log = new EventLog() { RCB = task.RUBUser, EventType = "Exception", ApplicationName = "EmailService", Details = "Error Sending Email" + obj.Error.Message, RCT = DateTime.Now, RUT = DateTime.Now };
        //            logService.Add(log);

        //        }
        //    }
        //    catch { }
        
        //});
        //IDictionary<String, String> tokens = new Dictionary<string, string>();

        //cr.GetType().GetProperties().ToList().ForEach(it =>
        //{
        //    if (it.GetValue(cr) is DateTime) {
        //        DateTime dt = (DateTime)it.GetValue(cr);
        //        tokens.Add(it.Name,
        //    dt.ToString("dd-MMM-yyyy"));

        //    }
        //    else if (it.Name == "PendingWithUser") {
        //        if(it.GetValue(cr)==null)
        //            tokens.Add(it.Name,
        //                    cr.RCB.Title);
        //        else
        //        tokens.Add(it.Name,
        //                it.GetValue(cr).ToString());
        //    }
        //    else
        //    {
        //        if (it.GetValue(cr) != null)
        //            tokens.Add(it.Name,
        //            it.GetValue(cr).ToString());
        //    }
        //});
        //tokens.Add("ApplicationURL", System.Configuration.ConfigurationManager.AppSettings["applicationurl"]);
        //    EmailWriter a = new EmailWriter();
        //    //a.BodyTemplateFilePath = EmailTemplate.TemplateName;
        //    a.Body = emailTemplate.TemplateBody;
        //    a.Subject= emailTemplate.Subject;
        //    a.From = new  MailAddress(FromEmail);
        //    a.Tokens = tokens;
            
        //    a.To = new List<MailAddress>();
        //    a.To.Add(new MailAddress(ToEmail));

        //    //a.CC.Add(new MailAddress(CC));
        //    if (!string.IsNullOrEmpty(CCEmail))
        //    {
        //        //var arr1=;
        //        CCEmail.Split(new char[2] { ',', ';' }).ToList().ForEach(it =>
        //        {
        //            //a.CC = new List<MailAddress>();
        //            a.CC = new MailAddressCollection();
        //            a.CC.Add(new MailAddress(it));
        //        });
        //    }
        //    bool obj1=false;
        //    a.BeginExecute(callback, obj1);
        //}


        public void SendEmail(EmailTemplate EmailTemplate, string FromEmail, string ToEmail, string CCEmail, string body)
        {
            AsyncCallback callback = new AsyncCallback(it =>
            {
                try
                {
                    var obj = it as Teckraft.Core.Workflow.EmailWriter.SendMailAsyncResult;
                    if (obj.Error != null)
                    {
                        var log = new EventLog() { RCB = null, EventType = "Exception", ApplicationName = "EmailService", Details = "Error Sending Email" + obj.Error.Message, RCT = DateTime.Now, RUT = DateTime.Now };
                        logService.Add(log);

                    }
                }
                catch { }

            });
            EmailWriter a = new EmailWriter();
            //a.BodyTemplateFilePath = EmailTemplate.TemplateName;
           // a.Body = body;

            a.Body = EmailTemplate.TemplateBody;

            a.Subject = EmailTemplate.Subject;
            a.From = new MailAddress(FromEmail);
           // a.Tokens = tokens;

            a.To = new List<MailAddress>();
            a.To.Add(new MailAddress(ToEmail));

            //a.CC.Add(new MailAddress(CC));
            if (!string.IsNullOrEmpty(CCEmail))
            {
                //var arr1=;
                a.CC = new MailAddressCollection();
                CCEmail.Split(new char[2] { ',', ';' }).ToList().ForEach(it =>
                {
                    //a.CC = new List<MailAddress>();
                  //  a.CC = new MailAddressCollection();
                    a.CC.Add(new MailAddress(it));
                });
            }
            bool obj1 = false;
            try
            {
                a.BeginExecute(callback, obj1);
            }
            catch { }
        }
    }
}
