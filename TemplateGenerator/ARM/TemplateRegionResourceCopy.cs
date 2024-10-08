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
    public class TemplateRegionResourceCopyCollection : Dictionary<int, TemplateRegionResourceCopy>
    {

        #region Constructors

        public TemplateRegionResourceCopyCollection()
        {
        }

        public TemplateRegionResourceCopyCollection(string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourceCopysFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateRegionResourceCopy oTemplateRegionResourceCopy = new TemplateRegionResourceCopy();
                    oTemplateRegionResourceCopy.TemplateRegionResourceCopyID = dr["TemplateRegionResourceCopyID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionResourceCopyID"]);
                    oTemplateRegionResourceCopy.Count = dr["Count"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Count"]);
                    oTemplateRegionResourceCopy.PropertyName = dr["PropertyName"] == DBNull.Value ? "" : dr["PropertyName"].ToString().Trim();
                    oTemplateRegionResourceCopy.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    oTemplateRegionResourceCopy.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateRegionResourceCopy.TRRID = dr["TRRID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TRRID"]);
                    if (!this.ContainsKey(oTemplateRegionResourceCopy.TemplateRegionResourceCopyID))
                        this.Add(oTemplateRegionResourceCopy.TemplateRegionResourceCopyID, oTemplateRegionResourceCopy);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourceCopyCollectionConstructor", Exc.Message, LogPath);
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
                foreach (TemplateRegionResourceCopy o in this.Values)
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
                Log.LogErr("TemplateRegionResourceCopyCollection Save", Exc.Message, LogPath);
                oPR.Exception = Exc;
                return (oPR);
            }
        }
        #endregion Save
    }


    [Serializable]
    public class TemplateRegionResourceCopy
    {

        #region Vars

        string _PropertyName;
        string _ModifiedDate;
        int _Count;
        int _TemplateRegionResourceCopyID;
        string _CreatedDate;
        int _TRRID;

        #endregion Vars

        TemplateRegionResourceCopyInputFieldCollection _InputFields = new TemplateRegionResourceCopyInputFieldCollection();

        #region Get/Sets

        public string PropertyName
        {
            get { return (_PropertyName); }
            set { _PropertyName = value; }
        }

        public string ModifiedDate
        {
            get { return (_ModifiedDate); }
            set { _ModifiedDate = value; }
        }

        public int Count
        {
            get { return (_Count); }
            set { _Count = value; }
        }

        public int TemplateRegionResourceCopyID
        {
            get { return (_TemplateRegionResourceCopyID); }
            set { _TemplateRegionResourceCopyID = value; }
        }

        public string CreatedDate
        {
            get { return (_CreatedDate); }
            set { _CreatedDate = value; }
        }

        public int TRRID
        {
            get { return (_TRRID); }
            set { _TRRID = value; }
        }

        public TemplateRegionResourceCopyInputFieldCollection InputFields { get => _InputFields; set => _InputFields = value; }

        #endregion Get/Sets

        #region Constructors

        public TemplateRegionResourceCopy()
        {
        }

        public TemplateRegionResourceCopy(int TemplateRegionResourceCopyID, string CnxnString, string LogPath)
        {
            // fill props from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourceCopyInfoFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourceCopyID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourceCopyID"].Value = TemplateRegionResourceCopyID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    this.TemplateRegionResourceCopyID = dr["TemplateRegionResourceCopyID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionResourceCopyID"]);
                    this.Count = dr["Count"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Count"]);
                    this.PropertyName = dr["PropertyName"] == DBNull.Value ? "" : dr["PropertyName"].ToString().Trim();
                    this.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    this.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    this.TRRID = dr["TRRID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TRRID"]);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourceCopyConstructor", Exc.Message, LogPath);
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

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourceCopySave", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                #region Parameters
                // parameters for tblTemplateRegionResourceCopy
                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourceCopyID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourceCopyID"].Value = this.TemplateRegionResourceCopyID;

                cmd.Parameters.Add(new SqlParameter("@Count", SqlDbType.Int));
                cmd.Parameters["@Count"].Value = this.Count;

                cmd.Parameters.Add(new SqlParameter("@PropertyName", SqlDbType.VarChar, 50));
                cmd.Parameters["@PropertyName"].Value = this.PropertyName ?? "";

                cmd.Parameters.Add(new SqlParameter("@TRRID", SqlDbType.Int));
                cmd.Parameters["@TRRID"].Value = this.TRRID;

                // assign output param
                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourceCopyIDOut", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourceCopyIDOut"].Direction = ParameterDirection.Output;

                #endregion Parameters

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();

                int iTemplateRegionResourceCopyID = Convert.ToInt32(cmd.Parameters["@TemplateRegionResourceCopyIDOut"].Value);
                this.TemplateRegionResourceCopyID = iTemplateRegionResourceCopyID;

                oPR.ObjectProcessed = this;
                oPR.Result += "Saved";
                return (oPR);

            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourceCopySave", Exc.Message, LogPath);

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


        public static bool Delete(int TemplateRegionResourceCopyID, string CnxnString, string LogPath)
        {
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourceCopyDelete", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourceCopyID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourceCopyID"].Value = TemplateRegionResourceCopyID;

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();
                return (true);
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourceCopyDelete", Exc.Message, LogPath);
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
