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
    public class TemplateRegionResourcePropertyCollection : Dictionary<int, TemplateRegionResourceProperty>
    {

        #region Constructors

        public TemplateRegionResourcePropertyCollection()
        {
        }

        public TemplateRegionResourcePropertyCollection(string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourcePropertiesFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateRegionResourceProperty oTemplateRegionResourcePropertie = new TemplateRegionResourceProperty();
                    oTemplateRegionResourcePropertie.TemplateRegionResourcePropertyID = dr["TemplateRegionResourcePropertyID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionResourcePropertyID"]);
                    oTemplateRegionResourcePropertie.PropertyName = dr["PropertyName"] == DBNull.Value ? "" : dr["PropertyName"].ToString().Trim();
                    oTemplateRegionResourcePropertie.PropertyValue = dr["PropertyValue"] == DBNull.Value ? "" : dr["PropertyValue"].ToString().Trim();
                    oTemplateRegionResourcePropertie.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    oTemplateRegionResourcePropertie.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateRegionResourcePropertie.TRRID = dr["TRRID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TRRID"]);
                    if (!this.ContainsKey(oTemplateRegionResourcePropertie.TemplateRegionResourcePropertyID))
                        this.Add(oTemplateRegionResourcePropertie.TemplateRegionResourcePropertyID, oTemplateRegionResourcePropertie);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourcePropertieCollectionConstructor", Exc.Message, LogPath);
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
                foreach (TemplateRegionResourceProperty o in this.Values)
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
                Log.LogErr("TemplateRegionResourcePropertieCollection Save", Exc.Message, LogPath);
                oPR.Exception = Exc;
                return (oPR);
            }
        }
        #endregion Save
        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            foreach (TemplateRegionResourceProperty d in this.Values)
            {
                if (d.PropertyName != "AutoScaler")
                    sb.Append(F.Tab + d.Render());
                else
                {
                    TemplateAutoScaler s = (TemplateAutoScaler)d;
                    sb.Append(F.Tab + s.Render());
                }

            }

            int iLast = sb.ToString().LastIndexOf(",");
            sb.Remove(iLast, 1);

            return sb.ToString();
        }
    }


    [Serializable]
    public class TemplateRegionResourceProperty
    {

        #region Vars

        string _PropertyName;
        string _PropertyValue;
        string _CreatedDate;
        int _TemplateRegionResourcePropertyID;
        string _ModifiedDate;
        int _TRRID;

        #endregion Vars

        private TemplateRegionResourcePropertyCopyCollection copies = new TemplateRegionResourcePropertyCopyCollection();

        #region Get/Sets

        public string PropertyName
        {
            get { return (_PropertyName); }
            set { _PropertyName = value; }
        }

        public string PropertyValue
        {
            get { return (_PropertyValue); }
            set { _PropertyValue = value; }
        }

        public string CreatedDate
        {
            get { return (_CreatedDate); }
            set { _CreatedDate = value; }
        }

        public int TemplateRegionResourcePropertyID
        {
            get { return (_TemplateRegionResourcePropertyID); }
            set { _TemplateRegionResourcePropertyID = value; }
        }

        public string ModifiedDate
        {
            get { return (_ModifiedDate); }
            set { _ModifiedDate = value; }
        }

        public int TRRID
        {
            get { return (_TRRID); }
            set { _TRRID = value; }
        }

        public TemplateRegionResourcePropertyCopyCollection Copies { get => copies; set => copies = value; }

        #endregion Get/Sets

        #region Constructors

        public TemplateRegionResourceProperty()
        {
        }

        public TemplateRegionResourceProperty(int TemplateRegionResourcePropertieID, string CnxnString, string LogPath)
        {
            // fill props from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourcePropertieInfoFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourcePropertieID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourcePropertieID"].Value = TemplateRegionResourcePropertieID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    this.TemplateRegionResourcePropertyID = dr["TemplateRegionResourcePropertyID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateRegionResourcePropertyID"]);
                    this.PropertyName = dr["PropertyName"] == DBNull.Value ? "" : dr["PropertyName"].ToString().Trim();
                    this.PropertyValue = dr["PropertyValue"] == DBNull.Value ? "" : dr["PropertyValue"].ToString().Trim();
                    this.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    this.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    this.TRRID = dr["TRRID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TRRID"]);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourcePropertieConstructor", Exc.Message, LogPath);
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

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourcePropertySave", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                #region Parameters
                // parameters for tblTemplateRegionResourceProperties
                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourcePropertyID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourcePropertyID"].Value = this.TemplateRegionResourcePropertyID;

                cmd.Parameters.Add(new SqlParameter("@PropertyName", SqlDbType.VarChar, 50));
                cmd.Parameters["@PropertyName"].Value = this.PropertyName ?? "";

                cmd.Parameters.Add(new SqlParameter("@PropertyValue", SqlDbType.VarChar, 500));
                cmd.Parameters["@PropertyValue"].Value = this.PropertyValue ?? "";

                cmd.Parameters.Add(new SqlParameter("@ModifiedDate", SqlDbType.VarChar, 50));
                cmd.Parameters["@ModifiedDate"].Value = this.ModifiedDate ?? "";

                cmd.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.VarChar, 50));
                cmd.Parameters["@CreatedDate"].Value = this.CreatedDate ?? "";

                cmd.Parameters.Add(new SqlParameter("@TRRID", SqlDbType.Int));
                cmd.Parameters["@TRRID"].Value = this.TRRID;

                // assign output param
                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourcePropertieIDOut", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourcePropertieIDOut"].Direction = ParameterDirection.Output;

                #endregion Parameters

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();

                int iTemplateRegionResourcePropertieID = Convert.ToInt32(cmd.Parameters["@TemplateRegionResourcePropertyIDOut"].Value);
                this.TemplateRegionResourcePropertyID = iTemplateRegionResourcePropertieID;

                oPR.ObjectProcessed = this;
                oPR.Result += "Saved";
                return (oPR);

            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourcePropertieSave", Exc.Message, LogPath);

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


        public static bool Delete(int TemplateRegionResourcePropertieID, string CnxnString, string LogPath)
        {
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateRegionResourcePropertieDelete", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateRegionResourcePropertieID", SqlDbType.Int));
                cmd.Parameters["@TemplateRegionResourcePropertieID"].Value = TemplateRegionResourcePropertieID;

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();
                return (true);
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateRegionResourcePropertieDelete", Exc.Message, LogPath);
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
            properties": {
               "<settings-for-the-resource>",
               "copy": [
                   {
                       "name": ,
                       "count": ,
                       "input": {}
                   }
               ]
           },
             */
            StringBuilder sb = new StringBuilder();
            sb.Append(F.Space(6) + F.Quote + this.PropertyName + F.Quote + F.Colon + F.Quote + this.PropertyValue + F.Quote + F.Comma + Environment.NewLine);
            if (this.Copies.Count > 0)
            {
                sb.Append(F.Tab + F.Space(6) + F.Quote + "copy" + F.Quote + F.Colon + F.SqB + Environment.NewLine);
                foreach (TemplateRegionResourcePropertyCopy c in this.Copies.Values)
                {
                    sb.Append(F.Tab + F.Space(9) + F.CB + Environment.NewLine);
                    sb.Append(c.Render());
                    sb.Append(F.Tab + F.Space(9) + F.ECB + F.Comma + Environment.NewLine);
                }
                int iLast = sb.ToString().LastIndexOf(",");
                sb.Remove(iLast, 1);
                sb.Append(F.Tab + F.Space(6) + F.ESqB + F.Comma + Environment.NewLine);
            }

            return sb.ToString();
        }

        public static TemplateRegionResourceProperty DummyData(TemplateRegionResource r, int x = 1)
        {
            TemplateRegionResourceProperty p = new TemplateRegionResourceProperty();
            p.TemplateRegionResourcePropertyID = x;
            p.TRRID = r.TemplateRegionResourceID;
            p.PropertyName = "someProperty" + x.ToString();
            p.PropertyValue = "someValue";
            p.Copies.Add(TemplateRegionResourcePropertyCopy.DummyData(p).TemplateRegionResourcePropertyCopyID, TemplateRegionResourcePropertyCopy.DummyData(p));


            return (p);
        }
    }
}
