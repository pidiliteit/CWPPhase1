using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.Settings;
using Teckraft.Data.Mappings;

namespace Teckraft.Data.Sql.Transaction
{
    public class IssueManagementCommentLogRepository : IRepository<Teckraft.Core.Domian.Settings.IssueManagementCommentLog>
    {
        private readonly IMappingProvider<Teckraft.Core.Domian.Settings.IssueManagementCommentLog, Sql.IssueManagementCommentLog> mappingProvider;
        public IssueManagementCommentLogRepository(IMappingProvider<Teckraft.Core.Domian.Settings.IssueManagementCommentLog, Sql.IssueManagementCommentLog> mappingProvider)
        {
            this.mappingProvider = mappingProvider;
        }

        public List<Core.Domian.Settings.IssueManagementCommentLog> ToList
        {
            get { throw new NotImplementedException(); }
        }

        public ListQueryResult<Core.Domian.Settings.IssueManagementCommentLog> GetByQuery(ListQuery<Core.Domian.Settings.IssueManagementCommentLog> query)
        {
            var result = new ListQueryResult<Teckraft.Core.Domian.Settings.IssueManagementCommentLog>();
            var list = new List<Teckraft.Core.Domian.Settings.IssueManagementCommentLog>();

            using (Teckraft.Data.Sql.InitiativeHubFinalEntities dbcontext = new Teckraft.Data.Sql.InitiativeHubFinalEntities())
            {
                var linqquery = dbcontext.IssueManagementCommentLogs.Where(it => it.Id > 0);
                if (query.Parameters.Count > 0)
                {
                    foreach (var item in query.Parameters)
                    {
                        if (item.Name == "RequestNo")
                        {
                            var val = item.Value;
                            linqquery = linqquery.Where(k => k.ID_Request == val);
                        }
                        else if (item.Name == "ChapterName")
                        {
                            var val = item.Value;
                            linqquery = linqquery.Where(j => j.id_System_Name == val);
                        }
                        else if (item.Name == "PendingWithEmail")
                        {
                            var val = (item.Value);
                            linqquery = linqquery.Where(it => (it.ID_Pending_With_Email == val));
                        }
                    }
                    linqquery = linqquery.OrderByDescending(it => it.RCT); 
                    foreach (var dbitem in linqquery)
                    {
                        list.Add(mappingProvider.Map(dbitem));
                    }

                }
            }

            result.Items = list;
            return result;
        }

        public Core.Domian.Settings.IssueManagementCommentLog GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Core.Domian.Settings.IssueManagementCommentLog Add(Core.Domian.Settings.IssueManagementCommentLog item)
        {
            using (var dbcontext = new Sql.InitiativeHubFinalEntities())
            {
                if (item.ID_Comments != null)
                {
                        var linqquery = dbcontext.IssueManagementCommentLogs.Where(it => it.id_System_Name == item.id_System_Name && it.ID_Request == item.ID_Request && it.ID_Comments == item.ID_Comments && it.ID_Pending_With_Email == item.ID_Pending_With_Email && it.ID_Status_Id == item.ID_Status_Id.Id).ToList();                    
                    if (linqquery.Count == 0)
                    {
                        var lgdb = dbcontext.IssueManagementCommentLogs.Where(it => it.id_System_Name == item.id_System_Name && it.ID_Request == item.ID_Request && it.ID_Pending_With_Email == item.ID_Pending_With_Email).ToList();
                        foreach (var f in lgdb)
                        {
                            f.Active = false;
                            f.RUB = item.RCB.Id;
                            f.RUT = DateTime.Now;
                            f.RUBEmail=item.RCB.Email;
                            f.RUBEmpCode = item.RCB.UserName;
                            f.RUBEmpName=item.RCB.Title;
                        }
                        dbcontext.SaveChanges();
                        string status = "";
                        if (item.ID_Status != "" || item.ID_Status != null)
                        {
                            if (item.ID_Status.ToLower() == "in progress") status = "Open";
                            if (item.ID_Status.ToLower() == "completed") status = "Closed";
                        }
                        else status = null;
                        dbcontext.IssueManagementCommentLogs.Add(new Sql.IssueManagementCommentLog()
                        {

                            ID_Comments = item.ID_Comments,
                            ID_Request = item.ID_Request,
                            ID_Pending_With_Email = item.ID_Pending_With_Email,
                            id_System_Name = item.id_System_Name,
                            ID_Status = status,
                            Active = true,
                            RCT = DateTime.Now,
                            RCB = item.RCB.Id,
                            ID_Status_Id = item.ID_Status_Id.Id,
                            RCBEmail=item.RCB.Email,
                            RCBEmpCode=item.RCB.UserName,
                            RCBEmpName=item.RCB.Title,
                            UploadFlag=false,
                        });
                        dbcontext.SaveChanges();
                        item.RCBEmpCode = item.RCB.UserName;
                        item.RCBEmpName = item.RCB.Title;
                        item.RCBEmail = item.RCB.Email;


                        item.EmailFlag = true;
                    }
                    else {
                        item.EmailFlag = false;
                    }
                }
             }
            return item;
        }

        public Core.Domian.Settings.IssueManagementCommentLog Update(Core.Domian.Settings.IssueManagementCommentLog item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
