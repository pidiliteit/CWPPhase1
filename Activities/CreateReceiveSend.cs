using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Activities.Presentation;
using System.Windows;
using System.Activities.Statements;
using System.ServiceModel.Activities;
using Teckraft.Core.Domian.Transactions;
using System.Net.Security;
using Teckraft.Services;


namespace TeckraftBuilds.Workflow.Activities
{

    public sealed class CreateReceiveSend : MessagingWrapperActivity, IActivityTemplateFactory
    {

        public CreateReceiveSend()
        {
            
       // this._service=service;
        }
        public Activity Create(DependencyObject target)
        {
           // _service = CustomUnityContainer.Container.Resolve<IService<CR>>();
            // Sequence container.
            var sequence = new Sequence();
            sequence.DisplayName = "Create";

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
            receive.DisplayName = "Create Receive";
            receive.Action = "http://tempuri.org/IPILCRWorkflowService/Create";
            receive.OperationName = "Create";
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
            send.DisplayName = "Create Reply";
            send.Action = "http://tempuri.org/IPILCRWorkflowService/CreteResponse";
            send.Request = receive;
            send.PersistBeforeSend = true;

            // Add parameters to SendReply
            var sendParameters = new SendParametersContent();
            sendParameters.Parameters.Add("CreateResponseResult", new InArgument<CR>(workItem));
            send.Content = sendParameters;

            // Create Create activity.
            Create create = new Create();
            create.DisplayName = "Create";
            create.WorkItem = workItem;

            // Add activities to sequence.
            sequence.Activities.Add(receive);
            sequence.Activities.Add(create);
            sequence.Activities.Add(send);

            return sequence;
        }
    }
}
