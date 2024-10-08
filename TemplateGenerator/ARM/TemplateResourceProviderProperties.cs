using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
namespace TemplateGenerator.ARM
{
    [Serializable]
    public class TemplateResourceProviderPropertyCollection : Dictionary<int, TemplateResourceProviderProperty>
    {

        #region Constructors

        public TemplateResourceProviderPropertyCollection()
        {
        }

        public TemplateResourceProviderPropertyCollection(string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateResourceProviderPropertiesFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateResourceProviderProperty oTemplateResourceProviderPropertie = new TemplateResourceProviderProperty();
                    oTemplateResourceProviderPropertie.PropertyValue = dr["PropertyValue"] == DBNull.Value ? "" : dr["PropertyValue"].ToString().Trim();
                    oTemplateResourceProviderPropertie.ResourceProviderPropertyID = dr["ResourceProviderPropertyID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ResourceProviderPropertyID"]);
                    oTemplateResourceProviderPropertie.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateResourceProviderPropertie.TemplateID = dr["TemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateID"]);
                    oTemplateResourceProviderPropertie.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    oTemplateResourceProviderPropertie.DefaultValue = dr["DefaultValue"] == DBNull.Value ? "" : dr["DefaultValue"].ToString().Trim();
                    oTemplateResourceProviderPropertie.TemplateResourceProviderPropertyID = dr["TemplateResourceProviderPropertyID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateResourceProviderPropertyID"]);
                    if (!this.ContainsKey(oTemplateResourceProviderPropertie.TemplateResourceProviderPropertyID))
                        this.Add(oTemplateResourceProviderPropertie.TemplateResourceProviderPropertyID, oTemplateResourceProviderPropertie);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateResourceProviderPropertieCollectionConstructor", Exc.Message, LogPath);
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
                foreach (TemplateResourceProviderProperty o in this.Values)
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
                Log.LogErr("TemplateResourceProviderPropertieCollection Save", Exc.Message, LogPath);
                oPR.Exception = Exc;
                return (oPR);
            }
        }
        #endregion Save
    }


    [Serializable]
    public class TemplateResourceProviderProperty
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

        public TemplateResourceProviderProperty()
        {
        }

        public TemplateResourceProviderProperty(int TemplateResourceProviderPropertyID, string CnxnString, string LogPath)
        {
            // fill props from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateResourceProviderPropertieInfoFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateResourceProviderPropertyID", SqlDbType.Int));
                cmd.Parameters["@TemplateResourceProviderPropertyID"].Value = TemplateResourceProviderPropertyID;

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
                Log.LogErr("TemplateResourceProviderPropertieConstructor", Exc.Message, LogPath);
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

                SqlCommand cmd = new SqlCommand("spTemplateResourceProviderPropertieSave", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                #region Parameters
                // parameters for tblTemplateResourceProviderProperties
                cmd.Parameters.Add(new SqlParameter("@PropertyValue", SqlDbType.VarChar, 500));
                cmd.Parameters["@PropertyValue"].Value = this.PropertyValue ?? "";

                cmd.Parameters.Add(new SqlParameter("@ResourceProviderPropertyID", SqlDbType.Int));
                cmd.Parameters["@ResourceProviderPropertyID"].Value = this.ResourceProviderPropertyID;

                cmd.Parameters.Add(new SqlParameter("@TemplateID", SqlDbType.Int));
                cmd.Parameters["@TemplateID"].Value = this.TemplateID;

                cmd.Parameters.Add(new SqlParameter("@DefaultValue", SqlDbType.VarChar, 500));
                cmd.Parameters["@DefaultValue"].Value = this.DefaultValue ?? "";

                cmd.Parameters.Add(new SqlParameter("@TemplateResourceProviderPropertyID", SqlDbType.Int));
                cmd.Parameters["@TemplateResourceProviderPropertyID"].Value = this.TemplateResourceProviderPropertyID;

                // assign output param
                cmd.Parameters.Add(new SqlParameter("@TemplateResourceProviderPropertyIDOut", SqlDbType.Int));
                cmd.Parameters["@TemplateResourceProviderPropertyIDOut"].Direction = ParameterDirection.Output;

                #endregion Parameters

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();

                int iTemplateResourceProviderPropertyID = Convert.ToInt32(cmd.Parameters["@TemplateResourceProviderPropertyIDOut"].Value);
                this.TemplateResourceProviderPropertyID = iTemplateResourceProviderPropertyID;

                oPR.ObjectProcessed = this;
                oPR.Result += "Saved";
                return (oPR);

            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateResourceProviderPropertieSave", Exc.Message, LogPath);

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


        public static bool Delete(int TemplateResourceProviderPropertyID, string CnxnString, string LogPath)
        {
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateResourceProviderPropertieDelete", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateResourceProviderPropertyID", SqlDbType.Int));
                cmd.Parameters["@TemplateResourceProviderPropertyID"].Value = TemplateResourceProviderPropertyID;

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();
                return (true);
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateResourceProviderPropertieDelete", Exc.Message, LogPath);
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
