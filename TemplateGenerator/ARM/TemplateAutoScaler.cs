using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace TemplateGenerator.ARM
{
    [Serializable]
    public class TemplateAutoScalerCollection : Dictionary<int, TemplateAutoScaler>
    {

        #region Constructors

        public TemplateAutoScalerCollection()
        {
        }

        public TemplateAutoScalerCollection(string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateAzureAutoScalersFetchAll", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateAutoScaler oTemplateAzureAutoScaler = new TemplateAutoScaler();
                    oTemplateAzureAutoScaler.TargetResourceUri = dr["TargetResourceUri"] == DBNull.Value ? "" : dr["TargetResourceUri"].ToString().Trim();
                    oTemplateAzureAutoScaler.IsEnabled = dr["IsEnabled"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsEnabled"]);
                    oTemplateAzureAutoScaler.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateAzureAutoScaler.TemplateID = dr["TemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateID"]);
                    oTemplateAzureAutoScaler.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    oTemplateAzureAutoScaler.ProfileName = dr["ProfileName"] == DBNull.Value ? "" : dr["ProfileName"].ToString().Trim();
                    oTemplateAzureAutoScaler.TemplateAzureAutoScalerID = dr["TemplateAzureAutoScalerID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateAzureAutoScalerID"]);
                    oTemplateAzureAutoScaler.CapacityMax = dr["CapacityMax"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CapacityMax"]);
                    oTemplateAzureAutoScaler.CapacityDefault = dr["CapacityDefault"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CapacityDefault"]);
                    oTemplateAzureAutoScaler.CapacityMin = dr["CapacityMin"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CapacityMin"]);
                    if (!this.ContainsKey(oTemplateAzureAutoScaler.TemplateAzureAutoScalerID))
                        this.Add(oTemplateAzureAutoScaler.TemplateAzureAutoScalerID, oTemplateAzureAutoScaler);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateAzureAutoScalerCollectionConstructor", Exc.Message, LogPath);
            }
            finally
            {
                if (Cnxn.State == ConnectionState.Open) Cnxn.Close();
            }
        }
        public TemplateAutoScalerCollection(int TemplateID, string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateAzureAutoScalersFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateID", SqlDbType.Int));
                cmd.Parameters["@TemplateID"].Value = TemplateID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateAutoScaler oTemplateAzureAutoScaler = new TemplateAutoScaler();
                    oTemplateAzureAutoScaler.TargetResourceUri = dr["TargetResourceUri"] == DBNull.Value ? "" : dr["TargetResourceUri"].ToString().Trim();
                    oTemplateAzureAutoScaler.IsEnabled = dr["IsEnabled"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsEnabled"]);
                    oTemplateAzureAutoScaler.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateAzureAutoScaler.TemplateID = dr["TemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateID"]);
                    oTemplateAzureAutoScaler.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    oTemplateAzureAutoScaler.ProfileName = dr["ProfileName"] == DBNull.Value ? "" : dr["ProfileName"].ToString().Trim();
                    oTemplateAzureAutoScaler.TemplateAzureAutoScalerID = dr["TemplateAzureAutoScalerID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateAzureAutoScalerID"]);
                    oTemplateAzureAutoScaler.CapacityMax = dr["CapacityMax"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CapacityMax"]);
                    oTemplateAzureAutoScaler.CapacityDefault = dr["CapacityDefault"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CapacityDefault"]);
                    oTemplateAzureAutoScaler.CapacityMin = dr["CapacityMin"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CapacityMin"]);
                    if (!this.ContainsKey(oTemplateAzureAutoScaler.TemplateAzureAutoScalerID))
                        this.Add(oTemplateAzureAutoScaler.TemplateAzureAutoScalerID, oTemplateAzureAutoScaler);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateAzureAutoScalerCollectionConstructor", Exc.Message, LogPath);
            }
            finally
            {
                if (Cnxn.State == ConnectionState.Open) Cnxn.Close();
            }
        }

        #endregion Constructors


        #region Save
        public ProcessResult Save(string CnxnString, string LogPath)
        {
            ProcessResult oPR = new ProcessResult();
            try
            {
                foreach (TemplateAutoScaler o in this.Values)
                {
                    oPR = o.Save(CnxnString, LogPath);
                    if (oPR.Exception != null)
                        throw oPR.Exception;
                }
                oPR.Result += "Collection Saved";
                return (oPR);

            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateAzureAutoScalerCollection Save", Exc.Message, LogPath);
                oPR.Exception = Exc;
                return (oPR);
            }
        }
        #endregion Save
    }


    [Serializable]
    public class TemplateAutoScaler : TemplateRegionResourceProperty
    {

        #region Vars

        bool _IsEnabled;
        int _TemplateID;
        int _TemplateAzureAutoScalerID;
        string _ProfileName;
        string _CreatedDate;
        int _CapacityDefault;
        string _ModifiedDate;
        string _TargetResourceUri;
        int _CapacityMin;
        int _CapacityMax;

        #endregion Vars
        TemplateAutoScalerRuleCollection _Rules = new TemplateAutoScalerRuleCollection();

        #region Get/Sets

        public bool IsEnabled
        {
            get { return (_IsEnabled); }
            set { _IsEnabled = value; }
        }

        public int TemplateID
        {
            get { return (_TemplateID); }
            set { _TemplateID = value; }
        }

        public int TemplateAzureAutoScalerID
        {
            get { return (_TemplateAzureAutoScalerID); }
            set { _TemplateAzureAutoScalerID = value; }
        }

        public string ProfileName
        {
            get { return (_ProfileName); }
            set { _ProfileName = value; }
        }

        public string CreatedDate
        {
            get { return (_CreatedDate); }
            set { _CreatedDate = value; }
        }

        public int CapacityDefault
        {
            get { return (_CapacityDefault); }
            set { _CapacityDefault = value; }
        }

        public string ModifiedDate
        {
            get { return (_ModifiedDate); }
            set { _ModifiedDate = value; }
        }

        public string TargetResourceUri
        {
            get { return (_TargetResourceUri); }
            set { _TargetResourceUri = value; }
        }

        public int CapacityMin
        {
            get { return (_CapacityMin); }
            set { _CapacityMin = value; }
        }

        public int CapacityMax
        {
            get { return (_CapacityMax); }
            set { _CapacityMax = value; }
        }

        public TemplateAutoScalerRuleCollection Rules { get => _Rules; set => _Rules = value; }

        #endregion Get/Sets

        #region Constructors

        public TemplateAutoScaler()
        {
        }

        public TemplateAutoScaler(int TemplateAzureAutoScalerID, string CnxnString, string LogPath)
        {
            // fill props from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateAzureAutoScalerInfoFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateAzureAutoScalerID", SqlDbType.Int));
                cmd.Parameters["@TemplateAzureAutoScalerID"].Value = TemplateAzureAutoScalerID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    this.TargetResourceUri = dr["TargetResourceUri"] == DBNull.Value ? "" : dr["TargetResourceUri"].ToString().Trim();
                    this.IsEnabled = dr["IsEnabled"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsEnabled"]);
                    this.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    this.TemplateID = dr["TemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateID"]);
                    this.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    this.ProfileName = dr["ProfileName"] == DBNull.Value ? "" : dr["ProfileName"].ToString().Trim();
                    this.TemplateAzureAutoScalerID = dr["TemplateAzureAutoScalerID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateAzureAutoScalerID"]);
                    this.CapacityMax = dr["CapacityMax"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CapacityMax"]);
                    this.CapacityDefault = dr["CapacityDefault"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CapacityDefault"]);
                    this.CapacityMin = dr["CapacityMin"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CapacityMin"]);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateAzureAutoScalerConstructor", Exc.Message, LogPath);
            }
            finally
            {
                if (Cnxn.State == ConnectionState.Open) Cnxn.Close();
            }


        }

        #endregion Constructors

        #region Save
        public ProcessResult Save(string CnxnString, string LogPath)
        {


            ProcessResult oPR = new ProcessResult();
            if (this.Rules.Count < 2)
            {
                oPR.Exception = new Exception("AutoScaler requires a minimum of 1 scale-in and 1 scale-out rule");
                return (oPR);
            }
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateAzureAutoScalerSave", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                #region Parameters
                // parameters for tblTemplateAzureAutoScaler
                cmd.Parameters.Add(new SqlParameter("@TargetResourceUri", SqlDbType.VarChar, 500));
                cmd.Parameters["@TargetResourceUri"].Value = this.TargetResourceUri ?? "";

                cmd.Parameters.Add(new SqlParameter("@IsEnabled", SqlDbType.Bit));
                cmd.Parameters["@IsEnabled"].Value = this.IsEnabled;

                cmd.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.VarChar, 50));
                cmd.Parameters["@CreatedDate"].Value = this.CreatedDate ?? "";

                cmd.Parameters.Add(new SqlParameter("@TemplateID", SqlDbType.Int));
                cmd.Parameters["@TemplateID"].Value = this.TemplateID;

                cmd.Parameters.Add(new SqlParameter("@ModifiedDate", SqlDbType.VarChar, 50));
                cmd.Parameters["@ModifiedDate"].Value = this.ModifiedDate ?? "";

                cmd.Parameters.Add(new SqlParameter("@ProfileName", SqlDbType.VarChar, 50));
                cmd.Parameters["@ProfileName"].Value = this.ProfileName ?? "";

                cmd.Parameters.Add(new SqlParameter("@TemplateAzureAutoScalerID", SqlDbType.Int));
                cmd.Parameters["@TemplateAzureAutoScalerID"].Value = this.TemplateAzureAutoScalerID;

                cmd.Parameters.Add(new SqlParameter("@CapacityMax", SqlDbType.Int));
                cmd.Parameters["@CapacityMax"].Value = this.CapacityMax;

                cmd.Parameters.Add(new SqlParameter("@CapacityDefault", SqlDbType.Int));
                cmd.Parameters["@CapacityDefault"].Value = this.CapacityDefault;

                cmd.Parameters.Add(new SqlParameter("@CapacityMin", SqlDbType.Int));
                cmd.Parameters["@CapacityMin"].Value = this.CapacityMin;

                // assign output param
                cmd.Parameters.Add(new SqlParameter("@TemplateAzureAutoScalerIDOut", SqlDbType.Int));
                cmd.Parameters["@TemplateAzureAutoScalerIDOut"].Direction = ParameterDirection.Output;

                #endregion Parameters

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();

                int iTemplateAzureAutoScalerID = Convert.ToInt32(cmd.Parameters["@TemplateAzureAutoScalerIDOut"].Value);
                this.TemplateAzureAutoScalerID = iTemplateAzureAutoScalerID;

                oPR.ObjectProcessed = this;
                oPR.Result += "Saved";
                return (oPR);

            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateAzureAutoScalerSave", Exc.Message, LogPath);

                oPR.Exception = Exc;
                oPR.Result += "Error";
                return (oPR);
            }
            finally
            {
                if (Cnxn.State == ConnectionState.Open) Cnxn.Close();
            }
        }
        #endregion Save

        #region Delete


        public static bool Delete(int TemplateAzureAutoScalerID, string CnxnString, string LogPath)
        {
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateAzureAutoScalerDelete", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateAzureAutoScalerID", SqlDbType.Int));
                cmd.Parameters["@TemplateAzureAutoScalerID"].Value = TemplateAzureAutoScalerID;

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();
                return (true);
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateAzureAutoScalerDelete", Exc.Message, LogPath);
                return (false);
            }
            finally
            {
                if (Cnxn.State == ConnectionState.Open) Cnxn.Close();
            }


        }
        #endregion Delete

        public new string Render()
        {
            /* Microsoft.insights/autoscalesettings resource provider
                      * {
                         "properties": {
                           "profiles": [
                             {
                               "name": "Autoscale by percentage based on CPU usage",
                               "capacity": {
                                 "minimum": "2",
                                 "maximum": "10",
                                 "default": "2"
                               },
                             "rules": [

                             ]
                             }
                           ],                  
                         "enabled": "[parameters('autoscaleEnabled')]",
                         "targetResourceUri": "[variables('targetResourceId')]"
                      }
                     */
            StringBuilder sb = new StringBuilder();
            sb.Append(F.Tab + F.Quote + "profiles" + F.Quote + F.Colon + F.SqB + Environment.NewLine);
            sb.Append(F.Tab + F.Space(5) + F.CB + Environment.NewLine);
            sb.Append(F.Tab + F.Space(5) + F.Quote + "capacity" + F.Quote + F.Colon + F.CB + Environment.NewLine);
            sb.Append(F.Tab + F.Space(7) + F.Quote + "minimum" + F.Quote + F.Colon + F.Quote + this.CapacityMin + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(7) + F.Quote + "maximum" + F.Quote + F.Colon + F.Quote + this.CapacityMax + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(7) + F.Quote + "default" + F.Quote + F.Colon + F.Quote + this.CapacityDefault + F.Quote + Environment.NewLine);
            sb.Append(F.Tab + F.Space(5) + F.ECB + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(4) + F.Quote + "rules" + F.Quote + F.Colon + F.SqB + Environment.NewLine);
            sb.Append(this.Rules.Render());
            sb.Append(F.Tab + F.Tab + F.Space(1) + F.ESqB + Environment.NewLine);
            sb.Append(F.Tab + F.Space(8) + F.ECB + Environment.NewLine);
            sb.Append(F.Tab + F.Space(4) + F.ESqB + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(3) + F.Quote + "enabled" + F.Quote + F.Colon + F.Quote + this.IsEnabled + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(3) + F.Quote + "targetResourceUri" + F.Quote + F.Colon + F.Quote + this.TargetResourceUri + F.Quote + F.Comma + Environment.NewLine);


            return (sb.ToString());
        }

        public static TemplateAutoScaler DummyData()
        {
            TemplateAutoScaler s = new TemplateAutoScaler();
            s.PropertyName = "AutoScaler";
            s.TemplateID = 1;
            s.TemplateAzureAutoScalerID = 1;
            s.CapacityDefault = 10;
            s.CapacityMin = 1;
            s.CapacityMax = 10;
            s.IsEnabled = true;
            s.ProfileName = "someProfile";
            s.TargetResourceUri = "someURI";
            s.CreatedDate = DateTime.Today.ToString();
            s.ModifiedDate = DateTime.Today.ToString();
            s.Rules.Add(s.Rules.Count, TemplateAutoScalerRule.DummyData(s));
            s.Rules.Add(s.Rules.Count, TemplateAutoScalerRule.DummyData(s));
            return (s);
        }
    }
}