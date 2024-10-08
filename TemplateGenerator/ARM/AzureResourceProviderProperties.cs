using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
namespace TemplateGenerator.ARM
{
    [Serializable]
    public class AzureResourceProviderPropertyCollection : Dictionary<int, AzureResourceProviderProperty>
    {

        #region Constructors

        public AzureResourceProviderPropertyCollection()
        {
        }

        public AzureResourceProviderPropertyCollection(string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spAzureResourceProviderPropertiesFetchAll", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    AzureResourceProviderProperty oAzureResourceProviderPropertie = new AzureResourceProviderProperty();
                    oAzureResourceProviderPropertie.ResourceProviderID = dr["ResourceProviderID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ResourceProviderID"]);
                    oAzureResourceProviderPropertie.AzureResourceProviderPropertyID = dr["AzureResourceProviderPropertyID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["AzureResourceProviderPropertyID"]);
                    oAzureResourceProviderPropertie.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    oAzureResourceProviderPropertie.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oAzureResourceProviderPropertie.PropertyName = dr["PropertyName"] == DBNull.Value ? "" : dr["PropertyName"].ToString().Trim();
                    oAzureResourceProviderPropertie.DefaultValue = dr["DefaultValue"] == DBNull.Value ? "" : dr["DefaultValue"].ToString().Trim();
                    if (!this.ContainsKey(oAzureResourceProviderPropertie.AzureResourceProviderPropertyID))
                        this.Add(oAzureResourceProviderPropertie.AzureResourceProviderPropertyID, oAzureResourceProviderPropertie);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("AzureResourceProviderPropertieCollectionConstructor", Exc.Message, LogPath);
            }
            finally
            {
                if (Cnxn.State == ConnectionState.Open) Cnxn.Close();
            }
        }
        public AzureResourceProviderPropertyCollection(int ResourceProviderID, string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spAzureResourceProviderPropertiesFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ResourceProviderID", SqlDbType.Int));
                cmd.Parameters["@ResourceProviderID"].Value = ResourceProviderID;


                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    AzureResourceProviderProperty oAzureResourceProviderPropertie = new AzureResourceProviderProperty();
                    oAzureResourceProviderPropertie.ResourceProviderID = dr["ResourceProviderID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ResourceProviderID"]);
                    oAzureResourceProviderPropertie.AzureResourceProviderPropertyID = dr["AzureResourceProviderPropertyID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["AzureResourceProviderPropertyID"]);
                    oAzureResourceProviderPropertie.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    oAzureResourceProviderPropertie.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oAzureResourceProviderPropertie.PropertyName = dr["PropertyName"] == DBNull.Value ? "" : dr["PropertyName"].ToString().Trim();
                    oAzureResourceProviderPropertie.DefaultValue = dr["DefaultValue"] == DBNull.Value ? "" : dr["DefaultValue"].ToString().Trim();
                    if (!this.ContainsKey(oAzureResourceProviderPropertie.AzureResourceProviderPropertyID))
                        this.Add(oAzureResourceProviderPropertie.AzureResourceProviderPropertyID, oAzureResourceProviderPropertie);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("AzureResourceProviderPropertieCollectionConstructor", Exc.Message, LogPath);
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
                foreach (AzureResourceProviderProperty o in this.Values)
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
                Log.LogErr("AzureResourceProviderPropertieCollection Save", Exc.Message, LogPath);
                oPR.Exception = Exc;
                return (oPR);
            }
        }
        #endregion Save
    }


    [Serializable]
    public class AzureResourceProviderProperty
    {

        #region Vars

        string _CreatedDate;
        string _PropertyName;
        string _PropertyValue;
        int _ParentPropertyID;
        int _ResourceProviderID;
        string _DefaultValue;
        int _AzureResourceProviderPropertyID;
        string _ModifiedDate;

        #endregion Vars

        #region Get/Sets

        public string CreatedDate
        {
            get { return (_CreatedDate); }
            set { _CreatedDate = value; }
        }

        public string PropertyName
        {
            get { return (_PropertyName); }
            set { _PropertyName = value; }
        }

        public string PropertyValue
        {
            get { return (_PropertyValue); }
            set { _PropertyValue = value; }
        }

        public int ParentPropertyID
        {
            get { return (_ParentPropertyID); }
            set { _ParentPropertyID = value; }
        }

        public int ResourceProviderID
        {
            get { return (_ResourceProviderID); }
            set { _ResourceProviderID = value; }
        }

        public string DefaultValue
        {
            get { return (_DefaultValue); }
            set { _DefaultValue = value; }
        }

        public int AzureResourceProviderPropertyID
        {
            get { return (_AzureResourceProviderPropertyID); }
            set { _AzureResourceProviderPropertyID = value; }
        }

        public string ModifiedDate
        {
            get { return (_ModifiedDate); }
            set { _ModifiedDate = value; }
        }

        #endregion Get/Sets

        #region Constructors

        public AzureResourceProviderProperty()
        {
        }

        public AzureResourceProviderProperty(int AzureResourceProviderPropertyID, string CnxnString, string LogPath)
        {
            // fill props from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spAzureResourceProviderPropertieInfoFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@AzureResourceProviderPropertyID", SqlDbType.Int));
                cmd.Parameters["@AzureResourceProviderPropertyID"].Value = AzureResourceProviderPropertyID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    this.ResourceProviderID = dr["ResourceProviderID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ResourceProviderID"]);
                    this.AzureResourceProviderPropertyID = dr["AzureResourceProviderPropertyID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["AzureResourceProviderPropertyID"]);
                    this.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    this.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    this.PropertyName = dr["PropertyName"] == DBNull.Value ? "" : dr["PropertyName"].ToString().Trim();
                    this.DefaultValue = dr["DefaultValue"] == DBNull.Value ? "" : dr["DefaultValue"].ToString().Trim();
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("AzureResourceProviderPropertieConstructor", Exc.Message, LogPath);
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

                SqlCommand cmd = new SqlCommand("spAzureResourceProviderPropertieSave", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                #region Parameters
                // parameters for tblAzureResourceProviderProperties
                cmd.Parameters.Add(new SqlParameter("@ResourceProviderID", SqlDbType.Int));
                cmd.Parameters["@ResourceProviderID"].Value = this.ResourceProviderID;

                cmd.Parameters.Add(new SqlParameter("@PropertyValue", SqlDbType.VarChar, 500));
                cmd.Parameters["@PropertyValue"].Value = this.PropertyValue ?? "";

                cmd.Parameters.Add(new SqlParameter("@AzureResourceProviderPropertyID", SqlDbType.Int));
                cmd.Parameters["@AzureResourceProviderPropertyID"].Value = this.AzureResourceProviderPropertyID;

                cmd.Parameters.Add(new SqlParameter("@ModifiedDate", SqlDbType.VarChar, 50));
                cmd.Parameters["@ModifiedDate"].Value = this.ModifiedDate ?? "";

                cmd.Parameters.Add(new SqlParameter("@ParentPropertyID", SqlDbType.Int));
                cmd.Parameters["@ParentPropertyID"].Value = this.ParentPropertyID;

                cmd.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.VarChar, 50));
                cmd.Parameters["@CreatedDate"].Value = this.CreatedDate ?? "";

                cmd.Parameters.Add(new SqlParameter("@PropertyName", SqlDbType.VarChar, 50));
                cmd.Parameters["@PropertyName"].Value = this.PropertyName ?? "";

                cmd.Parameters.Add(new SqlParameter("@DefaultValue", SqlDbType.VarChar, 500));
                cmd.Parameters["@DefaultValue"].Value = this.DefaultValue ?? "";

                // assign output param
                cmd.Parameters.Add(new SqlParameter("@AzureResourceProviderPropertyIDOut", SqlDbType.Int));
                cmd.Parameters["@AzureResourceProviderPropertyIDOut"].Direction = ParameterDirection.Output;

                #endregion Parameters

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();

                int iAzureResourceProviderPropertyID = Convert.ToInt32(cmd.Parameters["@AzureResourceProviderPropertyIDOut"].Value);
                this.AzureResourceProviderPropertyID = iAzureResourceProviderPropertyID;

                oPR.ObjectProcessed = this;
                oPR.Result += "Saved";
                return (oPR);

            }
            catch (Exception Exc)
            {
                Log.LogErr("AzureResourceProviderPropertieSave", Exc.Message, LogPath);

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


        public static bool Delete(int AzureResourceProviderPropertyID, string CnxnString, string LogPath)
        {
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spAzureResourceProviderPropertieDelete", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@AzureResourceProviderPropertyID", SqlDbType.Int));
                cmd.Parameters["@AzureResourceProviderPropertyID"].Value = AzureResourceProviderPropertyID;

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();
                return (true);
            }
            catch (Exception Exc)
            {
                Log.LogErr("AzureResourceProviderPropertieDelete", Exc.Message, LogPath);
                return (false);
            }
            finally
            {
                if (Cnxn.State == ConnectionState.Open) Cnxn.Close();
            }


        }
        #endregion Delete

        public override string ToString()
        {
            return this.PropertyName + ":" + (this.PropertyValue.Trim().Length > 0 ? this.PropertyValue : this.DefaultValue);
        }
    }
}