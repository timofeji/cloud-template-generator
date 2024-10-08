using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGenerator.ARM.ResourceClasses
{
    public class AKSCluster : ResourceBase
    {
        private string _clusterName;  
        private string _location; 
        private string _existingSubnetName;
        private string _existingVirtualNetworkName; 
        private string _existingVirtualNetworkResourceGroup;
        private string _existingServicePrincipalObjectId; 
        private string _existingServicePrincipalClientId;
        private string _existingServicePrincipalClientSecret; 
        private string _osType; 
        private string _dnsPrefix;
        private string _osDiskSizeGB;  
        private int _agentCount;
        private int _maxPods; 
        private string _agentVMSize;      
        private string _linuxAdminUsername;  
        private string _sshRSAPublicKey;
        private string _dnsServiceIP; 
        private string _dockerBridgeCidr; 
        private string _kubernetesVersion;
        private string _networkPlugin;
        private string _serviceCidr;
        private bool _enableRBAC; 
        private bool _enableHttpApplicationRouting; 

        public string ClusterName { get => _clusterName; set => _clusterName = value; }
        public string Location { get => _location; set => _location = value; }
        public string ExistingSubnetName { get => _existingSubnetName; set => _existingSubnetName = value; }
        public string ExistingVirtualNetworkName { get => _existingVirtualNetworkName; set => _existingVirtualNetworkName = value; }
        public string ExistingVirtualNetworkResourceGroup { get => _existingVirtualNetworkResourceGroup; set => _existingVirtualNetworkResourceGroup = value; }
        public string ExistingServicePrincipalClientId { get => _existingServicePrincipalClientId; set => _existingServicePrincipalClientId = value; }
        public string ExistingServicePrincipalObjectId { get => _existingServicePrincipalObjectId; set => _existingServicePrincipalObjectId = value; }
        public string ExistingServicePrincipalClientSecret { get => _existingServicePrincipalClientSecret; set => _existingServicePrincipalClientSecret = value; }
        public string DnsPrefix { get => _dnsPrefix; set => _dnsPrefix = value; }
        public string OsDiskSizeGB { get => _osDiskSizeGB; set => _osDiskSizeGB = value; }
        public int AgentCount { get => _agentCount; set => _agentCount = value; }
        public int MaxPods { get => _maxPods; set => _maxPods = value; }
        public string AgentVMSize { get => _agentVMSize; set => _agentVMSize = value; }
        public string LinuxAdminUsername { get => _linuxAdminUsername; set => _linuxAdminUsername = value; }
        public string SshRSAPublicKey { get => _sshRSAPublicKey; set => _sshRSAPublicKey = value; }
        public string DnsServiceIP { get => _dnsServiceIP; set => _dnsServiceIP = value; }
        public string DockerBridgeCidr { get => _dockerBridgeCidr; set => _dockerBridgeCidr = value; }
        public string OsType { get => _osType; set => _osType = value; }
        public string NetworkPlugin { get => _networkPlugin; set => _networkPlugin = value; }
        public string ServiceCidr { get => _serviceCidr; set => _serviceCidr = value; }
        public string KubernetesVersion { get => _kubernetesVersion; set => _kubernetesVersion = value; }
        public bool EnableRBAC { get => _enableRBAC; set => _enableRBAC = value; }
        public bool EnableHttpApplicationRouting { get => _enableHttpApplicationRouting; set => _enableHttpApplicationRouting = value; }

    }
}
