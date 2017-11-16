using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teckraft.Core.Domian.Transactions;
using Teckraft.Services;
using Microsoft.Practices.Unity;
using Teckraft.Core.Domian.Settings;
using Teckraft.Data;

namespace TeckraftBuilds.Workflow.Activities
{
    [Serializable]
    public class Create : CodeActivity
    {
        public InOutArgument<CR> WorkItem { get; set; }

        private IService<CR> _service;
        private IService<User> userservice;
        
        public Create(){
            //this._service = service;
        }
        private User getItFHUser(CR entity)
        {
            var query = new ListQuery<User>() { PageSize = 1, CurrentPage = 1 };
            query.Parameters = new List<QueryParameter>();
           // query.Parameters.Add(new QueryParameter() { Name = "isitfh", Value = "true", Operator = CompareOperator.Equals });
            query.Parameters.Add(new QueryParameter() { Name = "crtypeid", Value = entity.CRTypeId.ToString(), Operator = CompareOperator.Equals });
            //   query.Parameters.Add(new QueryParameter() { Name = "submoduleid", Value = entity.SubModule.Id.ToString(), Operator = CompareOperator.Equals });
            return userservice.GetByQuery(query).Items.FirstOrDefault();
        }

        protected override void Execute(CodeActivityContext context)
        {
            userservice = CustomUnityContainer.Container.Resolve<IService<User>>();
            _service = CustomUnityContainer.Container.Resolve<IService<CR>>();
            // Argument initialization (if any).
            CR workitem = context.GetValue(this.WorkItem);
            workitem.WFInstianceId = context.WorkflowInstanceId;

            // Instantiate business components.

            if (workitem.ITFHUser == null)
            {
                workitem.ITFHUser = getItFHUser(workitem);

            }

            // Call business component methods.
            workitem.PendingWithUser = Teckraft.Services.Workflow.WorkflowHelper.GetAssignedToUser(workitem, null);
            if (workitem.Id != 0)
            {
                if (workitem.Logs == null) workitem.Logs = new List<CRLog>();
                //workitem.Logs.Add(new CRLog() { RCB = new Teckraft.Core.Domian.Settings.User() { Id = 1 }, StatusCode = workitem.StatusCode, LogType = "Info" });
                _service.Update(workitem);
            }
            else
            {
                if (workitem.Logs == null) workitem.Logs = new List<CRLog>();
               // workitem.Logs.Add(new CRLog() { RCB = new Teckraft.Core.Domian.Settings.User() { Id = 1 }, StatusCode = workitem.StatusCode, LogType = "Info" });
                workitem = _service.Add(workitem);
            }
           // workitem.Message = Teckraft.Core.Workflow.WorkflowHelper.GetResponseMessage(workitem, workitem.StatusCode);

            // Set value to Out arguments (if any).
            context.SetValue(this.WorkItem, workitem);
        }
    }
}
