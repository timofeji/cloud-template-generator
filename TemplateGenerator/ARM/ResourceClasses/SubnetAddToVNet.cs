using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGenerator.ARM.ResourceClasses
{
    public class SubnetAddToVNet : ResourceBase
    {
        private string _existingVNETName;
        private string _newSubnetName;
        private string _newSubnetAddressPrefix;
        private string _apiVersion;
        private string _location;

        public string ExistingVNETName { get => _existingVNETName; set => _existingVNETName = value; }
        public string NewSubnetName { get => _newSubnetName; set => _newSubnetName = value; }
        public string NewSubnetAddressPrefix { get => _newSubnetAddressPrefix; set => _newSubnetAddressPrefix = value; }
        public string ApiVersion { get => _apiVersion; set => _apiVersion = value; }
        public string Location { get => _location; set => _location = value; }
    }
}
