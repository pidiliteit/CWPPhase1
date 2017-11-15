using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Microsoft.Practices.Unity;
using Teckraft.Services;
using Teckraft.Core.Domian.Transactions;
namespace TeckraftBuilds.Workflow.Activities
{

    public sealed class GetOpenPhaseCount : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> Text { get; set; }
        public InOutArgument<int> PhaseCount { get; set; }
        public InArgument<int> CRId { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            var _service = CustomUnityContainer.Container.Resolve<IService<CR>>();

            // Obtain the runtime value of the Text input argument
            string text = context.GetValue(this.Text);
            int phasecount = context.GetValue(this.PhaseCount);
            int crid = context.GetValue(this.CRId);
            var item=_service.GetById(crid);
            phasecount= item.OpenPhases;
            context.SetValue(this.PhaseCount, phasecount);


        }
    }
}
