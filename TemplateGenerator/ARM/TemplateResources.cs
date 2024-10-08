using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
namespace TemplateGenerator.ARM
{
    [Serializable]
    public class TemplateResourceCollection : Dictionary<int, TemplateResource>
    {

        #region Constructors

        public TemplateResourceCollection()
        {
        }

        public TemplateResourceCollection(string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateResourcesFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateResource oTemplateResource = new TemplateResource();
                    oTemplateResource.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateResource.TemplateID = dr["TemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateID"]);
                    oTemplateResource.TemplateResourceID = dr["TemplateResourceID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateResourceID"]);
                    oTemplateResource.ResourceProviderID = dr["ResourceProviderID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ResourceProviderID"]);
                    if (!this.ContainsKey(oTemplateResource.TemplateResourceID))
                        this.Add(oTemplateResource.TemplateResourceID, oTemplateResource);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateResourceCollectionConstructor", Exc.Message, LogPath);
            }
            finally
            {
                if (Cnxn.State == ConnectionState.Open) Cnxn.Close();
            }
        }
        public TemplateResourceCollection(int TemplateID, string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateResourcesFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateID", SqlDbType.Int));
                cmd.Parameters["@TemplateID"].Value = TemplateID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateResource oTemplateResource = new TemplateResource();
                    oTemplateResource.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateResource.TemplateID = dr["TemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateID"]);
                    oTemplateResource.TemplateResourceID = dr["TemplateResourceID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateResourceID"]);
                    oTemplateResource.ResourceProviderID = dr["ResourceProviderID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ResourceProviderID"]);
                    if (!this.ContainsKey(oTemplateResource.TemplateResourceID))
                        this.Add(oTemplateResource.TemplateResourceID, oTemplateResource);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateResourceCollectionConstructor", Exc.Message, LogPath);
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
                foreach (TemplateResource o in this.Values)
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
                Log.LogErr("TemplateResourceCollection Save", Exc.Message, LogPath);
                oPR.Exception = Exc;
                return (oPR);
            }
        }
        #endregion Save
    }


    [Serializable]
    public class TemplateResource
    {

        #region Vars

        string _CreatedDate;
        int _ResourceProviderID;
        int _TemplateID;
        int _TemplateResourceID;

        #endregion Vars

        #region Get/Sets

        public string CreatedDate
        {
            get { return (_CreatedDate); }
            set { _CreatedDate = value; }
        }

        public int ResourceProviderID
        {
            get { return (_ResourceProviderID); }
            set { _ResourceProviderID = value; }
        }

        public int TemplateID
        {
            get { return (_TemplateID); }
            set { _TemplateID = value; }
        }

        public int TemplateResourceID
        {
            get { return (_TemplateResourceID); }
            set { _TemplateResourceID = value; }
        }

        #endregion Get/Sets

        #region Constructors

        public TemplateResource()
        {
        }

        public TemplateResource(int TemplateResourceID, string CnxnString, string LogPath)
        {
            // fill props from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateResourceInfoFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateResourceID", SqlDbType.Int));
                cmd.Parameters["@TemplateResourceID"].Value = TemplateResourceID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    this.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    this.TemplateID = dr["TemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateID"]);
                    this.TemplateResourceID = dr["TemplateResourceID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateResourceID"]);
                    this.ResourceProviderID = dr["ResourceProviderID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ResourceProviderID"]);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateResourceConstructor", Exc.Message, LogPath);
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

                SqlCommand cmd = new SqlCommand("spTemplateResourceSave", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                #region Parameters
                // parameters for tblTemplateResources
                cmd.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.VarChar, 50));
                cmd.Parameters["@CreatedDate"].Value = this.CreatedDate ?? "";

                cmd.Parameters.Add(new SqlParameter("@TemplateID", SqlDbType.Int));
                cmd.Parameters["@TemplateID"].Value = this.TemplateID;

                cmd.Parameters.Add(new SqlParameter("@TemplateResourceID", SqlDbType.Int));
                cmd.Parameters["@TemplateResourceID"].Value = this.TemplateResourceID;

                cmd.Parameters.Add(new SqlParameter("@ResourceProviderID", SqlDbType.Int));
                cmd.Parameters["@ResourceProviderID"].Value = this.ResourceProviderID;

                // assign output param
                cmd.Parameters.Add(new SqlParameter("@TemplateResourceIDOut", SqlDbType.Int));
                cmd.Parameters["@TemplateResourceIDOut"].Direction = ParameterDirection.Output;

                #endregion Parameters

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();

                int iTemplateResourceID = Convert.ToInt32(cmd.Parameters["@TemplateResourceIDOut"].Value);
                this.TemplateResourceID = iTemplateResourceID;

                oPR.ObjectProcessed = this;
                oPR.Result += "Saved";
                return (oPR);

            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateResourceSave", Exc.Message, LogPath);

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


        public static bool Delete(int TemplateResourceID, string CnxnString, string LogPath)
        {
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateResourceDelete", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateResourceID", SqlDbType.Int));
                cmd.Parameters["@TemplateResourceID"].Value = TemplateResourceID;

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();
                return (true);
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateResourceDelete", Exc.Message, LogPath);
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
            return this.ResourceProviderID.ToString();
        }
    }
}
