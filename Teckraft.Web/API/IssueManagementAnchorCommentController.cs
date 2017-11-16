using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using Teckraft.Core.Domian.Settings;
using Teckraft.Data;
using Teckraft.Services;
using Teckraft.Core.Workflow;
using Teckraft.Web.Framework;
using Teckraft.Web.Models;

namespace Teckraft.Web.API
{
    public class IssueManagementAnchorCommentController:ApiController
    {
        IService<Teckraft.Core.Domian.Settings.User> _userservice;
        private readonly IService<IssueManagementAnchorComment> IssueService;
        

        public IssueManagementAnchorCommentController(IService<IssueManagementAnchorComment> IssueService, IService<User> _userservice)
        {
            this.IssueService = IssueService;
            this._userservice = _userservice;
            
        }
        public ListQueryResult<IssueManagementAnchorComment> Get(string q)
        {
            ListQuery<IssueManagementAnchorComment> query = new ListQuery<IssueManagementAnchorComment>();
            if (!string.IsNullOrEmpty(q))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                query = js.Deserialize<ListQuery<IssueManagementAnchorComment>>(q);
            }
            var curuser = _userservice.GetCurrentUser();
            string curRole = "";

            return this.IssueService.GetByQuery(query);
        }

        public DbActionResult<IssueManagementAnchorComment> Post(IssueManagementAnchorComment items)
        {
            var result = new DbActionResult<IssueManagementAnchorComment>();
            bool EFlag = false;
            string EmailTo = "";
            try
            {
                var data = items;

                string[] emailRegex = { "-", "<", ">", ";", "'" };
                for (var i = 0; i < emailRegex.Length; i++)
                {
                    if (data.ID_Comments != null)
                        data.ID_Comments = data.ID_Comments.Replace(emailRegex[i], " ");
                }
                using (var dbcontext = new Teckraft.Data.Sql.InitiativeHubFinalEntities())
                {
                    var chap = dbcontext.ChapterMasters.FirstOrDefault(it => it.ChapterNameFromSystem == data.id_System_Name && it.EmailFlag == true);
                    if (chap != null)
                    {
                        data.id_System_Name = chap.ChapterNameFromSystem;
                        EFlag = chap.EmailFlag.Value;
                        EmailTo = chap.EmailTo;
                    }
                }

                data.RCB = _userservice.GetCurrentUser();
                data = IssueService.Add(data);

               // if (EFlag == true)
              //  {
                   // if (data.EmailFlag == true && EmailTo != null)
                    //{
                        //var tmplt1 = templateService.GetTemplateforComments(data);
                        // emailService.SendEmail(tmplt1, "CustomerCare@pidilite.com", EmailTo, "", "");
                       // emailService.SendEmail(tmplt1, "CustomerCare@pidilite.com", EmailTo, "teckraft.pidilite@teckraft.com", "");
                   // }
                //}

                result.Success = true;
                result.Message = "Comments added successfully ";
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