﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SUC_Sys.WeChartInterfaceApi {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WeChartInterfaceApi.WeChartInterfaceSoap")]
    public interface WeChartInterfaceSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string HelloWorld();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        System.Threading.Tasks.Task<string> HelloWorldAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/WebChartDataAdd", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string WebChartDataAdd(string ProjectCode, string ProjectName, string UserWebChartId, string Remark, string reccreateby);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/WebChartDataAdd", ReplyAction="*")]
        System.Threading.Tasks.Task<string> WebChartDataAddAsync(string ProjectCode, string ProjectName, string UserWebChartId, string Remark, string reccreateby);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetWebChartID", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetWebChartID(string projectCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetWebChartID", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetWebChartIDAsync(string projectCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AddWXUsedData", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string AddWXUsedData(string projectCode, string projectName, string userName, string userWeChartId, string appTokenCode, string url, string topicId, string remark, string reccreateby);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AddWXUsedData", ReplyAction="*")]
        System.Threading.Tasks.Task<string> AddWXUsedDataAsync(string projectCode, string projectName, string userName, string userWeChartId, string appTokenCode, string url, string topicId, string remark, string reccreateby);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetWeChatPortInfor", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetWeChatPortInfor(string projectCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetWeChatPortInfor", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetWeChatPortInforAsync(string projectCode);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WeChartInterfaceSoapChannel : SUC_Sys.WeChartInterfaceApi.WeChartInterfaceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WeChartInterfaceSoapClient : System.ServiceModel.ClientBase<SUC_Sys.WeChartInterfaceApi.WeChartInterfaceSoap>, SUC_Sys.WeChartInterfaceApi.WeChartInterfaceSoap {
        
        public WeChartInterfaceSoapClient() {
        }
        
        public WeChartInterfaceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WeChartInterfaceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WeChartInterfaceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WeChartInterfaceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string HelloWorld() {
            return base.Channel.HelloWorld();
        }
        
        public System.Threading.Tasks.Task<string> HelloWorldAsync() {
            return base.Channel.HelloWorldAsync();
        }
        
        public string WebChartDataAdd(string ProjectCode, string ProjectName, string UserWebChartId, string Remark, string reccreateby) {
            return base.Channel.WebChartDataAdd(ProjectCode, ProjectName, UserWebChartId, Remark, reccreateby);
        }
        
        public System.Threading.Tasks.Task<string> WebChartDataAddAsync(string ProjectCode, string ProjectName, string UserWebChartId, string Remark, string reccreateby) {
            return base.Channel.WebChartDataAddAsync(ProjectCode, ProjectName, UserWebChartId, Remark, reccreateby);
        }
        
        public System.Data.DataSet GetWebChartID(string projectCode) {
            return base.Channel.GetWebChartID(projectCode);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetWebChartIDAsync(string projectCode) {
            return base.Channel.GetWebChartIDAsync(projectCode);
        }
        
        public string AddWXUsedData(string projectCode, string projectName, string userName, string userWeChartId, string appTokenCode, string url, string topicId, string remark, string reccreateby) {
            return base.Channel.AddWXUsedData(projectCode, projectName, userName, userWeChartId, appTokenCode, url, topicId, remark, reccreateby);
        }
        
        public System.Threading.Tasks.Task<string> AddWXUsedDataAsync(string projectCode, string projectName, string userName, string userWeChartId, string appTokenCode, string url, string topicId, string remark, string reccreateby) {
            return base.Channel.AddWXUsedDataAsync(projectCode, projectName, userName, userWeChartId, appTokenCode, url, topicId, remark, reccreateby);
        }
        
        public System.Data.DataSet GetWeChatPortInfor(string projectCode) {
            return base.Channel.GetWeChatPortInfor(projectCode);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetWeChatPortInforAsync(string projectCode) {
            return base.Channel.GetWeChatPortInforAsync(projectCode);
        }
    }
}
