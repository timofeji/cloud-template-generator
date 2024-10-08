using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace TemplateGenerator.ARM
{
    public class ARMTemplateMaster
    {
        List<ARMTemplate> _templates = new List<ARMTemplate>();
        private List<string> stages = new List<string>();
        string _TestURI = "https://management.azure.com/subscriptions/{subscriptionId}/resourcegroups/{resourceGroupName}/providers/Microsoft.Resources/deployments/{deploymentName}/validate?api-version=2019-10-01";
        string _DBCnxnString = "";
        string _SnippetFilePath = "";
        string _LogPath = "";
        AzureResourceProviderCollection _Providers = new AzureResourceProviderCollection();
        List<string> _ReplaceValues = new List<string>();
        AzureResourceProviderPropertyCollection _ProviderProperties = new AzureResourceProviderPropertyCollection();
        private NetworkSecurityGroupRuleCollection _SecurityGroupRules;
        private ARMConfig _Config = new ARMConfig();
        public List<ARMTemplate> Templates { get => _templates; set => _templates = value; }
        public AzureResourceProviderCollection Providers { get => _Providers; set => _Providers = value; }
        public List<string> Stages { get => stages; set => stages = value; }
        public string TestURI { get => _TestURI; set => _TestURI = value; }
        public string DBCnxnString { get => _DBCnxnString; set => _DBCnxnString = value; }
        public string SnippetFilePath { get => _SnippetFilePath; set => _SnippetFilePath = value; }
        public string LogPath { get => _LogPath; set => _LogPath = value; }
        public AzureResourceProviderPropertyCollection ProviderProperties { get => _ProviderProperties; set => _ProviderProperties = value; }
        public NetworkSecurityGroupRuleCollection SecurityGroupRules { get => _SecurityGroupRules; set => _SecurityGroupRules = value; }
        public List<string> ReplacementValues { get => _ReplaceValues; set => _ReplaceValues = value; }
        public ARMConfig Config { get => _Config; set => _Config = value; }

        public enum SecurityGroupRuleTypes
        {
            HTTP = 1,
            SSH = 2,
            HTTPS = 3,
            AllowRD = 4,
            AllowPSRemoting = 5,
            AllowSyncWithAzureAD = 6
        }

        public ARMTemplateMaster(ARMConfig Config, string DBCnxnString, string SnippetFilePath, string LogPath)
        {
            this.Config = Config;
            //try
            //{
            Stages.Add("Common");
            Stages.Add("Resources");
            Stages.Add("Parameters");
            Stages.Add("Variables");
            Stages.Add("Functions");
            Stages.Add("Outputs");

            this.SnippetFilePath = SnippetFilePath;
            this.DBCnxnString = DBCnxnString;
            this.LogPath = LogPath;

            this.Providers = new AzureResourceProviderCollection(this.DBCnxnString, this.LogPath);
            this.ProviderProperties = new AzureResourceProviderPropertyCollection(this.DBCnxnString, this.LogPath);
            this.SecurityGroupRules = new NetworkSecurityGroupRuleCollection(this.DBCnxnString, this.LogPath);

            foreach (AzureResourceProviderProperty p in this.ProviderProperties.Values)
            {
                this.ReplacementValues.Add("***" + p.PropertyName + "***");
            }
            //}
            //catch(Exception Ec)
            //{
            //    Log.LogErr("ARMTM", Ec.Message, this.LogPath);
            //}
        }
        public static string TestTemplate(string JSON)
        {
            string sResult = "";


            return (sResult);
        }
        public string RenderFinalTemplate(string FunctionsNamespace)
        {
            StringBuilder sb = new StringBuilder();
            string sParams = "";
            string sVars = "";
            foreach (string RegionType in Stages)
            {
                if (RegionType.ToUpper() == "COMMON")
                {
                    TemplateRegionCommon c = new TemplateRegionCommon();
                    sb.Append(c.Render());
                }
                else
                {
                    switch (RegionType.ToUpper())
                    {
                        case "PARAMETERS":
                            sb.Append(F.Quote + "parameters" + F.Quote + F.Colon + F.CB + Environment.NewLine);
                            break;
                        case "VARIABLES":
                            sb.Append(F.Quote + "variables" + F.Quote + F.Colon + F.CB + Environment.NewLine);
                            break;
                        case "FUNCTIONS":
                            /*
                              "functions": [
                                {
                                "namespace": "<namespace-for-functions>",
                                "members": {
                             */
                            sb.Append(F.Quote + "functions" + F.Quote + F.Colon + F.SqB + Environment.NewLine);
                            sb.Append(F.Space(1) + F.CB + Environment.NewLine);
                            sb.Append(F.Space(3) + F.Quote + "namespace" + F.Quote + F.Colon + F.Quote + FunctionsNamespace + F.Quote + F.Comma + Environment.NewLine);
                            sb.Append(F.Space(3) + F.Quote + "members" + F.Quote + F.Colon + F.CB + Environment.NewLine);
                            break;
                        case "RESOURCES":
                            sb.Append(F.Quote + "resources" + F.Quote + F.Colon + F.SqB + Environment.NewLine);
                            break;
                        case "OUTPUTS":
                            sb.Append(F.Quote + "outputs" + F.Quote + F.Colon + F.CB + Environment.NewLine);
                            break;
                    }
                    foreach (ARMTemplate Template in this.Templates)
                    {
                        foreach (TemplateRegion region in Template.Regions)
                        {
                            if (region.RegionType.ToUpper() == RegionType.ToUpper())
                            {
                                switch (RegionType.ToUpper())
                                {
                                    case "PARAMETERS":
                                        foreach (TemplateResource r in Template.Resources.Values)
                                        {
                                            AzureResourceProvider p = this.Providers[r.ResourceProviderID];
                                            sParams += this.InsertSnippet(p.AzureService, "Parameter");
                                            switch (p.AzureService)
                                            {
                                                case "Virtual Machines":
                                                    // build the appropriate permissions
                                                    string sNSGRVM = "";
                                                    if (this.Config.SGRAllowSyncWithAzureAD)
                                                        sNSGRVM += File.ReadAllText(this.SnippetFilePath + "SGR/SGRAllowSyncWithAzureAD.txt") + "," + Environment.NewLine;
                                                    if (this.Config.SGRAllowPSRemoting)
                                                        sNSGRVM += File.ReadAllText(this.SnippetFilePath + "SGR/SGRAllowPSRemoting.txt") + "," + Environment.NewLine;
                                                    if (this.Config.SGRAllowRD)
                                                        sNSGRVM += File.ReadAllText(this.SnippetFilePath + "SGR/SGRAllowRD.txt") + "," + Environment.NewLine;

                                                    if (sNSGRVM.Length > 0)
                                                        sNSGRVM = sNSGRVM.Remove(sNSGRVM.LastIndexOf(","), 1);
                                                    // replace in file with content
                                                    sParams = sParams.Replace("***NSGR***", sNSGRVM);
                                                    //  sParams += F.Comma + Environment.NewLine;
                                                    break;
                                            }
                                        }
                                        break;
                                    case "RESOURCES":
                                        foreach (TemplateResource r in Template.Resources.Values)
                                        {
                                            AzureResourceProvider p = this.Providers[r.ResourceProviderID];

                                            string sOut = this.InsertSnippet(p.AzureService, "Resource");
                                            switch (p.AzureService)
                                            {
                                                case "Azure Active Directory Domain Services":

                                                    // build the appropriate permissions
                                                    string sNSGR = "";
                                                    if (this.Config.SGRHttpOK)
                                                        sNSGR += File.ReadAllText(this.SnippetFilePath + "SGR/SGRHTTP.txt") + "," + Environment.NewLine;

                                                    if (this.Config.SGRHttpsOK)
                                                        sNSGR += File.ReadAllText(this.SnippetFilePath + "SGR/SGRHTTPS.txt") + "," + Environment.NewLine;

                                                    if (this.Config.SGRSSHOK)
                                                        sNSGR += File.ReadAllText(this.SnippetFilePath + "SGR/SGRSSH.txt") + "," + Environment.NewLine;

                                                    if (sNSGR.Length > 0)
                                                        sNSGR = sNSGR.Remove(sNSGR.LastIndexOf(","), 1);

                                                    // replace in file with content
                                                    sOut = sOut.Replace("***NSGR***", sNSGR);
                                                    sOut += F.Comma + Environment.NewLine;
                                                    break;  
                                                case "Azure Kubernetes Service (AKS)":
                                                    sOut += F.Comma + Environment.NewLine;
                                                    break;
                                                case "Storage":
                                                    sOut += F.Comma + Environment.NewLine;
                                                    break;
                                                case "Azure Cache for Redis":
                                                    sOut += F.Comma + Environment.NewLine;
                                                    break;
                                            }
                                            sb.Append(sOut);
                                        }
                                        break;
                                    case "VARIABLES":
                                        foreach (TemplateResource r in Template.Resources.Values)
                                        {
                                            AzureResourceProvider p = this.Providers[r.ResourceProviderID];
                                            string s = this.InsertSnippet(p.AzureService, "Variable");
                                            sVars += s;
                                        }
                                        break;
                                    case "OUTPUTS":
                                        foreach (TemplateResource r in Template.Resources.Values)
                                        {
                                            AzureResourceProvider p = this.Providers[r.ResourceProviderID];
                                            sb.Append(this.InsertSnippet(p.AzureService, "Output"));
                                        }
                                        break;
                                    default:
                                        sb.Append(region.Render());
                                        break;
                                }
                            }
                        }
                    }
                    switch (RegionType.ToUpper())
                    {
                        case "PARAMETERS":
                            // replace the specific parameters with db values
                            foreach (string sKey in this.ReplacementValues)
                            {
                                string prop = sKey.Replace("*", "").ToUpper();
                                AzureResourceProviderProperty Prop = this.ProviderProperties.Values.ToList().Find(t => t.PropertyName.ToUpper() == prop);
                                if (Prop != null)
                                {
                                    int i = sParams.IndexOf(sKey);
                                    if (i > 0)
                                    {
                                        string sValue = Prop.DefaultValue;
                                        if (sKey == "diagnosticsStorageAccountId")
                                            sParams = sParams.Replace(sKey, sValue + F.Quote); // the in-file variables are surrounded by ***
                                        else
                                            sParams = sParams.Replace(sKey, F.Quote + sValue + F.Quote); // the in-file variables are surrounded by ***
                                    }
                                }
                            }
                            sb.Append(sParams);
                            sb.Remove(sb.ToString().LastIndexOf(","), 1); // remove the last comma
                            sb.Append(Environment.NewLine + F.ECB + F.Comma + Environment.NewLine);
                            break;
                        case "VARIABLES":
                            if (sVars.Length > 0)
                            {
                                sVars = sVars.Remove(sVars.LastIndexOf(","), 1); // remove the last comma
                                sb.Append(sVars);
                            }
                            sb.Append(F.ECB + F.Comma + Environment.NewLine);
                            break;
                        case "FUNCTIONS":
                            sb.Append(F.Space(5) + F.ECB + Environment.NewLine);
                            sb.Append(F.Space(1) + F.ECB + Environment.NewLine);
                            sb.Append(F.ESqB + F.Comma + Environment.NewLine);
                            break;
                        case "RESOURCES":
                            sb.Remove(sb.ToString().LastIndexOf(","), 1); // remove the last comma
                            sb.Append(Environment.NewLine + F.ESqB + F.Comma + Environment.NewLine);
                            break;
                        case "OUTPUTS":
                            sb.Remove(sb.ToString().LastIndexOf(","), 1); // remove the last comma
                            sb.Append(Environment.NewLine + F.Space(5) + F.ECB + Environment.NewLine);
                            break;
                    }
                }
            }
            //  sb.Remove(sb.Length - 1, 1); // remove the last comma
            sb.Append(F.ECB + Environment.NewLine);
            return (sb.ToString());
        }

        private string InsertSnippet(string ResourceType, string RegionType)
        {
            string sFileName = "";
            switch (ResourceType)
            {
                case "Azure Active Directory Domain Services":
                    switch (RegionType)
                    {
                        case "Parameter":
                            sFileName = this.SnippetFilePath + "AADParameters.txt";
                            break;
                        case "Resource":
                            sFileName = this.SnippetFilePath + "AADResource.txt";
                            break;
                    }
                    break;
                case "Azure Kubernetes Service (AKS)":
                    switch (RegionType)
                    {
                        case "Parameter":
                            sFileName = this.SnippetFilePath + "AKSClusterParameters.txt";
                            break;
                        case "Output":
                            sFileName = this.SnippetFilePath + "AKSClusterOutput.txt";
                            break;
                        case "Resource":
                            sFileName = this.SnippetFilePath + "AKSClusterResource.txt";
                            break;
                    }
                    break;
                case "Container Instances":
                    switch (RegionType)
                    {
                        case "Parameter":
                            sFileName = this.SnippetFilePath + "DockerParameters.txt";
                            break;
                        case "Resource":
                            sFileName = this.SnippetFilePath + "DockerResource.txt";
                            break;
                    }
                    break;
                case "Virtual Machines":
                    switch (RegionType)
                    {
                        case "Parameter":
                            sFileName = this.SnippetFilePath + "VMAddtoVNetParameters.txt";
                            break;
                        case "Resource":
                            sFileName = this.SnippetFilePath + "VMAddtoVNetResource.txt";
                            break;
                        case "Variable":
                            sFileName = this.SnippetFilePath + "VMAddtoVNetVars.txt";
                            break;
                        case "Output":
                            sFileName = this.SnippetFilePath + "VMAddtoVNetOutput.txt";
                            break;
                    }
                    break;
                case "Virtual Network":
                    switch (RegionType)
                    {
                        case "Parameter":
                            sFileName = this.SnippetFilePath + "VNet1SubParameters.txt";
                            break;
                        case "Resource":
                            sFileName = this.SnippetFilePath + "VNet1SubResource.txt";
                            break;
                    }
                    break;
                case "Azure Cache for Redis":
                    switch (RegionType)
                    {
                        case "Parameter":
                            sFileName = this.SnippetFilePath + "RedisCacheParameters.txt";
                            break;
                        case "Resource":
                            sFileName = this.SnippetFilePath + "RedisCacheResource.txt";
                            break;
                    }
                    break;
                case "Storage":
                    switch (RegionType)
                    {
                        case "Parameter":
                            sFileName = this.SnippetFilePath + "StorageParameters.txt";
                            break;
                        case "Resource":
                            sFileName = this.SnippetFilePath + "StorageResource.txt";
                            break;
                    }
                    break;

            }
            if (sFileName.Length > 0)
            {
                string sOut = File.ReadAllText(sFileName);
                if (sOut.IndexOf("||") > 0)
                    sOut = sOut.Substring(sOut.IndexOf("||") + 2); // this assumes a description inside the file that ends with ||
                return (sOut);
            }
            else
                return "";
        }
    }
}
