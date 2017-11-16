using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Teckraft.Core.Domian.Transactions;
using Teckraft.Services;
using Microsoft.Practices.Unity;
namespace TeckraftBuilds.Workflow.Activities
{

    public sealed class RefereshCR : CodeActivity
    {
        private IService<CR> _service;
        // Define an activity input argument of type string
        public InArgument<string> Text { get; set; }
        public InArgument<CR> WorkItem { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            _service = CustomUnityContainer.Container.Resolve<IService<CR>>();

            CR workitem = context.GetValue(this.WorkItem);
            workitem =_service.GetById(workitem.Id);
//            if(workitem.Phases!=null) workitem.OpenPhases= workitem.Phases.Count(it=>it.Tasks.Count(t=>t.Completed==false)>0);
            //workitem.OpenPhases =workitem.Phases.

            // Obtain the runtime value of the Text input argument
            string text = context.GetValue(this.Text);
            context.SetValue(this.WorkItem, workitem);
        }
    }
}
