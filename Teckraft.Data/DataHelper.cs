using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teckraft.Data
{
   public static class DataHelper
    {
       //private static int getFA(int divisionId, int businessfunctionId, string projectCategory) { 
       //    return GetFAUser(divisionId, businessfunctionId, projectCategory).Id;
       ////return 1;
       //}

       //public static Core.Domian.Settings.User GetFAUser(int divisionId, int businessfunctionId, string projectCategory)
       //{

       //    using (var dbcontext = new Teckraft.Data.Sql.InitiativeHubFinalEntities())
       //    {
       //        var rec = dbcontext.FunctionApprovers.FirstOrDefault(it => it.DivisionId == divisionId && it.BusinessFunctionId == businessfunctionId &&
       //            it.ProjectCategory.ProjectCategoryCode == projectCategory);
       //        if (rec == null) return new Core.Domian.Settings.User() { Id = 1 };
       //        else return new Core.Domian.Settings.User() { Id = rec.UserId, Title = rec.UserDetail.Title, Email = rec.UserDetail.Email, UserName = rec.UserDetail.UserName };
       //    }
       //}

       //public static Core.Domian.Settings.User GetITEvaluator(string ProjectCategory,int TotalCost)
       //{
           //using (var dbcontext = new Teckraft.Data.Sql.InitiativeHubFinalEntities())
           //{
           //    var rec = dbcontext.UserDetailss.FirstOrDefault(it => it.IsITEvaluator == true);
           //    if (ProjectCategory == "A" && TotalCost >= 1000000)
           //    {
           //        rec = dbcontext.UserDetailss.FirstOrDefault(it => it.UserId == rec.RA1.Value);
           //    }
           //    return new Core.Domian.Settings.User() { Id = rec.UserId, Title = rec.Title, Email = rec.Email, UserName = rec.UserName };
           //}
      // }

       public static Core.Domian.Settings.User GetInfraUser()
       {
           using (
           var dbcontext = new Teckraft.Data.Sql.InitiativeHubFinalEntities())
           {
               var rec = dbcontext.UserDetails.FirstOrDefault(it => it.UserId == 9794);
               return new Core.Domian.Settings.User() { Id = rec.UserId, Title = rec.Title, Email = rec.Email, UserName = rec.UserName };
           }
           
       }

       public static Core.Domian.Settings.User GetBasisUser()
       {
           using (
          var dbcontext = new Teckraft.Data.Sql.InitiativeHubFinalEntities())
           {
               var rec = dbcontext.UserDetails.FirstOrDefault(it => it.UserId == 10723);
               return new Core.Domian.Settings.User() { Id = rec.UserId, Title = rec.Title, Email = rec.Email, UserName = rec.UserName };
           }
       }
    }
}
