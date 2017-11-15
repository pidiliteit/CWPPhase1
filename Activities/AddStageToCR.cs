using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using Teckraft.Core.Domian.Transactions;
using Teckraft.Services;
using Microsoft.Practices.Unity;
using Teckraft.Services.Extention;
namespace TeckraftBuilds.Workflow.Activities
{
    public class AddStageToCR:CodeActivity
    {
        private IService<CR> _service;

        public InOutArgument<CR> WorkItem { get; set; }
        public InOutArgument<string> StageCode { get; set; }
        private IService<CRStage> _stageService;
        // Define an activity input argument of type string
        public InArgument<string> Text { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            _service = CustomUnityContainer.Container.Resolve<IService<CR>>();
            _stageService = CustomUnityContainer.Container.Resolve<IService<CRStage>>();

            // Argument initialization (if any).
            CR workitem = context.GetValue(this.WorkItem);
            var strStageCode = context.GetValue(this.StageCode);
            //if (workitem.Stages == null) workitem.Stages = new List<CRStage>();
                CRStage stage = _stageService.GetCRStageByCode(workitem.Id, strStageCode,1,"", true);
                _service.Update(workitem);
                workitem = _service.GetById(workitem.Id);
            context.SetValue(this.WorkItem, workitem);
        }
    }
}
