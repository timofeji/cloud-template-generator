using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using TemplateGenerator.ARM;
using System.Text;
namespace TemplateGenerator.ARM
{

    [Serializable]
    public class TemplateRegionParameterCollection : Dictionary<int, TemplateRegionParameter>
    {

        #region Constructors

        public TemplateRegionParameterCollection()
        {
        }

        public TemplateRegionParameterCollection(string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionParametersFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateRegionParameter oTemplateRegionParameter = new TemplateRegionParameter();
                    oTemplateRegionParameter.MaxValue = dr["MaxValue"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MaxValue"]);
                    oTemplateRegionParameter.MinLength = dr["MinLength"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MinLength"]);
                    oTemplateRegionParameter.MaxLength = dr["MaxLength"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MaxLength"]);
                    oTemplateRegionParameter.ParameterName = dr["ParameterName"] == DBNull.Value ? "" : dr["ParameterName"].ToString().Trim();
                    oTemplateRegionParameter.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateRegionParameter.Description = dr["Description"] == DBNull.Value ? "" : dr["Description"].ToString().Trim();
                    oTemplateRegionParameter.TemplateID = dr["TemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateID"]);
                    oTemplateRegionParameter.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    oTemplateRegionParameter.MinValue = dr["MinValue"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MinValue"]);
                    oTemplateRegionParameter.TemplateRegionParameterID = dr["TemplateRegionParameterID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionParameterID"]);
                    oTemplateRegionParameter.DefaultValue = dr["DefaultValue"] == DBNull.Value ? "" : dr["DefaultValue"].ToString().Trim();
                    oTemplateRegionParameter.DataType = dr["DataType"] == DBNull.Value ? "" : dr["DataType"].ToString().Trim();
                    if (!this.ContainsKey(oTemplateRegionParameter.TemplateRegionParameterID))
                        this.Add(oTemplateRegionParameter.TemplateRegionParameterID, oTemplateRegionParameter);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionParameterCollectionConstructor", Exc.Message, LogPath);
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
                foreach (TemplateRegionParameter o in this.Values)
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
                Log.LogErr("TemplateRegionParameterCollection Save", Exc.Message, LogPath);
                oPR.Exception = Exc;
                return (oPR);
            }
        }
        #endregion Save
        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            foreach (TemplateRegionParameter p in this.Values)
            {
                sb.Append(p.Render());
            }

            int iLast = sb.ToString().LastIndexOf(",");
            sb.Remove(iLast, 1);

            return sb.ToString();
        }
    }


    [Serializable]
    public class TemplateRegionParameter : TemplateRegion
    {

        #region Vars

        int _MinLength;
        string _DataType;
        int _TemplateID;
        int _TemplateRegionParameterID;
        int _MinValue;
        int _MaxValue;
        int _MaxLength;
        string _DefaultValue;
        string _ParameterName;
        string _ModifiedDate;
        string _CreatedDate;
        string _Description;

        #endregion Vars
        List<TemplateRegionParameterAllowedValue> _AllowedValues = new List<TemplateRegionParameterAllowedValue>();

        #region Get/Sets

        public int MinLength
        {
            get { return (_MinLength); }
            set { _MinLength = value; }
        }

        public string DataType
        {
            get { return (_DataType); }
            set { _DataType = value; }
        }

        public int TemplateID
        {
            get { return (_TemplateID); }
            set { _TemplateID = value; }
        }

        public int TemplateRegionParameterID
        {
            get { return (_TemplateRegionParameterID); }
            set { _TemplateRegionParameterID = value; }
        }

        public int MinValue
        {
            get { return (_MinValue); }
            set { _MinValue = value; }
        }

        public int MaxValue
        {
            get { return (_MaxValue); }
            set { _MaxValue = value; }
        }

        public int MaxLength
        {
            get { return (_MaxLength); }
            set { _MaxLength = value; }
        }

        public string DefaultValue
        {
            get { return (_DefaultValue); }
            set { _DefaultValue = value; }
        }

        public string ParameterName
        {
            get { return (_ParameterName); }
            set { _ParameterName = value; }
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

        public string Description
        {
            get { return (_Description); }
            set { _Description = value; }
        }

        public List<TemplateRegionParameterAllowedValue> AllowedValues { get => _AllowedValues; set => _AllowedValues = value; }

        #endregion Get/Sets

        #region Constructors

        public TemplateRegionParameter()
        {
            this.RegionType = "Parameters";
        }

        public TemplateRegionParameter(int TemplateID, string CnxnString, string LogPath)
        {
            this.RegionType = "Parameters";
            // fill props from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionParametersFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateID", SqlDbType.Int));
                cmd.Parameters["@TemplateID"].Value = TemplateRegionParameterID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    this.MaxValue = dr["MaxValue"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MaxValue"]);
                    this.MinLength = dr["MinLength"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MinLength"]);
                    this.MaxLength = dr["MaxLength"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MaxLength"]);
                    this.ParameterName = dr["ParameterName"] == DBNull.Value ? "" : dr["ParameterName"].ToString().Trim();
                    this.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    this.Description = dr["Description"] == DBNull.Value ? "" : dr["Description"].ToString().Trim();
                    this.TemplateID = dr["TemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateID"]);
                    this.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    this.MinValue = dr["MinValue"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MinValue"]);
                    this.TemplateRegionParameterID = dr["TemplateRegionParameterID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionParameterID"]);
                    this.DefaultValue = dr["DefaultValue"] == DBNull.Value ? "" : dr["DefaultValue"].ToString().Trim();
                    this.DataType = dr["DataType"] == DBNull.Value ? "" : dr["DataType"].ToString().Trim();
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionParameterConstructor", Exc.Message, LogPath);
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

                SqlCommand cmd = new SqlCommand("spTemplateRegionParameterSave", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                #region Parameters
                // parameters for tblTemplateRegionParameters
                cmd.Parameters.Add(new SqlParameter("@MaxValue", SqlDbType.Int));
                cmd.Parameters["@MaxValue"].Value = this.MaxValue;

                cmd.Parameters.Add(new SqlParameter("@MinLength", SqlDbType.Int));
                cmd.Parameters["@MinLength"].Value = this.MinLength;

                cmd.Parameters.Add(new SqlParameter("@MaxLength", SqlDbType.Int));
                cmd.Parameters["@MaxLength"].Value = this.MaxLength;

                cmd.Parameters.Add(new SqlParameter("@ParameterName", SqlDbType.VarChar, 50));
                cmd.Parameters["@ParameterName"].Value = this.ParameterName ?? "";

                cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 500));
                cmd.Parameters["@Description"].Value = this.Description ?? "";

                cmd.Parameters.Add(new SqlParameter("@TemplateID", SqlDbType.Int));
                cmd.Parameters["@TemplateID"].Value = this.TemplateID;

                cmd.Parameters.Add(new SqlParameter("@MinValue", SqlDbType.Int));
                cmd.Parameters["@MinValue"].Value = this.MinValue;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionParameterID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionParameterID"].Value = this.TemplateRegionParameterID;

                cmd.Parameters.Add(new SqlParameter("@DefaultValue", SqlDbType.VarChar, 50));
                cmd.Parameters["@DefaultValue"].Value = this.DefaultValue ?? "";

                cmd.Parameters.Add(new SqlParameter("@DataType", SqlDbType.VarChar, 50));
                cmd.Parameters["@DataType"].Value = this.DataType ?? "";

                // assign output param
                cmd.Parameters.Add(new SqlParameter("@TemplateRegionParameterIDOut", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionParameterIDOut"].Direction = ParameterDirection.Output;

                #endregion Parameters

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();

                int iTemplateRegionParameterID = Convert.ToInt32(cmd.Parameters["@TemplateRegionParameterIDOut"].Value);
                this.TemplateRegionParameterID = iTemplateRegionParameterID;

                oPR.ObjectProcessed = this;
                oPR.Result += "Saved";
                return (oPR);

            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionParameterSave", Exc.Message, LogPath);

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


        public static bool Delete(int TemplateRegionParameterID, string CnxnString, string LogPath)
        {
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionParameterDelete", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionParameterID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionParameterID"].Value = TemplateRegionParameterID;

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();
                return (true);
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionParameterDelete", Exc.Message, LogPath);
                return (false);
            }
            finally
            {
                if (Cnxn.State == ConnectionState.Open) Cnxn.Close();
            }


        }
        #endregion Delete

        public override string ToString()
        {
            return this.ParameterName + ":" + this.DefaultValue;
        }

        #region Render
        public override string Render()
        {
            /*
             "parameters": {
              "<parameter-name>" : {
                "type" : "<type-of-parameter-value>",
                "defaultValue": "<default-value-of-parameter>",
                "allowedValues": [ "<array-of-allowed-values>" ],
                "minValue": <minimum-value-for-int>,
                "maxValue": <maximum-value-for-int>,
                "minLength": <minimum-length-for-string-or-array>,
                "maxLength": <maximum-length-for-string-or-array-parameters>,
                "metadata": {
                  "description": "<description-of-the parameter>"
                }
              }
            } 
             */
            StringBuilder sb = new StringBuilder();
            sb.Append(F.Tab + F.Space(1) + F.Quote + this.ParameterName + F.Quote + F.Colon + F.CB + Environment.NewLine);
            sb.Append(F.Tab + F.Space(3) + F.Quote + "type" + F.Quote + F.Colon + F.Quote + this.DataType + F.Quote + F.Comma + Environment.NewLine);
            if (this.DefaultValue.Length > 0)
                sb.Append(F.Tab + F.Space(3) + F.Quote + "defaultValue" + F.Quote + F.Colon + F.Quote + this.DefaultValue + F.Quote + F.Comma + Environment.NewLine);
            if (this.AllowedValues.Count > 0)
            {
                sb.Append(F.Tab + F.Space(3) + F.Quote + "allowedValues" + F.Quote + F.Colon + F.SqB);
                switch (this.DataType)
                {
                    case "int":
                        {
                            foreach (TemplateRegionParameterAllowedValue v in this.AllowedValues)
                            {
                                sb.Append(v.AllowedValue + ",");
                            }
                            break;
                        }
                    case "string":
                        {
                            foreach (TemplateRegionParameterAllowedValue v in this.AllowedValues)
                            {
                                sb.Append(F.Quote + v.AllowedValue + F.Quote + ",");
                            }
                            break;
                        }
                }
                int iComma = sb.ToString().LastIndexOf(",");
                sb.Remove(iComma, 1);

                sb.Append(F.ESqB + F.Comma + Environment.NewLine);
            }

            if (this.MinValue > 0)
                sb.Append(F.Tab + F.Space(3) + F.Quote + "minValue" + F.Quote + F.Colon + this.MinValue + F.Comma + Environment.NewLine);
            if (this.MaxValue > 0)
                sb.Append(F.Tab + F.Space(3) + F.Quote + "maxValue" + F.Quote + F.Colon + this.MaxValue + F.Comma + Environment.NewLine);
            if (this.MinLength > 0)
                sb.Append(F.Tab + F.Space(3) + F.Quote + "minLength" + F.Quote + F.Colon + this.MinLength + F.Comma + Environment.NewLine);
            if (this.MaxLength > 0)
                sb.Append(F.Tab + F.Space(3) + F.Quote + "maxLength" + F.Quote + F.Colon + this.MaxLength + F.Comma + Environment.NewLine);
            if (this.Description.Length > 0)
            {
                sb.Append(F.Tab + F.Space(3) + F.Quote + "metadata" + F.Quote + F.Colon + F.CB + Environment.NewLine);
                sb.Append(F.Tab + F.Space(5) + F.Quote + "description" + F.Quote + F.Colon + F.Quote + this.Description + F.Quote + Environment.NewLine);
                sb.Append(F.Tab + F.Space(3) + F.ECB + F.Comma + Environment.NewLine);
            }
            sb.Append(F.Tab + F.Space(1) + F.ECB + Environment.NewLine);
            int iLast = sb.ToString().LastIndexOf(",");
            sb.Remove(iLast, 1);
            return sb.ToString();
        }
        #endregion Render

        #region DummyData
        public static TemplateRegionParameter DummyData(ARMTemplate ARMTemplate)
        {
            TemplateRegionParameter p = new TemplateRegionParameter();
            p.DataType = "string";
            p.DataType = ARMTemplate.ValidateParameterType(p.DataType) ? p.DataType : "invalid data type";
            p.DefaultValue = "smith";
            p.ParameterName = "LastName";
            p.MinLength = 2;
            p.TemplateID = ARMTemplate.TemplateID;
            p.TemplateRegionParameterID = 1;
            p.Description = "LastName Parameter";
            p.CreatedDate = DateTime.Now.ToString();
            p.ModifiedDate = DateTime.Now.ToString();

            TemplateRegionParameterAllowedValue v = new TemplateRegionParameterAllowedValue();
            v.AllowedValue = "hello";
            p.AllowedValues.Add(v);

            v = new TemplateRegionParameterAllowedValue();
            v.AllowedValue = "howzit";
            p.AllowedValues.Add(v);

            v = new TemplateRegionParameterAllowedValue();
            v.AllowedValue = "aloha";
            p.AllowedValues.Add(v);

            return (p);
        }
        #endregion DummyData
    }
}