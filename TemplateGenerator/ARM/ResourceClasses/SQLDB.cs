using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGenerator.ARM.ResourceClasses
{
    public class SQLDB : ResourceBase
    {
        private bool _enableADS = true;
        private bool _allowAzureIps =  true;
        private bool _allowClientIp = true;
        private string _clientIpValue =  "72.235.180.106";
        private string _databaseTagscategory = "basic";
        private string _serverTagscategory= "basic";
        private bool _enableVA =  true;
        private bool _enablePrivateEndpoint = false;
        private string _privateEndpointNestedTemplateId = "pe-36cb5099-2bb2-4e23-b2f9-5fbd53bf700e";
        private string _privateEndpointSubscriptionId = "";
        private string _privateEndpointResourceGroup =  "";
        private string _privateEndpointName =  "";
        private string _privateEndpointLocation = "";
        private string _privateEndpointSubnetId = "";
        private string _privateLinkServiceName = "";
        private string _privateLinkServiceServiceId = "";
        private string _privateEndpointVnetSubscriptionId = "";
        private string _privateEndpointVnetResourceGroup = "";
        private string _privateEndpointVnetName =  "";
        private string _privateEndpointSubnetName =  "";
        private bool _enablePrivateDnsZone = true;
        private string _privateEndpointDnsRecordUniqueId =  "36cb5099-2bb2-4e23-b2f9-5fbd53bf7011";
        private string _privateEndpointTemplateLink = "https://sqlazureextension.hosting.portal.azure.net/sqlazureextension/Content/2.1.0118727/DeploymentTemplates/PrivateEndpoint.json";
        private string _privateDnsForPrivateEndpointTemplateLink = "https://sqlazureextension.hosting.portal.azure.net/sqlazureextension/Content/2.1.0118727/DeploymentTemplates/PrivateDnsForPrivateEndpoint.json";
        private string _privateDnsForPrivateEndpointNicTemplateLink = "https://sqlazureextension.hosting.portal.azure.net/sqlazureextension/Content/2.1.0118727/DeploymentTemplates/PrivateDnsForPrivateEndpointNic.json";
        private string _privateDnsForPrivateEndpointIpConfigTemplateLink = "https://sqlazureextension.hosting.portal.azure.net/sqlazureextension/Content/2.1.0118727/DeploymentTemplates/PrivateDnsForPrivateEndpointIpConfig.json";
        private string _clientIpRuleName = "ClientIp-2020-4-21_18-40-42";

        public bool EnableADS { get => _enableADS; set => _enableADS = value; }
        public bool AllowAzureIps { get => _allowAzureIps; set => _allowAzureIps = value; }
        public bool AllowClientIp { get => _allowClientIp; set => _allowClientIp = value; }
        public string ClientIpValue { get => _clientIpValue; set => _clientIpValue = value; }
        public string DatabaseTagscategory { get => _databaseTagscategory; set => _databaseTagscategory = value; }
        public string ServerTagscategory { get => _serverTagscategory; set => _serverTagscategory = value; }
        public bool EnableVA { get => _enableVA; set => _enableVA = value; }
        public bool EnablePrivateEndpoint { get => _enablePrivateEndpoint; set => _enablePrivateEndpoint = value; }
        public string PrivateEndpointNestedTemplateId { get => _privateEndpointNestedTemplateId; set => _privateEndpointNestedTemplateId = value; }
        public string PrivateEndpointSubscriptionId { get => _privateEndpointSubscriptionId; set => _privateEndpointSubscriptionId = value; }
        public string PrivateEndpointResourceGroup { get => _privateEndpointResourceGroup; set => _privateEndpointResourceGroup = value; }
        public string PrivateEndpointName { get => _privateEndpointName; set => _privateEndpointName = value; }
        public string PrivateEndpointLocation { get => _privateEndpointLocation; set => _privateEndpointLocation = value; }
        public string PrivateEndpointSubnetId { get => _privateEndpointSubnetId; set => _privateEndpointSubnetId = value; }
        public string PrivateLinkServiceName { get => _privateLinkServiceName; set => _privateLinkServiceName = value; }
        public string PrivateLinkServiceServiceId { get => _privateLinkServiceServiceId; set => _privateLinkServiceServiceId = value; }
        public string PrivateEndpointVnetSubscriptionId { get => _privateEndpointVnetSubscriptionId; set => _privateEndpointVnetSubscriptionId = value; }
        public string PrivateEndpointVnetResourceGroup { get => _privateEndpointVnetResourceGroup; set => _privateEndpointVnetResourceGroup = value; }
        public string PrivateEndpointVnetName { get => _privateEndpointVnetName; set => _privateEndpointVnetName = value; }
        public string PrivateEndpointSubnetName { get => _privateEndpointSubnetName; set => _privateEndpointSubnetName = value; }
        public bool EnablePrivateDnsZone { get => _enablePrivateDnsZone; set => _enablePrivateDnsZone = value; }
        public string PrivateEndpointDnsRecordUniqueId { get => _privateEndpointDnsRecordUniqueId; set => _privateEndpointDnsRecordUniqueId = value; }
        public string PrivateEndpointTemplateLink { get => _privateEndpointTemplateLink; set => _privateEndpointTemplateLink = value; }
        public string PrivateDnsForPrivateEndpointTemplateLink { get => _privateDnsForPrivateEndpointTemplateLink; set => _privateDnsForPrivateEndpointTemplateLink = value; }
        public string PrivateDnsForPrivateEndpointNicTemplateLink { get => _privateDnsForPrivateEndpointNicTemplateLink; set => _privateDnsForPrivateEndpointNicTemplateLink = value; }
        public string PrivateDnsForPrivateEndpointIpConfigTemplateLink { get => _privateDnsForPrivateEndpointIpConfigTemplateLink; set => _privateDnsForPrivateEndpointIpConfigTemplateLink = value; }
        public string ClientIpRuleName { get => _clientIpRuleName; set => _clientIpRuleName = value; }
    }
}
