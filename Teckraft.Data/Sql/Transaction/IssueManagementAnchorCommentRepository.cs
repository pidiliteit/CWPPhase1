using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.Settings;
using Teckraft.Data.Mappings;

namespace Teckraft.Data.Sql.Transaction
{
    public class IssueManagementAnchorCommentRepository : IRepository<Teckraft.Core.Domian.Settings.IssueManagementAnchorComment>
    {
        private readonly IMappingProvider<Teckraft.Core.Domian.Settings.IssueManagementAnchorComment, Sql.IssueManagementAnchorComment> mappingProvider;
        public IssueManagementAnchorCommentRepository(IMappingProvider<Teckraft.Core.Domian.Settings.IssueManagementAnchorComment, Sql.IssueManagementAnchorComment> mappingProvider)
        {
            this.mappingProvider = mappingProvider;
        }

        public List<Core.Domian.Settings.IssueManagementAnchorComment> ToList
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Core.Domian.Settings.IssueManagementAnchorComment Add(Core.Domian.Settings.IssueManagementAnchorComment item)
        {
            using (var dbcontext = new Sql.InitiativeHubFinalEntities())
            {
                if (item.ID_Comments != null)
                {
                    var linqquery = dbcontext.IssueManagementAnchorComments.Where(it => it.id_System_Name == item.id_System_Name && it.ID_Request == item.ID_Request && it.ID_Comments == item.ID_Comments && it.ID_Pending_With_Email == item.ID_Pending_With_Email).ToList();
                    if (linqquery.Count == 0)
                    {
                        var lgdb = dbcontext.IssueManagementAnchorComments.Where(it => it.id_System_Name == item.id_System_Name && it.ID_Request == item.ID_Request && it.ID_Pending_With_Email == item.ID_Pending_With_Email).ToList();
                        foreach (var f in lgdb)
                        {
                            f.Active = false;
                            f.RUB = item.RCB.Id;
                            f.RUT = DateTime.Now;
                        }
                        dbcontext.SaveChanges();


                        dbcontext.IssueManagementAnchorComments.Add(new Sql.IssueManagementAnchorComment()
                        {
                            ID_Comments = item.ID_Comments,
                            ID_Request = item.ID_Request,
                            ID_Pending_With_Email = item.ID_Pending_With_Email,
                            id_System_Name = item.id_System_Name,
                            Active = true,
                            RCT = DateTime.Now,
                            RCB = item.RCB.Id,
                         });
                        dbcontext.SaveChanges();
                        
                    }
                    
                }
            }
            return item;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Core.Domian.Settings.IssueManagementAnchorComment GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ListQueryResult<Core.Domian.Settings.IssueManagementAnchorComment> GetByQuery(ListQuery<Core.Domian.Settings.IssueManagementAnchorComment> query)
        {
            var result = new ListQueryResult<Teckraft.Core.Domian.Settings.IssueManagementAnchorComment>();
            var list = new List<Teckraft.Core.Domian.Settings.IssueManagementAnchorComment>();

            using (Teckraft.Data.Sql.InitiativeHubFinalEntities dbcontext = new Teckraft.Data.Sql.InitiativeHubFinalEntities())
            {
                var linqquery = dbcontext.IssueManagementAnchorComments.Where(it => it.Id > 0);
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
                        //else if (item.Name == "PendingWithEmail")
                        //{
                        //    var val = (item.Value);
                        //    linqquery = linqquery.Where(it => (it.ID_Pending_With_Email == val));
                        //}
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

        public Core.Domian.Settings.IssueManagementAnchorComment Update(Core.Domian.Settings.IssueManagementAnchorComment item)
        {
            throw new NotImplementedException();
        }
    }
}
