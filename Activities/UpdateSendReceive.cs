
using System;
using System.Activities;
using System.Activities.Presentation;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.ServiceModel.Activities;
using System.Text;
using System.Windows;
using Teckraft.Core.Domian.Transactions;

namespace TeckraftBuilds.Workflow.Activities
{
    public class UpdateSendReceive : MessagingWrapperActivity, IActivityTemplateFactory
    {

        public Activity Create(System.Windows.DependencyObject target)
        {
        
           // _service = CustomUnityContainer.Container.Resolve<IService<CR>>();
            // Sequence container.
            var sequence = new Sequence();
            sequence.DisplayName = "Update";

            // Correlation Handle.
            var handle = new Variable<CorrelationHandle>("_handle");
            var workItem = new Variable<CR>("_workItem");

            // Get variable collection.
            var variables = GetVariableCollection(target);

            if (variables == null)
            {
                sequence.Variables.Add(handle);
                sequence.Variables.Add(workItem);
            }
            else
            {
                handle = AddVariable<CorrelationHandle>(variables, handle);
                workItem = AddVariable<CR>(variables, workItem);
            }

            // Create Receive Activity.
            var receive = new Receive();
            receive.DisplayName = "Update Receive";
            receive.Action = "http://tempuri.org/IPILCRWorkflowService/Update";
            receive.OperationName = "Update";
            receive.ProtectionLevel = ProtectionLevel.None;
            receive.ServiceContractName = "IPILCRWorkflowService";

            // Add parameters to Receive
            var parameters = new ReceiveParametersContent();
            parameters.Parameters.Add("workItem", new OutArgument<CR>(workItem));
            receive.Content = parameters;

            // Add CorrelationInitializer to Receive
            var initializer = new RequestReplyCorrelationInitializer();
            initializer.CorrelationHandle = new InArgument<CorrelationHandle>(handle);
            receive.CorrelationInitializers.Add(initializer);

            // Create SendReply Activity
            var send = new SendReply();
            send.DisplayName = "Update Reply";
            send.Action = "http://tempuri.org/IPILCRWorkflowService/UpdateResponse";
            send.Request = receive;
            send.PersistBeforeSend = true;

            // Add parameters to SendReply
            var sendParameters = new SendParametersContent();
            sendParameters.Parameters.Add("UpdateResponseResult", new InArgument<CR>(workItem));
            send.Content = sendParameters;

            // Create Create activity.
            Update activty = new Update();
            activty.DisplayName = "Update";
            activty.WorkItem = workItem;

            // Add activities to sequence.
            sequence.Activities.Add(receive);
            sequence.Activities.Add(activty);
            sequence.Activities.Add(send);

            return sequence;
        
        }
    }
}
