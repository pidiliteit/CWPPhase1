using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teckraft.Core.Domian.Loging;
//using Teckraft.Core.Domian.Reports;
//using Teckraft.Core.Domian.SAP;
using Teckraft.Core.Domian.Settings;
using Teckraft.Core.Domian.Transactions;
using Teckraft.Core.Workflow;
using Teckraft.Data;
using Teckraft.Data.Mappings;
//using Teckraft.Data.Reports;
using Teckraft.Data.Settings;
//using Teckraft.Data.Sql.Transaction;
using Teckraft.Data.Transaction;
//using Teckraft.Data.Transaction;
//using Teckraft.Data.Transaction;
using Teckraft.Services;
using Teckraft.Services.Loging;
using Teckraft.Services.Notification;
using Teckraft.Services.Settings;
//using Teckraft.Services.Transaction;

namespace Teckraft.Web.Framework.Infrastructure
{
    public class RegisterDependency
    {
        public static void Register(IUnityContainer container)
        {

            container.RegisterType<Teckraft.Data.Mappings.IMappingProvider<Teckraft.Core.Domian.SAP.Division, Teckraft.Data.Sql.Division>, Teckraft.Data.Mappings.DivisionMappingProvider>();
            container.RegisterType<Teckraft.Data.Mappings.IMappingProvider<User, Teckraft.Data.Sql.UserProfile>, Teckraft.Data.Mappings.UserMappingProvider>();
            container.RegisterType<Teckraft.Data.Mappings.IMappingProvider<User, Teckraft.Data.Sql.UserDetail>, Teckraft.Data.Mappings.UserDetailMapping>();

            container.RegisterType<Teckraft.Data.Mappings.IMappingProvider<HodNames, Teckraft.Data.Sql.spHodName_Result>, Teckraft.Data.Mappings.HodNamesMapping>();
            container.RegisterType<Teckraft.Data.Mappings.IMappingProvider<HODWiseCount, Teckraft.Data.Sql.spHodCount_Result>, Teckraft.Data.Mappings.HODWiseCountMapping>();

            container.RegisterType<Teckraft.Data.Mappings.IMappingProvider<IssueManagementAnchorComment, Teckraft.Data.Sql.IssueManagementAnchorComment>, Teckraft.Data.Mappings.IssueManagementAnchorCommentMapping>();
            container.RegisterType<IService<Teckraft.Core.Domian.Settings.IssueManagementAnchorComment>, Services.Settings.IssueManagementAnchorCommentService>();
            container.RegisterType<IRepository<Teckraft.Core.Domian.Settings.IssueManagementAnchorComment>, Data.Sql.Transaction.IssueManagementAnchorCommentRepository>();

            container.RegisterType<IReadService<Teckraft.Core.Domian.SAP.Division>, Services.SAP.DivisionService>();
            container.RegisterType<IReadableRepository<Teckraft.Core.Domian.SAP.Division>, Data.SAP.DivisionRepository>();
            container.RegisterType<IService<User>, Services.Settings.UserService>();
            container.RegisterType<IRepository<User>, Data.Settings.UserRepository>();
            container.RegisterType<IEmailService, EmailService>();
            container.RegisterType<IService<EmailTemplate>, TemplateService>();
            container.RegisterType<IRepository<EventLog>, EventLogRepository>();
            container.RegisterType<IService<EventLog>, EventLogService>();
            container.RegisterType<IReadableRepository<Department>,DepartmentRepository>();
            container.RegisterType<IReadService<Department>, DepartmentService>();
            container.RegisterType<IMappingProvider<Department, Teckraft.Data.Sql.Division>,DepartmentMappingProvider>();

            container.RegisterType<Teckraft.Data.Mappings.IMappingProvider<IssueManagementCommentLog, Teckraft.Data.Sql.IssueManagementCommentLog>, Teckraft.Data.Mappings.IssueManagementCommentLogMapping>();
            container.RegisterType<IService<Teckraft.Core.Domian.Settings.IssueManagementCommentLog>, Services.Settings.IssueManagementCommentLogService>();
            container.RegisterType<IRepository<Teckraft.Core.Domian.Settings.IssueManagementCommentLog>, Data.Sql.Transaction.IssueManagementCommentLogRepository>();

            container.RegisterType<Teckraft.Data.Mappings.IMappingProvider<ChapterMaster, Teckraft.Data.Sql.ChapterMaster>, Teckraft.Data.Mappings.ChapterMasterMapping>();
            container.RegisterType<Teckraft.Data.Mappings.IMappingProvider<StatusMaster, Teckraft.Data.Sql.StatusMaster>, Teckraft.Data.Mappings.StatusMasterMapping>();

            container.RegisterType<Teckraft.Data.Mappings.IMappingProvider<UserWiseColumnName, Teckraft.Data.Sql.UserWiseColumnName>, Teckraft.Data.Mappings.UserWiseColumnNameMapping>();
        }
    }
}