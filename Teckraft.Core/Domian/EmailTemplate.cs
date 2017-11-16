using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teckraft.Core.Workflow;

namespace Teckraft.Core.Domian
{
    public class EmailTemplates
    {
        public static EmailTemplate OTHCRSubmited { get { 
        return new EmailTemplate("New CR {crcode} submitted by {initiator}","OTHCRSubmited.htm");
        } }
        public static EmailTemplate GRCCRSubmited
        {
            get
            {
                return new EmailTemplate("New CR {crcode} submitted by {initiator}", "GRCCRSubmited.htm");
            }
        }
        public static EmailTemplate MDCCRSubmited
        {
            get
            {
                return new EmailTemplate("New CR {crcode} submitted by {initiator}", "MDCCRSubmited.htm");
            }
        }

        public static EmailTemplate MDCSLA1
        {
            get
            {
                return new EmailTemplate("MDC Task not completed within SLA", "MDCSLA1.htm");
            }
        }

        public static EmailTemplate TaskAssigned
        {
            get
            {
                return new EmailTemplate("Task Assigned", "MDCSLA1.htm");
            }
        }

        public static EmailTemplate GetTemplate(string p, string tasktypecode)
        {
            return null;
        }
    }
}