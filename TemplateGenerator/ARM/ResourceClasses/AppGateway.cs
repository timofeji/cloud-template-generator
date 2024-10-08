using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGenerator.ARM.ResourceClasses
{
    public class AppGateway
    {
        private string _location =  "[resourceGroup().location]";
        private string _applicationGatewayName = "DropGate1";
        private string _tier = "Standard_v2";
        private string _skuSize = "Standard_v2";
        private int _capacity = 0;
        private string _subnetName =  "default";
        private string [] _zones;
        private string _virtualNetworkName = "DropVPN1";
        private string _virtualNetworkPrefix =  "10.0.0.0/16";
        private string _publicIpAddressName = "DropPubIP1";
        private string _sku = "Standard";
        private string _allocationMethod = "Static";
        private string[] _publicIpZones;
        private int _autoScaleMaxCapacity = 10;

        public string Location { get => _location; set => _location = value; }
        public string ApplicationGatewayName { get => _applicationGatewayName; set => _applicationGatewayName = value; }
        public string Tier { get => _tier; set => _tier = value; }
        public string SkuSize { get => _skuSize; set => _skuSize = value; }
        public int Capacity { get => _capacity; set => _capacity = value; }
        public string SubnetName { get => _subnetName; set => _subnetName = value; }
        public string[] Zones { get => _zones; set => _zones = value; }
        public string VirtualNetworkName { get => _virtualNetworkName; set => _virtualNetworkName = value; }
        public string VirtualNetworkPrefix { get => _virtualNetworkPrefix; set => _virtualNetworkPrefix = value; }
        public string PublicIpAddressName { get => _publicIpAddressName; set => _publicIpAddressName = value; }
        public string Sku { get => _sku; set => _sku = value; }
        public string AllocationMethod { get => _allocationMethod; set => _allocationMethod = value; }
        public string[] PublicIpZones { get => _publicIpZones; set => _publicIpZones = value; }
        public int AutoScaleMaxCapacity { get => _autoScaleMaxCapacity; set => _autoScaleMaxCapacity = value; }
    }
}
