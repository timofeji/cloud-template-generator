using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
namespace TemplateGenerator.ARM
{

    [Serializable]
    public class TemplateRegionResourceCopyInputFieldCollection : Dictionary<int, TemplateRegionResourceCopyInputField>
    {

        #region Constructors

        public TemplateRegionResourceCopyInputFieldCollection()
        {
        }

        public TemplateRegionResourceCopyInputFieldCollection(string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourceCopyInputFieldsFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateRegionResourceCopyInputField oTemplateRegionResourceCopyInputField = new TemplateRegionResourceCopyInputField();
                    oTemplateRegionResourceCopyInputField.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateRegionResourceCopyInputField.TRRCID = dr["TRRCID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TRRCID"]);
                    oTemplateRegionResourceCopyInputField.FieldValue = dr["FieldValue"] == DBNull.Value ? "" : dr["FieldValue"].ToString().Trim();
                    oTemplateRegionResourceCopyInputField.TemplateRegionResourceCopyInputFieldID = dr["TemplateRegionResourceCopyInputFieldID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionResourceCopyInputFieldID"]);
                    oTemplateRegionResourceCopyInputField.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    oTemplateRegionResourceCopyInputField.FieldName = dr["FieldName"] == DBNull.Value ? "" : dr["FieldName"].ToString().Trim();
                    if (!this.ContainsKey(oTemplateRegionResourceCopyInputField.TemplateRegionResourceCopyInputFieldID))
                        this.Add(oTemplateRegionResourceCopyInputField.TemplateRegionResourceCopyInputFieldID, oTemplateRegionResourceCopyInputField);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourceCopyInputFieldCollectionConstructor", Exc.Message, LogPath);
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
                foreach (TemplateRegionResourceCopyInputField o in this.Values)
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
                Log.LogErr("TemplateRegionResourceCopyInputFieldCollection Save", Exc.Message, LogPath);
                oPR.Exception = Exc;
                return (oPR);
            }
        }
        #endregion Save
    }


    [Serializable]
    public class TemplateRegionResourceCopyInputField
    {

        #region Vars

        int _TemplateRegionResourceCopyInputFieldID;
        string _ModifiedDate;
        string _CreatedDate;
        string _FieldValue;
        int _TRRCID;
        string _FieldName;

        #endregion Vars

        #region Get/Sets

        public int TemplateRegionResourceCopyInputFieldID
        {
            get { return (_TemplateRegionResourceCopyInputFieldID); }
            set { _TemplateRegionResourceCopyInputFieldID = value; }
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

        public string FieldValue
        {
            get { return (_FieldValue); }
            set { _FieldValue = value; }
        }

        public int TRRCID
        {
            get { return (_TRRCID); }
            set { _TRRCID = value; }
        }

        public string FieldName
        {
            get { return (_FieldName); }
            set { _FieldName = value; }
        }

        #endregion Get/Sets

        #region Constructors

        public TemplateRegionResourceCopyInputField()
        {
        }

        public TemplateRegionResourceCopyInputField(int TemplateRegionResourceCopyInputFieldID, string CnxnString, string LogPath)
        {
            // fill props from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourceCopyInputFieldInfoFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourceCopyInputFieldID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourceCopyInputFieldID"].Value = TemplateRegionResourceCopyInputFieldID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    this.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    this.TRRCID = dr["TRRCID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TRRCID"]);
                    this.FieldValue = dr["FieldValue"] == DBNull.Value ? "" : dr["FieldValue"].ToString().Trim();
                    this.TemplateRegionResourceCopyInputFieldID = dr["TemplateRegionResourceCopyInputFieldID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionResourceCopyInputFieldID"]);
                    this.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    this.FieldName = dr["FieldName"] == DBNull.Value ? "" : dr["FieldName"].ToString().Trim();
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourceCopyInputFieldConstructor", Exc.Message, LogPath);
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

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourceCopyInputFieldSave", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                #region Parameters
                // parameters for tblTemplateRegionResourceCopyInputFields

                cmd.Parameters.Add(new SqlParameter("@TRRCID", SqlDbType.Int));
                cmd.Parameters["@TRRCID"].Value = this.TRRCID;

                cmd.Parameters.Add(new SqlParameter("@FieldValue", SqlDbType.VarChar, 500));
                cmd.Parameters["@FieldValue"].Value = this.FieldValue ?? "";

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourceCopyInputFieldID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourceCopyInputFieldID"].Value = this.TemplateRegionResourceCopyInputFieldID;

                cmd.Parameters.Add(new SqlParameter("@FieldName", SqlDbType.VarChar, 50));
                cmd.Parameters["@FieldName"].Value = this.FieldName ?? "";

                // assign output param
                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourceCopyInputFieldIDOut", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourceCopyInputFieldIDOut"].Direction = ParameterDirection.Output;

                #endregion Parameters

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();

                int iTemplateRegionResourceCopyInputFieldID = Convert.ToInt32(cmd.Parameters["@TemplateRegionResourceCopyInputFieldIDOut"].Value);
                this.TemplateRegionResourceCopyInputFieldID = iTemplateRegionResourceCopyInputFieldID;

                oPR.ObjectProcessed = this;
                oPR.Result += "Saved";
                return (oPR);

            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourceCopyInputFieldSave", Exc.Message, LogPath);

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


        public static bool Delete(int TemplateRegionResourceCopyInputFieldID, string CnxnString, string LogPath)
        {
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourceCopyInputFieldDelete", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourceCopyInputFieldID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourceCopyInputFieldID"].Value = TemplateRegionResourceCopyInputFieldID;

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();
                return (true);
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourceCopyInputFieldDelete", Exc.Message, LogPath);
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
