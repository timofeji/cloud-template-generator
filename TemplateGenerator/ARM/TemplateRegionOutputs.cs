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
    public class TemplateRegionOutputCollection : Dictionary<int, TemplateRegionOutput>
    {

        #region Constructors

        public TemplateRegionOutputCollection()
        {
        }

        public TemplateRegionOutputCollection(string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionOutputsFetchAll", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateRegionOutput oTemplateRegionOutput = new TemplateRegionOutput();
                    oTemplateRegionOutput.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateRegionOutput.Condition = dr["Condition"] == DBNull.Value ? "" : dr["Condition"].ToString().Trim();
                    oTemplateRegionOutput.OutputType = dr["OutputType"] == DBNull.Value ? "" : dr["OutputType"].ToString().Trim();
                    oTemplateRegionOutput.OutputName = dr["OutputName"] == DBNull.Value ? "" : dr["OutputName"].ToString().Trim();
                    oTemplateRegionOutput.OutputCopyCount = dr["OutputCopyCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["OutputCopyCount"]);
                    oTemplateRegionOutput.OutputCopyInput = dr["OutputCopyInput"] == DBNull.Value ? "" : dr["OutputCopyInput"].ToString().Trim();
                    oTemplateRegionOutput.TemplateID = dr["TemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateID"]);
                    oTemplateRegionOutput.OutputValue = dr["OutputValue"] == DBNull.Value ? "" : dr["OutputValue"].ToString().Trim();
                    oTemplateRegionOutput.TemplateRegionOutputID = dr["TemplateRegionOutputID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionOutputID"]);
                    oTemplateRegionOutput.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    if (!this.ContainsKey(oTemplateRegionOutput.TemplateRegionOutputID))
                        this.Add(oTemplateRegionOutput.TemplateRegionOutputID, oTemplateRegionOutput);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionOutputCollectionConstructor", Exc.Message, LogPath);
            }
            finally
            {
                if (Cnxn.State == ConnectionState.Open) Cnxn.Close();
            }
        }
        public TemplateRegionOutputCollection(int TemplateID, string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionOutputsFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateID", SqlDbType.Int));
                cmd.Parameters["@TemplateID"].Value = TemplateID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateRegionOutput oTemplateRegionOutput = new TemplateRegionOutput();
                    oTemplateRegionOutput.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateRegionOutput.Condition = dr["Condition"] == DBNull.Value ? "" : dr["Condition"].ToString().Trim();
                    oTemplateRegionOutput.OutputType = dr["OutputType"] == DBNull.Value ? "" : dr["OutputType"].ToString().Trim();
                    oTemplateRegionOutput.OutputName = dr["OutputName"] == DBNull.Value ? "" : dr["OutputName"].ToString().Trim();
                    oTemplateRegionOutput.OutputCopyCount = dr["OutputCopyCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["OutputCopyCount"]);
                    oTemplateRegionOutput.OutputCopyInput = dr["OutputCopyInput"] == DBNull.Value ? "" : dr["OutputCopyInput"].ToString().Trim();
                    oTemplateRegionOutput.TemplateID = dr["TemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateID"]);
                    oTemplateRegionOutput.OutputValue = dr["OutputValue"] == DBNull.Value ? "" : dr["OutputValue"].ToString().Trim();
                    oTemplateRegionOutput.TemplateRegionOutputID = dr["TemplateRegionOutputID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionOutputID"]);
                    oTemplateRegionOutput.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    if (!this.ContainsKey(oTemplateRegionOutput.TemplateRegionOutputID))
                        this.Add(oTemplateRegionOutput.TemplateRegionOutputID, oTemplateRegionOutput);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionOutputCollectionConstructor", Exc.Message, LogPath);
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
                foreach (TemplateRegionOutput o in this.Values)
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
                Log.LogErr("TemplateRegionOutputCollection Save", Exc.Message, LogPath);
                oPR.Exception = Exc;
                return (oPR);
            }
        }
        #endregion Save
    }


    [Serializable]
    public class TemplateRegionOutput : TemplateRegion
    {

        #region Vars

        string _OutputName;
        int _TemplateID;
        string _CreatedDate;
        string _Condition;
        int _OutputCopyCount;
        string _OutputCopyInput;
        string _OutputType;
        string _ModifiedDate;
        string _OutputValue;
        int _TemplateRegionOutputID;

        #endregion Vars

        #region Get/Sets

        public string OutputName
        {
            get { return (_OutputName); }
            set { _OutputName = value; }
        }

        public int TemplateID
        {
            get { return (_TemplateID); }
            set { _TemplateID = value; }
        }

        public string CreatedDate
        {
            get { return (_CreatedDate); }
            set { _CreatedDate = value; }
        }

        public string Condition
        {
            get { return (_Condition); }
            set { _Condition = value; }
        }

        public int OutputCopyCount
        {
            get { return (_OutputCopyCount); }
            set { _OutputCopyCount = value; }
        }

        public string OutputCopyInput
        {
            get { return (_OutputCopyInput); }
            set { _OutputCopyInput = value; }
        }

        public string OutputType
        {
            get { return (_OutputType); }
            set { _OutputType = value; }
        }

        public string ModifiedDate
        {
            get { return (_ModifiedDate); }
            set { _ModifiedDate = value; }
        }

        public string OutputValue
        {
            get { return (_OutputValue); }
            set { _OutputValue = value; }
        }

        public int TemplateRegionOutputID
        {
            get { return (_TemplateRegionOutputID); }
            set { _TemplateRegionOutputID = value; }
        }

        #endregion Get/Sets

        #region Constructors

        public TemplateRegionOutput()
        {
            this.RegionType = "outputs";
        }

        public TemplateRegionOutput(int TemplateRegionOutputID, string CnxnString, string LogPath)
        {
            this.RegionType = "outputs";
            // fill props from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionOutputInfoFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionOutputID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionOutputID"].Value = TemplateRegionOutputID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    this.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    this.Condition = dr["Condition"] == DBNull.Value ? "" : dr["Condition"].ToString().Trim();
                    this.OutputType = dr["OutputType"] == DBNull.Value ? "" : dr["OutputType"].ToString().Trim();
                    this.OutputName = dr["OutputName"] == DBNull.Value ? "" : dr["OutputName"].ToString().Trim();
                    this.OutputCopyCount = dr["OutputCopyCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["OutputCopyCount"]);
                    this.OutputCopyInput = dr["OutputCopyInput"] == DBNull.Value ? "" : dr["OutputCopyInput"].ToString().Trim();
                    this.TemplateID = dr["TemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateID"]);
                    this.OutputValue = dr["OutputValue"] == DBNull.Value ? "" : dr["OutputValue"].ToString().Trim();
                    this.TemplateRegionOutputID = dr["TemplateRegionOutputID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionOutputID"]);
                    this.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionOutputConstructor", Exc.Message, LogPath);
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

                SqlCommand cmd = new SqlCommand("spTemplateRegionOutputSave", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                #region Parameters
                // parameters for tblTemplateRegionOutputs
                cmd.Parameters.Add(new SqlParameter("@Condition", SqlDbType.VarChar, 50));
                cmd.Parameters["@Condition"].Value = this.Condition ?? "";

                cmd.Parameters.Add(new SqlParameter("@OutputType", SqlDbType.VarChar, 50));
                cmd.Parameters["@OutputType"].Value = this.OutputType ?? "";

                cmd.Parameters.Add(new SqlParameter("@OutputName", SqlDbType.VarChar, 50));
                cmd.Parameters["@OutputName"].Value = this.OutputName ?? "";

                cmd.Parameters.Add(new SqlParameter("@OutputCopyCount", SqlDbType.VarChar, 50));
                cmd.Parameters["@OutputCopyCount"].Value = this.OutputCopyCount;

                cmd.Parameters.Add(new SqlParameter("@OutputCopyInput", SqlDbType.VarChar, 50));
                cmd.Parameters["@OutputCopyInput"].Value = this.OutputCopyInput ?? "";

                cmd.Parameters.Add(new SqlParameter("@TemplateID", SqlDbType.Int));
                cmd.Parameters["@TemplateID"].Value = this.TemplateID;

                cmd.Parameters.Add(new SqlParameter("@OutputValue", SqlDbType.VarChar, 50));
                cmd.Parameters["@OutputValue"].Value = this.OutputValue ?? "";

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionOutputID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionOutputID"].Value = this.TemplateRegionOutputID;

                // assign output param
                cmd.Parameters.Add(new SqlParameter("@TemplateRegionOutputIDOut", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionOutputIDOut"].Direction = ParameterDirection.Output;

                #endregion Parameters

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();

                int iTemplateRegionOutputID = Convert.ToInt32(cmd.Parameters["@TemplateRegionOutputIDOut"].Value);
                this.TemplateRegionOutputID = iTemplateRegionOutputID;

                oPR.ObjectProcessed = this;
                oPR.Result += "Saved";
                return (oPR);

            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionOutputSave", Exc.Message, LogPath);

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


        public static bool Delete(int TemplateRegionOutputID, string CnxnString, string LogPath)
        {
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionOutputDelete", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionOutputID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionOutputID"].Value = TemplateRegionOutputID;

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();
                return (true);
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionOutputDelete", Exc.Message, LogPath);
                return (false);
            }
            finally
            {
                if (Cnxn.State == ConnectionState.Open) Cnxn.Close();
            }


        }
        #endregion Delete
        public override string Render()
        {
            /*
             "outputs": {
              "<output-name>": {
                "condition": "<boolean-value-whether-to-output-value>",
                "type": "<type-of-output-value>",
                "value": "<output-value-expression>",
                "copy": {
                  "count": <number-of-iterations>,
                  "input": <values-for-the-variable>
                }
              }
            }
             */
            StringBuilder sb = new StringBuilder();
            sb.Append(F.Tab + F.Space(1) + F.Quote + this.OutputName + F.Quote + F.Colon + F.CB + Environment.NewLine);

            sb.Append(F.Tab + F.Space(3) + F.Quote + "condition" + F.Quote + F.Colon +
                F.Quote + this.Condition + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(3) + F.Quote + "type" + F.Quote + F.Colon +
                F.Quote + this.OutputType + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(3) + F.Quote + "value" + F.Quote + F.Colon +
                F.Quote + this.OutputValue + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(3) + F.Quote + "copy" + F.Quote + F.Colon + F.CB + Environment.NewLine);
            sb.Append(F.Tab + F.Space(5) + F.Quote + "count" + F.Quote + F.Colon +
                F.Quote + this.OutputCopyCount + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(5) + F.Quote + "input" + F.Quote + F.Colon +
                F.Quote + this.OutputCopyInput + F.Quote + Environment.NewLine);
            sb.Append(F.Tab + F.Space(3) + F.ECB + Environment.NewLine);

            sb.Append(F.Space(6) + F.ECB + Environment.NewLine);
            return (sb.ToString());
        }

        #region DummyData
        public static TemplateRegionOutput DummyData()
        {
            TemplateRegionOutput p = new TemplateRegionOutput();
            p.OutputName = "OooName";
            p.OutputType = "string";
            p.OutputValue = "deployed";
            p.OutputCopyCount = 1;
            p.OutputCopyInput = "Not an array?";
            p.CreatedDate = DateTime.Now.ToString();
            p.ModifiedDate = DateTime.Now.ToString();
            return (p);
        }
        #endregion DummyData

    }
}