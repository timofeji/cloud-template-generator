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
    public class TemplateRegionResourceCollection : Dictionary<int, TemplateRegionResource>
    {

        #region Constructors

        public TemplateRegionResourceCollection()
        {
        }

        public TemplateRegionResourceCollection(string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourcesFetchAll", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateRegionResource oTemplateRegionResource = new TemplateRegionResource();
                    oTemplateRegionResource.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    oTemplateRegionResource.CopyCount = dr["CopyCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CopyCount"]);
                    oTemplateRegionResource.Comments = dr["Comments"] == DBNull.Value ? "" : dr["Comments"].ToString().Trim();
                    oTemplateRegionResource.CopyName = dr["CopyName"] == DBNull.Value ? "" : dr["CopyName"].ToString().Trim();
                    oTemplateRegionResource.CopyMode = dr["CopyMode"] == DBNull.Value ? "" : dr["CopyMode"].ToString().Trim();
                    oTemplateRegionResource.PlanName = dr["PlanName"] == DBNull.Value ? "" : dr["PlanName"].ToString().Trim();
                    oTemplateRegionResource.Kind = dr["Kind"] == DBNull.Value ? "" : dr["Kind"].ToString().Trim();
                    oTemplateRegionResource.RegionType = dr["Type"] == DBNull.Value ? "" : dr["Type"].ToString().Trim();
                    oTemplateRegionResource.SkuSize = dr["SkuSize"] == DBNull.Value ? "" : dr["SkuSize"].ToString().Trim();
                    oTemplateRegionResource.ApiVersion = dr["ApiVersion"] == DBNull.Value ? "" : dr["ApiVersion"].ToString().Trim();
                    oTemplateRegionResource.CopyBatchSize = dr["CopyBatchSize"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CopyBatchSize"]);
                    oTemplateRegionResource.SkuCapacity = dr["SkuCapacity"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SkuCapacity"]);
                    oTemplateRegionResource.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateRegionResource.ResourceProviderID = dr["ResourceProviderID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ResourceProviderID"]);
                    oTemplateRegionResource.SkuFamily = dr["SkuFamily"] == DBNull.Value ? "" : dr["SkuFamily"].ToString().Trim();
                    oTemplateRegionResource.PlanPromotionCode = dr["PlanPromotionCode"] == DBNull.Value ? "" : dr["PlanPromotionCode"].ToString().Trim();
                    oTemplateRegionResource.SkuName = dr["SkuName"] == DBNull.Value ? "" : dr["SkuName"].ToString().Trim();
                    oTemplateRegionResource.SkuTier = dr["SkuTier"] == DBNull.Value ? "" : dr["SkuTier"].ToString().Trim();
                    oTemplateRegionResource.Condition = dr["Condition"] == DBNull.Value ? false : Convert.ToBoolean(dr["Condition"]);
                    oTemplateRegionResource.Location = dr["Location"] == DBNull.Value ? "" : dr["Location"].ToString().Trim();
                    oTemplateRegionResource.PlanPublisher = dr["PlanPublisher"] == DBNull.Value ? "" : dr["PlanPublisher"].ToString().Trim();
                    oTemplateRegionResource.Name = dr["Name"] == DBNull.Value ? "" : dr["Name"].ToString().Trim();
                    oTemplateRegionResource.TemplateRegionResourceID = dr["TemplateRegionResourceID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionResourceID"]);
                    oTemplateRegionResource.PlanProduct = dr["PlanProduct"] == DBNull.Value ? "" : dr["PlanProduct"].ToString().Trim();
                    oTemplateRegionResource.PlanVersion = dr["PlanVersion"] == DBNull.Value ? "" : dr["PlanVersion"].ToString().Trim();
                    oTemplateRegionResource.TemplateID = dr["TemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateID"]);
                    if (!this.ContainsKey(oTemplateRegionResource.TemplateRegionResourceID))
                        this.Add(oTemplateRegionResource.TemplateRegionResourceID, oTemplateRegionResource);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourceCollectionConstructor", Exc.Message, LogPath);
            }
            finally
            {
                if (Cnxn.State == ConnectionState.Open) Cnxn.Close();
            }
        }
        public TemplateRegionResourceCollection(int TemplateID, string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourcesFetchAll", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateID", SqlDbType.Int));
                cmd.Parameters["@TemplateID"].Value = TemplateID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateRegionResource oTemplateRegionResource = new TemplateRegionResource();
                    oTemplateRegionResource.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    oTemplateRegionResource.CopyCount = dr["CopyCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CopyCount"]);
                    oTemplateRegionResource.Comments = dr["Comments"] == DBNull.Value ? "" : dr["Comments"].ToString().Trim();
                    oTemplateRegionResource.CopyName = dr["CopyName"] == DBNull.Value ? "" : dr["CopyName"].ToString().Trim();
                    oTemplateRegionResource.CopyMode = dr["CopyMode"] == DBNull.Value ? "" : dr["CopyMode"].ToString().Trim();
                    oTemplateRegionResource.PlanName = dr["PlanName"] == DBNull.Value ? "" : dr["PlanName"].ToString().Trim();
                    oTemplateRegionResource.Kind = dr["Kind"] == DBNull.Value ? "" : dr["Kind"].ToString().Trim();
                    oTemplateRegionResource.RegionType = dr["Type"] == DBNull.Value ? "" : dr["Type"].ToString().Trim();
                    oTemplateRegionResource.SkuSize = dr["SkuSize"] == DBNull.Value ? "" : dr["SkuSize"].ToString().Trim();
                    oTemplateRegionResource.ApiVersion = dr["ApiVersion"] == DBNull.Value ? "" : dr["ApiVersion"].ToString().Trim();
                    oTemplateRegionResource.CopyBatchSize = dr["CopyBatchSize"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CopyBatchSize"]);
                    oTemplateRegionResource.SkuCapacity = dr["SkuCapacity"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SkuCapacity"]);
                    oTemplateRegionResource.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateRegionResource.SkuFamily = dr["SkuFamily"] == DBNull.Value ? "" : dr["SkuFamily"].ToString().Trim();
                    oTemplateRegionResource.PlanPromotionCode = dr["PlanPromotionCode"] == DBNull.Value ? "" : dr["PlanPromotionCode"].ToString().Trim();
                    oTemplateRegionResource.SkuName = dr["SkuName"] == DBNull.Value ? "" : dr["SkuName"].ToString().Trim();
                    oTemplateRegionResource.SkuTier = dr["SkuTier"] == DBNull.Value ? "" : dr["SkuTier"].ToString().Trim();
                    oTemplateRegionResource.Condition = dr["Condition"] == DBNull.Value ? false : Convert.ToBoolean(dr["Condition"]);
                    oTemplateRegionResource.Location = dr["Location"] == DBNull.Value ? "" : dr["Location"].ToString().Trim();
                    oTemplateRegionResource.PlanPublisher = dr["PlanPublisher"] == DBNull.Value ? "" : dr["PlanPublisher"].ToString().Trim();
                    oTemplateRegionResource.Name = dr["Name"] == DBNull.Value ? "" : dr["Name"].ToString().Trim();
                    oTemplateRegionResource.TemplateRegionResourceID = dr["TemplateRegionResourceID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionResourceID"]);
                    oTemplateRegionResource.PlanProduct = dr["PlanProduct"] == DBNull.Value ? "" : dr["PlanProduct"].ToString().Trim();
                    oTemplateRegionResource.PlanVersion = dr["PlanVersion"] == DBNull.Value ? "" : dr["PlanVersion"].ToString().Trim();
                    oTemplateRegionResource.TemplateID = dr["TemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateID"]);
                    if (!this.ContainsKey(oTemplateRegionResource.TemplateRegionResourceID))
                        this.Add(oTemplateRegionResource.TemplateRegionResourceID, oTemplateRegionResource);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourceCollectionConstructor", Exc.Message, LogPath);
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
                foreach (TemplateRegionResource o in this.Values)
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
                Log.LogErr("TemplateRegionResourceCollection Save", Exc.Message, LogPath);
                oPR.Exception = Exc;
                return (oPR);
            }
        }
        #endregion Save

        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            foreach (TemplateRegionResource r in this.Values)
            {
                sb.Append(r.Render() + F.Comma);
            }
            int iLast = sb.ToString().LastIndexOf(",");
            sb.Remove(iLast, 1);

            return sb.ToString();
        }
    }


    [Serializable]
    public class TemplateRegionResource : TemplateRegion
    {

        #region Vars

        string _PlanVersion;
        string _PlanName;
        string _PlanProduct;
        string _CreatedDate;
        string _Kind;
        string _SkuSize;
        string _PlanPromotionCode;
        string _SkuName;
        int _TemplateRegionResourceID;
        int _SkuCapacity;
        int _ResourceProviderID;
        int _TemplateID;
        string _CopyName;
        string _Name;
        int _CopyBatchSize;
        string _CopyMode;
        string _ApiVersion;
        string _PlanPublisher;
        int _CopyCount;
        string _SkuTier;
        string _Type;
        string _ModifiedDate;
        string _SkuFamily;
        string _Comments;
        bool _Condition;
        string _Location = "[resourceGroup().location]";

        #endregion Vars
        TemplateRegionResourceCopyCollection _Copies = new TemplateRegionResourceCopyCollection();
        TemplateRegionResourceDependsOnCollection _DependsOn = new TemplateRegionResourceDependsOnCollection();
        TemplateRegionResourcePropertyCollection _Properties = new TemplateRegionResourcePropertyCollection();
        TemplateRegionResourceTagCollection _Tags = new TemplateRegionResourceTagCollection();
        TemplateAutoScalerCollection _AutoScalers = new TemplateAutoScalerCollection();
        TemplateRegionResourceCollection _ChildResources = new TemplateRegionResourceCollection();
        #region Get/Sets

        public string PlanVersion
        {
            get { return (_PlanVersion); }
            set { _PlanVersion = value; }
        }

        public string PlanName
        {
            get { return (_PlanName); }
            set { _PlanName = value; }
        }

        public string PlanProduct
        {
            get { return (_PlanProduct); }
            set { _PlanProduct = value; }
        }

        public string CreatedDate
        {
            get { return (_CreatedDate); }
            set { _CreatedDate = value; }
        }

        public string Kind
        {
            get { return (_Kind); }
            set { _Kind = value; }
        }

        public string SkuSize
        {
            get { return (_SkuSize); }
            set { _SkuSize = value; }
        }

        public string PlanPromotionCode
        {
            get { return (_PlanPromotionCode); }
            set { _PlanPromotionCode = value; }
        }

        public string SkuName
        {
            get { return (_SkuName); }
            set { _SkuName = value; }
        }

        public int TemplateRegionResourceID
        {
            get { return (_TemplateRegionResourceID); }
            set { _TemplateRegionResourceID = value; }
        }

        public int ResourceProviderID
        {
            get { return (_ResourceProviderID); }
            set { _ResourceProviderID = value; }
        }
        public int SkuCapacity
        {
            get { return (_SkuCapacity); }
            set { _SkuCapacity = value; }
        }

        public int TemplateID
        {
            get { return (_TemplateID); }
            set { _TemplateID = value; }
        }

        public string CopyName
        {
            get { return (_CopyName); }
            set { _CopyName = value; }
        }

        public int CopyBatchSize
        {
            get { return (_CopyBatchSize); }
            set { _CopyBatchSize = value; }
        }

        public string CopyMode
        {
            get { return (_CopyMode); }
            set { _CopyMode = value; }
        }

        public string ApiVersion
        {
            get { return (_ApiVersion); }
            set { _ApiVersion = value; }
        }

        public string PlanPublisher
        {
            get { return (_PlanPublisher); }
            set { _PlanPublisher = value; }
        }

        public int CopyCount
        {
            get { return (_CopyCount); }
            set { _CopyCount = value; }
        }

        public string SkuTier
        {
            get { return (_SkuTier); }
            set { _SkuTier = value; }
        }

        public string ProviderType
        {
            get { return (_Type); }
            set { _Type = value; }
        }

        public string ModifiedDate
        {
            get { return (_ModifiedDate); }
            set { _ModifiedDate = value; }
        }

        public string SkuFamily
        {
            get { return (_SkuFamily); }
            set { _SkuFamily = value; }
        }

        public string Comments
        {
            get { return (_Comments); }
            set { _Comments = value; }
        }

        public bool Condition
        {
            get { return (_Condition); }
            set { _Condition = value; }
        }

        public string Location
        {
            get { return (_Location); }
            set { _Location = value; }
        }

        public TemplateRegionResourceCopyCollection Copies { get => _Copies; set => _Copies = value; }
        public TemplateRegionResourceDependsOnCollection DependsOn { get => _DependsOn; set => _DependsOn = value; }
        public TemplateRegionResourcePropertyCollection Properties { get => _Properties; set => _Properties = value; }
        public TemplateRegionResourceTagCollection Tags { get => _Tags; set => _Tags = value; }
        public TemplateAutoScalerCollection AutoScalers { get => _AutoScalers; set => _AutoScalers = value; }
        public TemplateRegionResourceCollection ChildResources { get => _ChildResources; set => _ChildResources = value; }

        #endregion Get/Sets

        #region Constructors

        public TemplateRegionResource()
        {
            this.RegionType = "resources";
        }

        public TemplateRegionResource(int TemplateRegionResourceID, string CnxnString, string LogPath)
        {
            this.RegionType = "resources";
            // fill props from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourceFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourceID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourceID"].Value = TemplateRegionResourceID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    this.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    this.CopyCount = dr["CopyCount"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CopyCount"]);
                    this.Comments = dr["Comments"] == DBNull.Value ? "" : dr["Comments"].ToString().Trim();
                    this.CopyName = dr["CopyName"] == DBNull.Value ? "" : dr["CopyName"].ToString().Trim();
                    this.CopyMode = dr["CopyMode"] == DBNull.Value ? "" : dr["CopyMode"].ToString().Trim();
                    this.PlanName = dr["PlanName"] == DBNull.Value ? "" : dr["PlanName"].ToString().Trim();
                    this.Kind = dr["Kind"] == DBNull.Value ? "" : dr["Kind"].ToString().Trim();
                    this.RegionType = dr["Type"] == DBNull.Value ? "" : dr["Type"].ToString().Trim();
                    this.SkuSize = dr["SkuSize"] == DBNull.Value ? "" : dr["SkuSize"].ToString().Trim();
                    this.ApiVersion = dr["ApiVersion"] == DBNull.Value ? "" : dr["ApiVersion"].ToString().Trim();
                    this.CopyBatchSize = dr["CopyBatchSize"] == DBNull.Value ? 0 : Convert.ToInt32(dr["CopyBatchSize"]);
                    this.SkuCapacity = dr["SkuCapacity"] == DBNull.Value ? 0 : Convert.ToInt32(dr["SkuCapacity"]);
                    this.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    this.SkuFamily = dr["SkuFamily"] == DBNull.Value ? "" : dr["SkuFamily"].ToString().Trim();
                    this.PlanPromotionCode = dr["PlanPromotionCode"] == DBNull.Value ? "" : dr["PlanPromotionCode"].ToString().Trim();
                    this.SkuName = dr["SkuName"] == DBNull.Value ? "" : dr["SkuName"].ToString().Trim();
                    this.SkuTier = dr["SkuTier"] == DBNull.Value ? "" : dr["SkuTier"].ToString().Trim();
                    this.Condition = dr["Condition"] == DBNull.Value ? false : Convert.ToBoolean(dr["Condition"]);
                    this.Location = dr["Location"] == DBNull.Value ? "" : dr["Location"].ToString().Trim();
                    this.PlanPublisher = dr["PlanPublisher"] == DBNull.Value ? "" : dr["PlanPublisher"].ToString().Trim();
                    this.Name = dr["Name"] == DBNull.Value ? "" : dr["Name"].ToString().Trim();
                    this.TemplateRegionResourceID = dr["TemplateRegionResourceID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionResourceID"]);
                    this.ResourceProviderID = dr["ResourceProviderID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ResourceProviderID"]);
                    this.PlanProduct = dr["PlanProduct"] == DBNull.Value ? "" : dr["PlanProduct"].ToString().Trim();
                    this.PlanVersion = dr["PlanVersion"] == DBNull.Value ? "" : dr["PlanVersion"].ToString().Trim();
                    this.TemplateID = dr["TemplateID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateID"]);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourceConstructor", Exc.Message, LogPath);
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

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourceSave", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                #region Parameters
                // parameters for tblTemplateRegionResource
                cmd.Parameters.Add(new SqlParameter("@CopyCount", SqlDbType.Int));
                cmd.Parameters["@CopyCount"].Value = this.CopyCount;

                cmd.Parameters.Add(new SqlParameter("@Comments", SqlDbType.VarChar, 50));
                cmd.Parameters["@Comments"].Value = this.Comments ?? "";

                cmd.Parameters.Add(new SqlParameter("@CopyName", SqlDbType.VarChar, 50));
                cmd.Parameters["@CopyName"].Value = this.CopyName ?? "";

                cmd.Parameters.Add(new SqlParameter("@CopyMode", SqlDbType.VarChar, 50));
                cmd.Parameters["@CopyMode"].Value = this.CopyMode ?? "";

                cmd.Parameters.Add(new SqlParameter("@PlanName", SqlDbType.VarChar, 50));
                cmd.Parameters["@PlanName"].Value = this.PlanName ?? "";

                cmd.Parameters.Add(new SqlParameter("@Kind", SqlDbType.VarChar, 50));
                cmd.Parameters["@Kind"].Value = this.Kind ?? "";

                cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.VarChar, 50));
                cmd.Parameters["@Type"].Value = this.ProviderType ?? "";

                cmd.Parameters.Add(new SqlParameter("@SkuSize", SqlDbType.VarChar, 50));
                cmd.Parameters["@SkuSize"].Value = this.SkuSize ?? "";

                cmd.Parameters.Add(new SqlParameter("@ApiVersion", SqlDbType.VarChar, 50));
                cmd.Parameters["@ApiVersion"].Value = this.ApiVersion ?? "";

                cmd.Parameters.Add(new SqlParameter("@ResourceProviderID", SqlDbType.Int));
                cmd.Parameters["@ResourceProviderID"].Value = this.ResourceProviderID;

                cmd.Parameters.Add(new SqlParameter("@CopyBatchSize", SqlDbType.Int));
                cmd.Parameters["@CopyBatchSize"].Value = this.CopyBatchSize;

                cmd.Parameters.Add(new SqlParameter("@SkuCapacity", SqlDbType.Int));
                cmd.Parameters["@SkuCapacity"].Value = this.SkuCapacity;

                cmd.Parameters.Add(new SqlParameter("@SkuFamily", SqlDbType.VarChar, 50));
                cmd.Parameters["@SkuFamily"].Value = this.SkuFamily ?? "";

                cmd.Parameters.Add(new SqlParameter("@PlanPromotionCode", SqlDbType.VarChar, 50));
                cmd.Parameters["@PlanPromotionCode"].Value = this.PlanPromotionCode ?? "";

                cmd.Parameters.Add(new SqlParameter("@SkuName", SqlDbType.VarChar, 50));
                cmd.Parameters["@SkuName"].Value = this.SkuName ?? "";

                cmd.Parameters.Add(new SqlParameter("@SkuTier", SqlDbType.VarChar, 50));
                cmd.Parameters["@SkuTier"].Value = this.SkuTier ?? "";

                cmd.Parameters.Add(new SqlParameter("@Condition", SqlDbType.Bit));
                cmd.Parameters["@Condition"].Value = this.Condition;

                cmd.Parameters.Add(new SqlParameter("@Location", SqlDbType.VarChar, 50));
                cmd.Parameters["@Location"].Value = this.Location ?? "";

                cmd.Parameters.Add(new SqlParameter("@PlanPublisher", SqlDbType.VarChar, 50));
                cmd.Parameters["@PlanPublisher"].Value = this.PlanPublisher ?? "";

                cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 50));
                cmd.Parameters["@Name"].Value = this.Name ?? "";

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourceID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourceID"].Value = this.TemplateRegionResourceID;

                cmd.Parameters.Add(new SqlParameter("@PlanProduct", SqlDbType.VarChar, 50));
                cmd.Parameters["@PlanProduct"].Value = this.PlanProduct ?? "";

                cmd.Parameters.Add(new SqlParameter("@PlanVersion", SqlDbType.VarChar, 50));
                cmd.Parameters["@PlanVersion"].Value = this.PlanVersion ?? "";

                cmd.Parameters.Add(new SqlParameter("@TemplateID", SqlDbType.Int));
                cmd.Parameters["@TemplateID"].Value = this.TemplateID;

                // assign output param
                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourceIDOut", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourceIDOut"].Direction = ParameterDirection.Output;

                #endregion Parameters

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();

                int iTemplateRegionResourceID = Convert.ToInt32(cmd.Parameters["@TemplateRegionResourceIDOut"].Value);
                this.TemplateRegionResourceID = iTemplateRegionResourceID;

                oPR.ObjectProcessed = this;
                oPR.Result += "Saved";
                return (oPR);

            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourceSave", Exc.Message, LogPath);

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


        public static bool Delete(int TemplateRegionResourceID, string CnxnString, string LogPath)
        {
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourceDelete", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourceID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourceID"].Value = TemplateRegionResourceID;

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();
                return (true);
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourceDelete", Exc.Message, LogPath);
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
            return this.Name + " (" + this.Kind + ")";
        }
        public override string Render()
        {
            #region Sample
            /*
          * "resources": [
        {
           "condition": "<true-to-deploy-this-resource>",
           "type": "<resource-provider-namespace/resource-type-name>",
           "apiVersion": "<api-version-of-resource>",
           "name": "<name-of-the-resource>",
           "comments": "<your-reference-notes>",
           "location": "<location-of-resource>",
           "dependsOn": [
               "<array-of-related-resource-names>"
           ],
           "tags": {
               "<tag-name1>": "<tag-value1>",
               "<tag-name2>": "<tag-value2>"
           },
           "sku": {
               "name": "<sku-name>",
               "tier": "<sku-tier>",
               "size": "<sku-size>",
               "family": "<sku-family>",
               "capacity": <sku-capacity>
           },
           "kind": "<type-of-resource>",
           "copy": {
               "name": "<name-of-copy-loop>",
               "count": <number-of-iterations>,
               "mode": "<serial-or-parallel>",
               "batchSize": <number-to-deploy-serially>
           },
           "plan": {
               "name": "<plan-name>",
               "promotionCode": "<plan-promotion-code>",
               "publisher": "<plan-publisher>",
               "product": "<plan-product>",
               "version": "<plan-version>"
           },
           "properties": {
               "<settings-for-the-resource>",
               "copy": [
                   {
                       "name": ,
                       "count": ,
                       "input": {}
                   }
               ]
           },
           "resources": [
               "<array-of-child-resources>"
           ]
        }
        ]
          */
            #endregion Sample

            StringBuilder sb = new StringBuilder();
            sb.Append(F.Tab + F.CB + Environment.NewLine);
            sb.Append(F.Tab + F.Space(3) + F.Quote + "condition" + F.Quote + F.Colon + F.Quote + this.Condition + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(3) + F.Quote + "type" + F.Quote + F.Colon + F.Quote + this.ProviderType + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(3) + F.Quote + "apiVersion" + F.Quote + F.Colon + F.Quote + this.ApiVersion + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(3) + F.Quote + "name" + F.Quote + F.Colon + F.Quote + this.Name + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(3) + F.Quote + "comments" + F.Quote + F.Colon + F.Quote + this.Comments + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(3) + F.Quote + "location" + F.Quote + F.Colon + F.Quote + this.Location + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(3) + F.Quote + "dependsOn" + F.Quote + F.Colon + F.SqB + Environment.NewLine);
            sb.Append(this.DependsOn.Render());
            sb.Append(F.Tab + F.Space(3) + F.ESqB + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(3) + F.Quote + "tags" + F.Quote + F.Colon + F.CB + Environment.NewLine);
            sb.Append(this.Tags.Render());
            sb.Append(F.Tab + F.Space(3) + F.ECB + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(3) + F.Quote + "sku" + F.Quote + F.Colon + F.CB + Environment.NewLine);
            sb.Append(F.Tab + F.Space(6) + F.Quote + "name" + F.Quote + F.Colon + F.Quote + this.SkuName + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(6) + F.Quote + "tier" + F.Quote + F.Colon + F.Quote + this.SkuTier + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(6) + F.Quote + "size" + F.Quote + F.Colon + F.Quote + this.SkuSize + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(6) + F.Quote + "family" + F.Quote + F.Colon + F.Quote + this.SkuFamily + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(6) + F.Quote + "capacity" + F.Quote + F.Colon + F.Quote + this.SkuCapacity + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(3) + F.ECB + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(3) + F.Quote + "kind" + F.Quote + F.Colon + F.Quote + this.Kind + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(3) + F.Quote + "copy" + F.Quote + F.Colon + F.CB + Environment.NewLine);
            sb.Append(F.Tab + F.Space(6) + F.Quote + "name" + F.Quote + F.Colon + F.Quote + this.CopyName + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(6) + F.Quote + "count" + F.Quote + F.Colon + F.Quote + this.CopyCount + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(6) + F.Quote + "mode" + F.Quote + F.Colon + F.Quote + this.CopyMode + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(6) + F.Quote + "batchSize" + F.Quote + F.Colon + F.Quote + this.CopyBatchSize + F.Quote + Environment.NewLine);
            sb.Append(F.Tab + F.Space(3) + F.ECB + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(3) + F.Quote + "plan" + F.Quote + F.Colon + F.CB + Environment.NewLine);
            sb.Append(F.Tab + F.Space(6) + F.Quote + "name" + F.Quote + F.Colon + F.Quote + this.PlanName + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(6) + F.Quote + "promotionCode" + F.Quote + F.Colon + F.Quote + this.PlanPromotionCode + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(6) + F.Quote + "publisher" + F.Quote + F.Colon + F.Quote + this.PlanPublisher + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(6) + F.Quote + "product" + F.Quote + F.Colon + F.Quote + this.PlanProduct + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(6) + F.Quote + "version" + F.Quote + F.Colon + F.Quote + this.PlanVersion + F.Quote + Environment.NewLine);
            sb.Append(F.Tab + F.Space(3) + F.ECB + F.Comma + Environment.NewLine);

            if (this.Properties.Count > 0)
            {
                sb.Append(F.Tab + F.Space(3) + F.Quote + "properties" + F.Quote + F.Colon + F.CB + Environment.NewLine);
                sb.Append(this.Properties.Render());
            }

            sb.Append(F.Tab + F.Space(3) + F.ECB + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Space(3) + F.Quote + "resources" + F.Quote + F.Colon + F.SqB + Environment.NewLine);
            if (this.ChildResources.Count > 0)
            {
                sb.Append(F.Tab + F.Space(3) + F.CB + Environment.NewLine);
                sb.Append(this.ChildResources.Render());
            }
            sb.Append(F.Tab + F.Space(3) + F.ESqB + Environment.NewLine);


            sb.Append(F.Tab + F.ECB + Environment.NewLine);
            return (sb.ToString());

        }

        public static TemplateRegionResource DummyData(int x = 1)
        {
            TemplateRegionResource r = new TemplateRegionResource();
            r.ProviderType = "anyKindofResource";
            r.TemplateID = 1;
            r.TemplateRegionResourceID = 1;
            r.Name = "someResource" + x.ToString();
            r.Comments = "this is any type of resource that depends on other resources";
            r.Condition = true;
            r.ApiVersion = "1.0";
            r.Location = "someLocation";

            r.DependsOn.Add(TemplateRegionResourceDependsOn.DummyData().TemplateRegionResourceDependsOnID, TemplateRegionResourceDependsOn.DummyData());
            r.DependsOn.Add(TemplateRegionResourceDependsOn.DummyData(2).TemplateRegionResourceDependsOnID, TemplateRegionResourceDependsOn.DummyData(2));
            r.DependsOn.Add(TemplateRegionResourceDependsOn.DummyData(3).TemplateRegionResourceDependsOnID, TemplateRegionResourceDependsOn.DummyData(3));
            r.DependsOn.Add(TemplateRegionResourceDependsOn.DummyData(4).TemplateRegionResourceDependsOnID, TemplateRegionResourceDependsOn.DummyData(4));

            r.Tags.Add(TemplateRegionResourceTag.DummyData(r).TemplateRegionResourceTagID, TemplateRegionResourceTag.DummyData(r));
            r.Tags.Add(TemplateRegionResourceTag.DummyData(r, 2).TemplateRegionResourceTagID, TemplateRegionResourceTag.DummyData(r, 2));
            r.Tags.Add(TemplateRegionResourceTag.DummyData(r, 3).TemplateRegionResourceTagID, TemplateRegionResourceTag.DummyData(r, 3));

            r.Properties.Add(r.Properties.Count, TemplateRegionResourceProperty.DummyData(r));
            r.Properties.Add(r.Properties.Count, TemplateRegionResourceProperty.DummyData(r, 2));
            r.Properties.Add(r.Properties.Count, TemplateAutoScaler.DummyData());
            return (r);
        }
    }
}
