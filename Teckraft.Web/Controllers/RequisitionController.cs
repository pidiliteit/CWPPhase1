using ClosedXML.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Teckraft.Core.Domian.Settings;
using Teckraft.Data;
using Teckraft.Services;
using Teckraft.Web.Framework;
using Teckraft.Web.Models;

//using Teckraft.Data;

namespace ASF.Controllers
{
    public class RequisitionController : Controller
    {
        
        IService<IssueManagementCommentLog> commentLogService;
        private readonly Teckraft.Data.Mappings.IMappingProvider<UserWiseColumnName, Teckraft.Data.Sql.UserWiseColumnName> mapper;

        public RequisitionController(IService<IssueManagementCommentLog> commentLogService, Teckraft.Data.Mappings.IMappingProvider<UserWiseColumnName, Teckraft.Data.Sql.UserWiseColumnName> mapper)
        {
            this.commentLogService = commentLogService;
            this.mapper = mapper;
        }
        public System.Web.Mvc.ActionResult IssueDetails()
        {
            return PartialView();
        }
        public System.Web.Mvc.ActionResult IssueDetails1()
        {
            return PartialView();
        }
        public System.Web.Mvc.ActionResult IssueDetailsNew()
        {
            return PartialView();
        }
        public System.Web.Mvc.ActionResult PilworldWSSfeedback()
        {
            return PartialView();
        }
        public System.Web.Mvc.ActionResult mypidilite()
        {
            return PartialView();
        }
        public System.Web.Mvc.ActionResult WSSServiceCell()
        {
            return PartialView();
        }
        public System.Web.Mvc.ActionResult RiskManagement()
        {
            return PartialView();
        }
        public System.Web.Mvc.ActionResult DealerFeedback()
        {
            return PartialView();
        }

        public System.Web.Mvc.ActionResult AllOther()
        {
            return PartialView();
        }

        public JsonResult getChapterDetails(string ResponsibleEmail, string Owner)
        {
            var result = new Teckraft.Data.ListQueryResult<ChapterMaster>();
            var list = new List<ChapterMaster>();
            string constrSSO = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string newUserName = "";
           
          
                using (SqlConnection con1 = new SqlConnection(constrSSO))
                {
                using (SqlCommand cmd1 = new SqlCommand())
                {

                    // SqlCommand cmd1 = new SqlCommand("spGetChaptersforResponsiblePerson", con1);
                    cmd1.CommandText = "spGetChaptersforResponsiblePerson";
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@ResponsibleEmail", ResponsibleEmail);
                    cmd1.Parameters.AddWithValue("@Owner", Owner);
                    con1.Open();
                    cmd1.Connection = con1;
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd1;
                    da.Fill(dt);
                    string TempChapterName = "";
                    int countChapter = 0;
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            TempChapterName = row["ChapterNameFromSystem"].ToString();
                            using (var dbcontext = new Teckraft.Data.Sql.InitiativeHubFinalEntities())
                            {
                                if (TempChapterName == "MyPidilite" || TempChapterName == "WSS Service Cell")
                                {
                                    var getChapterName = dbcontext.spGetChaptersNameforResponsiblePerson(ResponsibleEmail, Owner, TempChapterName).ToList();
                                    countChapter = getChapterName.Count();
                                }
                            }

                            // DateTime dt1 = DateTime.ParseExact((row["id_data_load_date"].ToString()), "yyyy-MM-dd", null);
                            if ((TempChapterName != "MyPidilite" && TempChapterName != "WSS Service Cell") || countChapter > 0)
                            {
                                list.Add(new ChapterMaster
                                {
                                    ChapterNameDisplay = row["chapternamedisplay"].ToString(),
                                    ChapterOwner = row["ChapterOwner"].ToString(),
                                    RunDate = row["id_data_load_date"].ToString(),
                                    ChapterNameFromSystem = row["ChapterNameFromSystem"].ToString(),
                                    EditFlag = bool.Parse(row["editflag"].ToString()),
                                    IssueCount = int.Parse(row["issueCount"].ToString()),
                                });
                            }

                        }
                    }
                    else {
                        using (var dbcontext = new Teckraft.Data.Sql.InitiativeHubFinalEntities())
                        {                            
                                var getChapterName = dbcontext.spGetChaptersNameforResponsiblePerson(ResponsibleEmail, Owner, TempChapterName).ToList();
                                foreach (var i in getChapterName) {
                                    list.Add(new ChapterMaster
                                    {
                                        ChapterNameDisplay = i.chapternamedisplay.ToString(),
                                        ChapterOwner = i.ChapterOwner.ToString(),
                                        RunDate = i.id_data_load_date.ToString(),
                                        ChapterNameFromSystem = i.chapternamefromsystem.ToString(),
                                        EditFlag = bool.Parse(i.editflag.ToString()),
                                        IssueCount = int.Parse(i.issueCount.ToString()),
                                    });
                                }
                            
                        }
                    }
                    con1.Close();
                }
            }
         
            result.Items = list;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getIssueDetails(string Division,string ResponsibleEmail, string ChapterName, string Owner,string RoleName,string UserName)
        {
            var result = new Teckraft.Data.ListQueryResult<IssueDetails>();
            var list = new List<IssueDetails>();
            string constrSSO = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string newUserName = "";

            using (SqlConnection con1 = new SqlConnection(constrSSO))   
            {
                using (SqlCommand cmd1 = new SqlCommand())
                {
                    cmd1.CommandText = "spIssueDetails";
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@Division", Division);
                    cmd1.Parameters.AddWithValue("@ResponsibleEmail", ResponsibleEmail);
                    cmd1.Parameters.AddWithValue("@ChapterName", ChapterName);
                    cmd1.Parameters.AddWithValue("@Owner", Owner);
                    cmd1.Parameters.AddWithValue("@RoleName", RoleName);
                    cmd1.Parameters.AddWithValue("@UserName", UserName);
                    con1.Open();
                    cmd1.Connection = con1;
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd1;
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new IssueDetails
                        {
                            ChapterName = row["chapternamedisplay"].ToString(),
                            RunDate = row["id_data_load_date"].ToString(),
                            ChapterOwner = row["ChapterOwner"].ToString(),
                            RequestNo = row["ID_Request"].ToString(),
                            IssueDetail1 = row["ID_Issue_Detail1"].ToString(),
                            Status = row["ID_status"].ToString(),
                            ReportedByName = row["ID_Reported_by_name"].ToString(),
                            Department = row["ID_Dept"].ToString(),
                            Location = row["ID_Location"].ToString(),
                            PendingWithName = row["ID_Pending_with_Name"].ToString(),
                            PendingWithEmail = row["ID_Pending_with_Email"].ToString(),
                            PendingSince = Convert.ToInt16(row["pendingSince"].ToString()),
                            ResponsibleWithName = row["ID_Responsible_Name"].ToString(),
                            ResponsibleWithEmail = row["ID_Responsible_Email"].ToString(),

                            Comments = row["ID_Comments"].ToString(),
                            LoggedDate = row["ID_Logged_Date"].ToString(),
                            SystemChapterName = row["ChapterNameFromSystem"].ToString(),
                            ReportedByEmail = row["ID_Reported_by_email"].ToString(),
                            TAT = row["id_tat_status"].ToString(),
                            ID_Category = row["ID_Category"].ToString(),
                            ID_Target_Date = row["ID_Target_Date"].ToString(),
                            AnchorSentBackComments = row["AnchorSentBackComments"].ToString(),
                            AnchorCommentDate = row["AnchorCommentDate"].ToString(),
                            AnchorName =row["AnchorName"].ToString(),
                            PendingStatus = row["pendingStatus"].ToString()


                        });
                    }
                    con1.Close();
                }
            }

            result.Items = list;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getUserRole(string Username)
        {
            var result = new Teckraft.Data.ListQueryResult<UserRole>();
            var list = new List<UserRole>();
            string constrSSO = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string newUserName = "";


            using (SqlConnection con1 = new SqlConnection(constrSSO))
            {
                   SqlCommand cmd = new SqlCommand("spGetUserRole", con1);
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.Parameters.AddWithValue("@Username", Username);
                   cmd.Parameters.Add("@UserRole1", System.Data.SqlDbType.VarChar, 100).Direction = System.Data.ParameterDirection.Output;
                   cmd.Parameters.Add("@UserRole2", System.Data.SqlDbType.VarChar, 100).Direction = System.Data.ParameterDirection.Output;
                   cmd.Parameters.Add("@UserRole3", System.Data.SqlDbType.VarChar, 100).Direction = System.Data.ParameterDirection.Output;
                   cmd.Parameters.Add("@UserRole4", System.Data.SqlDbType.VarChar, 100).Direction = System.Data.ParameterDirection.Output;

                   con1.Open();
                   cmd.ExecuteNonQuery();
                   
                   list.Add(new UserRole
                   {
                       RoleName1 = cmd.Parameters["@UserRole1"].SqlValue.ToString(),
                       RoleName2 = cmd.Parameters["@UserRole2"].SqlValue.ToString(),
                       RoleName3 = cmd.Parameters["@UserRole3"].SqlValue.ToString(),
                       RoleName4 = cmd.Parameters["@UserRole4"].SqlValue.ToString(),
                   });
                   con1.Close();
            }

            result.Items = list;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getHodwiseCount(string SystemChapName, string RoleName, string Email)
        {
            var result = new Teckraft.Data.ListQueryResult<HODWiseCount>();
            var list = new List<HODWiseCount>();

            using (Teckraft.Data.Sql.InitiativeHubFinalEntities dbcontext = new Teckraft.Data.Sql.InitiativeHubFinalEntities())
            {
                var lq = dbcontext.spHodCount(SystemChapName,RoleName,Email).AsQueryable();
                foreach (var t in lq)
                {
                    list.Add(new HODWiseCount
                    {
                         SAPDivisionName=t.SAPDivisionName,
                         count=t.count,
                    });
                }

            }
            result.Items = list;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getHodName(string SystemChapterName,string RoleName,string Email)
        {
            var result = new Teckraft.Data.ListQueryResult<HodNames>();
            var list = new List<HodNames>();

            using (Teckraft.Data.Sql.InitiativeHubFinalEntities dbcontext = new Teckraft.Data.Sql.InitiativeHubFinalEntities())
            {
                //var lq = dbcontext.spHodName(SystemChapterName);
                var lq = dbcontext.spHodName(SystemChapterName, RoleName,Email);
                foreach (var t in lq)
                {
                    list.Add(new HodNames
                    {
                        SAPDivisionName = t.SAPDivisionName,
                        SAPDivisionCode = t.SAPDivisionCode,
                        HodEmail = t.HodEmail,
                    });
                }
            }
            result.Items = list;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getColumnDetails(int UserId,string SystemChapterName)
        {
            var result = new Teckraft.Data.ListQueryResult<UserWiseColumnName>();
            var list = new List<UserWiseColumnName>();

            using (Teckraft.Data.Sql.InitiativeHubFinalEntities dbcontext = new Teckraft.Data.Sql.InitiativeHubFinalEntities())
            {
                var chap = dbcontext.ChapterMasters.FirstOrDefault(it => it.ChapterNameFromSystem == SystemChapterName);
                if (chap != null) {
                    var lq = dbcontext.UserWiseColumnNames.FirstOrDefault(it => it.ChapterId == chap.Id && it.UserId == UserId);
                    if (lq != null) {
                       // lq.ChapterId = chap.Id;
                        list.Add(mapper.Map(lq));
                    }
                 }
                // var lq = dbcontext.(SystemChapterName, RoleName, Email);
                //foreach (var t in lq)
                //{
                //    list.Add(new HodNames
                //    {
                //        SAPDivisionName = t.SAPDivisionName,
                //        SAPDivisionCode = t.SAPDivisionCode,
                //        HodEmail = t.HodEmail,
                //    });
                //}
            }
            result.Items = list;
           // return Json(result, JsonRequestBehavior.AllowGet);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public string getExcelReport(ExcelAction req,string ChapterName)
        {
            System.Data.OleDb.OleDbConnection conn = null;
            string concateFile = "";
            try
            {
                string filename = "";
                concateFile = "\\datafiles\\Dashboard\\IssueDetails" + HttpContext.User.Identity.Name + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString() + ".xls";

                var oldfilename = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\datafiles\\IssueDetails.xlsx";
                filename = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + concateFile;

                System.IO.File.Copy(oldfilename, filename, true);

                //Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=YES;\""
                //Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=YES;\""

                conn = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + "; Extended Properties=\"Excel 12.0 Xml;HDR=NO\";");
                conn.Open();
                int i = 1;
                
                foreach (var item in req.Items)
                {
                        var strsql = "insert into [Sheet1$A" + (i) + ":K" + (i) + "] (f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11)values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')";
                        var cmd = conn.CreateCommand();
                        cmd.CommandText = string.Format(strsql,
                            item.RequestNo,
                           // item.IssueDetail1,
                           "Aaliya",
                            item.Comments,
                            item.Status,
                            item.ReportedByName,
                            item.Department,
                            item.Location,
                            item.PendingWithEmail,
                            item.ResponsibleWithEmail,
                            item.PendingSince,
                            item.LoggedDate
                            );
                        var rowcount = cmd.ExecuteNonQuery();
                        i++;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open) conn.Close();
            }
            var filePath = concateFile;
            return filePath;

        }
        public System.Web.Mvc.ActionResult DownloadExcelInitiative(string path)
        {
            return File(Request.PhysicalApplicationPath + path, "Application/MS-excel", "IssueDetails.xls");
        }

        public System.Web.Mvc.ActionResult ExportFullDumpReport(string ResponsibleEmail,string ChapterName,string Owner,string RoleName,string UserName,string TAT)
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            DataTable table = new DataTable();
            
            using (SqlConnection con1 = new SqlConnection(constr))
            {
                using (SqlCommand cmd1 = new SqlCommand())
                {
                    cmd1.CommandText = "spIssueDetails";
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@Division", ResponsibleEmail);
                    cmd1.Parameters.AddWithValue("@ResponsibleEmail", ResponsibleEmail);
                    cmd1.Parameters.AddWithValue("@ChapterName", ChapterName);
                    cmd1.Parameters.AddWithValue("@Owner", Owner);
                    cmd1.Parameters.AddWithValue("@RoleName", RoleName);
                    cmd1.Parameters.AddWithValue("@UserName", UserName);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    con1.Open();
                    cmd1.Connection = con1;
                    //  da.SelectCommand = cmd1;
                    //da.Fill(dt);
                    using (var da = new SqlDataAdapter(cmd1))
                    {
                        da.Fill(table);
                    }
                    // }
                   
                    for (int c = 0; c < table.Rows.Count - 1; c++)
                    {
                        ListQuery<IssueManagementCommentLog> query = new ListQuery<IssueManagementCommentLog>();
                        query.Parameters = new List<QueryParameter>();
                        string RequestNo = table.Rows[c]["ID_Request"].ToString();
                        string ChapterNameNew = table.Rows[c]["ChapterNameFromSystem"].ToString();
                        string PendingWithEmail = table.Rows[c]["ID_Pending_with_Email"].ToString();
                        string ResponsibleWithEmail = table.Rows[c]["ID_Responsible_Email"].ToString();

                        if (ChapterNameNew == "MyPidilite" || ChapterNameNew == "WSS Service Cell")
                        {
                            query.Parameters.Add(new QueryParameter() { Name = "RequestNo", Value = RequestNo });
                            query.Parameters.Add(new QueryParameter() { Name = "ChapterName", Value = ChapterNameNew });
                            query.Parameters.Add(new QueryParameter() { Name = "ID_Responsible_Email", Value = ResponsibleWithEmail });
                        }
                        else {
                            query.Parameters.Add(new QueryParameter() { Name = "RequestNo", Value = RequestNo });
                            query.Parameters.Add(new QueryParameter() { Name = "ChapterName", Value = ChapterNameNew });
                            query.Parameters.Add(new QueryParameter() { Name = "PendingWithEmail", Value = PendingWithEmail });
                        }

                        var data = commentLogService.GetByQuery(query);
                        if (data.Items.Count > 0) {
                            table.Rows[c]["ID_Comments"] = data.Items[0].ID_Comments.ToString();
                        }
                    }
                }

                DataRow[] DR=null;


                System.Web.UI.WebControls.GridView gd = new System.Web.UI.WebControls.GridView();
               
                if (ChapterName == "Market_Sudhar" || ChapterName== "Pilworld_WSS_feedback") {
                    gd.Columns.Add(new BoundField() { HeaderText = "Issue Details", DataField = "ID_Issue_Detail1" });
                    //gd.Columns.Add(new BoundField() { HeaderText = "Location", DataField = "ID_Location" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Name", DataField = "ID_Pending_with_Email" });
                    //gd.Columns.Add(new BoundField() { HeaderText = "PendingSince", DataField = "pendingSince" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Comment", DataField = "ID_Comments" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Anchor Comment", DataField = "AnchorSentBackComments" });

                    if (TAT == "Beyond")
                    {
                         DR = table.Select("PendingSince>45");
                    }
                    else if (TAT == "Within")
                    {
                         DR = table.Select("PendingSince<=45");
                    }
                }
                else if (ChapterName == "Dealer Feedback")
                {
                    //gd.Columns.Add(new BoundField() { HeaderText = "Request No.", DataField = "ID_Request" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Issue Details", DataField = "ID_Issue_Detail1" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Name", DataField = "ID_Pending_with_Email" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Comment", DataField = "ID_Comments" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Anchor Comment", DataField = "AnchorSentBackComments" });

                    //gd.Columns.Add(new BoundField() { HeaderText = "Action With Timeline", DataField = "ID_Comments" });


                    if (TAT == "Beyond")
                    {
                        DR = table.Select("PendingSince>7");
                    }
                    else if (TAT == "Within")
                    {
                        DR = table.Select("PendingSince<=7");
                    }
                }
                else if (ChapterName == "Customer Service Cell")
                {
                    //gd.Columns.Add(new BoundField() { HeaderText = "Request No.", DataField = "ID_Request" });
                    //gd.Columns.Add(new BoundField() { HeaderText = "Issue Details", DataField = "ID_Issue_Detail1" });
                    //gd.Columns.Add(new BoundField() { HeaderText = "PendingWithEmail", DataField = "ID_Pending_with_Email" });
                    //gd.Columns.Add(new BoundField() { HeaderText = "Action With Timeline", DataField = "ID_Comments" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Issue Details", DataField = "ID_Issue_Detail1" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Name", DataField = "ID_Pending_with_Email" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Comment", DataField = "ID_Comments" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Anchor Comment", DataField = "AnchorSentBackComments" });

                    if (TAT == "Beyond")
                    {
                        DR = table.Select("PendingSince>7");
                    }
                    else if (TAT == "Within")
                    {
                        DR = table.Select("PendingSince<=7");
                    }
                }
                else if (ChapterName == "MyPidilite")
                {
                    gd.Columns.Add(new BoundField() { HeaderText = "Issue Details", DataField = "ID_Issue_Detail1" });
                    //gd.Columns.Add(new BoundField() { HeaderText = "PendingWithEmail", DataField = "ID_Pending_with_Email" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Name", DataField = "ID_Responsible_Email" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Latest Comments", DataField = "ID_Comments" });


                    if (TAT == "Beyond")
                    {
                        DR = table.Select("id_TAT_status='Beyond TAT'");
                    }
                    else if (TAT == "Within")
                    {
                        DR = table.Select("id_TAT_status='Within TAT'");
                    }
                }
                else if (ChapterName == "WSS Service Cell")
                {
                    //gd.Columns.Add(new BoundField() { HeaderText = "Request No.", DataField = "ID_Request" });
                    //gd.Columns.Add(new BoundField() { HeaderText = "Issue Details", DataField = "ID_Issue_Detail1" });
                    //gd.Columns.Add(new BoundField() { HeaderText = "Reported By Name", DataField = "ID_Reported_by_name" });
                    //gd.Columns.Add(new BoundField() { HeaderText = "Location", DataField = "ID_Location" });
                    //gd.Columns.Add(new BoundField() { HeaderText = "PendingWithEmail", DataField = "ID_Pending_with_Email" });
                    //gd.Columns.Add(new BoundField() { HeaderText = "ResponsibleWithEmail", DataField = "ID_Responsible_Email" });

                    gd.Columns.Add(new BoundField() { HeaderText = "Issue Details", DataField = "ID_Issue_Detail1" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Name", DataField = "ID_Responsible_Email" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Comment", DataField = "ID_Comments" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Anchor Comment", DataField = "AnchorSentBackComments" });
                    
                    if (TAT == "Beyond")
                    {
                        DR = table.Select("id_TAT_status='Beyond TAT'");
                    }
                    else if (TAT == "Within")
                    {
                        DR = table.Select("id_TAT_status='Within TAT'");
                    }
                }
                else if (ChapterName == "Risk_Management")
                {
                    //gd.Columns.Add(new BoundField() { HeaderText = "Request No.", DataField = "ID_Request" });
                    //gd.Columns.Add(new BoundField() { HeaderText = "Category", DataField = "ID_Category" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Issue Details", DataField = "ID_Issue_Detail1" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Name", DataField = "ID_Pending_with_Email" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Comment", DataField = "ID_Comments" });
                    //gd.Columns.Add(new BoundField() { HeaderText = "Target Date", DataField = "ID_Target_Date" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Anchor Comment", DataField = "AnchorSentBackComments" });

                    DR = table.Select("PendingSince>0");

                }
                else if (ChapterName == "BirthdayLunch" || ChapterName == "Sampark" || ChapterName== "Suggestion_at_Facebook" || ChapterName== "SimplifiedSuggestion")
                {
                  
                    gd.Columns.Add(new BoundField() { HeaderText = "Issue Details", DataField = "ID_Issue_Detail1" });
                    //gd.Columns.Add(new BoundField() { HeaderText = "Reported By Name", DataField = "ID_Reported_by_name" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Name", DataField = "ID_Pending_with_Email" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Comment", DataField = "ID_Comments" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Anchor Comment", DataField = "AnchorSentBackComments" });
                    //gd.Columns.Add(new BoundField() { HeaderText = "PendingSince", DataField = "pendingSince" });

                    DR = table.Select("PendingSince>0");

                }
                DataTable dtFinal = table.Clone();
                foreach (DataRow d in DR)
                {
                    dtFinal.ImportRow(d);
                }
                gd.DataSource = dtFinal;//table;
                gd.AutoGenerateColumns = false;



                //gd.Columns.Add(new BoundField() { HeaderText = "Request No.", DataField = "ID_Request" });
                //gd.Columns.Add(new BoundField() { HeaderText = "Issue Details", DataField = "ID_Issue_Detail1" });
                //gd.Columns.Add(new BoundField() { HeaderText = "Comments", DataField = "ID_Comments" });
                ////gd.Columns.Add(new BoundField() { HeaderText = "Status", DataField = "ID_status" });
                //gd.Columns.Add(new BoundField() { HeaderText = "Reported By Name", DataField = "ID_Reported_by_name" });
                //gd.Columns.Add(new BoundField() { HeaderText = "Department", DataField = "ID_Dept" });
                //gd.Columns.Add(new BoundField() { HeaderText = "Location", DataField = "ID_Location" });
                //gd.Columns.Add(new BoundField() { HeaderText = "PendingWithEmail", DataField = "ID_Pending_with_Email" });
                //gd.Columns.Add(new BoundField() { HeaderText = "PendingSince", DataField = "pendingSince" });
                //gd.Columns.Add(new BoundField() { HeaderText = "LoggedDate", DataField = "ID_Logged_Date" });
                gd.DataBind();

                var sb = new System.Text.StringBuilder();
                var tw = new System.IO.StringWriter(sb);

                var htw = new System.Web.UI.HtmlTextWriter(tw);
                gd.RenderControl(htw);

                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=IssueDetails.xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                Response.Output.Write(sb.ToString());
                Response.Flush();
                Response.End();

                return RedirectToAction("Full Dump Report");

                con1.Close();
            }
        }

        public System.Web.Mvc.ActionResult ExportFullDumpReportClose( string ChapterName)
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            DataTable table = new DataTable();

            using (SqlConnection con1 = new SqlConnection(constr))
            {
                using (SqlCommand cmd1 = new SqlCommand())
                {
                    cmd1.CommandText = "SpissuedetailsClose";
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@ChapterName", ChapterName);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    con1.Open();
                    cmd1.Connection = con1;
                    using (var da = new SqlDataAdapter(cmd1))
                    {
                        da.Fill(table);
                    }

                    //for (int c = 0; c < table.Rows.Count - 1; c++)
                    //{
                    //    ListQuery<IssueManagementCommentLog> query = new ListQuery<IssueManagementCommentLog>();
                    //    query.Parameters = new List<QueryParameter>();
                    //    string RequestNo = table.Rows[c]["ID_Request"].ToString();
                    //    string ChapterNameNew = table.Rows[c]["ChapterNameFromSystem"].ToString();
                    //    string PendingWithEmail = table.Rows[c]["ID_Pending_with_Email"].ToString();

                    //    query.Parameters.Add(new QueryParameter() { Name = "RequestNo", Value = RequestNo });
                    //    query.Parameters.Add(new QueryParameter() { Name = "ChapterName", Value = ChapterNameNew });
                    //    query.Parameters.Add(new QueryParameter() { Name = "PendingWithEmail", Value = PendingWithEmail });

                    //    var data = commentLogService.GetByQuery(query);
                    //    if (data.Items.Count > 0)
                    //    {
                    //        table.Rows[c]["ID_Comments"] = data.Items[0].ID_Comments.ToString();
                    //    }

                    //}
                }

                DataRow[] DR = null;


                System.Web.UI.WebControls.GridView gd = new System.Web.UI.WebControls.GridView();

                if (ChapterName == "Market_Sudhar" || ChapterName == "Pilworld_WSS_feedback")
                {
                    //gd.Columns.Add(new BoundField() { HeaderText = "Department", DataField = "ID_dept" });
                    //gd.Columns.Add(new BoundField() { HeaderText = "Location", DataField = "ID_Location" });
                    //gd.Columns.Add(new BoundField() { HeaderText = "PendingSince", DataField = "pendingSince" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Issue Details", DataField = "ID_Issue_Detail1" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Name", DataField = "ID_Pending_with_Email" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Last Comments", DataField = "ID_Comments" });

                    
                }
                else if (ChapterName == "Dealer Feedback")
                {
                    //gd.Columns.Add(new BoundField() { HeaderText = "Request No.", DataField = "ID_Request" });
                    //gd.Columns.Add(new BoundField() { HeaderText = "Department", DataField = "ID_dept" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Issue Details", DataField = "ID_Issue_Detail1" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Name", DataField = "ID_Pending_with_Email" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Last Comments", DataField = "ID_Comments" });                    
                }
                else if (ChapterName == "Customer Service Cell")
                {
                    //gd.Columns.Add(new BoundField() { HeaderText = "Request No.", DataField = "ID_Request" });
                    //gd.Columns.Add(new BoundField() { HeaderText = "Department", DataField = "ID_dept" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Issue Details", DataField = "ID_Issue_Detail1" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Name", DataField = "ID_Pending_with_Email" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Last Comments", DataField = "ID_Comments" });                    
                }
                else if (ChapterName == "MyPidilite")
                {
                    //gd.Columns.Add(new BoundField() { HeaderText = "Department", DataField = "ID_dept" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Issue Details", DataField = "ID_Issue_Detail1" });                    
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Name", DataField = "ID_Responsible_Email" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Last Comments", DataField = "ID_Comments" });                    
                }
                else if (ChapterName == "WSS Service Cell")
                {
                    //gd.Columns.Add(new BoundField() { HeaderText = "Request No.", DataField = "ID_Request" });
                    //gd.Columns.Add(new BoundField() { HeaderText = "Department", DataField = "ID_dept" });
                    //gd.Columns.Add(new BoundField() { HeaderText = "Reported By Name", DataField = "ID_Reported_by_name" });
                    //gd.Columns.Add(new BoundField() { HeaderText = "Location", DataField = "ID_Location" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Issue Details", DataField = "ID_Issue_Detail1" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Name", DataField = "ID_Responsible_Email" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Last Comments", DataField = "ID_Comments" });                    
                }
                else if (ChapterName == "Birthday Lunch")
                {
                    gd.Columns.Add(new BoundField() { HeaderText = "Issue Details", DataField = "ID_Issue_Detail1" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Name", DataField = "ID_Responsible_Email" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Last Comments", DataField = "ID_Comments" });
                }
                else if (ChapterName == "Simplified Suggestion")
                {
                    gd.Columns.Add(new BoundField() { HeaderText = "Issue Details", DataField = "ID_Issue_Detail1" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Name", DataField = "ID_Responsible_Email" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Last Comments", DataField = "ID_Comments" });
                }
                else if (ChapterName == "Sampark")
                {
                    gd.Columns.Add(new BoundField() { HeaderText = "Issue Details", DataField = "ID_Issue_Detail1" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Name", DataField = "ID_Responsible_Email" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Last Comments", DataField = "ID_Comments" });
                }
                else if (ChapterName == "Pidilite Ideas Initiatives")
                {
                    gd.Columns.Add(new BoundField() { HeaderText = "Issue Details", DataField = "ID_Issue_Detail1" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Name", DataField = "ID_Responsible_Email" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Last Comments", DataField = "ID_Comments" });
                }
                else if (ChapterName == "Risk Management")
                {
                    gd.Columns.Add(new BoundField() { HeaderText = "Issue Details", DataField = "ID_Issue_Detail1" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Name", DataField = "ID_Responsible_Email" });
                    gd.Columns.Add(new BoundField() { HeaderText = "Collaborator Last Comments", DataField = "ID_Comments" });
                }
                //DataTable dtFinal = table.Clone();
                //foreach (DataRow d in DR)
                //{
                //    dtFinal.ImportRow(d);
                //}
                 var col = gd.Columns.Count;
                gd.DataSource = table;
                gd.AutoGenerateColumns = false;
                //gd.Columns.Add(new BoundField() { HeaderText = "Request No.", DataField = "ID_Request" });
                //gd.Columns.Add(new BoundField() { HeaderText = "Issue Details", DataField = "ID_Issue_Detail1" });
                //gd.Columns.Add(new BoundField() { HeaderText = "Comments", DataField = "ID_Comments" });
                ////gd.Columns.Add(new BoundField() { HeaderText = "Status", DataField = "ID_status" });
                //gd.Columns.Add(new BoundField() { HeaderText = "Reported By Name", DataField = "ID_Reported_by_name" });
                //gd.Columns.Add(new BoundField() { HeaderText = "Department", DataField = "ID_Dept" });
                //gd.Columns.Add(new BoundField() { HeaderText = "Location", DataField = "ID_Location" });
                //gd.Columns.Add(new BoundField() { HeaderText = "PendingWithEmail", DataField = "ID_Pending_with_Email" });
                //gd.Columns.Add(new BoundField() { HeaderText = "PendingSince", DataField = "pendingSince" });
                //gd.Columns.Add(new BoundField() { HeaderText = "LoggedDate", DataField = "ID_Logged_Date" });
                gd.DataBind();

                var sb = new System.Text.StringBuilder();
                var tw = new System.IO.StringWriter(sb);

                var htw = new System.Web.UI.HtmlTextWriter(tw);
                gd.RenderControl(htw);

                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=IssueDetailsClose.xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                Response.Output.Write(sb.ToString());
                Response.Flush();
                Response.End();

                return RedirectToAction("Full Dump Report");

                con1.Close();
            }
        }
    }
}

