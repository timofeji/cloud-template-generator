using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
namespace TemplateGenerator.ARM
{
    [Serializable]
    public class TemplateResourceProviderPropertyValueCollection : Dictionary<int, TemplateResourceProviderPropertyValue>
    {

        #region Constructors

        public TemplateResourceProviderPropertyValueCollection()
        {
        }

        public TemplateResourceProviderPropertyValueCollection(string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateResourceProviderPropertyValuesFetchAll", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateResourceProviderPropertyValue oTemplateResourceProviderPropertyValue = new TemplateResourceProviderPropertyValue();
                    oTemplateResourceProviderPropertyValue.PropertyValue = dr["PropertyValue"] == DBNull.Value ? "" : dr["PropertyValue"].ToString().Trim();
                    oTemplateResourceProviderPropertyValue.ResourceProviderPropertyID = dr["ResourceProviderPropertyID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ResourceProviderPropertyID"]);
                    oTemplateResourceProviderPropertyValue.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateResourceProviderPropertyValue.TemplateID = dr["TemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateID"]);
                    oTemplateResourceProviderPropertyValue.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    oTemplateResourceProviderPropertyValue.DefaultValue = dr["DefaultValue"] == DBNull.Value ? "" : dr["DefaultValue"].ToString().Trim();
                    oTemplateResourceProviderPropertyValue.TemplateResourceProviderPropertyID = dr["TemplateResourceProviderPropertyID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateResourceProviderPropertyID"]);
                    if (!this.ContainsKey(oTemplateResourceProviderPropertyValue.TemplateResourceProviderPropertyID))
                        this.Add(oTemplateResourceProviderPropertyValue.TemplateResourceProviderPropertyID, oTemplateResourceProviderPropertyValue);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateResourceProviderPropertyValueCollectionConstructor", Exc.Message, LogPath);
            }
            finally
            {
                if (Cnxn.State == ConnectionState.Open) Cnxn.Close();
            }
        }
        public TemplateResourceProviderPropertyValueCollection(int TemplateID, string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateResourceProviderPropertyValuesFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateID", SqlDbType.Int));
                cmd.Parameters["@TemplateID"].Value = TemplateID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateResourceProviderPropertyValue oTemplateResourceProviderPropertyValue = new TemplateResourceProviderPropertyValue();
                    oTemplateResourceProviderPropertyValue.PropertyValue = dr["PropertyValue"] == DBNull.Value ? "" : dr["PropertyValue"].ToString().Trim();
                    oTemplateResourceProviderPropertyValue.ResourceProviderPropertyID = dr["ResourceProviderPropertyID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ResourceProviderPropertyID"]);
                    oTemplateResourceProviderPropertyValue.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateResourceProviderPropertyValue.TemplateID = dr["TemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateID"]);
                    oTemplateResourceProviderPropertyValue.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    oTemplateResourceProviderPropertyValue.DefaultValue = dr["DefaultValue"] == DBNull.Value ? "" : dr["DefaultValue"].ToString().Trim();
                    oTemplateResourceProviderPropertyValue.TemplateResourceProviderPropertyID = dr["TemplateResourceProviderPropertyID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateResourceProviderPropertyID"]);
                    if (!this.ContainsKey(oTemplateResourceProviderPropertyValue.TemplateResourceProviderPropertyID))
                        this.Add(oTemplateResourceProviderPropertyValue.TemplateResourceProviderPropertyID, oTemplateResourceProviderPropertyValue);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateResourceProviderPropertyValueCollectionConstructor", Exc.Message, LogPath);
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
                foreach (TemplateResourceProviderPropertyValue o in this.Values)
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
                Log.LogErr("TemplateResourceProviderPropertyValueCollection Save", Exc.Message, LogPath);
                oPR.Exception = Exc;
                return (oPR);
            }
        }
        #endregion Save
    }


    [Serializable]
    public class TemplateResourceProviderPropertyValue
    {

        #region Vars

        int _ResourceProviderPropertyID;
        int _TemplateID;
        int _TemplateResourceProviderPropertyID;
        string _PropertyValue;
        string _DefaultValue;
        string _ModifiedDate;
        string _CreatedDate;

        #endregion Vars

        #region Get/Sets

        public int ResourceProviderPropertyID
        {
            get { return (_ResourceProviderPropertyID); }
            set { _ResourceProviderPropertyID = value; }
        }

        public int TemplateID
        {
            get { return (_TemplateID); }
            set { _TemplateID = value; }
        }

        public int TemplateResourceProviderPropertyID
        {
            get { return (_TemplateResourceProviderPropertyID); }
            set { _TemplateResourceProviderPropertyID = value; }
        }

        public string PropertyValue
        {
            get { return (_PropertyValue); }
            set { _PropertyValue = value; }
        }

        public string DefaultValue
        {
            get { return (_DefaultValue); }
            set { _DefaultValue = value; }
        }

        public string ModifiedDate
        {
            get { return (_ModifiedDate); }
            set { _ModifiedDate = value; }
        }

        public string CreatedDate
        {
            get { return (_CreatedDate); }
            set { _CreatedDate = value; }
        }

        #endregion Get/Sets

        #region Constructors

        public TemplateResourceProviderPropertyValue()
        {
        }

        public TemplateResourceProviderPropertyValue(int TemplateResourceProviderPropertyValueID, string CnxnString, string LogPath)
        {
            // fill props from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateResourceProviderPropertyValueInfoFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateResourceProviderPropertyValueID", SqlDbType.Int));
                cmd.Parameters["@TemplateResourceProviderPropertyValueID"].Value = TemplateResourceProviderPropertyValueID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    this.PropertyValue = dr["PropertyValue"] == DBNull.Value ? "" : dr["PropertyValue"].ToString().Trim();
                    this.ResourceProviderPropertyID = dr["ResourceProviderPropertyID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ResourceProviderPropertyID"]);
                    this.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    this.TemplateID = dr["TemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateID"]);
                    this.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    this.DefaultValue = dr["DefaultValue"] == DBNull.Value ? "" : dr["DefaultValue"].ToString().Trim();
                    this.TemplateResourceProviderPropertyID = dr["TemplateResourceProviderPropertyID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateResourceProviderPropertyID"]);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateResourceProviderPropertyValueConstructor", Exc.Message, LogPath);
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

                SqlCommand cmd = new SqlCommand("spTemplateResourceProviderPropertyValueSave", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                #region Parameters
                // parameters for tblTemplateResourceProviderPropertyValues
                cmd.Parameters.Add(new SqlParameter("@PropertyValue", SqlDbType.VarChar, 500));
                cmd.Parameters["@PropertyValue"].Value = this.PropertyValue ?? "";

                cmd.Parameters.Add(new SqlParameter("@ResourceProviderPropertyID", SqlDbType.Int));
                cmd.Parameters["@ResourceProviderPropertyID"].Value = this.ResourceProviderPropertyID;

                cmd.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.VarChar, 50));
                cmd.Parameters["@CreatedDate"].Value = this.CreatedDate ?? "";

                cmd.Parameters.Add(new SqlParameter("@TemplateID", SqlDbType.Int));
                cmd.Parameters["@TemplateID"].Value = this.TemplateID;

                cmd.Parameters.Add(new SqlParameter("@ModifiedDate", SqlDbType.VarChar, 50));
                cmd.Parameters["@ModifiedDate"].Value = this.ModifiedDate ?? "";

                cmd.Parameters.Add(new SqlParameter("@DefaultValue", SqlDbType.VarChar, 500));
                cmd.Parameters["@DefaultValue"].Value = this.DefaultValue ?? "";

                cmd.Parameters.Add(new SqlParameter("@TemplateResourceProviderPropertyID", SqlDbType.Int));
                cmd.Parameters["@TemplateResourceProviderPropertyID"].Value = this.TemplateResourceProviderPropertyID;

                // assign output param
                cmd.Parameters.Add(new SqlParameter("@TemplateResourceProviderPropertyValueIDOut", SqlDbType.Int));
                cmd.Parameters["@TemplateResourceProviderPropertyValueIDOut"].Direction = ParameterDirection.Output;

                #endregion Parameters

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();

                int iTemplateResourceProviderPropertyValueID = Convert.ToInt32(cmd.Parameters["@TemplateResourceProviderPropertyValueIDOut"].Value);
                this.TemplateResourceProviderPropertyID = iTemplateResourceProviderPropertyValueID;

                oPR.ObjectProcessed = this;
                oPR.Result += "Saved";
                return (oPR);

            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateResourceProviderPropertyValueSave", Exc.Message, LogPath);

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


        public static bool Delete(int TemplateResourceProviderPropertyValueID, string CnxnString, string LogPath)
        {
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateResourceProviderPropertyValueDelete", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateResourceProviderPropertyValueID", SqlDbType.Int));
                cmd.Parameters["@TemplateResourceProviderPropertyValueID"].Value = TemplateResourceProviderPropertyValueID;

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();
                return (true);
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateResourceProviderPropertyValueDelete", Exc.Message, LogPath);
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