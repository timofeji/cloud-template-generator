using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TemplateGenerator.ARM.ResourceClasses;
namespace TemplateGenerator.ARM
{
    public static class FileMaker
    {
        #region CreateAKSWithACRFile
        public static string CreateAKSWithACRFile(AKSWithACR AKS, string TemplateJSON)
        {
            string sOut = TemplateJSON;
            List<string> ReplacementValues = new List<string>();
            foreach (PropertyInfo p in AKS.GetType().GetProperties())
            {
                ReplacementValues.Add("***" + p.Name + " ***");
            }

            foreach (string sKey in ReplacementValues)
            {
                string prop = sKey.Replace("*", "").ToUpper(); // the in-file variables are surrounded by ***

                int i = TemplateJSON.IndexOf(sKey);
                if (i > 0)
                {
                    switch (sKey)
                    {
                        case "acrName":
                            sOut = sOut.Replace(sKey, F.Quote + AKS.AcrName + F.Quote);
                            break;
                        case "aadSessionKey":
                            sOut = sOut.Replace(sKey, F.Quote + AKS.AadSessionKey + F.Quote); // client name?
                            break;
                        case "acrResourceGroup":
                            sOut = sOut.Replace(sKey, F.Quote + AKS.AcrResourceGroup + F.Quote); // /29 is smallest, allows 3 usable IP addresses
                            break;
                        case "location":
                            sOut = sOut.Replace(sKey, F.Quote + "[resourceGroup().location]" + F.Quote);
                            break;
                        case "dnsPrefix":
                            sOut = sOut.Replace(sKey, F.Quote + AKS.DnsPrefix + F.Quote);
                            break;
                        case "enableHttpApplicationRouting":
                            sOut = sOut.Replace(sKey, F.Quote + AKS.EnableHttpApplicationRouting + F.Quote);
                            break;
                        case "enableOmsAgent":
                            sOut = sOut.Replace(sKey, F.Quote + AKS.EnableOmsAgent + F.Quote);
                            break;
                        case "enablePrivateCluster":
                            sOut = sOut.Replace(sKey, F.Quote + AKS.EnablePrivateCluster + F.Quote);
                            break;
                        case "enableRBAC":
                            sOut = sOut.Replace(sKey, F.Quote + AKS.EnableRBAC + F.Quote);
                            break;
                        case "guidValue":
                            sOut = sOut.Replace(sKey, F.Quote + AKS.GuidValue + F.Quote);
                            break;
                        case "kubernetesVersion":
                            sOut = sOut.Replace(sKey, F.Quote + AKS.KubernetesVersion + F.Quote);
                            break;
                        case "maxPods":
                            sOut = sOut.Replace(sKey, F.Quote + AKS.MaxPods + F.Quote);
                            break;
                        case "networkPlugin":
                            sOut = sOut.Replace(sKey, F.Quote + AKS.NetworkPlugin + F.Quote);
                            break;
                        case "omsSku":
                            sOut = sOut.Replace(sKey, F.Quote + AKS.OmsSku + F.Quote);
                            break;
                        case "omsWorkspaceId":
                            sOut = sOut.Replace(sKey, F.Quote + AKS.OmsWorkspaceId + F.Quote);
                            break;
                        case "osDiskSizeGB":
                            sOut = sOut.Replace(sKey, F.Quote + AKS.OsDiskSizeGB + F.Quote);
                            break;
                        case "vmssNodePool":
                            sOut = sOut.Replace(sKey, F.Quote + AKS.VmssNodePool + F.Quote);
                            break;
                        case "windowsProfile":
                            sOut = sOut.Replace(sKey, F.Quote + AKS.WindowsProfile + F.Quote);
                            break;
                        case "workspaceName":
                            sOut = sOut.Replace(sKey, F.Quote + AKS.WorkspaceName + F.Quote);
                            break;
                        case "workspaceRegion":
                            sOut = sOut.Replace(sKey, F.Quote + AKS.WorkspaceRegion + F.Quote);
                            break;


                    }
                }
            }

            return (sOut);
        }
        #endregion CreateAKSWithACRFile

        #region CreateSubnetFile
        public static string CreateSubnetFile(SubnetAddToVNet SAVN, string TemplateJSON, string DBCnxnString)
        {
            // get client count to generate unique subnet prefix (or use ClientID from login but limited by 255 and/or number of subnets)
            SqlConnection Cnxn = new SqlConnection(DBCnxnString);

            SqlCommand cmd = new SqlCommand("spClientCount", Cnxn);
            cmd.CommandType = CommandType.StoredProcedure;

            Cnxn.Open();
            int iCount = (int)cmd.ExecuteScalar();
            Cnxn.Close();

            List<string> ReplacementValues = new List<string>();
            foreach (PropertyInfo p in SAVN.GetType().GetProperties())
            {
                ReplacementValues.Add("***" + p.Name + " ***");
            }
            // replace the specific parameters with db values
            string sOut = TemplateJSON;
            foreach (string sKey in ReplacementValues)
            {
                string prop = sKey.Replace("*", "").ToUpper(); // the in-file variables are surrounded by ***

                int i = TemplateJSON.IndexOf(sKey);
                if (i > 0)
                {
                    switch (sKey)
                    {
                        case "VNetName":
                            sOut = sOut.Replace(sKey, F.Quote + ConfigurationManager.AppSettings["VPN"] + F.Quote);
                            break;
                        case "newSubnetName":
                            sOut = sOut.Replace(sKey, F.Quote + SAVN.NewSubnetName + F.Quote); // client name?
                            break;
                        case "subnetAddressPrefix":
                            sOut = sOut.Replace(sKey, F.Quote + "10." + iCount + ".0.0/29" + F.Quote); // /29 is smallest, allows 3 usable IP addresses
                            break;
                        case "location":
                            sOut = sOut.Replace(sKey, F.Quote + "[resourceGroup().location]" + F.Quote);
                            break;
                        case "apiVersion":
                            sOut = sOut.Replace(sKey, F.Quote + ConfigurationManager.AppSettings["APIVersion"] + F.Quote);
                            break;


                    }
                }
            }
            // update the client with the subnet prefix
            return (sOut);
        }
        #endregion CreateSubnetFile

    }
}
