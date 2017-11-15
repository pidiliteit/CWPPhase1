using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Teckraft.Core.Domian.Settings;
using Teckraft.Services;
using Teckraft.Web.Models;
using Teckraft.Web.Framework;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Teckraft.Data;
using System.Web.Script.Serialization;
using Teckraft.Web.Security;
using Teckraft.Core.Workflow;
using System.Data.Entity.Infrastructure;

namespace Teckraft.Web.API
{
    public class IssueManagementCommentLogController:ApiController
    {
        IService<Teckraft.Core.Domian.Settings.User> _userservice;
        private readonly IService<IssueManagementCommentLog> IssueService;
        private readonly IEmailService emailService = null;
        private readonly IService<EmailTemplate> templateService = null;

        public IssueManagementCommentLogController(IService<IssueManagementCommentLog> IssueService, IService<User> _userservice, IEmailService emailService, IService<EmailTemplate> templateService)
        {
            this.IssueService = IssueService;
            this._userservice = _userservice;
            this.emailService = emailService;
            this.templateService = templateService;
        }


        public ListQueryResult<IssueManagementCommentLog> Get(string q)
        {
            ListQuery<IssueManagementCommentLog> query = new ListQuery<IssueManagementCommentLog>();
            if (!string.IsNullOrEmpty(q))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                query = js.Deserialize<ListQuery<IssueManagementCommentLog>>(q);
            }
            var curuser = _userservice.GetCurrentUser();
            string curRole = "";

            return this.IssueService.GetByQuery(query);
        }

        public DbActionResult<IssueManagementCommentLog> Post(IssueManagementCommentLog items)
        {
            var result = new DbActionResult<IssueManagementCommentLog>();
            bool EFlag = false;
            string EmailTo = "";
            string AncEmail = "";
            string EmailCC = "";
            string AnchorName = "";
            try
            {
                var data = items;

                string[] emailRegex = { "-", "<", ">", ";", "'" };
                for (var i = 0; i < emailRegex.Length; i++)
                {
                    if (data.ID_Comments != null)
                        data.ID_Comments = data.ID_Comments.Replace(emailRegex[i], " ");
                }

                var csc = new CSC();
                using (var dbcontext = new Teckraft.Data.Sql.InitiativeHubFinalEntities())
                {
                    //var chap = dbcontext.ChapterMasters.FirstOrDefault(it => it.ChapterNameFromSystem == data.id_System_Name && it.EmailFlag == true);
                    //if (chap != null)
                    //{
                    //    data.id_System_Name = chap.ChapterNameFromSystem;
                    //    EFlag = chap.EmailFlag.Value;
                    //    EmailTo = chap.EmailTo;
                    //}

                    var chap1 = dbcontext.ChapterMasters.FirstOrDefault(it => it.ChapterNameFromSystem == data.id_System_Name);
                    if (chap1 != null)
                    {
                        if (chap1.ChapterNameFromSystem == "Dealer Feedback" || chap1.ChapterNameFromSystem == "WSS Service Cell" || chap1.ChapterNameFromSystem == "MyPidilite" || chap1.ChapterNameFromSystem == "Customer Service Cell")
                        {
                            var em = dbcontext.ChapterMasters.FirstOrDefault(o => o.ChapterNameFromSystem == data.id_System_Name);
                            EmailCC = em.EmailCC;
                            AncEmail = em.EmailTo;
                        }
                        else
                        {
                            string AncData = dbcontext.ChapterOwnerMappings.FirstOrDefault(it => it.ChapterId == chap1.Id).ChapterOwner.ToString();
                            AncEmail = dbcontext.UserDetails.FirstOrDefault(p => p.UserName == AncData).Email.ToString();
                        }
                        //for getting Anchor Name
                        var q = (from pd in dbcontext.ChapterOwnerMappings
                                 join od in dbcontext.UserDetails on pd.ChapterOwner equals od.UserName
                                 where pd.ChapterId==chap1.Id
                                 select new
                                 {
                                     od.Title,
                                 }).FirstOrDefault();
                        if(q!=null)
                            AnchorName = q.Title;
                    }

                    
                    if (items.id_System_Name == "Customer Service Cell")
                    {

                        var cs = dbcontext.GetCSCDataBYRequestNo(items.ID_Request).FirstOrDefault();
                        csc.ContactEmail = cs.ContactEmail;
                        csc.ContactNo = cs.contactNo;
                    }


                }

                data.RCB = _userservice.GetCurrentUser();
                data = IssueService.Add(data);
                //if (EFlag == true) {
                //    if (data.EmailFlag == true && EmailTo != null) {
                //        var tmplt1 = templateService.GetTemplateforComments(data);
                //       // emailService.SendEmail(tmplt1, "CustomerCare@pidilite.com", EmailTo, "", "");
                //        emailService.SendEmail(tmplt1, "CustomerCare@pidilite.com", EmailTo, "teckraft.pidilite@teckraft.com", "");
                //    }
                //}

               
                    
                    var tmplt1 = templateService.GetTemplateforAnchor(data, AnchorName,csc);
                    //emailService.SendEmail(tmplt1, "collaborativeworking@pidilite.com ", "teckraft.pidilite@teckraft.com","", "");
                    if (tmplt1.Subject != "")
                    {
                        //emailService.SendEmail(tmplt1, "collaborativeworking@pidilite.com ", "teckraft.pidilite@teckraft.com", "", "");
                        emailService.SendEmail(tmplt1, "collaborativeworking@pidilite.com ", AncEmail, EmailCC, "");
                    }
                
                result.Success = true;
                result.Message = "Comments added successfully";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Unable to Save Record!";
                result.Exception = ex;
            }

            return result;
        }
    }
}