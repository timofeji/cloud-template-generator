using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGenerator.ARM
{
    public class ARMConfig
    {
        private bool _SGRSSHOK = false;
        private bool _SGRHTTPOK = false;
        private bool _SGRHTTPSOK = false;
        private bool _SGRAllowPSRemoting = false;
        private bool _SGRAllowRD = false;
        private bool _AllowSyncWithAzureAD = false;

        public bool SGRSSHOK { get => _SGRSSHOK; set => _SGRSSHOK = value; }
        public bool SGRHttpOK { get => _SGRHTTPOK; set => _SGRHTTPOK = value; }
        public bool SGRHttpsOK { get => _SGRHTTPSOK; set => _SGRHTTPSOK = value; }
        public bool SGRAllowPSRemoting { get => _SGRAllowPSRemoting; set => _SGRAllowPSRemoting = value; }
        public bool SGRAllowRD { get => _SGRAllowRD; set => _SGRAllowRD = value; }
        public bool SGRAllowSyncWithAzureAD { get => _AllowSyncWithAzureAD; set => _AllowSyncWithAzureAD = value; }

        public ARMConfig() { }
    }
}
