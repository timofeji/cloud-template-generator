using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
namespace TemplateGenerator.ARM
{
    [Serializable]
    public class NetworkSecurityGroupRuleCollection : Dictionary<int, NetworkSecurityGroupRule>
    {

        #region Constructors

        public NetworkSecurityGroupRuleCollection()
        {
        }

        public NetworkSecurityGroupRuleCollection(string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spNetworkSecurityGroupRulesFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    NetworkSecurityGroupRule oNetworkSecurityGroupRule = new NetworkSecurityGroupRule();
                    oNetworkSecurityGroupRule.Priority = dr["Priority"] == DBNull.Value ? "" : dr["Priority"].ToString().Trim();
                    oNetworkSecurityGroupRule.SourceAddressPrefix = dr["SourceAddressPrefix"] == DBNull.Value ? "" : dr["SourceAddressPrefix"].ToString().Trim();
                    oNetworkSecurityGroupRule.RuleName = dr["RuleName"] == DBNull.Value ? "" : dr["RuleName"].ToString().Trim();
                    oNetworkSecurityGroupRule.NetworkSecurityGroupRuleID = dr["NetworkSecurityGroupRuleID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["NetworkSecurityGroupRuleID"]);
                    oNetworkSecurityGroupRule.Access = dr["Access"] == DBNull.Value ? "" : dr["Access"].ToString().Trim();
                    oNetworkSecurityGroupRule.DestinationAddressPrefix = dr["DestinationAddressPrefix"] == DBNull.Value ? "" : dr["DestinationAddressPrefix"].ToString().Trim();
                    oNetworkSecurityGroupRule.DestinationPortRange = dr["DestinationPortRange"] == DBNull.Value ? "" : dr["DestinationPortRange"].ToString().Trim();
                    oNetworkSecurityGroupRule.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    oNetworkSecurityGroupRule.Direction = dr["Direction"] == DBNull.Value ? "" : dr["Direction"].ToString().Trim();
                    oNetworkSecurityGroupRule.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oNetworkSecurityGroupRule.SourcePortRange = dr["SourcePortRange"] == DBNull.Value ? "" : dr["SourcePortRange"].ToString().Trim();
                    oNetworkSecurityGroupRule.Protocol = dr["Protocol"] == DBNull.Value ? "" : dr["Protocol"].ToString().Trim();
                    if (!this.ContainsKey(oNetworkSecurityGroupRule.NetworkSecurityGroupRuleID))
                        this.Add(oNetworkSecurityGroupRule.NetworkSecurityGroupRuleID, oNetworkSecurityGroupRule);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("NetworkSecurityGroupRuleCollectionConstructor", Exc.Message, LogPath);
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
                foreach (NetworkSecurityGroupRule o in this.Values)
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
                Log.LogErr("NetworkSecurityGroupRuleCollection Save", Exc.Message, LogPath);
                oPR.Exception = Exc;
                return (oPR);
            }
        }
        #endregion Save
    }


    [Serializable]
    public class NetworkSecurityGroupRule
    {

        #region Vars

        string _RuleName;
        string _SourcePortRange;
        string _Direction;
        string _SourceAddressPrefix;
        int _NetworkSecurityGroupRuleID;
        string _Priority;
        string _DestinationAddressPrefix;
        string _ModifiedDate;
        string _DestinationPortRange;
        string _Access;
        string _CreatedDate;
        string _Protocol;

        #endregion Vars

        #region Get/Sets

        public string RuleName
        {
            get { return (_RuleName); }
            set { _RuleName = value; }
        }

        public string SourcePortRange
        {
            get { return (_SourcePortRange); }
            set { _SourcePortRange = value; }
        }

        public string Direction
        {
            get { return (_Direction); }
            set { _Direction = value; }
        }

        public string SourceAddressPrefix
        {
            get { return (_SourceAddressPrefix); }
            set { _SourceAddressPrefix = value; }
        }

        public int NetworkSecurityGroupRuleID
        {
            get { return (_NetworkSecurityGroupRuleID); }
            set { _NetworkSecurityGroupRuleID = value; }
        }

        public string Priority
        {
            get { return (_Priority); }
            set { _Priority = value; }
        }

        public string DestinationAddressPrefix
        {
            get { return (_DestinationAddressPrefix); }
            set { _DestinationAddressPrefix = value; }
        }

        public string ModifiedDate
        {
            get { return (_ModifiedDate); }
            set { _ModifiedDate = value; }
        }

        public string DestinationPortRange
        {
            get { return (_DestinationPortRange); }
            set { _DestinationPortRange = value; }
        }

        public string Access
        {
            get { return (_Access); }
            set { _Access = value; }
        }

        public string CreatedDate
        {
            get { return (_CreatedDate); }
            set { _CreatedDate = value; }
        }

        public string Protocol
        {
            get { return (_Protocol); }
            set { _Protocol = value; }
        }

        #endregion Get/Sets

        #region Constructors

        public NetworkSecurityGroupRule()
        {
        }

        public NetworkSecurityGroupRule(int NetworkSecurityGroupRuleID, string CnxnString, string LogPath)
        {
            // fill props from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spNetworkSecurityGroupRuleInfoFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@NetworkSecurityGroupRuleID", SqlDbType.Int));
                cmd.Parameters["@NetworkSecurityGroupRuleID"].Value = NetworkSecurityGroupRuleID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    this.Priority = dr["Priority"] == DBNull.Value ? "" : dr["Priority"].ToString().Trim();
                    this.SourceAddressPrefix = dr["SourceAddressPrefix"] == DBNull.Value ? "" : dr["SourceAddressPrefix"].ToString().Trim();
                    this.RuleName = dr["RuleName"] == DBNull.Value ? "" : dr["RuleName"].ToString().Trim();
                    this.NetworkSecurityGroupRuleID = dr["NetworkSecurityGroupRuleID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["NetworkSecurityGroupRuleID"]);
                    this.Access = dr["Access"] == DBNull.Value ? "" : dr["Access"].ToString().Trim();
                    this.DestinationAddressPrefix = dr["DestinationAddressPrefix"] == DBNull.Value ? "" : dr["DestinationAddressPrefix"].ToString().Trim();
                    this.DestinationPortRange = dr["DestinationPortRange"] == DBNull.Value ? "" : dr["DestinationPortRange"].ToString().Trim();
                    this.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    this.Direction = dr["Direction"] == DBNull.Value ? "" : dr["Direction"].ToString().Trim();
                    this.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    this.SourcePortRange = dr["SourcePortRange"] == DBNull.Value ? "" : dr["SourcePortRange"].ToString().Trim();
                    this.Protocol = dr["Protocol"] == DBNull.Value ? "" : dr["Protocol"].ToString().Trim();
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("NetworkSecurityGroupRuleConstructor", Exc.Message, LogPath);
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
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spNetworkSecurityGroupRuleSave", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                #region Parameters
                // parameters for tblNetworkSecurityGroupRules
                cmd.Parameters.Add(new SqlParameter("@Priority", SqlDbType.VarChar, 50));
                cmd.Parameters["@Priority"].Value = this.Priority ?? "";

                cmd.Parameters.Add(new SqlParameter("@SourceAddressPrefix", SqlDbType.VarChar, 50));
                cmd.Parameters["@SourceAddressPrefix"].Value = this.SourceAddressPrefix ?? "";

                cmd.Parameters.Add(new SqlParameter("@RuleName", SqlDbType.VarChar, 50));
                cmd.Parameters["@RuleName"].Value = this.RuleName ?? "";

                cmd.Parameters.Add(new SqlParameter("@NetworkSecurityGroupRuleID", SqlDbType.Int));
                cmd.Parameters["@NetworkSecurityGroupRuleID"].Value = this.NetworkSecurityGroupRuleID;

                cmd.Parameters.Add(new SqlParameter("@Access", SqlDbType.VarChar, 50));
                cmd.Parameters["@Access"].Value = this.Access ?? "";

                cmd.Parameters.Add(new SqlParameter("@DestinationAddressPrefix", SqlDbType.VarChar, 50));
                cmd.Parameters["@DestinationAddressPrefix"].Value = this.DestinationAddressPrefix ?? "";

                cmd.Parameters.Add(new SqlParameter("@DestinationPortRange", SqlDbType.VarChar, 50));
                cmd.Parameters["@DestinationPortRange"].Value = this.DestinationPortRange ?? "";

                cmd.Parameters.Add(new SqlParameter("@ModifiedDate", SqlDbType.VarChar, 50));
                cmd.Parameters["@ModifiedDate"].Value = this.ModifiedDate ?? "";

                cmd.Parameters.Add(new SqlParameter("@Direction", SqlDbType.VarChar, 50));
                cmd.Parameters["@Direction"].Value = this.Direction ?? "";

                cmd.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.VarChar, 50));
                cmd.Parameters["@CreatedDate"].Value = this.CreatedDate ?? "";

                cmd.Parameters.Add(new SqlParameter("@SourcePortRange", SqlDbType.VarChar, 50));
                cmd.Parameters["@SourcePortRange"].Value = this.SourcePortRange ?? "";

                cmd.Parameters.Add(new SqlParameter("@Protocol", SqlDbType.VarChar, 50));
                cmd.Parameters["@Protocol"].Value = this.Protocol ?? "";

                // assign output param
                cmd.Parameters.Add(new SqlParameter("@NetworkSecurityGroupRuleIDOut", SqlDbType.Int));
                cmd.Parameters["@NetworkSecurityGroupRuleIDOut"].Direction = ParameterDirection.Output;

                #endregion Parameters

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();

                int iNetworkSecurityGroupRuleID = Convert.ToInt32(cmd.Parameters["@NetworkSecurityGroupRuleIDOut"].Value);
                this.NetworkSecurityGroupRuleID = iNetworkSecurityGroupRuleID;

                oPR.ObjectProcessed = this;
                oPR.Result += "Saved";
                return (oPR);

            }
            catch (Exception Exc)
            {
                Log.LogErr("NetworkSecurityGroupRuleSave", Exc.Message, LogPath);

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


        public static bool Delete(int NetworkSecurityGroupRuleID, string CnxnString, string LogPath)
        {
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spNetworkSecurityGroupRuleDelete", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@NetworkSecurityGroupRuleID", SqlDbType.Int));
                cmd.Parameters["@NetworkSecurityGroupRuleID"].Value = NetworkSecurityGroupRuleID;

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();
                return (true);
            }
            catch (Exception Exc)
            {
                Log.LogErr("NetworkSecurityGroupRuleDelete", Exc.Message, LogPath);
                return (false);
            }
            finally
            {
                if (Cnxn.State == ConnectionState.Open) Cnxn.Close();
            }


        }
        #endregion Delete

    }
}