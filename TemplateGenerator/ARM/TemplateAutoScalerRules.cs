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
    public class TemplateAutoScalerRuleCollection : Dictionary<int, TemplateAutoScalerRule>
    {

        #region Constructors

        public TemplateAutoScalerRuleCollection()
        {
        }

        public TemplateAutoScalerRuleCollection(string CnxnString, string LogPath)
        {
            // fetch all from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateAzureAutoScalerRulesFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TemplateAutoScalerRule oTemplateAzureAutoScalerRule = new TemplateAutoScalerRule();
                    oTemplateAzureAutoScalerRule.MetricTriggerMetricResourceURI = dr["MetricTriggerMetricResourceURI"] == DBNull.Value ? "" : dr["MetricTriggerMetricResourceURI"].ToString().Trim();
                    oTemplateAzureAutoScalerRule.MetricTriggerThreshold = dr["MetricTriggerThreshold"] == DBNull.Value ? "" : dr["MetricTriggerThreshold"].ToString().Trim();
                    oTemplateAzureAutoScalerRule.ScaleActionDirection = dr["ScaleActionDirection"] == DBNull.Value ? "" : dr["ScaleActionDirection"].ToString().Trim();
                    oTemplateAzureAutoScalerRule.TemplateAzureAutoScalerRuleID = dr["TemplateAzureAutoScalerRuleID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateAzureAutoScalerRuleID"]);
                    oTemplateAzureAutoScalerRule.MetricTriggerMetricName = dr["MetricTriggerMetricName"] == DBNull.Value ? "" : dr["MetricTriggerMetricName"].ToString().Trim();
                    oTemplateAzureAutoScalerRule.ScaleActionCooldown = dr["ScaleActionCooldown"] == DBNull.Value ? "" : dr["ScaleActionCooldown"].ToString().Trim();
                    oTemplateAzureAutoScalerRule.MetricTriggerMetricNamespace = dr["MetricTriggerMetricNamespace"] == DBNull.Value ? "" : dr["MetricTriggerMetricNamespace"].ToString().Trim();
                    oTemplateAzureAutoScalerRule.TemplateAzureAutoScalerID = dr["TemplateAzureAutoScalerID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateAzureAutoScalerID"]);
                    oTemplateAzureAutoScalerRule.MetricTriggerTimeWindow = dr["MetricTriggerTimeWindow"] == DBNull.Value ? "" : dr["MetricTriggerTimeWindow"].ToString().Trim();
                    oTemplateAzureAutoScalerRule.MetricTriggerOperator = dr["MetricTriggerOperator"] == DBNull.Value ? "" : dr["MetricTriggerOperator"].ToString().Trim();
                    oTemplateAzureAutoScalerRule.MetricTriggerStatistic = dr["MetricTriggerStatistic"] == DBNull.Value ? "" : dr["MetricTriggerStatistic"].ToString().Trim();
                    oTemplateAzureAutoScalerRule.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    oTemplateAzureAutoScalerRule.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    oTemplateAzureAutoScalerRule.MetricTriggerTimeAggregation = dr["MetricTriggerTimeAggregation"] == DBNull.Value ? "" : dr["MetricTriggerTimeAggregation"].ToString().Trim();
                    oTemplateAzureAutoScalerRule.ScaleActionType = dr["ScaleActionType"] == DBNull.Value ? "" : dr["ScaleActionType"].ToString().Trim();
                    oTemplateAzureAutoScalerRule.MetricTriggerTimeGrain = dr["MetricTriggerTimeGrain"] == DBNull.Value ? "" : dr["MetricTriggerTimeGrain"].ToString().Trim();
                    oTemplateAzureAutoScalerRule.ScaleActionValue = dr["ScaleActionValue"] == DBNull.Value ? "" : dr["ScaleActionValue"].ToString().Trim();
                    if (!this.ContainsKey(oTemplateAzureAutoScalerRule.TemplateAzureAutoScalerRuleID))
                        this.Add(oTemplateAzureAutoScalerRule.TemplateAzureAutoScalerRuleID, oTemplateAzureAutoScalerRule);
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateAzureAutoScalerRuleCollectionConstructor", Exc.Message, LogPath);
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
                foreach (TemplateAutoScalerRule o in this.Values)
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
                Log.LogErr("TemplateAzureAutoScalerRuleCollection Save", Exc.Message, LogPath);
                oPR.Exception = Exc;
                return (oPR);
            }
        }
        #endregion Save

        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            foreach (TemplateAutoScalerRule t in this.Values)
            {
                sb.Append(t.Render());
            }
            int iLast = sb.ToString().LastIndexOf(",");
            sb.Remove(iLast, 1);

            return sb.ToString();
        }
    }


    [Serializable]
    public class TemplateAutoScalerRule
    {

        #region Vars

        int _TemplateAzureAutoScalerID;
        string _ScaleActionCooldown;
        string _MetricTriggerMetricName;
        string _MetricTriggerOperator;
        string _ScaleActionType;
        string _MetricTriggerStatistic;
        string _ScaleActionDirection;
        string _MetricTriggerTimeGrain;
        string _ModifiedDate;
        string _MetricTriggerThreshold;
        string _MetricTriggerMetricNamespace;
        string _CreatedDate;
        string _MetricTriggerMetricResourceURI;
        int _TemplateAzureAutoScalerRuleID;
        string _MetricTriggerTimeWindow;
        string _MetricTriggerTimeAggregation;
        string _ScaleActionValue;

        #endregion Vars

        #region Get/Sets

        public int TemplateAzureAutoScalerID
        {
            get { return (_TemplateAzureAutoScalerID); }
            set { _TemplateAzureAutoScalerID = value; }
        }

        public string ScaleActionCooldown
        {
            get { return (_ScaleActionCooldown); }
            set { _ScaleActionCooldown = value; }
        }

        public string MetricTriggerMetricName
        {
            get { return (_MetricTriggerMetricName); }
            set { _MetricTriggerMetricName = value; }
        }

        public string MetricTriggerOperator
        {
            get { return (_MetricTriggerOperator); }
            set { _MetricTriggerOperator = value; }
        }

        public string ScaleActionType
        {
            get { return (_ScaleActionType); }
            set { _ScaleActionType = value; }
        }

        public string MetricTriggerStatistic
        {
            get { return (_MetricTriggerStatistic); }
            set { _MetricTriggerStatistic = value; }
        }

        public string ScaleActionDirection
        {
            get { return (_ScaleActionDirection); }
            set { _ScaleActionDirection = value; }
        }

        public string MetricTriggerTimeGrain
        {
            get { return (_MetricTriggerTimeGrain); }
            set { _MetricTriggerTimeGrain = value; }
        }

        public string ModifiedDate
        {
            get { return (_ModifiedDate); }
            set { _ModifiedDate = value; }
        }

        public string MetricTriggerThreshold
        {
            get { return (_MetricTriggerThreshold); }
            set { _MetricTriggerThreshold = value; }
        }

        public string MetricTriggerMetricNamespace
        {
            get { return (_MetricTriggerMetricNamespace); }
            set { _MetricTriggerMetricNamespace = value; }
        }

        public string CreatedDate
        {
            get { return (_CreatedDate); }
            set { _CreatedDate = value; }
        }

        public string MetricTriggerMetricResourceURI
        {
            get { return (_MetricTriggerMetricResourceURI); }
            set { _MetricTriggerMetricResourceURI = value; }
        }

        public int TemplateAzureAutoScalerRuleID
        {
            get { return (_TemplateAzureAutoScalerRuleID); }
            set { _TemplateAzureAutoScalerRuleID = value; }
        }

        public string MetricTriggerTimeWindow
        {
            get { return (_MetricTriggerTimeWindow); }
            set { _MetricTriggerTimeWindow = value; }
        }

        public string MetricTriggerTimeAggregation
        {
            get { return (_MetricTriggerTimeAggregation); }
            set { _MetricTriggerTimeAggregation = value; }
        }

        public string ScaleActionValue
        {
            get { return (_ScaleActionValue); }
            set { _ScaleActionValue = value; }
        }

        #endregion Get/Sets

        #region Constructors

        public TemplateAutoScalerRule()
        {
        }

        public TemplateAutoScalerRule(int TemplateAzureAutoScalerRuleID, string CnxnString, string LogPath)
        {
            // fill props from db
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateAzureAutoScalerRuleInfoFetch", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateAzureAutoScalerRuleID", SqlDbType.Int));
                cmd.Parameters["@TemplateAzureAutoScalerRuleID"].Value = TemplateAzureAutoScalerRuleID;

                Cnxn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    this.MetricTriggerMetricResourceURI = dr["MetricTriggerMetricResourceURI"] == DBNull.Value ? "" : dr["MetricTriggerMetricResourceURI"].ToString().Trim();
                    this.MetricTriggerThreshold = dr["MetricTriggerThreshold"] == DBNull.Value ? "" : dr["MetricTriggerThreshold"].ToString().Trim();
                    this.ScaleActionDirection = dr["ScaleActionDirection"] == DBNull.Value ? "" : dr["ScaleActionDirection"].ToString().Trim();
                    this.TemplateAzureAutoScalerRuleID = dr["TemplateAzureAutoScalerRuleID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateAzureAutoScalerRuleID"]);
                    this.MetricTriggerMetricName = dr["MetricTriggerMetricName"] == DBNull.Value ? "" : dr["MetricTriggerMetricName"].ToString().Trim();
                    this.ScaleActionCooldown = dr["ScaleActionCooldown"] == DBNull.Value ? "" : dr["ScaleActionCooldown"].ToString().Trim();
                    this.MetricTriggerMetricNamespace = dr["MetricTriggerMetricNamespace"] == DBNull.Value ? "" : dr["MetricTriggerMetricNamespace"].ToString().Trim();
                    this.TemplateAzureAutoScalerID = dr["TemplateAzureAutoScalerID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateAzureAutoScalerID"]);
                    this.MetricTriggerTimeWindow = dr["MetricTriggerTimeWindow"] == DBNull.Value ? "" : dr["MetricTriggerTimeWindow"].ToString().Trim();
                    this.MetricTriggerOperator = dr["MetricTriggerOperator"] == DBNull.Value ? "" : dr["MetricTriggerOperator"].ToString().Trim();
                    this.MetricTriggerStatistic = dr["MetricTriggerStatistic"] == DBNull.Value ? "" : dr["MetricTriggerStatistic"].ToString().Trim();
                    this.CreatedDate = dr["CreatedDate"] == DBNull.Value ? "" : dr["CreatedDate"].ToString().Trim();
                    this.ModifiedDate = dr["ModifiedDate"] == DBNull.Value ? "" : dr["ModifiedDate"].ToString().Trim();
                    this.MetricTriggerTimeAggregation = dr["MetricTriggerTimeAggregation"] == DBNull.Value ? "" : dr["MetricTriggerTimeAggregation"].ToString().Trim();
                    this.ScaleActionType = dr["ScaleActionType"] == DBNull.Value ? "" : dr["ScaleActionType"].ToString().Trim();
                    this.MetricTriggerTimeGrain = dr["MetricTriggerTimeGrain"] == DBNull.Value ? "" : dr["MetricTriggerTimeGrain"].ToString().Trim();
                    this.ScaleActionValue = dr["ScaleActionValue"] == DBNull.Value ? "" : dr["ScaleActionValue"].ToString().Trim();
                }

                dr.Close();
                Cnxn.Close();
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateAzureAutoScalerRuleConstructor", Exc.Message, LogPath);
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

                SqlCommand cmd = new SqlCommand("spTemplateAzureAutoScalerRuleSave", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                #region Parameters
                // parameters for tblTemplateAzureAutoScalerRules
                cmd.Parameters.Add(new SqlParameter("@MetricTriggerMetricResourceURI", SqlDbType.VarChar, 500));
                cmd.Parameters["@MetricTriggerMetricResourceURI"].Value = this.MetricTriggerMetricResourceURI ?? "";

                cmd.Parameters.Add(new SqlParameter("@MetricTriggerThreshold", SqlDbType.VarChar, 50));
                cmd.Parameters["@MetricTriggerThreshold"].Value = this.MetricTriggerThreshold ?? "";

                cmd.Parameters.Add(new SqlParameter("@ScaleActionDirection", SqlDbType.VarChar, 50));
                cmd.Parameters["@ScaleActionDirection"].Value = this.ScaleActionDirection ?? "";

                cmd.Parameters.Add(new SqlParameter("@TemplateAzureAutoScalerRuleID", SqlDbType.Int));
                cmd.Parameters["@TemplateAzureAutoScalerRuleID"].Value = this.TemplateAzureAutoScalerRuleID;

                cmd.Parameters.Add(new SqlParameter("@MetricTriggerMetricName", SqlDbType.VarChar, 50));
                cmd.Parameters["@MetricTriggerMetricName"].Value = this.MetricTriggerMetricName ?? "";

                cmd.Parameters.Add(new SqlParameter("@ScaleActionCooldown", SqlDbType.VarChar, 50));
                cmd.Parameters["@ScaleActionCooldown"].Value = this.ScaleActionCooldown ?? "";

                cmd.Parameters.Add(new SqlParameter("@MetricTriggerMetricNamespace", SqlDbType.VarChar, 100));
                cmd.Parameters["@MetricTriggerMetricNamespace"].Value = this.MetricTriggerMetricNamespace ?? "";

                cmd.Parameters.Add(new SqlParameter("@TemplateAzureAutoScalerID", SqlDbType.Int));
                cmd.Parameters["@TemplateAzureAutoScalerID"].Value = this.TemplateAzureAutoScalerID;

                cmd.Parameters.Add(new SqlParameter("@MetricTriggerTimeWindow", SqlDbType.VarChar, 50));
                cmd.Parameters["@MetricTriggerTimeWindow"].Value = this.MetricTriggerTimeWindow ?? "";

                cmd.Parameters.Add(new SqlParameter("@MetricTriggerOperator", SqlDbType.VarChar, 50));
                cmd.Parameters["@MetricTriggerOperator"].Value = this.MetricTriggerOperator ?? "";

                cmd.Parameters.Add(new SqlParameter("@MetricTriggerStatistic", SqlDbType.VarChar, 50));
                cmd.Parameters["@MetricTriggerStatistic"].Value = this.MetricTriggerStatistic ?? "";

                cmd.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.VarChar, 50));
                cmd.Parameters["@CreatedDate"].Value = this.CreatedDate ?? "";

                cmd.Parameters.Add(new SqlParameter("@ModifiedDate", SqlDbType.VarChar, 50));
                cmd.Parameters["@ModifiedDate"].Value = this.ModifiedDate ?? "";

                cmd.Parameters.Add(new SqlParameter("@MetricTriggerTimeAggregation", SqlDbType.VarChar, 50));
                cmd.Parameters["@MetricTriggerTimeAggregation"].Value = this.MetricTriggerTimeAggregation ?? "";

                cmd.Parameters.Add(new SqlParameter("@ScaleActionType", SqlDbType.VarChar, 50));
                cmd.Parameters["@ScaleActionType"].Value = this.ScaleActionType ?? "";

                cmd.Parameters.Add(new SqlParameter("@MetricTriggerTimeGrain", SqlDbType.VarChar, 50));
                cmd.Parameters["@MetricTriggerTimeGrain"].Value = this.MetricTriggerTimeGrain ?? "";

                cmd.Parameters.Add(new SqlParameter("@ScaleActionValue", SqlDbType.VarChar, 50));
                cmd.Parameters["@ScaleActionValue"].Value = this.ScaleActionValue ?? "";

                // assign output param
                cmd.Parameters.Add(new SqlParameter("@TemplateAzureAutoScalerRuleIDOut", SqlDbType.Int));
                cmd.Parameters["@TemplateAzureAutoScalerRuleIDOut"].Direction = ParameterDirection.Output;

                #endregion Parameters

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();

                int iTemplateAzureAutoScalerRuleID = Convert.ToInt32(cmd.Parameters["@TemplateAzureAutoScalerRuleIDOut"].Value);
                this.TemplateAzureAutoScalerRuleID = iTemplateAzureAutoScalerRuleID;

                oPR.ObjectProcessed = this;
                oPR.Result += "Saved";
                return (oPR);

            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateAzureAutoScalerRuleSave", Exc.Message, LogPath);

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


        public static bool Delete(int TemplateAzureAutoScalerRuleID, string CnxnString, string LogPath)
        {
            SqlConnection Cnxn = new SqlConnection(CnxnString);
            try
            {

                SqlCommand cmd = new SqlCommand("spTemplateAzureAutoScalerRuleDelete", Cnxn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TemplateAzureAutoScalerRuleID", SqlDbType.Int));
                cmd.Parameters["@TemplateAzureAutoScalerRuleID"].Value = TemplateAzureAutoScalerRuleID;

                Cnxn.Open();
                cmd.ExecuteNonQuery();
                Cnxn.Close();
                return (true);
            }
            catch (Exception Exc)
            {
                Log.LogErr("TemplateAzureAutoScalerRuleDelete", Exc.Message, LogPath);
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
             * "rules": [
                     {
                       "metricTrigger": {
                         "metricName": "[parameters('metricName')]",
                         "metricNamespace": "",
                         "metricResourceUri": "[variables('targetResourceId')]",
                         "timeGrain": "PT5M",
                         "statistic": "Average",
                         "timeWindow": "PT10M",
                         "timeAggregation": "Average",
                         "operator": "GreaterThan",
                         "threshold": "[parameters('metricThresholdToScaleOut')]"
                       },
                       "scaleAction": {
                         "direction": "Increase",
                         "type": "PercentChangeCount",
                         "value": "[parameters('changePercentScaleOut')]",
                         "cooldown": "PT10M"
                       }
                     },
                     {
                       "metricTrigger": {
                         "metricName": "[parameters('metricName')]",
                         "metricNamespace": "",
                         "metricResourceUri": "[variables('targetResourceId')]",
                         "timeGrain": "PT5M",
                         "statistic": "Average",
                         "timeWindow": "PT10M",
                         "timeAggregation": "Average",
                         "operator": "LessThan",
                         "threshold": "[parameters('metricThresholdToScaleIn')]"
                       },
                       "scaleAction": {
                         "direction": "Decrease",
                         "type": "PercentChangeCount",
                         "value": "[parameters('changePercentScaleIn')]",
                         "cooldown": "PT10M"
                       }
                    }
                     */
            StringBuilder sb = new StringBuilder();
            sb.Append(F.Tab + F.Tab + F.Space(3) + F.CB + Environment.NewLine);
            sb.Append(F.Tab + F.Tab + F.Space(5) + F.Quote + "metricTrigger" + F.Quote + F.Colon + F.CB + Environment.NewLine);
            sb.Append(F.Tab + F.Tab + F.Space(7) + F.Quote + "metricName" + F.Quote + F.Colon + F.Quote + this.MetricTriggerMetricName + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Tab + F.Space(7) + F.Quote + "metricNamespace" + F.Quote + F.Colon + F.Quote + this.MetricTriggerMetricNamespace + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Tab + F.Space(7) + F.Quote + "metricResourceUri" + F.Quote + F.Colon + F.Quote + this.MetricTriggerMetricResourceURI + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Tab + F.Space(7) + F.Quote + "timeGrain" + F.Quote + F.Colon + F.Quote + this.MetricTriggerTimeGrain + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Tab + F.Space(7) + F.Quote + "statistic" + F.Quote + F.Colon + F.Quote + this.MetricTriggerStatistic + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Tab + F.Space(7) + F.Quote + "timeWindow" + F.Quote + F.Colon + F.Quote + this.MetricTriggerTimeWindow + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Tab + F.Space(7) + F.Quote + "timeAggregation" + F.Quote + F.Colon + F.Quote + this.MetricTriggerTimeAggregation + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Tab + F.Space(7) + F.Quote + "operator" + F.Quote + F.Colon + F.Quote + this.MetricTriggerOperator + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Tab + F.Space(7) + F.Quote + "threshold" + F.Quote + F.Colon + F.Quote + this.MetricTriggerThreshold + F.Quote + Environment.NewLine);
            sb.Append(F.Tab + F.Tab + F.Space(5) + F.ECB + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Tab + F.Space(5) + F.Quote + "scaleAction" + F.Quote + F.Colon + F.CB + Environment.NewLine);
            sb.Append(F.Tab + F.Tab + F.Space(7) + F.Quote + "direction" + F.Quote + F.Colon + F.Quote + this.ScaleActionDirection + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Tab + F.Space(7) + F.Quote + "type" + F.Quote + F.Colon + F.Quote + this.ScaleActionType + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Tab + F.Space(7) + F.Quote + "value" + F.Quote + F.Colon + F.Quote + this.ScaleActionValue + F.Quote + F.Comma + Environment.NewLine);
            sb.Append(F.Tab + F.Tab + F.Space(7) + F.Quote + "cooldown" + F.Quote + F.Colon + F.Quote + this.ScaleActionCooldown + F.Quote + Environment.NewLine);
            sb.Append(F.Tab + F.Tab + F.Space(5) + F.ECB + Environment.NewLine);
            sb.Append(F.Tab + F.Tab + F.Space(3) + F.ECB + F.Comma + Environment.NewLine);

            return (sb.ToString());
        }
        public static TemplateAutoScalerRule DummyData(TemplateAutoScaler s)
        {
            TemplateAutoScalerRule r = new TemplateAutoScalerRule();
            r.TemplateAzureAutoScalerID = s.TemplateAzureAutoScalerID;
            r.TemplateAzureAutoScalerRuleID = 1;
            r.MetricTriggerMetricName = "nameOfMetric";
            r.MetricTriggerMetricNamespace = "someNamespace";
            r.MetricTriggerMetricResourceURI = "someURI";
            r.MetricTriggerOperator = "gt,lt,or eq";
            r.MetricTriggerStatistic = "someStat";
            r.MetricTriggerThreshold = "theThreshold";
            r.MetricTriggerTimeAggregation = "aggregate";
            r.MetricTriggerTimeGrain = "timeGrainValue";
            r.MetricTriggerTimeWindow = "timeWindow";
            r.ScaleActionCooldown = "theCooldown";
            r.ScaleActionDirection = "in or out";
            r.ScaleActionType = "actionType";
            r.ScaleActionValue = "the % or number change";
            r.CreatedDate = DateTime.Today.ToString();
            r.ModifiedDate = DateTime.Today.ToString();
            return (r);
        }
    }
}
