using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
namespace TemplateGenerator.ARM
{
    [Serializable]
    public class AzureResourceProviderCollection : Dictionary<int, AzureResourceProvider>
    {

        #region Constructors

        public AzureResourceProviderCollection()
        {
        }

        public AzureResourceProviderCollection(string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spAzureResourceProvidersFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    AzureResourceProvider oAzureResourceProvider = new AzureResourceProvider();
                    oAzureResourceProvider.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oAzureResourceProvider.IsRequired = dr["IsRequired"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsRequired"]);
                    oAzureResourceProvider.ResourceProviderID = dr["ResourceProviderID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ResourceProviderID"]);
                    oAzureResourceProvider.AzureService = dr["AzureService"] == DBNull.Value ? "" : dr["AzureService"].ToString().Trim();
                    oAzureResourceProvider.ResourceProviderNamespace = dr["ResourceProviderNamespace"] == DBNull.Value ? "" : dr["ResourceProviderNamespace"].ToString().Trim();
                    if (!this.ContainsKey(oAzureResourceProvider.ResourceProviderID))
                        this.Add(oAzureResourceProvider.ResourceProviderID, oAzureResourceProvider);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("AzureResourceProviderCollectionConstructor", Exc.Message, LogPath);
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
                foreach (AzureResourceProvider o in this.Values)
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
                Log.LogErr("AzureResourceProviderCollection Save", Exc.Message, LogPath);
                oPR.Exception = Exc;
                return (oPR);
            }
        }
        #endregion Save
    }


    [Serializable]
    public class AzureResourceProvider
    {

        #region Vars

        bool _IsRequired;
        string _CreatedDate;
        string _AzureService;
        int _ResourceProviderID;
        string _ResourceProviderNamespace;

        #endregion Vars

        AzureResourceProviderPropertyCollection _Properties = new AzureResourceProviderPropertyCollection();

        #region Get/Sets

        public bool IsRequired
        {
            get { return (_IsRequired); }
            set { _IsRequired = value; }
        }

        public string CreatedDate
        {
            get { return (_CreatedDate); }
            set { _CreatedDate = value; }
        }

        public string AzureService
        {
            get { return (_AzureService); }
            set { _AzureService = value; }
        }

        public int ResourceProviderID
        {
            get { return (_ResourceProviderID); }
            set { _ResourceProviderID = value; }
        }

        public string ResourceProviderNamespace
        {
            get { return (_ResourceProviderNamespace); }
            set { _ResourceProviderNamespace = value; }
        }

        public AzureResourceProviderPropertyCollection Properties { get => _Properties; set => _Properties = value; }

        #endregion Get/Sets

        #region Constructors

        public AzureResourceProvider()
        {
        }

        public AzureResourceProvider(int AzureResourceProviderID, string CnxnString, string LogPath)
        {
            // fill props from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spAzureResourceProviderInfoFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@AzureResourceProviderID", SqlDbType.Int));
                cmd.Parameters["@AzureResourceProviderID"].Value = AzureResourceProviderID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    this.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    this.IsRequired = dr["IsRequired"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsRequired"]);
                    this.ResourceProviderID = dr["ResourceProviderID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ResourceProviderID"]);
                    this.AzureService = dr["AzureService"] == DBNull.Value ? "" : dr["AzureService"].ToString().Trim();
                    this.ResourceProviderNamespace = dr["ResourceProviderNamespace"] == DBNull.Value ? "" : dr["ResourceProviderNamespace"].ToString().Trim();
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("AzureResourceProviderConstructor", Exc.Message, LogPath);
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

                SqlCommand cmd = new SqlCommand("spAzureResourceProviderSave", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                #region Parameters
                // parameters for tblAzureResourceProviders
                cmd.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.VarChar, 50));
                cmd.Parameters["@CreatedDate"].Value = this.CreatedDate ?? "";

                cmd.Parameters.Add(new SqlParameter("@IsRequired", SqlDbType.Bit));
                cmd.Parameters["@IsRequired"].Value = this.IsRequired;

                cmd.Parameters.Add(new SqlParameter("@ResourceProviderID", SqlDbType.Int));
                cmd.Parameters["@ResourceProviderID"].Value = this.ResourceProviderID;

                cmd.Parameters.Add(new SqlParameter("@AzureService", SqlDbType.VarChar, 500));
                cmd.Parameters["@AzureService"].Value = this.AzureService ?? "";

                cmd.Parameters.Add(new SqlParameter("@ResourceProviderNamespace", SqlDbType.VarChar, 500));
                cmd.Parameters["@ResourceProviderNamespace"].Value = this.ResourceProviderNamespace ?? "";

                // assign output param
                cmd.Parameters.Add(new SqlParameter("@AzureResourceProviderIDOut", SqlDbType.Int));
                cmd.Parameters["@AzureResourceProviderIDOut"].Direction = ParameterDirection.Output;

                #endregion Parameters

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();

                int iAzureResourceProviderID = Convert.ToInt32(cmd.Parameters["@AzureResourceProviderIDOut"].Value);
                this.ResourceProviderID = iAzureResourceProviderID;

                oPR.ObjectProcessed = this;
                oPR.Result += "Saved";
                return (oPR);

            }
            catch (Exception Exc)
            {
                Log.LogErr("AzureResourceProviderSave", Exc.Message, LogPath);

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


        public static bool Delete(int AzureResourceProviderID, string CnxnString, string LogPath)
        {
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spAzureResourceProviderDelete", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@AzureResourceProviderID", SqlDbType.Int));
                cmd.Parameters["@AzureResourceProviderID"].Value = AzureResourceProviderID;

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();
                return (true);
            }
            catch (Exception Exc)
            {
                Log.LogErr("AzureResourceProviderDelete", Exc.Message, LogPath);
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
            return this.ResourceProviderNamespace;
        }
    }
}