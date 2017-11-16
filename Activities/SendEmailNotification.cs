using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Activities;
using Teckraft.Core.Domian.Transactions;
using Teckraft.Core.Workflow;
using Teckraft.Services;
using Microsoft.Practices.Unity;
namespace TeckraftBuilds.Workflow.Activities
{
    public class SendEmailNotification:CodeActivity
    {
        public InArgument<string> FromEmail { get; set; }
        public InArgument<string> ToEmail { get; set; }
        public InArgument<string> CCEmail { get; set; }
        public InArgument<string> BCCEmail { get; set; }
        public InArgument<string> Subject { get; set; }
        public InArgument<string> Body { get; set; }
      //  public InArgument<CR> WorkItem { get; set; }
     //   public InArgument<Task> WorkItemTask { get; set; }
        public InArgument<Teckraft.Core.Workflow.EmailTemplate> Template { get; set; }
        
        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                IEmailService emailServicel = CustomUnityContainer.Container.Resolve<IEmailService>();
             //   CR workitem = context.GetValue(this.WorkItem);
                var fromEmail = context.GetValue(this.FromEmail);
                var toEmail = context.GetValue(this.ToEmail);
                var ccEmail = context.GetValue(this.CCEmail);
                var bccEmail = context.GetValue(this.BCCEmail);
                var subject = context.GetValue(this.Subject);
                var body = context.GetValue(this.Body);
                var template = context.GetValue(this.Template);
                //var workItemTask = context.GetValue(this.WorkItemTask);
                //if (fromEmail == null) fromEmail = Teckraft.Core.Infrastructure.EngineContext.Current.Settings.EmailSender;

                //emailServicel.SendEmail(template, fromEmail, toEmail, ccEmail, workitem, workItemTask);
            }
            catch { 
            }
        }
    }
}
