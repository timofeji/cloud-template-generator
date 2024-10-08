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
    public class TemplateRegionResourcePropertyCopyCollection : Dictionary<int, TemplateRegionResourcePropertyCopy>
    {

        #region Constructors

        public TemplateRegionResourcePropertyCopyCollection()
        {
        }

        public TemplateRegionResourcePropertyCopyCollection(string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourcePropertyCopysFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateRegionResourcePropertyCopy oTemplateRegionResourcePropertyCopy = new TemplateRegionResourcePropertyCopy();
                    oTemplateRegionResourcePropertyCopy.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateRegionResourcePropertyCopy.TRRPID = dr["TRRPID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TRRPID"]);
                    oTemplateRegionResourcePropertyCopy.PropertyName = dr["PropertyName"] == DBNull.Value ? "" : dr["PropertyName"].ToString().Trim();
                    oTemplateRegionResourcePropertyCopy.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    oTemplateRegionResourcePropertyCopy.Count = dr["Count"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Count"]);
                    oTemplateRegionResourcePropertyCopy.TemplateRegionResourcePropertyCopyID = dr["TemplateRegionResourcePropertyCopyID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionResourcePropertyCopyID"]);
                    if (!this.ContainsKey(oTemplateRegionResourcePropertyCopy.TemplateRegionResourcePropertyCopyID))
                        this.Add(oTemplateRegionResourcePropertyCopy.TemplateRegionResourcePropertyCopyID, oTemplateRegionResourcePropertyCopy);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourcePropertyCopyCollectionConstructor", Exc.Message, LogPath);
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
                foreach (TemplateRegionResourcePropertyCopy o in this.Values)
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
                Log.LogErr("TemplateRegionResourcePropertyCopyCollection Save", Exc.Message, LogPath);
                oPR.Exception = Exc;
                return (oPR);
            }
        }
        #endregion Save

        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            foreach (TemplateRegionResourcePropertyCopy copy in this.Values)
            {
                sb.Append(F.Tab + F.Space(3) + copy.Render());
            }

            int iLast = sb.ToString().LastIndexOf(",");
            sb.Remove(iLast, 1);

            return sb.ToString();
        }
    }


    [Serializable]
    public class TemplateRegionResourcePropertyCopy
    {

        #region Vars

        string _CreatedDate;
        int _Count;
        string _PropertyName;
        int _TRRPID;
        string _ModifiedDate;
        int _TemplateRegionResourcePropertyCopyID;

        #endregion Vars

        TemplateRegionResourcePropertyCopyInputFieldCollection _InputFields = new TemplateRegionResourcePropertyCopyInputFieldCollection();

        #region Get/Sets

        public string CreatedDate
        {
            get { return (_CreatedDate); }
            set { _CreatedDate = value; }
        }

        public int Count
        {
            get { return (_Count); }
            set { _Count = value; }
        }

        public string PropertyName
        {
            get { return (_PropertyName); }
            set { _PropertyName = value; }
        }

        public int TRRPID
        {
            get { return (_TRRPID); }
            set { _TRRPID = value; }
        }

        public string ModifiedDate
        {
            get { return (_ModifiedDate); }
            set { _ModifiedDate = value; }
        }

        public int TemplateRegionResourcePropertyCopyID
        {
            get { return (_TemplateRegionResourcePropertyCopyID); }
            set { _TemplateRegionResourcePropertyCopyID = value; }
        }

        public TemplateRegionResourcePropertyCopyInputFieldCollection InputFields { get => _InputFields; set => _InputFields = value; }

        #endregion Get/Sets

        #region Constructors

        public TemplateRegionResourcePropertyCopy()
        {
        }

        public TemplateRegionResourcePropertyCopy(int TemplateRegionResourcePropertyCopyID, string CnxnString, string LogPath)
        {
            // fill props from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourcePropertyCopyInfoFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourcePropertyCopyID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourcePropertyCopyID"].Value = TemplateRegionResourcePropertyCopyID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    this.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    this.TRRPID = dr["TRRPID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TRRPID"]);
                    this.PropertyName = dr["PropertyName"] == DBNull.Value ? "" : dr["PropertyName"].ToString().Trim();
                    this.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    this.Count = dr["Count"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Count"]);
                    this.TemplateRegionResourcePropertyCopyID = dr["TemplateRegionResourcePropertyCopyID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionResourcePropertyCopyID"]);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourcePropertyCopyConstructor", Exc.Message, LogPath);
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

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourcePropertyCopySave", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                #region Parameters
                // parameters for tblTemplateRegionResourcePropertyCopy
                cmd.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.VarChar, 50));
                cmd.Parameters["@CreatedDate"].Value = this.CreatedDate ?? "";

                cmd.Parameters.Add(new SqlParameter("@TRRPID", SqlDbType.Int));
                cmd.Parameters["@TRRPID"].Value = this.TRRPID;

                cmd.Parameters.Add(new SqlParameter("@PropertyName", SqlDbType.VarChar, 50));
                cmd.Parameters["@PropertyName"].Value = this.PropertyName ?? "";

                cmd.Parameters.Add(new SqlParameter("@ModifiedDate", SqlDbType.VarChar, 50));
                cmd.Parameters["@ModifiedDate"].Value = this.ModifiedDate ?? "";

                cmd.Parameters.Add(new SqlParameter("@Count", SqlDbType.Int));
                cmd.Parameters["@Count"].Value = this.Count;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourcePropertyCopyID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourcePropertyCopyID"].Value = this.TemplateRegionResourcePropertyCopyID;

                // assign output param
                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourcePropertyCopyIDOut", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourcePropertyCopyIDOut"].Direction = ParameterDirection.Output;

                #endregion Parameters

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();

                int iTemplateRegionResourcePropertyCopyID = Convert.ToInt32(cmd.Parameters["@TemplateRegionResourcePropertyCopyIDOut"].Value);
                this.TemplateRegionResourcePropertyCopyID = iTemplateRegionResourcePropertyCopyID;

                oPR.ObjectProcessed = this;
                oPR.Result += "Saved";
                return (oPR);

            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourcePropertyCopySave", Exc.Message, LogPath);

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


        public static bool Delete(int TemplateRegionResourcePropertyCopyID, string CnxnString, string LogPath)
        {
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourcePropertyCopyDelete", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourcePropertyCopyID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourcePropertyCopyID"].Value = TemplateRegionResourcePropertyCopyID;

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();
                return (true);
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourcePropertyCopyDelete", Exc.Message, LogPath);
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
               "copy": [
                   {
                       "name": ,
                       "count": ,
                       "input": {}
                   }
               ]
             */
            StringBuilder sb = new StringBuilder();
            sb.Append(F.Tab + F.Tab + F.Space(6) + F.Quote + "name" + F.Quote + F.Colon + F.Quote + this.PropertyName + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Tab + F.Space(6) + F.Quote + "count" + F.Quote + F.Colon + F.Quote + this.Count + F.Quote + F.Comma + Environment.NewLine);
            if (this.InputFields.Count > 0)
            {
                sb.Append(F.Tab + F.Tab + F.Space(6) + F.Quote + "input" + F.Quote + F.Colon + F.CB + Environment.NewLine);
                sb.Append(this.InputFields.Render());
                sb.Append(F.Tab + F.Tab + F.Space(8) + F.ECB + F.Comma + Environment.NewLine);
            }

            int iLast = sb.ToString().LastIndexOf(",");
            sb.Remove(iLast, 1);

            return sb.ToString();
        }

        #region DummyData
        public static TemplateRegionResourcePropertyCopy DummyData(TemplateRegionResourceProperty prop)
        {
            TemplateRegionResourcePropertyCopy copy = new TemplateRegionResourcePropertyCopy();
            copy.PropertyName = "propertyToCopy";
            copy.TemplateRegionResourcePropertyCopyID = 1;
            copy.TRRPID = prop.TemplateRegionResourcePropertyID;
            copy.CreatedDate = DateTime.Now.ToString();
            copy.ModifiedDate = DateTime.Now.ToString();

            copy.InputFields.Add(TemplateRegionResourcePropertyCopyInputField.DummyData(copy).TemplateRegionResourcePropertyCopyInputFieldID, TemplateRegionResourcePropertyCopyInputField.DummyData(copy));
            return (copy);
        }
        #endregion DummyData
    }
}