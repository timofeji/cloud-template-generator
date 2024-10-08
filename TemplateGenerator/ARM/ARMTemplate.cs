using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TemplateGenerator.ARM
{
    public class ARMTemplateCollection : Dictionary<int, ARMTemplate>
    {
        public ARMTemplateCollection()
        {
        }
        public ARMTemplateCollection(string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spARMTemplatesFetchAll", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ARMTemplate oARMTemplate = new ARMTemplate();
                    oARMTemplate.TemplateID = dr["TemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateID"]);
                    oARMTemplate.TemplateName = dr["TemplateName"] == DBNull.Value ? "" : dr["TemplateName"].ToString().Trim();
                    oARMTemplate.Description = dr["TemplateDescription"] == DBNull.Value ? "" : dr["TemplateDescription"].ToString().Trim();                    
                    if (!this.ContainsKey(oARMTemplate.TemplateID))
                        this.Add(oARMTemplate.TemplateID, oARMTemplate);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("ARMTemplateCollection", Exc.Message, LogPath);
            }
            finally
            {
                if (Cnxn.State == ConnectionState.Open) Cnxn.Close();
            }
        }
    }
    public class ARMTemplate
    {
        public enum RegionTypes
        {
            Common = 1,
            Resources = 2,
            Parameters = 3,
            Variables = 4,
            Functions = 5,
            Outputs = 6
        }
        string[] _AllowedParameterTypes = { "string", "securestring", "int", "bool", "object", "secureObject", "array" };
        List<TemplateRegion> _regions = new List<TemplateRegion>();
        TemplateResourceCollection _Resources = new TemplateResourceCollection();
        string _FunctionsNamespace;
        int _TemplateID;
        string _DBCnxnString = "";
        string _LogPath = "";
        List<string> _ReplaceValues = new List<string>();
        private string _TemplateName;
        private string _Description;
        private TemplateResourceProviderPropertyValueCollection _Values;

        public List<TemplateRegion> Regions { get => _regions; set => _regions = value; }
        public List<string> AllowedParameterTypes { get => new List<string>(_AllowedParameterTypes); }
        public string FunctionsNamespace { get => _FunctionsNamespace; set => _FunctionsNamespace = value; }
        public int TemplateID { get => _TemplateID; set => _TemplateID = value; }
        public TemplateResourceCollection Resources { get => _Resources; set => _Resources = value; }
        public TemplateResourceProviderPropertyValueCollection ResourcePropertyValues { get => _Values; set => _Values = value; }
        public string DBCnxnString { get => _DBCnxnString; set => _DBCnxnString = value; }
        public string LogPath { get => _LogPath; set => _LogPath = value; }
        public string TemplateName { get => _TemplateName; set => _TemplateName = value; }
        public string Description { get => _Description; set => _Description = value; }

        public ARMTemplate()
        {

        }
        public ARMTemplate(int TemplateID, string DBCnxnString, string LogPath)
        {
            this.Regions.Add(new TemplateRegionResource());
            this.Regions.Add(new TemplateRegionParameter());
            this.Regions.Add(new TemplateRegionOutput());
            this.Regions.Add(new TemplateRegionFunction());
            this.Regions.Add(new TemplateRegionVariable());

            this.Resources = new TemplateResourceCollection(TemplateID, DBCnxnString, LogPath);
            this.ResourcePropertyValues = new TemplateResourceProviderPropertyValueCollection(TemplateID, DBCnxnString, LogPath);
        }

        public string RenderTemplate()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(F.CB);
            sb.Append(F.Tab);

            foreach (TemplateRegion region in this.Regions)
            {
                sb.Append(region.Render());
            }
            sb.Append(F.ECB);
            return (sb.ToString());
        }

        public bool ValidateParameterType(string Type)
        {
            if (this.AllowedParameterTypes.Contains(Type))
                return true;
            return false;
        }

        public override string ToString()
        {
            return base.ToString();
        }
        public static ARMTemplate DummyData(string CnxnString, string LogPath)
        {
            ARMTemplate a = new ARMTemplate(1, CnxnString, LogPath);

            a.Regions.Add(new TemplateRegionCommon());

            a.Regions.Add(TemplateRegionParameter.DummyData(a));

            a.Regions.Add(TemplateRegionFunction.DummyData(a));

            a.Regions.Add(TemplateRegionVariable.DummyData(1));
            a.Regions.Add(TemplateRegionVariable.DummyData(2));

            TemplateRegionResource r = TemplateRegionResource.DummyData();
            a.Regions.Add(r);

            a.Regions.Add(TemplateRegionOutput.DummyData());

            return (a);
        }

    }

    public static class F // for Formatter
    {
        // created to simplify rendering syntax for all templates

        static string tab = "\t";
        static string cb = "{";
        static string ecb = "}";
        static string sqb = "[";
        static string esqb = "]";
        static string comma = ",";
        static string colon = ":";
        static string quote = "\"";

        public static string Tab { get => tab; }
        public static string CB { get => cb; }
        public static string ECB { get => ecb; }
        public static string SqB { get => sqb; }
        public static string ESqB { get => esqb; }
        public static string Comma { get => comma; }
        public static string Colon { get => colon; }
        public static string Quote { get => quote; set => quote = value; }

        public static string Space(int Spaces)
        {
            StringBuilder sb = new StringBuilder();
            for (int x = 1; x <= Spaces; x++)
                sb.Append(" ");
            return (sb.ToString());
        }
    }
}
