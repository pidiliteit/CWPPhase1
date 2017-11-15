using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teckraft.Core.Workflow
{
    public class EmailTemplate:Core.Domian.BaseEntity
    {
        
        public EmailTemplate(string Subject, string TemplateName)
        {
            // TODO: Complete member initialization
            this.Subject = Subject;
            this.TemplateName = TemplateName;
        }

        public EmailTemplate(string Subject)
        {
            // TODO: Complete member initialization
            this.Subject = Subject;
         
        }
        public string Subject { get; set; }
        public string TemplateName { get; set; }


        public  string TemplateBody { get; set; }
    }
}