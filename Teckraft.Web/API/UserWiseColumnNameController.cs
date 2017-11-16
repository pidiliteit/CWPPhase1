using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Teckraft.Core.Domian.Settings;
using Teckraft.Services;
using Teckraft.Web.Models;
using Teckraft.Web.Framework;

namespace Teckraft.Web.API
{
    public class UserWiseColumnNameController : ApiController
    {
        IService<Teckraft.Core.Domian.Settings.User> _userservice;
       /// private readonly IService<UserWiseColumnName> IssueService;

        public UserWiseColumnNameController(IService<User> _userservice)
        {
          //  this.IssueService = IssueService;
            this._userservice = _userservice;
        }

        public DbActionResult<UserWiseColumnName> Post(UserWiseColumnName items)
        {
            var result = new DbActionResult<UserWiseColumnName>();
             
            try
            {
                var data = items;
                using (var dbcontext = new Teckraft.Data.Sql.InitiativeHubFinalEntities())
                {
                    // foreach (var i in data) {
                    var chap = dbcontext.ChapterMasters.FirstOrDefault(it => it.ChapterNameFromSystem == data.chapterName);
                    if (chap != null)
                    {
                        data.ChapterId = chap.Id;
                    }
                        var existingList = dbcontext.UserWiseColumnNames.FirstOrDefault(it => it.UserId == data.UserId && it.ChapterId == data.ChapterId);
                    if (existingList != null)
                    {
                        //dbcontext.UserWiseColumnNames.Remove(existingList);
                        existingList.ChapterId = data.ChapterId;
                        existingList.UserId = data.UserId;
                            existingList.chapternamedisplay = data.chapternamedisplay;
                            existingList.chapterOwner = data.chapterOwner;
                            existingList.ID_Category = data.ID_Category;
                            existingList.id_comments = data.id_comments;
                            existingList.id_data_load_date = data.id_data_load_date;
                            existingList.id_dept = data.id_dept;
                            existingList.ID_Issue_Detail1 = data.ID_Issue_Detail1;
                            existingList.id_location = data.id_location;
                            existingList.ID_Logged_Date = data.ID_Logged_Date;
                            existingList.id_pending_with_email = data.id_pending_with_email;
                            existingList.id_pending_with_name = data.id_pending_with_name;
                            existingList.id_reported_by_email = data.id_reported_by_email;
                            existingList.id_reported_by_name = data.id_reported_by_name;
                            existingList.id_request = data.id_request;
                            existingList.id_status = data.id_status;
                            existingList.ID_Target_Date = data.ID_Target_Date;
                            existingList.id_tat_status = data.id_tat_status;
                            existingList.pendingSince = data.pendingSince;
                            existingList.title = data.title;
                        existingList.UserId= _userservice.GetCurrentUser().Id;
                        existingList.RUB = _userservice.GetCurrentUser().Id;
                            existingList.RUT = DateTime.Now;
                         dbcontext.SaveChanges();
                    }
                    else
                    {
                        dbcontext.UserWiseColumnNames.Add(new Teckraft.Data.Sql.UserWiseColumnName()
                        {
                            ChapterId = data.ChapterId,
                            UserId = _userservice.GetCurrentUser().Id,
                        chapternamedisplay = data.chapternamedisplay,
                            chapterOwner = data.chapterOwner,
                            ID_Category = data.ID_Category,
                            id_comments = data.id_comments,
                            id_data_load_date = data.id_data_load_date,
                            id_dept = data.id_dept,
                            ID_Issue_Detail1 = data.ID_Issue_Detail1,
                            id_location = data.id_location,
                            ID_Logged_Date = data.ID_Logged_Date,
                            id_pending_with_email = data.id_pending_with_email,
                            id_pending_with_name = data.id_pending_with_name,
                            id_reported_by_email = data.id_reported_by_email,
                            id_reported_by_name = data.id_reported_by_name,
                            id_request = data.id_request,
                            id_status = data.id_status,
                            ID_Target_Date = data.ID_Target_Date,
                            id_tat_status = data.id_tat_status,
                            pendingSince = data.pendingSince,
                            title = data.title,
                            
                            RCT = DateTime.Now,
                            RCB =  _userservice.GetCurrentUser().Id,
                    });
                    }
                    dbcontext.SaveChanges();
                    result.Success = true;
                    result.Message = "Comments added successfully ";
                }
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