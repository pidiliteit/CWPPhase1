using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teckraft.Core.Domian.Settings;
using Teckraft.Core.Workflow;
using Teckraft.Data;
using Teckraft.Services;

namespace Teckraft.Web.Framework
{
    public static class EmailTemplateServiceExtension
    {
        public static EmailTemplate GetTemplate(this IService<EmailTemplate> service,string NotificationType, string TaskTypeCode)
        {
            // 08Oct2015
            //return new EmailTemplate("New Change Request {ChangeRequestCode} pending for your action")
            return new EmailTemplate("Change Request {ChangeRequestCode} - {Subject}")
            {
                TemplateBody = "Dear {PendingWithUser}<br/> The Change Request number {ChangeRequestCode} has come for your action " +
                "<a href='{ApplicationURL}'>Click here</a> to login to Change Request System <br/>CR Team" +
                "" };
            var query = new ListQuery<EmailTemplate>();
            query.PageSize = 1;
            query.CurrentPage = 1;
           // query.AddParameter(new QueryParameter() { Name = "taskid", Value = TaskId.ToString() });
            return service.GetByQuery(query).Items.FirstOrDefault();
        }
        public static EmailTemplate GetTemplateforComments(this IService<EmailTemplate> service, IssueManagementCommentLog item)
        {
             
            return new EmailTemplate("Update From Issue Dashboard")
            {
                TemplateBody = "Dear Team, <br/> <br/>  This is kind informtion that, Request number " + item.ID_Request + ", for " + item.id_System_Name + 
                " comments updated by " +item.RCBEmpName + " on " + DateTime.Now + ". <br/><br/> " +
                "Latest Comments - "+item.ID_Comments + " <br/> <br/> " +
                "Status - " + item.ID_Status + " <br/> <br/>" +
                "Update in your system accordingly. <br/><br/> "+
                "This is an auto generated mail, do not reply.  <br/><br/>" +
                "Regards"
            };
            var query = new ListQuery<EmailTemplate>();
            query.PageSize = 1;
            query.CurrentPage = 1;
            // query.AddParameter(new QueryParameter() { Name = "taskid", Value = TaskId.ToString() });
            return service.GetByQuery(query).Items.FirstOrDefault();
        }

        public static EmailTemplate GetTemplateforAnchor(this IService<EmailTemplate> service, IssueManagementCommentLog item,string AnchorName,CSC csc)
        {
            string status = "";
            if (item.id_System_Name == "MyPidilite" || item.id_System_Name == "WSS Service Cell" || item.id_System_Name == "Dealer Feedback")
            {
                if (item.ID_Status != "" || item.ID_Status != null)
                {
                    if (item.ID_Status.ToLower() == "in progress") status = "Open";
                    if (item.ID_Status.ToLower() == "completed") status = "Closed";
                }
                return new EmailTemplate("CWP - Comments update - by - " + item.RCB.Title)
                {
                    TemplateBody = "Dear " + AnchorName + "/Team, <br/> <br/> " +
                    "Below is the comment added by " + item.RCB.Title + " for SR Number " + item.ID_Request +
                    " - Please make relevant updates on your system for relevant action. <br/><br/>" +
                    "Chapter Name:<b>: " + item.id_System_Name + " </b> <br/><br/> " +
                    "Date and Time Stamp: <b>" + DateTime.Now + "</b><br/><br/>" +
                    "Comments: <b>" + item.ID_Comments + "</b><br/><br/>" +
                    "Status: <b>" + status + "</b> <br/><br/><br/><br/>" +                   
                    "This is an auto generated mail, do not reply.  <br/><br/>"
                };
            }
            else if (item.id_System_Name == "Customer Service Cell")
            {
                if (item.ID_Status != "" || item.ID_Status != null)
                {
                    if (item.ID_Status.ToLower() == "in progress") status = "Open";
                    if (item.ID_Status.ToLower() == "completed") status = "Closed";
                }
                return new EmailTemplate("CWP - Comments update - by - " + item.RCB.Title)
                {
                    TemplateBody = "Dear " + AnchorName + "/Team, <br/> <br/> " +
                    "Below is the comment added by " + item.RCB.Title + " for SR Number " + item.ID_Request +
                    " - Please make relevant updates on your system for relevant action. <br/><br/>" +
                    "Chapter Name:<b>: " + item.id_System_Name + " </b> <br/><br/> " +
                    "Date and Time Stamp: <b>" + DateTime.Now + "</b><br/><br/>" +
                    "Comments: <b>" + item.ID_Comments + "</b><br/><br/>" +
                    "Status: <b>" + status + "</b> <br/><br/>" +
                    "Customer Mobile Number: <b>" + csc.ContactNo + "</b> <br/><br/>" +
                    "Customer Email: <b>" + csc.ContactEmail + "</b> <br/><br/><br/><br/>" +
                    "This is an auto generated mail, do not reply.  <br/><br/>"
                };
            }
            //else if (item.id_System_Name == "Dealer Feedback")
            //{
            //    return new EmailTemplate("CWP - Comments update - by - " + item.RCB.Title)
            //    {

            //        TemplateBody = "Dear " + AnchorName + "/Team, <br/> <br/> " +
            //        "Below is the comment added by " + item.RCB.Title + " for SR Number " + item.ID_Request +
            //        " - Please make relevant updates on your system for relevant action. <br/><br/>" +
            //        "Chapter Name:<b>: " + item.id_System_Name + " </b> <br/><br/> " +
            //        "Date and Time Stamp: <b>" + DateTime.Now + "</b><br/><br/>" +
            //        "Comments: <b>" + item.ID_Comments + "</b> <br/><br/><br/><br/>" +
            //        "This is an auto generated mail, do not reply.  <br/><br/>"
            //    };
            //}
            else
            {
                return new EmailTemplate("");
                //return new EmailTemplate("CWP - Comments update - by - " + item.RCB.Title)
                //{

                //    TemplateBody = "Dear " + AnchorName + "/Team, <br/> <br/> " +
                //    "Below is the comment added by " + item.RCB.Title + " for SR Number " + item.ID_Request +
                //    " - Please make relevant updates on your system for relevant action. <br/><br/>" +
                //    "Chapter Name:<b>: " + item.id_System_Name + " </b> <br/><br/> " +
                //    "Date and Time Stamp: <b>" + DateTime.Now + "</b><br/><br/>" +
                //    "Comments: <b>" + item.ID_Comments + "</b> <br/><br/><br/><br/>" +
                //    "This is an auto generated mail, do not reply.  <br/><br/>"

                //};
            }
            var query = new ListQuery<EmailTemplate>();
            query.PageSize = 1;
            query.CurrentPage = 1;
            // query.AddParameter(new QueryParameter() { Name = "taskid", Value = TaskId.ToString() });
            return service.GetByQuery(query).Items.FirstOrDefault();
        }
    }
}