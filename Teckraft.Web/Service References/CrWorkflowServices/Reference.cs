﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Teckraft.Web.CrWorkflowServices {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CrWorkflowServices.IPILCRWorkflowService")]
    public interface IPILCRWorkflowService {
        
       // [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPILCRWorkflowService/Update", ReplyAction="http://tempuri.org/IPILCRWorkflowService/UpdateResponse")]
       // [return: System.ServiceModel.MessageParameterAttribute(Name="UpdateResponseResult")]
       // //Teckraft.Core.Domian.Transactions.CR Update(Teckraft.Core.Domian.Transactions.CR workItem);
        
       // [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPILCRWorkflowService/Update", ReplyAction="http://tempuri.org/IPILCRWorkflowService/UpdateResponse")]
       // [return: System.ServiceModel.MessageParameterAttribute(Name="UpdateResponseResult")]
       // //System.Threading.Tasks.Task<Teckraft.Core.Domian.Transactions.CR> UpdateAsync(Teckraft.Core.Domian.Transactions.CR workItem);
        
       // [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPILCRWorkflowService/UpdateTask", ReplyAction="http://tempuri.org/IPILCRWorkflowService/UpdateTaskResponse")]
       // [return: System.ServiceModel.MessageParameterAttribute(Name="UpdateResponseResult")]
       ////Teckraft.Core.Workflow.Task UpdateTask(Teckraft.Core.Workflow.Task workItem);
        
       // [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPILCRWorkflowService/UpdateTask", ReplyAction="http://tempuri.org/IPILCRWorkflowService/UpdateTaskResponse")]
       // [return: System.ServiceModel.MessageParameterAttribute(Name="UpdateResponseResult")]
       // //System.Threading.Tasks.Task<Teckraft.Core.Workflow.Task> UpdateTaskAsync(Teckraft.Core.Workflow.Task workItem);
        
       // [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPILCRWorkflowService/Create", ReplyAction="http://tempuri.org/IPILCRWorkflowService/CreteResponse")]
       // [return: System.ServiceModel.MessageParameterAttribute(Name="CreateResponseResult")]
       // //Teckraft.Core.Domian.Transactions.CR Create(Teckraft.Core.Domian.Transactions.CR workItem);
        
       // [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPILCRWorkflowService/Create", ReplyAction="http://tempuri.org/IPILCRWorkflowService/CreteResponse")]
       // [return: System.ServiceModel.MessageParameterAttribute(Name="CreateResponseResult")]
       // System.Threading.Tasks.Task<Teckraft.Core.Domian.Transactions.CR> CreateAsync(Teckraft.Core.Domian.Transactions.CR workItem);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPILCRWorkflowServiceChannel : Teckraft.Web.CrWorkflowServices.IPILCRWorkflowService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PILCRWorkflowServiceClient : System.ServiceModel.ClientBase<Teckraft.Web.CrWorkflowServices.IPILCRWorkflowService>, Teckraft.Web.CrWorkflowServices.IPILCRWorkflowService {
        
        public PILCRWorkflowServiceClient() {
        }
        
        public PILCRWorkflowServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PILCRWorkflowServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PILCRWorkflowServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PILCRWorkflowServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        //public Teckraft.Core.Domian.Transactions.CR Update(Teckraft.Core.Domian.Transactions.CR workItem) {
        //    return base.Channel.Update(workItem);
        //}
        
        //public System.Threading.Tasks.Task<Teckraft.Core.Domian.Transactions.CR> UpdateAsync(Teckraft.Core.Domian.Transactions.CR workItem) {
        //    return base.Channel.UpdateAsync(workItem);
        //}
        
        //public Teckraft.Core.Workflow.Task UpdateTask(Teckraft.Core.Workflow.Task workItem) {
        //    return base.Channel.UpdateTask(workItem);
        //}
        
        //public System.Threading.Tasks.Task<Teckraft.Core.Workflow.Task> UpdateTaskAsync(Teckraft.Core.Workflow.Task workItem) {
        //    return base.Channel.UpdateTaskAsync(workItem);
        //}
        
        //public Teckraft.Core.Domian.Transactions.CR Create(Teckraft.Core.Domian.Transactions.CR workItem) {
        //    return base.Channel.Create(workItem);
        //}
        
        //public System.Threading.Tasks.Task<Teckraft.Core.Domian.Transactions.CR> CreateAsync(Teckraft.Core.Domian.Transactions.CR workItem) {
        //    return base.Channel.CreateAsync(workItem);
        //}
    }
}
