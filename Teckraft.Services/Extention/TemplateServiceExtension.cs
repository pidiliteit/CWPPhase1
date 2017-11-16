using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.Settings;
using Teckraft.Core.Workflow;

namespace Teckraft.Services.Extention
{
    public static class TemplateServiceExtension
    {
        public static EmailTemplate GetTemplateForResponsiblePerson(this IService<EmailTemplate> service, ResponsiblePerson item, string MilestoneTitle, string Division, string createdBy)
        {
            return new EmailTemplate("Initiative Hub : Task Assigned")
            {
                TemplateBody = "Dear " + item.ResponsiblePersonName.Title + ", <br/> Initiative Hub : Task has been assigned with Below Details <br/> " +
                "Milestone Title : " + MilestoneTitle + " <br/>" +
                "Division : " + Division + " <br/> " +
                "Created By : " + createdBy + "   <br/>" +
                "Status : " + item.Status.StatusDesc + "  <br/>" +
                "<a href='http://192.168.1.52:6090'>Click here</a> to login to Initiative Hub. <br/>" +
                "This is auto generated mail, please do not reply. <br/>" +
                "Pidilite Team"

            };

        }
        public static EmailTemplate GetTemplateForDependentPerson(this IService<EmailTemplate> service, DependentPerson item, string MilestoneTitle, string Division, string createdBy)
        {
            return new EmailTemplate("Initiative Hub : Task Assigned")
            {
                TemplateBody = "Dear " + item.DependentPersonName.Title + ", <br/> Initiative Hub : Task has been assigned with Below Details <br/> " +
                "Milestone Title : " + MilestoneTitle + " <br/>" +
                "Division : " + Division + " <br/> " +
                "Created By : " + createdBy + "  <br/>" +
                "Status :  " + item.Status.StatusDesc + "   <br/>" +
                "<a href='http://192.168.1.52:6090'>Click here</a> to login to Initiative Hub. <br/>" +
                "This is auto generated mail, please do not reply. <br/>" +
                "Pidilite Team"

            };

        }
        public static EmailTemplate GetTemplateForResponsiblePersonTopManagment(this IService<EmailTemplate> service, ResponsiblePerson item, string MilestoneTitle, string Division, string createdBy)
        {
            return new EmailTemplate("Initiative Hub : Task Assigned")
            {
                TemplateBody = "Dear " + item.ResponsiblePersonName.Title + ", <br/>  The below Milestones has been commented by  " +createdBy + ".  <br/> " +
                "This is for your information."+
                "Milestone Title : " + MilestoneTitle + " <br/>" +
                "Division : " + Division + " <br/> " +
                "Status : " + item.Status.StatusDesc + "  <br/>" +
                "Milestone Owner : " + item.ResponsiblePersonName.Title + " <br/>" +
                "Milestone Start Date :" + item.StartDate + " <br/>" +
                "Milestone End Date :" + item.EndDate + " <br/>" +
                "<a href='http://192.168.1.52:6090'>Click here</a> to login to Initiative Hub. <br/>" +
                "This is auto generated mail, please do not reply. <br/>" +
                "Pidilite Team"

            };

        }
        public static EmailTemplate GetReminderTemplate(this IService<EmailTemplate> service,string sb)
        {
            return new EmailTemplate("Initiative Hub Reminder : Task Assigned")
            {
                //TemplateBody = "Dear " + item.ResponsiblePersonName.Title + ", <br/> Initiative Hub : Task has been assigned with Below Details <br/> " +
                //"Milestone Title : " + MilestoneTitle + " <br/>" +
                //"Division : " + Division + " <br/> " +
                //"Created By : " + createdBy + "   <br/>" +
                //"Status : " + item.Status.StatusDesc + "  <br/>" +
                //"<a href='http://192.168.1.52:6090'>Click here</a> to login to Initiative Hub. <br/>" +
                //"This is auto generated mail, please do not reply. <br/>" +
                //"Pidilite Team"

            };

        }
        public static EmailTemplate GetReminderTemplateforSLA(this IService<EmailTemplate> service, string sb,string sb1)
        {
            return new EmailTemplate("Initiative Hub System – Task pending for Action")
            {
                TemplateBody = "Dear Sir/Madam, <br><br> Following listed Milestones are Overdue for Your Action" +
                "<br><br>" + sb +
                "<br><br>"+
                "Following listed milestones are Due in Next 2 days" +
                "<br><br>" + sb1 +
                "<br><br><a href='http://192.168.1.52:6090'> Click here </a> to login to Initiative Hub System <br/><br>" +
                "Its an auto generated mail, do not reply on this id. <br><br> IH Team"
            };

        }

        public static EmailTemplate GetTemplateForDependentPersonTopManagment(this IService<EmailTemplate> service, DependentPerson item, string MilestoneTitle, string Division, string createdBy)
        {
            return new EmailTemplate("Initiative Hub : Task Assigned")
            {
                TemplateBody = "Dear " + item.DependentPersonName.Title + ", <br/>  The below Milestones has been commented by  " + createdBy + ".  <br/> " +
                "This is for your information." +
                "Milestone Title : " + MilestoneTitle + " <br/>" +
                "Division : " + Division + " <br/> " +
                "Status : " + item.Status.StatusDesc + "  <br/>" +
                "Milestone Owner : " + item.DependentPersonName.Title + " <br/>" +
                "Milestone Start Date :" + item.dStartDate + " <br/>" +
                "Milestone End Date :" + item.dEndDate + " <br/>" +
                "<a href='http://192.168.1.52:6090'>Click here</a> to login to Initiative Hub. <br/>" +
                "This is auto generated mail, please do not reply. <br/>" +
                "Pidilite Team"

            };

        }
    }

}

