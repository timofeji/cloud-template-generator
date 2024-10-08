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
    public class TemplateRegionResourcePropertyCopyInputFieldCollection : Dictionary<int, TemplateRegionResourcePropertyCopyInputField>
    {

        #region Constructors

        public TemplateRegionResourcePropertyCopyInputFieldCollection()
        {
        }

        public TemplateRegionResourcePropertyCopyInputFieldCollection(string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourcePropertyCopyInputFieldsFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateRegionResourcePropertyCopyInputField oTemplateRegionResourcePropertyCopyInputField = new TemplateRegionResourcePropertyCopyInputField();
                    oTemplateRegionResourcePropertyCopyInputField.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateRegionResourcePropertyCopyInputField.FieldValue = dr["FieldValue"] == DBNull.Value ? "" : dr["FieldValue"].ToString().Trim();
                    oTemplateRegionResourcePropertyCopyInputField.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    oTemplateRegionResourcePropertyCopyInputField.FieldName = dr["FieldName"] == DBNull.Value ? "" : dr["FieldName"].ToString().Trim();
                    oTemplateRegionResourcePropertyCopyInputField.TemplateRegionResourcePropertyCopyInputFieldID = dr["TemplateRegionResourcePropertyCopyInputFieldID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionResourcePropertyCopyInputFieldID"]);
                    oTemplateRegionResourcePropertyCopyInputField.TRRPCID = dr["TRRPCID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TRRPCID"]);
                    if (!this.ContainsKey(oTemplateRegionResourcePropertyCopyInputField.TemplateRegionResourcePropertyCopyInputFieldID))
                        this.Add(oTemplateRegionResourcePropertyCopyInputField.TemplateRegionResourcePropertyCopyInputFieldID, oTemplateRegionResourcePropertyCopyInputField);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourcePropertyCopyInputFieldCollectionConstructor", Exc.Message, LogPath);
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
                foreach (TemplateRegionResourcePropertyCopyInputField o in this.Values)
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
                Log.LogErr("TemplateRegionResourcePropertyCopyInputFieldCollection Save", Exc.Message, LogPath);
                oPR.Exception = Exc;
                return (oPR);
            }
        }
        #endregion Save

        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            foreach (TemplateRegionResourcePropertyCopyInputField f in this.Values)
            {
                sb.Append(f.Render());
            }
            int iLast = sb.ToString().LastIndexOf(",");
            sb.Remove(iLast, 1);

            return sb.ToString();
        }
    }


    [Serializable]
    public class TemplateRegionResourcePropertyCopyInputField
    {

        #region Vars

        string _CreatedDate;
        string _FieldName;
        int _TemplateRegionResourcePropertyCopyInputFieldID;
        int _TRRPCID;
        string _ModifiedDate;
        string _FieldValue;

        #endregion Vars

        #region Get/Sets

        public string CreatedDate
        {
            get { return (_CreatedDate); }
            set { _CreatedDate = value; }
        }

        public string FieldName
        {
            get { return (_FieldName); }
            set { _FieldName = value; }
        }

        public int TemplateRegionResourcePropertyCopyInputFieldID
        {
            get { return (_TemplateRegionResourcePropertyCopyInputFieldID); }
            set { _TemplateRegionResourcePropertyCopyInputFieldID = value; }
        }

        public int TRRPCID
        {
            get { return (_TRRPCID); }
            set { _TRRPCID = value; }
        }

        public string ModifiedDate
        {
            get { return (_ModifiedDate); }
            set { _ModifiedDate = value; }
        }

        public string FieldValue
        {
            get { return (_FieldValue); }
            set { _FieldValue = value; }
        }

        #endregion Get/Sets

        #region Constructors

        public TemplateRegionResourcePropertyCopyInputField()
        {
        }

        public TemplateRegionResourcePropertyCopyInputField(int TemplateRegionResourcePropertyCopyInputFieldID, string CnxnString, string LogPath)
        {
            // fill props from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourcePropertyCopyInputFieldInfoFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourcePropertyCopyInputFieldID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourcePropertyCopyInputFieldID"].Value = TemplateRegionResourcePropertyCopyInputFieldID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    this.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    this.FieldValue = dr["FieldValue"] == DBNull.Value ? "" : dr["FieldValue"].ToString().Trim();
                    this.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    this.FieldName = dr["FieldName"] == DBNull.Value ? "" : dr["FieldName"].ToString().Trim();
                    this.TemplateRegionResourcePropertyCopyInputFieldID = dr["TemplateRegionResourcePropertyCopyInputFieldID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionResourcePropertyCopyInputFieldID"]);
                    this.TRRPCID = dr["TRRPCID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TRRPCID"]);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourcePropertyCopyInputFieldConstructor", Exc.Message, LogPath);
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

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourcePropertyCopyInputFieldSave", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                #region Parameters
                // parameters for tblTemplateRegionResourcePropertyCopyInputFields
                cmd.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.VarChar, 50));
                cmd.Parameters["@CreatedDate"].Value = this.CreatedDate ?? "";

                cmd.Parameters.Add(new SqlParameter("@FieldValue", SqlDbType.VarChar, 500));
                cmd.Parameters["@FieldValue"].Value = this.FieldValue ?? "";

                cmd.Parameters.Add(new SqlParameter("@ModifiedDate", SqlDbType.VarChar, 50));
                cmd.Parameters["@ModifiedDate"].Value = this.ModifiedDate ?? "";

                cmd.Parameters.Add(new SqlParameter("@FieldName", SqlDbType.VarChar, 50));
                cmd.Parameters["@FieldName"].Value = this.FieldName ?? "";

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourcePropertyCopyInputFieldID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourcePropertyCopyInputFieldID"].Value = this.TemplateRegionResourcePropertyCopyInputFieldID;

                cmd.Parameters.Add(new SqlParameter("@TRRPCID", SqlDbType.Int));
                cmd.Parameters["@TRRPCID"].Value = this.TRRPCID;

                // assign output param
                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourcePropertyCopyInputFieldIDOut", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourcePropertyCopyInputFieldIDOut"].Direction = ParameterDirection.Output;

                #endregion Parameters

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();

                int iTemplateRegionResourcePropertyCopyInputFieldID = Convert.ToInt32(cmd.Parameters["@TemplateRegionResourcePropertyCopyInputFieldIDOut"].Value);
                this.TemplateRegionResourcePropertyCopyInputFieldID = iTemplateRegionResourcePropertyCopyInputFieldID;

                oPR.ObjectProcessed = this;
                oPR.Result += "Saved";
                return (oPR);

            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourcePropertyCopyInputFieldSave", Exc.Message, LogPath);

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


        public static bool Delete(int TemplateRegionResourcePropertyCopyInputFieldID, string CnxnString, string LogPath)
        {
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourcePropertyCopyInputFieldDelete", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourcePropertyCopyInputFieldID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourcePropertyCopyInputFieldID"].Value = TemplateRegionResourcePropertyCopyInputFieldID;

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();
                return (true);
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourcePropertyCopyInputFieldDelete", Exc.Message, LogPath);
                return (false);
            }
            finally
            {
                if (Cnxn.State == ConnectionState.Open) Cnxn.Close();
            }


        }
        #endregion Delete

        public string Render()
        {
            /*
   
                       "input": {}
             */
            StringBuilder sb = new StringBuilder();
            sb.Append(F.Tab + F.Tab + F.Space(8) + F.Quote + this.FieldName + F.Quote + F.Colon + F.Quote + this.FieldValue + F.Quote + F.Comma + Environment.NewLine);
            return (sb.ToString());
        }

        #region DummyData
        public static TemplateRegionResourcePropertyCopyInputField DummyData(TemplateRegionResourcePropertyCopy c)
        {
            TemplateRegionResourcePropertyCopyInputField p = new TemplateRegionResourcePropertyCopyInputField();
            p.FieldName = "fieldToCopy";
            p.FieldValue = "fieldValue";
            p.TemplateRegionResourcePropertyCopyInputFieldID = 1;
            p.TRRPCID = c.TemplateRegionResourcePropertyCopyID;
            p.CreatedDate = DateTime.Now.ToString();
            p.ModifiedDate = DateTime.Now.ToString();
            return (p);
        }
        #endregion DummyData
    }
}
