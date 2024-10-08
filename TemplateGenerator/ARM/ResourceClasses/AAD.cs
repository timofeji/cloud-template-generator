using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGenerator.ARM.ResourceClasses
{
    public class AAD : ResourceBase
    {
        private string _apiVersion;
        private string _domainConfigurationType = "FullySynced";
        private string _domainName;
        private string _domainToJoin;
        private string _domainUsername;
        private string _domainPassword;
        private string _domainJoinOptions;
        private string _filteredSync;
        private string _location;
        private string _notifyGlobalAdmins = "Enabled";
        private string _notifyDcAdmins = "Enabled";
        private string[] _additionalRecipients;
        private string _subnetName;
        private string _vnetName;
        private string[] _vnetAddressPrefixes;
        private string _subnetAddressPrefix;
        private string _dnsLabelPrefix;
        private string _ouPath;
        private string _nsgName;

        public string ApiVersion { get => _apiVersion; set => _apiVersion = value; }
        public string DomainConfigurationType { get => _domainConfigurationType; set => _domainConfigurationType = value; }
        public string DomainName { get => _domainName; set => _domainName = value; }
        public string DomainToJoin { get => _domainToJoin; set => _domainToJoin = value; }
        public string DomainUsername { get => _domainUsername; set => _domainUsername = value; }
        public string DomainPassword { get => _domainPassword; set => _domainPassword = value; }
        public string FilteredSync { get => _filteredSync; set => _filteredSync = value; }
        public string Location { get => _location; set => _location = value; }
        public string NotifyGlobalAdmins { get => _notifyGlobalAdmins; set => _notifyGlobalAdmins = value; }
        public string NotifyDcAdmins { get => _notifyDcAdmins; set => _notifyDcAdmins = value; }
        public string[] AdditionalRecipients { get => _additionalRecipients; set => _additionalRecipients = value; }
        public string SubnetName { get => _subnetName; set => _subnetName = value; }
        public string VnetName { get => _vnetName; set => _vnetName = value; }
        public string[] VnetAddressPrefixes { get => _vnetAddressPrefixes; set => _vnetAddressPrefixes = value; }
        public string SubnetAddressPrefix { get => _subnetAddressPrefix; set => _subnetAddressPrefix = value; }
        public string DnsLabelPrefix { get => _dnsLabelPrefix; set => _dnsLabelPrefix = value; }
        public string OuPath { get => _ouPath; set => _ouPath = value; }
        public string NsgName { get => _nsgName; set => _nsgName = value; }
        public string DomainJoinOptions { get => _domainJoinOptions; set => _domainJoinOptions = value; }
    }
}
