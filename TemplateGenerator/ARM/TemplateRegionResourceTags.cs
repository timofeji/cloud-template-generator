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
    public class TemplateRegionResourceTagCollection : Dictionary<int, TemplateRegionResourceTag>
    {

        #region Constructors

        public TemplateRegionResourceTagCollection()
        {
        }

        public TemplateRegionResourceTagCollection(string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourceTagsFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateRegionResourceTag oTemplateRegionResourceTag = new TemplateRegionResourceTag();
                    oTemplateRegionResourceTag.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateRegionResourceTag.TemplateRegionResourceTagID = dr["TemplateRegionResourceTagID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionResourceTagID"]);
                    oTemplateRegionResourceTag.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    oTemplateRegionResourceTag.TagValue = dr["TagValue"] == DBNull.Value ? "" : dr["TagValue"].ToString().Trim();
                    oTemplateRegionResourceTag.TagName = dr["TagName"] == DBNull.Value ? "" : dr["TagName"].ToString().Trim();
                    oTemplateRegionResourceTag.TRRID = dr["TRRID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TRRID"]);
                    if (!this.ContainsKey(oTemplateRegionResourceTag.TemplateRegionResourceTagID))
                        this.Add(oTemplateRegionResourceTag.TemplateRegionResourceTagID, oTemplateRegionResourceTag);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourceTagCollectionConstructor", Exc.Message, LogPath);
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
                foreach (TemplateRegionResourceTag o in this.Values)
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
                Log.LogErr("TemplateRegionResourceTagCollection Save", Exc.Message, LogPath);
                oPR.Exception = Exc;
                return (oPR);
            }
        }
        #endregion Save

        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            foreach (TemplateRegionResourceTag t in this.Values)
            {
                sb.Append(t.Render());
            }
            int iLast = sb.ToString().LastIndexOf(",");
            sb.Remove(iLast, 1);

            return sb.ToString();
        }
    }


    [Serializable]
    public class TemplateRegionResourceTag
    {

        #region Vars

        string _ModifiedDate;
        string _CreatedDate;
        string _TagName;
        int _TemplateRegionResourceTagID;
        string _TagValue;
        int _TRRID;

        #endregion Vars

        #region Get/Sets

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

        public string TagName
        {
            get { return (_TagName); }
            set { _TagName = value; }
        }

        public int TemplateRegionResourceTagID
        {
            get { return (_TemplateRegionResourceTagID); }
            set { _TemplateRegionResourceTagID = value; }
        }

        public string TagValue
        {
            get { return (_TagValue); }
            set { _TagValue = value; }
        }

        public int TRRID
        {
            get { return (_TRRID); }
            set { _TRRID = value; }
        }

        #endregion Get/Sets

        #region Constructors

        public TemplateRegionResourceTag()
        {
        }

        public TemplateRegionResourceTag(int TemplateRegionResourceTagID, string CnxnString, string LogPath)
        {
            // fill props from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourceTagInfoFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourceTagID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourceTagID"].Value = TemplateRegionResourceTagID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    this.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    this.TemplateRegionResourceTagID = dr["TemplateRegionResourceTagID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionResourceTagID"]);
                    this.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    this.TagValue = dr["TagValue"] == DBNull.Value ? "" : dr["TagValue"].ToString().Trim();
                    this.TagName = dr["TagName"] == DBNull.Value ? "" : dr["TagName"].ToString().Trim();
                    this.TRRID = dr["TRRID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TRRID"]);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourceTagConstructor", Exc.Message, LogPath);
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

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourceTagSave", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                #region Parameters
                // parameters for tblTemplateRegionResourceTags
                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourceTagID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourceTagID"].Value = this.TemplateRegionResourceTagID;

                cmd.Parameters.Add(new SqlParameter("@TagValue", SqlDbType.VarChar, 500));
                cmd.Parameters["@TagValue"].Value = this.TagValue ?? "";

                cmd.Parameters.Add(new SqlParameter("@TagName", SqlDbType.VarChar, 50));
                cmd.Parameters["@TagName"].Value = this.TagName ?? "";

                cmd.Parameters.Add(new SqlParameter("@TRRID", SqlDbType.Int));
                cmd.Parameters["@TRRID"].Value = this.TRRID;

                // assign output param
                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourceTagIDOut", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourceTagIDOut"].Direction = ParameterDirection.Output;

                #endregion Parameters

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();

                int iTemplateRegionResourceTagID = Convert.ToInt32(cmd.Parameters["@TemplateRegionResourceTagIDOut"].Value);
                this.TemplateRegionResourceTagID = iTemplateRegionResourceTagID;

                oPR.ObjectProcessed = this;
                oPR.Result += "Saved";
                return (oPR);

            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourceTagSave", Exc.Message, LogPath);

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


        public static bool Delete(int TemplateRegionResourceTagID, string CnxnString, string LogPath)
        {
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourceTagDelete", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourceTagID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourceTagID"].Value = TemplateRegionResourceTagID;

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();
                return (true);
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourceTagDelete", Exc.Message, LogPath);
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
                     "tags": {
                  "<tag-name1>": "<tag-value1>",
                  "<tag-name2>": "<tag-value2>"
              },
             */
            StringBuilder sb = new StringBuilder();
            sb.Append(F.Tab + F.Space(6) + F.Quote + this.TagName + F.Quote + F.Colon + F.Quote + this.TagValue + F.Quote + F.Comma + Environment.NewLine);
            return (sb.ToString());
        }
        #region DummyData
        public static TemplateRegionResourceTag DummyData(TemplateRegionResource r, int x = 1)
        {
            TemplateRegionResourceTag p = new TemplateRegionResourceTag();
            p.TRRID = r.TemplateRegionResourceID;
            p.TemplateRegionResourceTagID = x;
            p.TagName = "Tag" + x.ToString();
            p.TagValue = "Value" + x.ToString();
            p.CreatedDate = DateTime.Now.ToString();
            p.ModifiedDate = DateTime.Now.ToString();
            return (p);
        }
        #endregion DummyData
    }
}