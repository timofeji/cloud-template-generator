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
    public class TemplateRegionFunctionParameterCollection : Dictionary<int, TemplateRegionFunctionParameter>
    {

        #region Constructors

        public TemplateRegionFunctionParameterCollection()
        {
        }

        public TemplateRegionFunctionParameterCollection(string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionFunctionParametersFetchAll", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateRegionFunctionParameter oTemplateRegionFunctionParameter = new TemplateRegionFunctionParameter();
                    oTemplateRegionFunctionParameter.TemplateRegionFunctionParameterID = dr["TemplateRegionFunctionParameterID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionFunctionParameterID"]);
                    oTemplateRegionFunctionParameter.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateRegionFunctionParameter.ParameterName = dr["ParameterName"] == DBNull.Value ? "" : dr["ParameterName"].ToString().Trim();
                    oTemplateRegionFunctionParameter.TemplateRegionFunctionID = dr["TemplateRegionFunctionID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionFunctionID"]);
                    oTemplateRegionFunctionParameter.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    oTemplateRegionFunctionParameter.ParameterType = dr["ParameterType"] == DBNull.Value ? "" : dr["ParameterType"].ToString().Trim();
                    if (!this.ContainsKey(oTemplateRegionFunctionParameter.TemplateRegionFunctionParameterID))
                        this.Add(oTemplateRegionFunctionParameter.TemplateRegionFunctionParameterID, oTemplateRegionFunctionParameter);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionFunctionParameterCollectionConstructor", Exc.Message, LogPath);
            }
            finally
            {
                if (Cnxn.State == ConnectionState.Open) Cnxn.Close();
            }
        }
        public TemplateRegionFunctionParameterCollection(int TemplateID, string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionFunctionParametersFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateID", SqlDbType.Int));
                cmd.Parameters["@TemplateID"].Value = TemplateID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateRegionFunctionParameter oTemplateRegionFunctionParameter = new TemplateRegionFunctionParameter();
                    oTemplateRegionFunctionParameter.TemplateRegionFunctionParameterID = dr["TemplateRegionFunctionParameterID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionFunctionParameterID"]);
                    oTemplateRegionFunctionParameter.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateRegionFunctionParameter.ParameterName = dr["ParameterName"] == DBNull.Value ? "" : dr["ParameterName"].ToString().Trim();
                    oTemplateRegionFunctionParameter.TemplateRegionFunctionID = dr["TemplateRegionFunctionID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionFunctionID"]);
                    oTemplateRegionFunctionParameter.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    oTemplateRegionFunctionParameter.ParameterType = dr["ParameterType"] == DBNull.Value ? "" : dr["ParameterType"].ToString().Trim();
                    if (!this.ContainsKey(oTemplateRegionFunctionParameter.TemplateRegionFunctionParameterID))
                        this.Add(oTemplateRegionFunctionParameter.TemplateRegionFunctionParameterID, oTemplateRegionFunctionParameter);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionFunctionParameterCollectionConstructor", Exc.Message, LogPath);
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
                foreach (TemplateRegionFunctionParameter o in this.Values)
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
                Log.LogErr("TemplateRegionFunctionParameterCollection Save", Exc.Message, LogPath);
                oPR.Exception = Exc;
                return (oPR);
            }
        }
        #endregion Save
        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            foreach (TemplateRegionFunctionParameter p in this.Values)
            {
                sb.Append(p.Render());
            }
            return sb.ToString();
        }
    }


    [Serializable]
    public class TemplateRegionFunctionParameter
    {

        #region Vars

        string _CreatedDate;
        int _TemplateRegionFunctionParameterID;
        string _ParameterName;
        int _TemplateRegionFunctionID;
        string _ModifiedDate;
        string _ParameterType;

        #endregion Vars

        #region Get/Sets

        public string CreatedDate
        {
            get { return (_CreatedDate); }
            set { _CreatedDate = value; }
        }

        public int TemplateRegionFunctionParameterID
        {
            get { return (_TemplateRegionFunctionParameterID); }
            set { _TemplateRegionFunctionParameterID = value; }
        }

        public string ParameterName
        {
            get { return (_ParameterName); }
            set { _ParameterName = value; }
        }

        public int TemplateRegionFunctionID
        {
            get { return (_TemplateRegionFunctionID); }
            set { _TemplateRegionFunctionID = value; }
        }

        public string ModifiedDate
        {
            get { return (_ModifiedDate); }
            set { _ModifiedDate = value; }
        }

        public string ParameterType
        {
            get { return (_ParameterType); }
            set { _ParameterType = value; }
        }

        #endregion Get/Sets

        #region Constructors

        public TemplateRegionFunctionParameter()
        {
        }

        public TemplateRegionFunctionParameter(int TemplateRegionFunctionParameterID, string CnxnString, string LogPath)
        {
            // fill props from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionFunctionParameterInfoFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionFunctionParameterID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionFunctionParameterID"].Value = TemplateRegionFunctionParameterID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    this.TemplateRegionFunctionParameterID = dr["TemplateRegionFunctionParameterID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionFunctionParameterID"]);
                    this.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    this.ParameterName = dr["ParameterName"] == DBNull.Value ? "" : dr["ParameterName"].ToString().Trim();
                    this.TemplateRegionFunctionID = dr["TemplateRegionFunctionID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionFunctionID"]);
                    this.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    this.ParameterType = dr["ParameterType"] == DBNull.Value ? "" : dr["ParameterType"].ToString().Trim();
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionFunctionParameterConstructor", Exc.Message, LogPath);
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

                SqlCommand cmd = new SqlCommand("spTemplateRegionFunctionParameterSave", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                #region Parameters
                // parameters for tblTemplateRegionFunctionParameters
                cmd.Parameters.Add(new SqlParameter("@TemplateRegionFunctionParameterID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionFunctionParameterID"].Value = this.TemplateRegionFunctionParameterID;

                cmd.Parameters.Add(new SqlParameter("@ParameterName", SqlDbType.VarChar, 50));
                cmd.Parameters["@ParameterName"].Value = this.ParameterName ?? "";

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionFunctionID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionFunctionID"].Value = this.TemplateRegionFunctionID;

                cmd.Parameters.Add(new SqlParameter("@ParameterType", SqlDbType.VarChar, 500));
                cmd.Parameters["@ParameterType"].Value = this.ParameterType ?? "";

                // assign output param
                cmd.Parameters.Add(new SqlParameter("@TemplateRegionFunctionParameterIDOut", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionFunctionParameterIDOut"].Direction = ParameterDirection.Output;

                #endregion Parameters

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();

                int iTemplateRegionFunctionParameterID = Convert.ToInt32(cmd.Parameters["@TemplateRegionFunctionParameterIDOut"].Value);
                this.TemplateRegionFunctionParameterID = iTemplateRegionFunctionParameterID;

                oPR.ObjectProcessed = this;
                oPR.Result += "Saved";
                return (oPR);

            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionFunctionParameterSave", Exc.Message, LogPath);

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


        public static bool Delete(int TemplateRegionFunctionParameterID, string CnxnString, string LogPath)
        {
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionFunctionParameterDelete", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionFunctionParameterID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionFunctionParameterID"].Value = TemplateRegionFunctionParameterID;

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();
                return (true);
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionFunctionParameterDelete", Exc.Message, LogPath);
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
             *parameters": [
          {
            "name": "<parameter-name>",
            "type": "<type-of-parameter-value>"
          }
        ]
             */
            StringBuilder sb = new StringBuilder();
            sb.Append(F.Tab + F.Space(3) + F.CB + Environment.NewLine);

            sb.Append(F.Tab + F.Space(5) + F.Quote + "name" + F.Quote + this.ParameterName + F.Quote + F.Colon + F.CB + Environment.NewLine);
            sb.Append(F.Tab + F.Space(5) + F.Quote + "type" + F.Quote + F.Colon + F.Quote + this.ParameterType + F.Quote + Environment.NewLine);

            sb.Append(F.Tab + F.Space(3) + F.ECB + Environment.NewLine);
            return (sb.ToString());
        }

        #region DummyData
        public static TemplateRegionFunctionParameter DummyData(ARMTemplate ARMTemplate)
        {
            TemplateRegionFunctionParameter p = new TemplateRegionFunctionParameter();
            p.ParameterType = "string";
            p.ParameterType = ARMTemplate.ValidateParameterType(p.ParameterType) ? p.ParameterType : "invalid data type";
            p.ParameterName = "LastName";
            p.CreatedDate = DateTime.Now.ToString();
            p.ModifiedDate = DateTime.Now.ToString();
            return (p);
        }
        #endregion DummyData
    }
}
