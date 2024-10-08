using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGenerator.ARM.ResourceClasses
{
    public class AKSWithACR : ResourceBase
    {
        private string _resourceName = "The name of the Managed Cluster resource.";
        private string _location = "The location of AKS resource.";
        private string _dnsPrefix = "Optional DNS prefix to use with hosted Kubernetes API server FQDN.";
        private string _osDiskSizeGB = "Disk size (in GiB) to provision for each of the agent pool nodes. This value ranges from 0 to 1023. Specifying 0 will apply the default disk size for that agentVMSize.";
        private string _kubernetesVersion = "1.7.7";
        private string _networkPlugin = "azure or kubenet";
        private int _maxPods = 30;
        private bool _enableRBAC = true;
        private bool _vmssNodePool = false;
        private string _windowsProfile = "Boolean flag to turn on and off of VM scale sets";
        private bool _enablePrivateCluster = false;
        private string _aadSessionKey = "";
        private bool _enableHttpApplicationRouting = true;
        private bool _enableOmsAgent = true;
        private string _workspaceRegion = "East US";
        private string _workspaceName = "Specify the name of the OMS workspace.";
        private string _omsWorkspaceId = "Specify the resource id of the OMS workspace.";
        private string _omsSku = "free or standalone or pernode";
        private string _acrName = "Specify the name of the Azure Container Registry.";
        private string _acrResourceGroup = "The name of the resource group the container registry is associated with.";
        private string _guidValue = "[newGuid()]";

        public string ResourceName { get => _resourceName; set => _resourceName = value; }
        public string Location { get => _location; set => _location = value; }
        public string DnsPrefix { get => _dnsPrefix; set => _dnsPrefix = value; }
        public string OsDiskSizeGB { get => _osDiskSizeGB; set => _osDiskSizeGB = value; }
        public string KubernetesVersion { get => _kubernetesVersion; set => _kubernetesVersion = value; }
        public string NetworkPlugin { get => _networkPlugin; set => _networkPlugin = value; }
        public int MaxPods { get => _maxPods; set => _maxPods = value; }
        public bool EnableRBAC { get => _enableRBAC; set => _enableRBAC = value; }
        public bool VmssNodePool { get => _vmssNodePool; set => _vmssNodePool = value; }
        public string WindowsProfile { get => _windowsProfile; set => _windowsProfile = value; }
        public bool EnablePrivateCluster { get => _enablePrivateCluster; set => _enablePrivateCluster = value; }
        public string AadSessionKey { get => _aadSessionKey; set => _aadSessionKey = value; }
        public bool EnableHttpApplicationRouting { get => _enableHttpApplicationRouting; set => _enableHttpApplicationRouting = value; }
        public bool EnableOmsAgent { get => _enableOmsAgent; set => _enableOmsAgent = value; }
        public string WorkspaceRegion { get => _workspaceRegion; set => _workspaceRegion = value; }
        public string WorkspaceName { get => _workspaceName; set => _workspaceName = value; }
        public string OmsWorkspaceId { get => _omsWorkspaceId; set => _omsWorkspaceId = value; }
        public string OmsSku { get => _omsSku; set => _omsSku = value; }
        public string AcrName { get => _acrName; set => _acrName = value; }
        public string AcrResourceGroup { get => _acrResourceGroup; set => _acrResourceGroup = value; }
        public string GuidValue { get => _guidValue; set => _guidValue = value; }

        public AKSWithACR(string DBCnxnString, string LogPath)
        {
            AzureResourceProviderCollection r = new AzureResourceProviderCollection(DBCnxnString, LogPath);
            AzureResourceProvider p = r.Values.ToList().Find(q => q.AzureService.Contains("AKS"));
            LoadDefaults(p.ResourceProviderID, this, DBCnxnString, LogPath);
        }
    }
}
