using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Teckraft.Core.Domian.SAP;
using Teckraft.Core.Domian.Settings;
using Teckraft.Core.Domian.Transactions;
using Teckraft.Data;
using Teckraft.Data.Mappings;
//using Teckraft.Data.SAP;
using Teckraft.Data.Settings;
//using Teckraft.Data.Transaction;
using Teckraft.Services;
//using Teckraft.Services.SAP;
//using Teckraft.Services.Transaction;
using Microsoft.Practices.Unity;
namespace TeckraftBuilds.WFActivities
{
   public class CustomUnityContainer
    {
       private static IUnityContainer _container = null;
       public static IUnityContainer Container {
           get {
               if (_container == null) _container = GetContainer();
               return _container;
           }
       }

       private static IUnityContainer GetContainer()
       {
           var container = new UnityContainer();
           Teckraft.Web.Framework.Infrastructure.RegisterDependency.Register(container);

          // container.RegisterType<Teckraft.Services.IService<Reason>, Teckraft.Services.Settings.ReasonService>();
          // //container.RegisterType<Teckraft.Data.IRepository<Reason>, Teckraft.Data.Settings.ReasonDbRepository>();
          //// container.RegisterType<IService<Rlam>, RlamService>();
          // //container.RegisterType<Teckraft.Data.IRepository<Rlam>, RlamDbRepository>();
          // //container.RegisterType<Teckraft.Data.Mappings.IMappingProvider<Rlam, Teckraft.Data.Sql.RLAMUserMapping>, Teckraft.Data.Mappings.RlamMappingProvider>();
          // //container.RegisterType<Teckraft.Data.Mappings.IMappingProvider<Reason, Teckraft.Data.Sql.Reason>, Teckraft.Data.Mappings.ReasonMappingProvider>();
          // container.RegisterType<Teckraft.Data.Mappings.IMappingProvider<Teckraft.Core.Domian.SAP.Division, Teckraft.Data.Sql.Division>, Teckraft.Data.Mappings.DivisionMappingProvider>();


          // container.RegisterType<Teckraft.Data.Mappings.IMappingProvider<Teckraft.Core.Domian.SAP.Company, Teckraft.Data.Sql.Company>, Teckraft.Data.Mappings.CompanyMapping>();

          // container.RegisterType<Teckraft.Data.Mappings.IMappingProvider<Teckraft.Core.Domian.Settings.Module, Teckraft.Data.Sql.Module>, Teckraft.Data.Mappings.ModuleMappingProvider>();

          // container.RegisterType<Teckraft.Data.Mappings.IMappingProvider<Teckraft.Core.Domian.Settings.SubModule, Teckraft.Data.Sql.SubModule>, Teckraft.Data.Mappings.SubModuleMappingProvider>();
          // container.RegisterType<Teckraft.Data.Mappings.IMappingProvider<Teckraft.Core.Domian.Settings.BusinessFunction, Teckraft.Data.Sql.BusinessFunction>, Teckraft.Data.Mappings.BusinessFunctionMappingProvider>();

          // //container.RegisterType<Teckraft.Data.Mappings.IMappingProvider<Plant, Teckraft.Data.Sql.Plant>, Teckraft.Data.Mappings.PlantMappingProvider>();
          // //container.RegisterType<Teckraft.Data.Mappings.IMappingProvider<SalesGroup, Teckraft.Data.Sql.SalesGroup>, Teckraft.Data.Mappings.SalesGroupMappingProvider>();
          // container.RegisterType<Teckraft.Data.Mappings.IMappingProvider<User, Teckraft.Data.Sql.UserProfile>, Teckraft.Data.Mappings.UserMappingProvider>();


          // //            container.RegisterType<IRepository<HierarchyLevel>, Teckraft.Data.Settings.ApprovalHierarchyRepository>();
          // container.RegisterType<IService<HierarchyLevel>, Teckraft.Services.Settings.ApprovalHierarchyService>();
          // //            container.RegisterType<IMappingProvider<HierarchyLevel, Teckraft.Data.Sql.ApprovalHierarchy>, Teckraft.Data.Mappings.HierarchyLevelMapping>();

          // container.RegisterType<IReadService<Teckraft.Core.Domian.SAP.Division>, DivisionService>();
          // container.RegisterType<IReadableRepository<Teckraft.Core.Domian.SAP.Division>, DivisionRepository>();

          // //Module
          // container.RegisterType<IReadService<Teckraft.Core.Domian.Settings.Module>, ModuleService>();
          // container.RegisterType<IReadableRepository<Teckraft.Core.Domian.Settings.Module>, ModuleRepository>();

          // //SubModule
          // container.RegisterType<IReadService<Teckraft.Core.Domian.Settings.SubModule>, SubModuleService>();
          // container.RegisterType<IReadableRepository<Teckraft.Core.Domian.Settings.SubModule>, SubModuleRepository>();

          // //BusinessFunction
          // container.RegisterType<IReadService<Teckraft.Core.Domian.Settings.BusinessFunction>, BusinessFunctionService>();
          // container.RegisterType<IReadableRepository<Teckraft.Core.Domian.Settings.BusinessFunction>, BusinessFunctionRepository>();

          // container.RegisterType<IReadService<Customer>, WssService>();
          // //container.RegisterType<IReadableRepository<Customer>, Data.SAP.CustomerRepository>();

          // container.RegisterType<IReadService<Plant>, PlantService>();
          // //container.RegisterType<IReadableRepository<Plant>, Data.SAP.PlantRepository>();

          // container.RegisterType<IReadService<SalesGroup>, SalesGroupService>();
          // //container.RegisterType<IReadableRepository<SalesGroup>, Data.SAP.SalesGroupRepository>();

          // //container.RegisterType<IMappingProvider<ApprovalMatrix, Data.Sql.ApprovalLimit>, Data.Mappings.ApprovalMatrixMappingProvider>();

          // //container.RegisterType<IService<ApprovalMatrix>, ApprovalMatrixService>();
          // //container.RegisterType<IRepository<ApprovalMatrix>, Data.Settings.ApprovalMatrixDbRepository>();

          // // container.RegisterType<IProductRepository, ProductRepository>();

          // container.RegisterType<IRepository<Teckraft.Core.Domian.Transactions.CR>, CRRepository>();
          // container.RegisterType<IService<Teckraft.Core.Domian.Transactions.CR>, CRService>();
          // container.RegisterType<IMappingProvider<Teckraft.Core.Domian.Transactions.CR, Teckraft.Data.Sql.CR>, CRMapping>();

          // container.RegisterType<IReadableRepository<RequestType>, Teckraft.Data.Settings.RequestTypeRepository>();
          // container.RegisterType<IReadService<RequestType>, Teckraft.Services.Settings.RequestTypeService>();
          // container.RegisterType<IMappingProvider<RequestType, Teckraft.Data.Sql.RequestType>, RequestTypeMapper>();

          // container.RegisterType<IReadableRepository<User>, ITFHUserRepository>();
          // container.RegisterType<IReadService<User>, Teckraft.Services.Settings.ITFHUserService>();
          // container.RegisterType<IMappingProvider<User, Teckraft.Data.Sql.UserDetail>, UserDetailMapping>();

          // container.RegisterType<IService<User>, Teckraft.Services.Settings.UserService>();
          // container.RegisterType<IRepository<User>, Teckraft.Data.Settings.UserRepository>();

          // container.RegisterType<IMappingProvider<Teckraft.Core.Domian.Transactions.CRLog, Teckraft.Data.Sql.CRLog>, LogMappingProvider>();


          // container.RegisterType<IRepository<Teckraft.Core.Workflow.Task>, PhaseTaskRepository>();
          // container.RegisterType<IService<Teckraft.Core.Workflow.Task>, PhaseTaskService>();
          // container.RegisterType<IMappingProvider<Teckraft.Core.Workflow.Task, Teckraft.Data.Sql.CRPhaseTask>, PhaseTaskMapping>();
          // container.RegisterType<IMappingProvider<Teckraft.Core.Workflow.TaskType, Teckraft.Data.Sql.TaskType>, TaskTypeMapping>();

          // container.RegisterType<IMappingProvider<CRStage, Teckraft.Data.Sql.CRStage>, CRStageMapping>();
          // container.RegisterType<IRepository<CRStage>, CRStageRepository>();
          // container.RegisterType<IService<CRStage>, CRStageService>();
          // container.RegisterType<IMappingProvider<VendorDetail, Teckraft.Data.Sql.CRVendorDetail>, VendorMapping>();

           return container;
       }
    }
}
