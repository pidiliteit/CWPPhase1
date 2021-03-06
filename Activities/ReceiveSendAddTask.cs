﻿
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

using Teckraft.Core.Workflow;

namespace TeckraftBuilds.Workflow.Activities
{
    public class ReceiveSendAddTask : MessagingWrapperActivity, IActivityTemplateFactory
    {
         public Activity Create(System.Windows.DependencyObject target)
        {
        
           // _service = CustomUnityContainer.Container.Resolve<IService<CR>>();
            // Sequence container.
            var sequence = new Sequence();
            sequence.DisplayName = "Add Task to Phase";

            // Correlation Handle.
            var handle = new Variable<CorrelationHandle>("_handle");
            var workItem = new Variable<Task>("_workItemTask");

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
                workItem = AddVariable<Task>(variables, workItem);
            }

            // Create Receive Activity.
            var receive = new Receive();
            receive.DisplayName = "Add Task Receive";
            receive.Action = "http://tempuri.org/IPILCRWorkflowService/AddTask";
            receive.OperationName = "AddTask";
            receive.ProtectionLevel = ProtectionLevel.None;
            receive.ServiceContractName = "IPILCRWorkflowService";

            // Add parameters to Receive
            var parameters = new ReceiveParametersContent();
            parameters.Parameters.Add("workItem", new OutArgument<Task>(workItem));
            receive.Content = parameters;

            // Add CorrelationInitializer to Receive
            var initializer = new RequestReplyCorrelationInitializer();
            initializer.CorrelationHandle = new InArgument<CorrelationHandle>(handle);
            receive.CorrelationInitializers.Add(initializer);

            // Create SendReply Activity
            var send = new SendReply();
            send.DisplayName = "Add Task Reply";
            send.Action = "http://tempuri.org/IPILCRWorkflowService/AddTaskResponse";
            send.Request = receive;
            send.PersistBeforeSend = true;

            // Add parameters to SendReply
            var sendParameters = new SendParametersContent();
            sendParameters.Parameters.Add("AddTaskResponseResult", new InArgument<Task>(workItem));
            send.Content = sendParameters;

            // Create Create activity.
            AssignTaskToUser activty = new AssignTaskToUser();
            activty.DisplayName = "AddTask";
            activty.Task = workItem;
            
            // Add activities to sequence.
            sequence.Activities.Add(receive);
            sequence.Activities.Add(activty);
            sequence.Activities.Add(send);

            return sequence;
        
        }
    }
}
