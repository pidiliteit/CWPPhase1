using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web;

namespace Teckraft.Core.Workflow
{
    public class EmailWriter
    {
        private void LoadBodyTemplate()
        {
            if (!string.IsNullOrEmpty(this.BodyTemplateFilePath))
            {
                using (StreamReader re = File.OpenText(this.BodyTemplateFilePath))
                {
                    this.Body = re.ReadToEnd();
                }
            }
        }
       
        private void ReplaceTokensInBody()
        {
            IDictionary<string, string> t = Tokens;

            foreach (string key in t.Keys)
            {
                var key1 = key;
//                if (key == "RCB") key1 = "Initiator";
                this.Body = this.Body.Replace("{"+key1+"}", t[key]);
                this.Subject = this.Subject.Replace("{" + key1 + "}", t[key]);
            }
        }

        private void AddTestInformationToBody()
        {
            StringBuilder buffer = new StringBuilder();

            buffer.Append("<br/>");
            buffer.Append("<hr/>");
            buffer.Append(string.Format("<b>Test Mode</b> - TestMailTo address is {0}", this.TestMailTo));
            buffer.Append("<hr/>");

            string bodyWithTestInfo = this.Body + buffer.ToString();

            this.Body = bodyWithTestInfo;
        }


        public IAsyncResult BeginExecute(AsyncCallback callback, object state)
        {
            SendMailAsyncResult sendMailAsyncResult =null;
            try
            {
                MailMessage message = new MailMessage();
                message.From = this.From;

                if (TestMailTo == null)
                {
                    foreach (MailAddress address in this.To)
                    {
                        message.To.Add(address);
                    }

                    MailAddressCollection ccList = this.CC;
                    if (ccList != null)
                    {
                        foreach (MailAddress address in ccList)
                        {
                            message.CC.Add(address);
                        }
                    }

                    MailAddressCollection bccList = this.Bcc;
                    if (bccList != null)
                    {
                        foreach (MailAddress address in bccList)
                        {
                            message.Bcc.Add(address);
                        }
                    }
                }
                else
                {
                    message.To.Add(TestMailTo);
                }

                List<Attachment> attachments = this.Attachments;
                if (attachments != null)
                {
                    foreach (Attachment attachment in attachments)
                    {
                        message.Attachments.Add(attachment);
                    }
                }

                if (!string.IsNullOrEmpty(this.BodyTemplateFilePath))
                {
                    LoadBodyTemplate();
                }

                if ((this.Tokens != null) && (this.Tokens.Count > 0))
                {
                    ReplaceTokensInBody();
                }


                if (this.TestMailTo != null)
                {
                    AddTestInformationToBody();
                }

                message.Subject = this.Subject;
                message.Body = this.Body;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient();
                ////client.EnableSsl = this.EnableSsl;

                //if (string.IsNullOrEmpty(this.UserName))
                //{
                //    client.UseDefaultCredentials = true;
                //}
                //else
                //{
                //    client.UseDefaultCredentials = false;
                //    client.Credentials = new NetworkCredential(this.UserName, this.Password);
                //}

                //if (!string.IsNullOrEmpty(this.TestDropPath))
                //{
                //    WriteMailInTestDropPath(context);
                //}
                sendMailAsyncResult = new SendMailAsyncResult(client, message, callback, state);
            }
            catch(System.Exception ex) {
                
            }
            
            //context.UserState = sendMailAsyncResult;
            return sendMailAsyncResult;
        }

        
        public string BodyTemplateFilePath { get; set; }

        public string Body { get; set; }
        public IDictionary<string, string> Tokens { get; set; }

        public MailAddress TestMailTo { get; set; }

        public class SendMailAsyncResult : IAsyncResult
        {
            SmtpClient client;
            AsyncCallback callback;
            object asyncState;
            EventWaitHandle asyncWaitHandle;

            public bool CompletedSynchronously { get { return false; } }
            public object AsyncState { get { return this.asyncState; } }
            public WaitHandle AsyncWaitHandle { get { return this.asyncWaitHandle; } }
            public bool IsCompleted { get { return true; } }
            public SmtpClient Client { get { return client; } }

            public SendMailAsyncResult(SmtpClient client, MailMessage message, AsyncCallback callback, object state)
            {
                this.client = client;
                this.callback = callback;
                this.asyncState = state;
                this.asyncWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
                client.SendCompleted += new SendCompletedEventHandler(SendCompleted);
                ThreadPool.QueueUserWorkItem(o=> 
                
                client.SendAsync(message,state)
                );
            }

            void SendCompleted(object sender, AsyncCompletedEventArgs e)
            
            {
                
                //var f=System.IO.File.CreateText(@"d:\log_"+Guid.NewGuid().ToString()+".log");
                //f.Write(DateTime.Now.ToString());
                //f.Close();
                //f.Dispose();
               // this.IsCompleted = e.Error == null ? true : false;
                this.asyncState = e.Error == null ? true : false;
                this.asyncWaitHandle.Set();
                this.Error = e.Error;
                callback(this);
            }

            public Exception Error { get; set; }
        }

        public List<MailAddress> To { get; set; }

        public MailAddress From { get; set; }

        public MailAddressCollection CC { get; set; }

        public MailAddressCollection Bcc { get; set; }

        public List<Attachment> Attachments { get; set; }

        public string Subject { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public bool EnableSsl { get; set; }

        public string UserName { get; set; }

        public System.Security.SecureString Password { get; set; }

        public string BodyTemplate { get; set; }
    }


}