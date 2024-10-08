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
    public class TemplateRegionFunctionCollection : Dictionary<int, TemplateRegionFunction>
    {

        #region Constructors

        public TemplateRegionFunctionCollection()
        {
        }
        public TemplateRegionFunctionCollection(string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionFunctionsFetchAll", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateRegionFunction oTemplateRegionFunction = new TemplateRegionFunction();
                    oTemplateRegionFunction.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateRegionFunction.OutputType = dr["OutputType"] == DBNull.Value ? "" : dr["OutputType"].ToString().Trim();
                    oTemplateRegionFunction.TemplateID = dr["TemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateID"]);
                    oTemplateRegionFunction.TemplateRegionFunctionID = dr["TemplateRegionFunctionID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionFunctionID"]);
                    oTemplateRegionFunction.OutputValue = dr["OutputValue"] == DBNull.Value ? "" : dr["OutputValue"].ToString().Trim();
                    oTemplateRegionFunction.FunctionName = dr["FunctionName"] == DBNull.Value ? "" : dr["FunctionName"].ToString().Trim();
                    oTemplateRegionFunction.FunctionNamespace = dr["FunctionNamespace"] == DBNull.Value ? "" : dr["FunctionNamespace"].ToString().Trim();
                    oTemplateRegionFunction.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    if (!this.ContainsKey(oTemplateRegionFunction.TemplateRegionFunctionID))
                        this.Add(oTemplateRegionFunction.TemplateRegionFunctionID, oTemplateRegionFunction);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionFunctionCollectionConstructor", Exc.Message, LogPath);
            }
            finally
            {
                if (Cnxn.State == ConnectionState.Open) Cnxn.Close();
            }
        }
        public TemplateRegionFunctionCollection(int TemplateID, string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionFunctionsFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateID", SqlDbType.Int));
                cmd.Parameters["@TemplateID"].Value = TemplateID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateRegionFunction oTemplateRegionFunction = new TemplateRegionFunction();
                    oTemplateRegionFunction.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateRegionFunction.OutputType = dr["OutputType"] == DBNull.Value ? "" : dr["OutputType"].ToString().Trim();
                    oTemplateRegionFunction.TemplateID = dr["TemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateID"]);
                    oTemplateRegionFunction.TemplateRegionFunctionID = dr["TemplateRegionFunctionID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionFunctionID"]);
                    oTemplateRegionFunction.OutputValue = dr["OutputValue"] == DBNull.Value ? "" : dr["OutputValue"].ToString().Trim();
                    oTemplateRegionFunction.FunctionName = dr["FunctionName"] == DBNull.Value ? "" : dr["FunctionName"].ToString().Trim();
                    oTemplateRegionFunction.FunctionNamespace = dr["FunctionNamespace"] == DBNull.Value ? "" : dr["FunctionNamespace"].ToString().Trim();
                    oTemplateRegionFunction.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    if (!this.ContainsKey(oTemplateRegionFunction.TemplateRegionFunctionID))
                        this.Add(oTemplateRegionFunction.TemplateRegionFunctionID, oTemplateRegionFunction);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionFunctionCollectionConstructor", Exc.Message, LogPath);
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
                foreach (TemplateRegionFunction o in this.Values)
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
                Log.LogErr("TemplateRegionFunctionCollection Save", Exc.Message, LogPath);
                oPR.Exception = Exc;
                return (oPR);
            }
        }
        #endregion Save

        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            foreach (TemplateRegionFunction f in this.Values)
            {
                sb.Append(f.Render());
            }
            return sb.ToString();
        }
    }

    [Serializable]
    public class TemplateRegionFunction : TemplateRegion
    {

        #region Vars

        int _TemplateID;
        string _FunctionName;
        string _CreatedDate;
        string _ModifiedDate;
        string _OutputType;
        int _TemplateRegionFunctionID;
        string _FunctionNamespace;
        string _OutputValue;

        #endregion Vars
        List<TemplateRegionFunctionParameter> parameters = new List<TemplateRegionFunctionParameter>();

        #region Get/Sets

        public int TemplateID
        {
            get { return (_TemplateID); }
            set { _TemplateID = value; }
        }

        public string FunctionName
        {
            get { return (_FunctionName); }
            set { _FunctionName = value; }
        }

        public string CreatedDate
        {
            get { return (_CreatedDate); }
            set { _CreatedDate = value; }
        }

        public string ModifiedDate
        {
            get { return (_ModifiedDate); }
            set { _ModifiedDate = value; }
        }

        public string OutputType
        {
            get { return (_OutputType); }
            set { _OutputType = value; }
        }

        public int TemplateRegionFunctionID
        {
            get { return (_TemplateRegionFunctionID); }
            set { _TemplateRegionFunctionID = value; }
        }

        public string FunctionNamespace
        {
            get { return (_FunctionNamespace); }
            set { _FunctionNamespace = value; }
        }

        public string OutputValue
        {
            get { return (_OutputValue); }
            set { _OutputValue = value; }
        }

        public List<TemplateRegionFunctionParameter> Parameters { get => parameters; set => parameters = value; }

        #endregion Get/Sets

        #region Constructors

        public TemplateRegionFunction()
        {
            this.RegionType = "functions";
        }

        public TemplateRegionFunction(int TemplateRegionFunctionID, string CnxnString, string LogPath)
        {
            this.RegionType = "functions";
            // fill props from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionFunctionsFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionFunctionID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionFunctionID"].Value = TemplateRegionFunctionID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    this.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    this.OutputType = dr["OutputType"] == DBNull.Value ? "" : dr["OutputType"].ToString().Trim();
                    this.TemplateID = dr["TemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateID"]);
                    this.TemplateRegionFunctionID = dr["TemplateRegionFunctionID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionFunctionID"]);
                    this.OutputValue = dr["OutputValue"] == DBNull.Value ? "" : dr["OutputValue"].ToString().Trim();
                    this.FunctionName = dr["FunctionName"] == DBNull.Value ? "" : dr["FunctionName"].ToString().Trim();
                    this.FunctionNamespace = dr["FunctionNamespace"] == DBNull.Value ? "" : dr["FunctionNamespace"].ToString().Trim();
                    this.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionFunctionConstructor", Exc.Message, LogPath);
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

                SqlCommand cmd = new SqlCommand("spTemplateRegionFunctionSave", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                #region Parameters
                // parameters for tblTemplateRegionFunctions
                cmd.Parameters.Add(new SqlParameter("@OutputType", SqlDbType.VarChar, 50));
                cmd.Parameters["@OutputType"].Value = this.OutputType ?? "";

                cmd.Parameters.Add(new SqlParameter("@TemplateID", SqlDbType.Int));
                cmd.Parameters["@TemplateID"].Value = this.TemplateID;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionFunctionID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionFunctionID"].Value = this.TemplateRegionFunctionID;

                cmd.Parameters.Add(new SqlParameter("@OutputValue", SqlDbType.VarChar, 5000));
                cmd.Parameters["@OutputValue"].Value = this.OutputValue ?? "";

                cmd.Parameters.Add(new SqlParameter("@FunctionName", SqlDbType.VarChar, 50));
                cmd.Parameters["@FunctionName"].Value = this.FunctionName ?? "";

                cmd.Parameters.Add(new SqlParameter("@FunctionNamespace", SqlDbType.VarChar, 500));
                cmd.Parameters["@FunctionNamespace"].Value = this.FunctionNamespace ?? "";

                // assign output param
                cmd.Parameters.Add(new SqlParameter("@TemplateRegionFunctionIDOut", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionFunctionIDOut"].Direction = ParameterDirection.Output;

                #endregion Parameters

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();

                int iTemplateRegionFunctionID = Convert.ToInt32(cmd.Parameters["@TemplateRegionFunctionIDOut"].Value);
                this.TemplateRegionFunctionID = iTemplateRegionFunctionID;

                oPR.ObjectProcessed = this;
                oPR.Result += "Saved";
                return (oPR);

            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionFunctionSave", Exc.Message, LogPath);

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


        public static bool Delete(int TemplateRegionFunctionID, string CnxnString, string LogPath)
        {
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionFunctionDelete", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionFunctionID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionFunctionID"].Value = TemplateRegionFunctionID;

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();
                return (true);
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionFunctionDelete", Exc.Message, LogPath);
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
             "functions": [
              {
                "namespace": "<namespace-for-functions>",
                "members": {
                  "<function-name>": {
                    "parameters": [
                      {
                        "name": "<parameter-name>",
                        "type": "<type-of-parameter-value>"
                      }
                    ],
                    "output": {
                      "type": "<type-of-output-value>",
                      "value": "<function-return-value>"
                    }
                  }
                }
              }
            ]
             */
            StringBuilder sb = new StringBuilder();
            sb.Append(F.Tab + F.Space(3) + F.Quote + this.Name + F.Quote + F.Colon + F.CB + Environment.NewLine);

            sb.Append(F.Tab + F.Space(4) + F.Quote + "parameters" + F.Quote + F.Colon + F.SqB + Environment.NewLine);

            int index = 1;
            foreach (TemplateRegionFunctionParameter p in this.Parameters)
            {
                sb.Append(F.Tab + F.Space(5) + F.CB + Environment.NewLine);
                sb.Append(F.Tab + F.Space(6) + F.Quote + "name" + F.Quote + F.Colon + F.Quote + p.ParameterName + F.Quote + F.Comma + Environment.NewLine);
                sb.Append(F.Tab + F.Space(6) + F.Quote + "type" + F.Quote + F.Colon + F.Quote + p.ParameterType + F.Quote + Environment.NewLine);
                sb.Append(F.Tab + F.Space(5) + F.ECB);
                if (index < this.Parameters.Count)
                    sb.Append(",");
                sb.Append(Environment.NewLine);
                index++;
            }
            sb.Append(F.Tab + F.Space(4) + F.ESqB + F.Comma + Environment.NewLine);

            sb.Append(F.Tab + F.Space(4) + F.Quote + "output" + F.Quote + F.Colon + F.CB + Environment.NewLine);
            sb.Append(F.Tab + F.Space(5) + F.Quote + "type" + F.Quote + F.Colon + F.Quote + this.OutputType + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(5) + F.Quote + "value" + F.Quote + F.Colon + F.Quote + this.OutputValue + F.Quote + Environment.NewLine);
            sb.Append(F.Tab + F.Space(4) + F.ECB + Environment.NewLine);

            sb.Append(F.Tab + F.Space(3) + F.ECB + Environment.NewLine);
            return (sb.ToString());
        }

        #region DummyData
        public static TemplateRegionFunction DummyData(ARMTemplate ARMTemplate)
        {
            TemplateRegionFunction f = new TemplateRegionFunction();
            f.Name = "FunctionX";
            f.OutputType = "string";
            f.OutputValue = "SMITH&JONES";
            f.Parameters.Add(TemplateRegionFunctionParameter.DummyData(ARMTemplate));
            f.CreatedDate = DateTime.Now.ToString();
            f.ModifiedDate = DateTime.Now.ToString();
            return (f);
        }
        #endregion DummyData
    }
}