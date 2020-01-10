﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MaterialEvaluationCal.page
{
    /// <summary>
    /// MCxaml.xaml 的交互逻辑
    /// </summary>
    public partial class MC : Window
    {
        public MC()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String type = "铝合金门窗";
            string err = "";
            string tablename = "SMC";

            Dictionary<string, string> testDatas = new Dictionary<string, string>();

            testDatas.Add("KFYSJYQ", this.KFYSJYQ.Text);  //抗风压设计要求
            testDatas.Add("QMXQ1YQ", this.QMXQ1YQ.Text); //气密性Q1设计要求
            testDatas.Add("QMXQ2YQ", this.QMXQ2YQ.Text); //气密性Q2设计要求
            testDatas.Add("SMXSJYQ", this.SMXSJYQ.Text); //水密性设计要求
            testDatas.Add("GGXH", this.GGXH.Text); //规格型号
            testDatas.Add("KFYP1", this.KFYP1.Text); //抗风压P1值
            testDatas.Add("KFYP2", this.KFYP2.Text); //抗风压P2值
            testDatas.Add("KFYP3", this.KFYP3.Text); //抗风压P3值
            testDatas.Add("FCQMZY", this.FCQMZY.Text);  //单位缝长气密性(正压)
            testDatas.Add("FCQMFY", this.FCQMFY.Text);  //单位缝长气密性(负压)
            testDatas.Add("MJQMZY", this.MJQMZY.Text);  //单位面积气密性(正压)
            testDatas.Add("MJQMFY", this.MJQMFY.Text);  //单位面积气密性(负压)
            testDatas.Add("WDSMXYL", this.WDSMXYL.Text);  //稳定水密性压力值
            testDatas.Add("BDSMXYL", this.FCQMFY.Text);  //波动水密性压力值

            //获取测试数据
            string JsonhelperData = GetAfferentDataJson("S_MC", "SMC");

            //获取主表数据
            //string sqlStr = "select * from  MGYC";
            //string m_data = GetDataJson(type, sqlStr, "M_GYC");


            //获取帮助表数据
            string extraDatajson = GetDataJson(type, "select * from MCDJ", "BZ_MC_DJ");

            var listExtraData = Base.JsonHelper.GetDictionary(extraDatajson, type);

            //kfyjb
            string kfysql = "select * from kfyjb";
            string kfyjb_data = GetDataJson(type, kfysql, "BZ_KFYJB");
            var retzlpcdata = Base.JsonHelper.GetDictionary(kfyjb_data, type);
            foreach (string item in retzlpcdata.Keys)
            {
                if (!listExtraData.ContainsKey(item))
                    listExtraData.Add(item, retzlpcdata[item]);
            }

            //qmxjb
            string qmxsql = "select * from qmxjb";
            string qmxjb_data = GetDataJson(type, qmxsql, "BZ_QMXJB");
            retzlpcdata = Base.JsonHelper.GetDictionary(qmxjb_data, type);
            foreach (string item in retzlpcdata.Keys)
            {
                if (!listExtraData.ContainsKey(item))
                    listExtraData.Add(item, retzlpcdata[item]);
            }

            //qmxjb
            string smxsql = "select * from smxjb";
            string smxjb_data = GetDataJson(type, smxsql, "BZ_SMXJB");
            retzlpcdata = Base.JsonHelper.GetDictionary(smxjb_data, type);
            foreach (string item in retzlpcdata.Keys)
            {
                if (!listExtraData.ContainsKey(item))
                    listExtraData.Add(item, retzlpcdata[item]);
            }

            //重量偏差帮助表
            //string sqlStr = "select * from ZLPCB";
            //string zlpc_data = GetDataJson(type, sqlStr, "BZ_ZLPCB");
            //var retzlpcdata = Base.JsonHelper.GetDictionary(zlpc_data, type);
            //foreach (string item in retzlpcdata.Keys)
            //{
            //    if (!listExtraData.ContainsKey(item))
            //        listExtraData.Add(item, retzlpcdata[item]);
            //}


            IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData = Base.JsonHelper.GetAfferentDictionary(JsonhelperData);
            //var retMData = Base.JsonHelper.GetDictionary(m_data, type);

            //IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData = new Dictionary<string, IDictionary<string, IList<IDictionary<string, string>>>>();

            //foreach (string key in retMData.Keys)
            //{
            //    if (!retSData.ContainsKey(key))
            //        retSData.Add(key, retMData[key]);
            //}
            //retData.Add("钢筋原材料", retSData);

            Calculates.MC_铝合金门窗.MC.Calc(listExtraData, ref retData, ref err);
        }

        public static string GetDataJson(string type, string sqlstr, string tableName, Dictionary<string, string> testDatas = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"calcData\":{\"" + type + "\":");

            string sql = string.Format(@sqlstr);
            Base.SqlBase sqlbase = new Base.SqlBase("jcjt");
            DataSet ds = sqlbase.ExecuteDataset(sql);
            ds.Tables[0].TableName = tableName;
            string json = Base.JsonHelper.SerializeObject(ds);
            sb.Append(json);
            sb.Append("},\"code\":1,\"message\":\"成功\"}");
            Base.JsonHelper.GetDictionary(sb.ToString(), type);
            return sb.ToString();
        }

        public static string GetAfferentDataJson(string type, string tableName)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            sb.Append("{\"calcData\":[{");
            string sql = string.Format(@"select top 1 * from " + tableName);
            string m_sql = string.Format(@"select top 1 * from MMC");
            Base.SqlBase sqlbase = new Base.SqlBase("local");
            DataSet ds = sqlbase.ExecuteDataset(sql);
            DataSet ds_m = sqlbase.ExecuteDataset(m_sql);
            DataTable dt = ds.Tables[0];
            DataTable dt_m = ds_m.Tables[0];
            string[] jcxm_list = dt.Rows[0]["jcxm"].ToString().Split('、');
            foreach (string item in jcxm_list)
            {
                sb2.Append("\"" + item + "\":{");
                DataTable dt_json = ToDataTable(dt.Select(" jcxm like '%" + item + "%'"));
                string json = Base.JsonHelper.SerializeObject(dt_json);
                sb2.Append("\"" + type + "\":" + json + "");
                string m_json =Base.JsonHelper.SerializeObject(dt_m);
                sb2.Append(",\"M_MC\":" + m_json + "");
                sb2.Append(",\"S_BY_RW_XQ\":[{");
                sb2.Append("\"recid\":\"19085206791636631332933\",");
                sb2.Append("\"sjwcjssj\":\"1900/1/1 0:00:00\",");
                sb2.Append("\"JCJG\":\"不合格\",");
                sb2.Append("\"JCJGMS\":\"\"");
                sb2.Append("}]");
                sb2.Append("},");
            }
            sb.Append(sb2.ToString().TrimEnd(','));
            sb.Append("}],\"code\":1, \"message\":\"成功\"}");
            return sb.ToString();
        }


        public static DataTable ToDataTable(DataRow[] rows)
        {
            if (rows == null || rows.Length == 0) return null;
            DataTable tmp = rows[0].Table.Clone();  // 复制DataRow的表结构  
            foreach (DataRow row in rows)
                tmp.Rows.Add(row.ItemArray);  // 将DataRow添加到DataTable中  
            return tmp;
        }
    }
}
