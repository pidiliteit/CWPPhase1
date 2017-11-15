using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teckraft.Core.Domian.Transactions;
using Microsoft.Practices.Unity;
using Teckraft.Services;
using Teckraft.Web.Framework;
using Teckraft.Services.Extention;


namespace TeckraftBuilds.WFActivities
{
    public class AddPhase:IWFActivity
    {
        public Argument<CR> WorkItem { get; set; }
        public Argument<CRPhase> Phase { get; set; }
        public Argument<string> StageCode { get; set; }

        public void Execute(WFContext context)
        {

            var _stageService = CustomUnityContainer.Container.Resolve<IService<CRStage>>();
            var _phaseService = CustomUnityContainer.Container.Resolve<IService<CRPhase>>();
            CR workitem = context.GetValue(this.WorkItem);
            var phase = context.GetValue(this.Phase);
            // var user = context.GetValue(this.AssignedTo);
            //var task = context.GetValue(this.Task);
            var strStageCode = context.GetValue(this.StageCode);
            if (workitem.Logs == null) workitem.Logs = new List<CRLog>();
            CRStage stage = _stageService.GetCRStageByCode(workitem.Id, strStageCode, 1, "", true);
            phase.RCB = workitem.RUB;

            phase.Stage = stage;
            _phaseService.Add(phase);
            
            context.SetValue(this.WorkItem, workitem);

        
        }
    }
}
