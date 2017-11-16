using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Teckraft.Core.Domian.Settings;
using Teckraft.Core.Workflow;
using Teckraft.Data;
 
using Teckraft.Services;
using Teckraft.Services.Extention;

namespace Teckraft.Web.Framework.Extention
{
    public static class PendingCREmailServiceExtension
    {
       
        public static void SendReminders(this IReadService<vwSLA> service, IService<EmailTemplate> templateService, IEmailService emailService)
        {
            var query = new ListQuery<vwSLA>();
            var list = service.GetByQuery(query);
            System.Web.UI.WebControls.GridView gd = new System.Web.UI.WebControls.GridView();
            System.Web.UI.WebControls.GridView gd1 = new System.Web.UI.WebControls.GridView();
            var distinctMails1 = list.Items.Select(x => x.Email).Distinct();

            var distinctMails = list.Items.Where(p => p.id>0).Select(g => g.Email).Distinct();
            foreach (var email in distinctMails)
            {
                if (email != null)
                {
                    var testlist = list.Items.Where(it => it.Email == email && it.PendingDays > 2).Select(p => new { p.MilestoneTitle,p.InitiativeTitle,p.EndDate,p.PendingDays});
                    gd.DataSource = testlist;
                    gd.DataBind();

                    var testlist1 = list.Items.Where(it => it.Email == email && it.PendingDays == -2).Select(p => new { p.MilestoneTitle,p.InitiativeTitle,p.EndDate});
                    gd1.DataSource = testlist1;
                    gd1.DataBind();

                    var RA1E = list.Items.Where(it => it.Email == email).Select(p => p.RA1Email).ToList();
                    var RA2E = list.Items.Where(it => it.Email == email).Select(p => p.RA2Email).ToList();

                    var sb = new System.Text.StringBuilder();
                    var tw = new System.IO.StringWriter(sb);
                    var htw = new System.Web.UI.HtmlTextWriter(tw);
                    gd.RenderControl(htw);

                    var sb1 = new System.Text.StringBuilder();
                    var tw1 = new System.IO.StringWriter(sb1);
                    var htw1 = new System.Web.UI.HtmlTextWriter(tw1);
                    gd1.RenderControl(htw1);

                    var tmplt1 = templateService.GetReminderTemplateforSLA(sb.ToString(),sb1.ToString());
                    emailService.SendEmail(tmplt1, "ihsystem@pidilite.com", email, (RA1E[0].ToString() + ";" + RA2E[0].ToString()), tmplt1.TemplateBody.ToString());
                    //emailService.SendEmail(tmplt1, "ihsystem@pidilite.com", "aaliya.nkhan@gmail.com","", tmplt1.TemplateBody.ToString());
                }

            }

            //var query = new ListQuery<MilestoneReport>();
            //var list = service.GetByQuery(query);
            //var result = new ListQueryResult<MilestoneReport>();
            //var listTemp1 = new List<MilestoneReport>();
            //var listTemp2 = new List<MilestoneReport>();
            //foreach (var item in list.Items)
            //{
            //    foreach (var Res in item.ResponsiblePerson)
            //    {
            //        if (Res.Status.StatusDesc != "Completed")
            //        {
            //            DateTime dt1 = Convert.ToDateTime(Res.EndDate);
            //            DateTime dt2 = DateTime.Now;
            //            var days = dt2.Subtract(dt1).TotalDays;
            //            if (days >= 2)
            //            {
            //                Res.message = "over due";

            //                listTemp1.Add(item);
            //            }
            //            else if (days <= -2)
            //            {
            //                Res.message = "Due in Next Two Days";
            //                listTemp2.Add(item);
            //            }
            //        }
                  
            //    }
            //}
            //System.Web.UI.WebControls.GridView gd1 = new System.Web.UI.WebControls.GridView();
            //gd1.DataSource = listTemp1;
            //gd1.DataBind();

            //System.Web.UI.WebControls.GridView gd2 = new System.Web.UI.WebControls.GridView();
            //gd2.DataSource = listTemp2;
            //gd2.DataBind();

            //var sb = new System.Text.StringBuilder();
            //var tw = new System.IO.StringWriter(sb);
            //var htw = new System.Web.UI.HtmlTextWriter(tw);
            //gd1.RenderControl(htw);

            //var sb1 = new System.Text.StringBuilder();
            //var tw1 = new System.IO.StringWriter(sb1);

            //var htw1 = new System.Web.UI.HtmlTextWriter(tw1);
            //gd2.RenderControl(htw1);

            //var tmplt1 = templateService.GetReminderTemplateforSLA(sb.ToString(),sb1.ToString());
            //emailService.SendEmail(tmplt1, "ihsystem@pidilite.com", 'aaliya.nkhan@gmail.com', (RA1E[0].ToString() + ";" + RA2E[0].ToString()), sb.ToString());

        }
    }
}