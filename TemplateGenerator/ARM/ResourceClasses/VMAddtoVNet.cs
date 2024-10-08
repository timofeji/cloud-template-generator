using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGenerator.ARM.ResourceClasses
{
    class VMAddtoVNet
    {
        private string _location;
        private string _networkInterfaceName;
        private string _networkSecurityGroupName;
        private string[] _networkSecurityGroupRules;
        private string _subnetName;
        private string _virtualNetworkName;
        private string[] _addressPrefixes;
        private string[] _subnets;
        private string _publicIpAddressName;
        private string _publicIpAddressType = "Dynamic";
        private string _publicIpAddressSku = "Basic";
        private string _virtualMachineName;
        private string _virtualMachineRG;
        private string _osDiskType;
        private string _virtualMachineSize;
        private string _adminUsername;
        private string _adminPassword;
        private string _diagnosticsStorageAccountName;
        private string _diagnosticsStorageAccountId;
        private string _diagnosticsStorageAccountType;
        private string _diagnosticsStorageAccountKind = "Storage";

        public string Location { get => _location; set => _location = value; }
        public string NetworkInterfaceName { get => _networkInterfaceName; set => _networkInterfaceName = value; }
        public string NetworkSecurityGroupName { get => _networkSecurityGroupName; set => _networkSecurityGroupName = value; }
        public string[] NetworkSecurityGroupRules { get => _networkSecurityGroupRules; set => _networkSecurityGroupRules = value; }
        public string SubnetName { get => _subnetName; set => _subnetName = value; }
        public string VirtualNetworkName { get => _virtualNetworkName; set => _virtualNetworkName = value; }
        public string[] AddressPrefixes { get => _addressPrefixes; set => _addressPrefixes = value; }
        public string[] Subnets { get => _subnets; set => _subnets = value; }
        public string PublicIpAddressName { get => _publicIpAddressName; set => _publicIpAddressName = value; }
        public string PublicIpAddressType { get => _publicIpAddressType; set => _publicIpAddressType = value; }
        public string PublicIpAddressSku { get => _publicIpAddressSku; set => _publicIpAddressSku = value; }
        public string VirtualMachineName { get => _virtualMachineName; set => _virtualMachineName = value; }
        public string VirtualMachineRG { get => _virtualMachineRG; set => _virtualMachineRG = value; }
        public string OsDiskType { get => _osDiskType; set => _osDiskType = value; }
        public string VirtualMachineSize { get => _virtualMachineSize; set => _virtualMachineSize = value; }
        public string AdminUsername { get => _adminUsername; set => _adminUsername = value; }
        public string AdminPassword { get => _adminPassword; set => _adminPassword = value; }
        public string DiagnosticsStorageAccountName { get => _diagnosticsStorageAccountName; set => _diagnosticsStorageAccountName = value; }
        public string DiagnosticsStorageAccountId { get => _diagnosticsStorageAccountId; set => _diagnosticsStorageAccountId = value; }
        public string DiagnosticsStorageAccountType { get => _diagnosticsStorageAccountType; set => _diagnosticsStorageAccountType = value; }
        public string DiagnosticsStorageAccountKind { get => _diagnosticsStorageAccountKind; set => _diagnosticsStorageAccountKind = value; }
    }
}
