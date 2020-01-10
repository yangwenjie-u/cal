using System;
using System.Collections.Generic;
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
using System.Data;

namespace MaterialEvaluationCal.page
{
    /// <summary>
    /// JBS.xaml 的交互逻辑
    /// </summary>
    public partial class JBS : Window
    {
        public JBS()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String type = "水泥搅拌桩";
            string err = "";
            string tablename = "SJBS";

            Dictionary<string, string> testDatas = new Dictionary<string, string>();

            


            //获取测试数据
            string JsonhelperData = GetAfferentDataJson("S_JBS", "SJBS");

            //获取主表数据
            //string sqlStr = "select * from  MGYC";
            //string m_data = GetDataJson(type, sqlStr, "M_GYC");


            //获取帮助表数据
            string extraDatajson = GetDataJson(type, "select * from JBSDJ", "BZ_JBS_DJ");

            var listExtraData = Base.JsonHelper.GetDictionary(extraDatajson, type);

            //水泥搅拌桩帮助表
            string sqlStr = "select * from JBGG";
            string JBGG_data = GetDataJson(type, sqlStr, "BZ_JBGG");
            var retzlpcdata = Base.JsonHelper.GetDictionary(JBGG_data, type);
            foreach (string item in retzlpcdata.Keys)
            {
                if (!listExtraData.ContainsKey(item))
                    listExtraData.Add(item, retzlpcdata[item]);
            }


            IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData = Base.JsonHelper.GetAfferentDictionary(JsonhelperData);
            //var retMData = Base.JsonHelper.GetDictionary(m_data, type);

            //IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData = new Dictionary<string, IDictionary<string, IList<IDictionary<string, string>>>>();

            //foreach (string key in retMData.Keys)
            //{
            //    if (!retSData.ContainsKey(key))
            //        retSData.Add(key, retMData[key]);
            //}
            //retData.Add("钢筋原材料", retSData);

            Calculates.JBS_水泥搅拌桩.JBS.Calc(listExtraData, ref retData, ref err);
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
            string m_sql = string.Format(@"select top 1 * from MJBS");
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
                string m_json = Base.JsonHelper.SerializeObject(dt_m);
                sb2.Append(",\"M_JBS\":" + m_json + "");
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
