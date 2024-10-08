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
    public class TemplateRegionResourceDependsOnCollection : Dictionary<int, TemplateRegionResourceDependsOn>
    {

        #region Constructors

        public TemplateRegionResourceDependsOnCollection()
        {
        }

        public TemplateRegionResourceDependsOnCollection(string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourceDependsOnFetchAll", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateRegionResourceDependsOn oTemplateRegionResourceDependsOn = new TemplateRegionResourceDependsOn();
                    oTemplateRegionResourceDependsOn.ResourceName = dr["ResourceName"] == DBNull.Value ? "" : dr["ResourceName"].ToString().Trim();
                    oTemplateRegionResourceDependsOn.TemplateRegionResourceDependsOnID = dr["TemplateRegionResourceDependsOnID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionResourceDependsOnID"]);
                    oTemplateRegionResourceDependsOn.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    oTemplateRegionResourceDependsOn.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateRegionResourceDependsOn.TRRID = dr["TRRID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TRRID"]);
                    if (!this.ContainsKey(oTemplateRegionResourceDependsOn.TemplateRegionResourceDependsOnID))
                        this.Add(oTemplateRegionResourceDependsOn.TemplateRegionResourceDependsOnID, oTemplateRegionResourceDependsOn);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourceDependsOnCollectionConstructor", Exc.Message, LogPath);
            }
            finally
            {
                if (Cnxn.State == ConnectionState.Open) Cnxn.Close();
            }
        }
        public TemplateRegionResourceDependsOnCollection(int TemplateRegionResourceID, string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourceDependsOnFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourceDependsOnID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourceID"].Value = TemplateRegionResourceID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateRegionResourceDependsOn oTemplateRegionResourceDependsOn = new TemplateRegionResourceDependsOn();
                    oTemplateRegionResourceDependsOn.ResourceName = dr["ResourceName"] == DBNull.Value ? "" : dr["ResourceName"].ToString().Trim();
                    oTemplateRegionResourceDependsOn.TemplateRegionResourceDependsOnID = dr["TemplateRegionResourceDependsOnID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionResourceDependsOnID"]);
                    oTemplateRegionResourceDependsOn.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    oTemplateRegionResourceDependsOn.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateRegionResourceDependsOn.TRRID = dr["TRRID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TRRID"]);
                    if (!this.ContainsKey(oTemplateRegionResourceDependsOn.TemplateRegionResourceDependsOnID))
                        this.Add(oTemplateRegionResourceDependsOn.TemplateRegionResourceDependsOnID, oTemplateRegionResourceDependsOn);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourceDependsOnCollectionConstructor", Exc.Message, LogPath);
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
                foreach (TemplateRegionResourceDependsOn o in this.Values)
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
                Log.LogErr("TemplateRegionResourceDependsOnCollection Save", Exc.Message, LogPath);
                oPR.Exception = Exc;
                return (oPR);
            }
        }
        #endregion Save

        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            foreach (TemplateRegionResourceDependsOn d in this.Values)
            {
               sb.Append(F.Tab + F.Space(3) + d.Render());
            }

            int iLast = sb.ToString().LastIndexOf(",");
            sb.Remove(iLast, 1);

            return sb.ToString();
        }
    }


    [Serializable]
    public class TemplateRegionResourceDependsOn : TemplateRegion
    {

        #region Vars

        string _ModifiedDate;
        string _ResourceName;
        int _TemplateRegionResourceDependsOnID;
        string _CreatedDate;
        int _TRRID;

        #endregion Vars

        #region Get/Sets

        public string ModifiedDate
        {
            get { return (_ModifiedDate); }
            set { _ModifiedDate = value; }
        }

        public string ResourceName
        {
            get { return (_ResourceName); }
            set { _ResourceName = value; }
        }

        public int TemplateRegionResourceDependsOnID
        {
            get { return (_TemplateRegionResourceDependsOnID); }
            set { _TemplateRegionResourceDependsOnID = value; }
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

        #endregion Get/Sets

        #region Constructors

        public TemplateRegionResourceDependsOn()
        {
            this.RegionType = "dependsOn";
        }

        public TemplateRegionResourceDependsOn(int TemplateRegionResourceDependsOnID, string CnxnString, string LogPath)
        {
            this.RegionType = "dependsOn";
            // fill props from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourceDependsOnInfoFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourceDependsOnID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourceDependsOnID"].Value = TemplateRegionResourceDependsOnID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    this.ResourceName = dr["ResourceName"] == DBNull.Value ? "" : dr["ResourceName"].ToString().Trim();
                    this.TemplateRegionResourceDependsOnID = dr["TemplateRegionResourceDependsOnID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionResourceDependsOnID"]);
                    this.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    this.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    this.TRRID = dr["TRRID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TRRID"]);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourceDependsOnConstructor", Exc.Message, LogPath);
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

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourceDependsOnSave", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                #region Parameters
                // parameters for tblTemplateRegionResourceDependsOn
                cmd.Parameters.Add(new SqlParameter("@ResourceName", SqlDbType.VarChar, 50));
                cmd.Parameters["@ResourceName"].Value = this.ResourceName ?? "";

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourceDependsOnID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourceDependsOnID"].Value = this.TemplateRegionResourceDependsOnID;

                cmd.Parameters.Add(new SqlParameter("@TRRID", SqlDbType.Int));
                cmd.Parameters["@TRRID"].Value = this.TRRID;

                // assign output param
                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourceDependsOnIDOut", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourceDependsOnIDOut"].Direction = ParameterDirection.Output;

                #endregion Parameters

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();

                int iTemplateRegionResourceDependsOnID = Convert.ToInt32(cmd.Parameters["@TemplateRegionResourceDependsOnIDOut"].Value);
                this.TemplateRegionResourceDependsOnID = iTemplateRegionResourceDependsOnID;

                oPR.ObjectProcessed = this;
                oPR.Result += "Saved";
                return (oPR);

            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourceDependsOnSave", Exc.Message, LogPath);

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


        public static bool Delete(int TemplateRegionResourceDependsOnID, string CnxnString, string LogPath)
        {
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourceDependsOnDelete", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourceDependsOnID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourceDependsOnID"].Value = TemplateRegionResourceDependsOnID;

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();
                return (true);
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourceDependsOnDelete", Exc.Message, LogPath);
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
              "<array-of-related-resource-names>",
             */
            StringBuilder sb = new StringBuilder();
            sb.Append(F.Tab + F.Space(1) + F.Quote + this.ResourceName + F.Quote + F.Comma + Environment.NewLine);
            return (sb.ToString());
        }

        public static TemplateRegionResourceDependsOn DummyData(int x = 1)
        {
            TemplateRegionResourceDependsOn d = new TemplateRegionResourceDependsOn();
            d.ResourceName = "someResource" + x.ToString();
            d.TemplateRegionResourceDependsOnID = x;
            return (d);
        }
    }
}