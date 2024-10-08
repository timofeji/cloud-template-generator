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
    public class TemplateRegionVariableCollection : Dictionary<int, TemplateRegionVariable>
    {

        #region Constructors

        public TemplateRegionVariableCollection()
        {
        }

        public TemplateRegionVariableCollection(int TemplateID, string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionVariablesFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateID", SqlDbType.Int));
                cmd.Parameters["@TemplateID"].Value = TemplateID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateRegionVariable oTemplateRegionVariable = new TemplateRegionVariable();
                    oTemplateRegionVariable.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateRegionVariable.VariableName = dr["VariableName"] == DBNull.Value ? "" : dr["VariableName"].ToString().Trim();
                    oTemplateRegionVariable.CurrentValue = dr["CurrentValue"] == DBNull.Value ? "" : dr["CurrentValue"].ToString().Trim();
                    oTemplateRegionVariable.TemplateID = dr["TemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateID"]);
                    oTemplateRegionVariable.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    oTemplateRegionVariable.TemplateRegionVariableID = dr["TemplateRegionVariableID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionVariableID"]);
                    if (!this.ContainsKey(oTemplateRegionVariable.TemplateRegionVariableID))
                        this.Add(oTemplateRegionVariable.TemplateRegionVariableID, oTemplateRegionVariable);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionVariableCollectionConstructor", Exc.Message, LogPath);
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
                foreach (TemplateRegionVariable o in this.Values)
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
                Log.LogErr("TemplateRegionVariableCollection Save", Exc.Message, LogPath);
                oPR.Exception = Exc;
                return (oPR);
            }
        }
        #endregion Save

        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            foreach (TemplateRegionVariable v in this.Values)
            {
                sb.Append(v.Render());
            }
            return sb.ToString();
        }
    }


    [Serializable]
    public class TemplateRegionVariable : TemplateRegion
    {

        #region Vars

        string _CurrentValue;
        int _TemplateID;
        int _TemplateRegionVariableID;
        string _ModifiedDate;
        string _CreatedDate;
        string _VariableName;

        #endregion Vars

        #region Get/Sets

        public string CurrentValue
        {
            get { return (_CurrentValue); }
            set { _CurrentValue = value; }
        }

        public int TemplateID
        {
            get { return (_TemplateID); }
            set { _TemplateID = value; }
        }

        public int TemplateRegionVariableID
        {
            get { return (_TemplateRegionVariableID); }
            set { _TemplateRegionVariableID = value; }
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

        public string VariableName
        {
            get { return (_VariableName); }
            set { _VariableName = value; }
        }

        #endregion Get/Sets

        #region Constructors

        public TemplateRegionVariable()
        {
            this.RegionType = "Variables";
        }

        public TemplateRegionVariable(int TemplateRegionVariableID, string CnxnString, string LogPath)
        {
            new TemplateRegionVariable();
            // fill props from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionVariableInfoFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionVariableID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionVariableID"].Value = TemplateRegionVariableID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    this.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    this.VariableName = dr["VariableName"] == DBNull.Value ? "" : dr["VariableName"].ToString().Trim();
                    this.CurrentValue = dr["CurrentValue"] == DBNull.Value ? "" : dr["CurrentValue"].ToString().Trim();
                    this.TemplateID = dr["TemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateID"]);
                    this.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    this.TemplateRegionVariableID = dr["TemplateRegionVariableID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionVariableID"]);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionVariableConstructor", Exc.Message, LogPath);
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

                SqlCommand cmd = new SqlCommand("spTemplateRegionVariableSave", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                #region Parameters
                // parameters for tblTemplateRegionVariables
                cmd.Parameters.Add(new SqlParameter("@VariableName", SqlDbType.VarChar, 50));
                cmd.Parameters["@VariableName"].Value = this.VariableName ?? "";

                cmd.Parameters.Add(new SqlParameter("@CurrentValue", SqlDbType.VarChar, 50));
                cmd.Parameters["@CurrentValue"].Value = this.CurrentValue ?? "";

                cmd.Parameters.Add(new SqlParameter("@TemplateID", SqlDbType.Int));
                cmd.Parameters["@TemplateID"].Value = this.TemplateID;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionVariableID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionVariableID"].Value = this.TemplateRegionVariableID;

                // assign output param
                cmd.Parameters.Add(new SqlParameter("@TemplateRegionVariableIDOut", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionVariableIDOut"].Direction = ParameterDirection.Output;

                #endregion Parameters

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();

                int iTemplateRegionVariableID = Convert.ToInt32(cmd.Parameters["@TemplateRegionVariableIDOut"].Value);
                this.TemplateRegionVariableID = iTemplateRegionVariableID;

                oPR.ObjectProcessed = this;
                oPR.Result += "Saved";
                return (oPR);

            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionVariableSave", Exc.Message, LogPath);

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


        public static bool Delete(int TemplateRegionVariableID, string CnxnString, string LogPath)
        {
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionVariableDelete", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionVariableID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionVariableID"].Value = TemplateRegionVariableID;

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();
                return (true);
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionVariableDelete", Exc.Message, LogPath);
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
             "variables": {
              "<variable-name>": "<variable-value>",
              }
             */
            StringBuilder sb = new StringBuilder();
            sb.Append(F.Tab + F.Space(1) + F.Quote + this.VariableName + F.Quote + F.Colon + 
                F.Quote + this.CurrentValue + F.Quote + F.Comma + Environment.NewLine);
            return (sb.ToString());
        }

        #region DummyData
        public static TemplateRegionVariable DummyData(int Count = 1)
        {
            TemplateRegionVariable p = new TemplateRegionVariable();
            p.VariableName = "LastName";
            p.CurrentValue = "Jones";
            if (Count == 2)
            {
                p.VariableName = "FirstName";
                p.CurrentValue = "Trevor";
            }
            if (Count == 3)
            {
                p.VariableName = "Age";
                p.CurrentValue = "21";
            }
            p.CreatedDate = DateTime.Now.ToString();
            p.ModifiedDate = DateTime.Now.ToString();
            return (p);
        }
        #endregion DummyData
    }
}