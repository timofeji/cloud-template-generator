using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
namespace TemplateGenerator.ARM
{
    [Serializable]
    public class TemplateRegionParameterAllowedValueCollection : Dictionary<int, TemplateRegionParameterAllowedValue>
    {

        #region Constructors

        public TemplateRegionParameterAllowedValueCollection()
        {
        }

        public TemplateRegionParameterAllowedValueCollection(string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionParameterAllowedValuesFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateRegionParameterAllowedValue oTemplateRegionParameterAllowedValue = new TemplateRegionParameterAllowedValue();
                    oTemplateRegionParameterAllowedValue.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateRegionParameterAllowedValue.TemplateRegionParameterID = dr["TemplateRegionParameterID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionParameterID"]);
                    oTemplateRegionParameterAllowedValue.TemplateRegionParametersAllowedValueID = dr["TemplateRegionParametersAllowedValueID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionParametersAllowedValueID"]);
                    oTemplateRegionParameterAllowedValue.AllowedValue = dr["AllowedValue"] == DBNull.Value ? "" : dr["AllowedValue"].ToString().Trim();
                    oTemplateRegionParameterAllowedValue.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    if (!this.ContainsKey(oTemplateRegionParameterAllowedValue.TemplateRegionParametersAllowedValueID))
                        this.Add(oTemplateRegionParameterAllowedValue.TemplateRegionParametersAllowedValueID, oTemplateRegionParameterAllowedValue);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionParameterAllowedValueCollectionConstructor", Exc.Message, LogPath);
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
                foreach (TemplateRegionParameterAllowedValue o in this.Values)
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
                Log.LogErr("TemplateRegionParameterAllowedValueCollection Save", Exc.Message, LogPath);
                oPR.Exception = Exc;
                return (oPR);
            }
        }
        #endregion Save
    }


    [Serializable]
    public class TemplateRegionParameterAllowedValue
    {

        #region Vars

        int _TemplateRegionParametersAllowedValueID;
        string _CreatedDate;
        int _TemplateRegionParameterID;
        string _ModifiedDate;
        string _AllowedValue;

        #endregion Vars

        #region Get/Sets

        public int TemplateRegionParametersAllowedValueID
        {
            get { return (_TemplateRegionParametersAllowedValueID); }
            set { _TemplateRegionParametersAllowedValueID = value; }
        }

        public string CreatedDate
        {
            get { return (_CreatedDate); }
            set { _CreatedDate = value; }
        }

        public int TemplateRegionParameterID
        {
            get { return (_TemplateRegionParameterID); }
            set { _TemplateRegionParameterID = value; }
        }

        public string ModifiedDate
        {
            get { return (_ModifiedDate); }
            set { _ModifiedDate = value; }
        }

        public string AllowedValue
        {
            get { return (_AllowedValue); }
            set { _AllowedValue = value; }
        }

        #endregion Get/Sets

        #region Constructors

        public TemplateRegionParameterAllowedValue()
        {
        }

        public TemplateRegionParameterAllowedValue(int TemplateRegionParameterAllowedValueID, string CnxnString, string LogPath)
        {
            // fill props from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionParameterAllowedValueInfoFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionParameterAllowedValueID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionParameterAllowedValueID"].Value = TemplateRegionParameterAllowedValueID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    this.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    this.TemplateRegionParameterID = dr["TemplateRegionParameterID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionParameterID"]);
                    this.TemplateRegionParametersAllowedValueID = dr["TemplateRegionParametersAllowedValueID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionParametersAllowedValueID"]);
                    this.AllowedValue = dr["AllowedValue"] == DBNull.Value ? "" : dr["AllowedValue"].ToString().Trim();
                    this.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionParameterAllowedValueConstructor", Exc.Message, LogPath);
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

                SqlCommand cmd = new SqlCommand("spTemplateRegionParameterAllowedValueSave", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                #region Parameters
                // parameters for tblTemplateRegionParameterAllowedValues
                cmd.Parameters.Add(new SqlParameter("@TemplateRegionParameterID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionParameterID"].Value = this.TemplateRegionParameterID;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionParametersAllowedValueID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionParametersAllowedValueID"].Value = this.TemplateRegionParametersAllowedValueID;

                cmd.Parameters.Add(new SqlParameter("@AllowedValue", SqlDbType.VarChar, 500));
                cmd.Parameters["@AllowedValue"].Value = this.AllowedValue ?? "";

                // assign output param
                cmd.Parameters.Add(new SqlParameter("@TemplateRegionParameterAllowedValueIDOut", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionParameterAllowedValueIDOut"].Direction = ParameterDirection.Output;

                #endregion Parameters

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();

                int iTemplateRegionParameterAllowedValueID = Convert.ToInt32(cmd.Parameters["@TemplateRegionParameterAllowedValueIDOut"].Value);
                this.TemplateRegionParametersAllowedValueID = iTemplateRegionParameterAllowedValueID;

                oPR.ObjectProcessed = this;
                oPR.Result += "Saved";
                return (oPR);

            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionParameterAllowedValueSave", Exc.Message, LogPath);

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


        public static bool Delete(int TemplateRegionParameterAllowedValueID, string CnxnString, string LogPath)
        {
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionParameterAllowedValueDelete", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionParameterAllowedValueID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionParameterAllowedValueID"].Value = TemplateRegionParameterAllowedValueID;

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();
                return (true);
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionParameterAllowedValueDelete", Exc.Message, LogPath);
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
