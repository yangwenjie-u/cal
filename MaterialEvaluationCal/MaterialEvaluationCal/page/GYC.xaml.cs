using System;
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
    /// GYC.xaml 的交互逻辑
    /// </summary>
    public partial class GYC : Window
    {
        public GYC()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            #region old
            //String type = "钢筋原材料";
            //string err = "";
            //string tablename = "sGYC";

            //Dictionary<string, string> testDatas = new Dictionary<string, string>();

            //testDatas.Add("ZJ", this.ZJ.Text);  //直径
            //testDatas.Add("Z_CD1", this.Z_CD1.Text); //重量偏差长度(MM)1
            //testDatas.Add("Z_CD2", this.Z_CD2.Text); //重量偏差长度(MM)2

            //testDatas.Add("Z_CD3", this.Z_CD3.Text);  //重量偏差长度(MM)3
            //testDatas.Add("Z_CD4", this.Z_CD4.Text);  //重量偏差长度(MM)4
            //testDatas.Add("Z_CD5", this.Z_CD5.Text);  //重量偏差长度(MM)5

            //testDatas.Add("Z_ZZL", this.Z_ZZL.Text);  //总质量
            //testDatas.Add("KLHZ1", this.KLHZ1.Text);  //抗拉荷重（KN）1
            //testDatas.Add("KLHZ2", this.KLHZ2.Text);  //抗拉荷重（KN）2

            //testDatas.Add("QFHZ1", this.QFHZ1.Text);  //屈服荷重（KN）1
            //testDatas.Add("QFHZ2", this.QFHZ2.Text);  //屈服荷重（KN）2
            //testDatas.Add("DHJL1", this.DHJL1.Text);  //断后距L(MM)1
            //testDatas.Add("DHJL2", this.DHJL2.Text);  //断后距L(MM)2
            //testDatas.Add("LW1", this.LW1.Text);  //冷弯1
            //testDatas.Add("LW2", this.LW2.Text);  //冷弯2
            //testDatas.Add("SCL1", this.SCL1.Text);  //伸长率1
            //testDatas.Add("SCL2", this.SCL2.Text);  //伸长率2
            //testDatas.Add("FXWQ", this.FXWQ.Text);  //反向弯曲
            //testDatas.Add("DQJL01", this.DQJL01.Text);  //断前距L0(MM)1
            //testDatas.Add("DQJL02", this.DQJL01.Text);  //断前距L0(MM)2


            ////获取测试数据
            //string JsonhelperData = GetAfferentDataJson("S_GYC", "SGYC");

            ////获取主表数据
            ////string sqlStr = "select * from  MGYC";
            ////string m_data = GetDataJson(type, sqlStr, "M_GYC");


            ////获取帮助表数据
            //string extraDatajson = GetDataJson(type, "select * from GYCDJ", "BZ_GYC_DJ");

            //var listExtraData = Base.JsonHelper.GetDictionary(extraDatajson, type);

            ////重量偏差帮助表
            //string sqlStr = "select * from ZLPCB";
            //string zlpc_data = GetDataJson(type, sqlStr, "BZ_ZLPCB");
            //var retzlpcdata = Base.JsonHelper.GetDictionary(zlpc_data, type);
            //foreach (string item in retzlpcdata.Keys)
            //{
            //    if (!listExtraData.ContainsKey(item))
            //        listExtraData.Add(item, retzlpcdata[item]);
            //}


            //IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData = Base.JsonHelper.GetAfferentDictionary(JsonhelperData);
            //try
            //{
            //    //Calculates.GYC_钢筋原材料.GYC gYC = new Calculates.GYC_钢筋原材料.GYC();
            //    //gYC.Calculate(listExtraData, retData, out err);
            //    //gYC.Calc();
            //}
            //catch (Exception ex)
            //{

            //}
            #endregion

            #region
            string type = "GYC 钢筋原材料";
            string err = "";

            //string sqlStr = "select * from SGYC where RECID='19125192353795645278146'";
            string sqlStr = "select * from S_GYC where RECID='19125759542332900579927'";
            //获取帮助表数据
            //string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from GYCDJ ", "BZ_GYC_DJ");
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from BZ_GYC_DJ ", "BZ_GYC_DJ");
            string extraZLPCBjson = Base.JsonHelper.GetDataJson(type, "select * from ZLPCB ", "BZ_ZLPCB");
            //string extraHSBjson = Base.JsonHelper.GetDataJson(type, "select * from CGLhsb ", "BZ_SC_HSB");
            //string extraCSjson = Base.JsonHelper.GetDataJson(type, "select * from CGLDRCS ", "BZ_CGL_CS");
            //string extraFFjson = Base.JsonHelper.GetDataJson(type, "select * from CGLDRFF ", "BZ_CGL_FF");
            //string extraGMDJBjson = Base.JsonHelper.GetDataJson(type, "select * from CGLGMDJB ", "BZ_CGLGMDJB");


            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);
            var extraZLPCBjsonData = Base.JsonHelper.GetDictionary(extraZLPCBjson, type);
            //var extraHSBjsonData = Base.JsonHelper.GetDictionary(extraHSBjson, type);
            //var extraCSjsonData = Base.JsonHelper.GetDictionary(extraCSjson, type);
            //var extraFFjsonData = Base.JsonHelper.GetDictionary(extraFFjson, type);
            //var extraGMDJBjsonData = Base.JsonHelper.GetDictionary(extraGMDJBjson, type);


            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_GYC_DJ", extraDJjsonData["BZ_GYC_DJ"]);
            listExtraData.Add("BZ_GYC_ZLPCB", extraZLPCBjsonData["BZ_GYC_ZLPCB"]);
            //listExtraData.Add("BZ_CGL_HSB", extraHSBjsonData["BZ_CGL_HSB"]);
            //listExtraData.Add("BZ_CGL_CS", extraCSjsonData["BZ_CGL_CS"]);
            //listExtraData.Add("BZ_CGL_FF", extraFFjsonData["BZ_CGL_FF"]);
            //listExtraData.Add("BZ_CGLGMDJB", extraGMDJBjsonData["BZ_CGLGMDJB"]);

            //获取测试数据
            //string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MGYC where RECID='19125192353795645278146' ", "M_GYC");
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from M_GYC where RECID='19125759542332900579927' ", "M_GYC");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_GYC", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.GYC gYC = new Calculates.GYC();
            gYC.Calculate(listExtraData, retSData, out err);

            try
            {
                gYC.Calc();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
            #endregion
        }


        #region old
        //public static string GetDataJson(string type, string sqlstr, string tableName, Dictionary<string, string> testDatas = null)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("{\"calcData\":{\"" + type + "\":");

        //    string sql = string.Format(@sqlstr);
        //    Base.SqlBase sqlbase = new Base.SqlBase("jcjt");
        //    DataSet ds = sqlbase.ExecuteDataset(sql);
        //    ds.Tables[0].TableName = tableName;
        //    string json = Base.JsonHelper.SerializeObject(ds);
        //    sb.Append(json);
        //    sb.Append("},\"code\":1,\"message\":\"成功\"}");
        //    Base.JsonHelper.GetDictionary(sb.ToString(), type);
        //    return sb.ToString();
        //}

        //public static string GetAfferentDataJson(string type, string tableName)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    StringBuilder sb2 = new StringBuilder();
        //    sb.Append("{\"calcData\":[{");
        //    string sql = string.Format(@"select top 1 * from " + tableName);
        //    string m_sql = string.Format(@"select top 1 * from MGYC");
        //    Base.SqlBase sqlbase = new Base.SqlBase("local");
        //    DataSet ds = sqlbase.ExecuteDataset(sql);
        //    DataSet ds_m = sqlbase.ExecuteDataset(m_sql);
        //    DataTable dt = ds.Tables[0];
        //    DataTable dt_m = ds_m.Tables[0];
        //    string[] jcxm_list = dt.Rows[0]["jcxm"].ToString().Split('、');
        //    foreach (string item in jcxm_list)
        //    {
        //        sb2.Append("\"" + item + "\":{");
        //        DataTable dt_json = ToDataTable(dt.Select(" jcxm like '%" + item + "%'"));
        //        string json = Base.JsonHelper.SerializeObject(dt_json);
        //        string m_json = Base.JsonHelper.SerializeObject(dt_m);
        //        sb2.Append("\"" + type + "\":" + json + "");
        //        sb2.Append(",\"M_GYC\":" + m_json + "");
        //        sb2.Append(",\"S_BY_RW_XQ\":[{");
        //        sb2.Append("\"recid\":\"19085206791636631332933\",");
        //        sb2.Append("\"sjwcjssj\":\"1900/1/1 0:00:00\",");
        //        sb2.Append("\"JCJG\":\"不合格\",");
        //        sb2.Append("\"JCJGMS\":\"\"");
        //        sb2.Append("}]");
        //        sb2.Append("},");
        //    }
        //    sb.Append(sb2.ToString().TrimEnd(','));
        //    sb.Append("}],\"code\":1, \"message\":\"成功\"}");
        //    return sb.ToString();
        //}


        //public static DataTable ToDataTable(DataRow[] rows)
        //{
        //    if (rows == null || rows.Length == 0) return null;
        //    DataTable tmp = rows[0].Table.Clone();  // 复制DataRow的表结构  
        //    foreach (DataRow row in rows)
        //        tmp.Rows.Add(row.ItemArray);  // 将DataRow添加到DataTable中  
        //    return tmp;
        //}
        #endregion
    }
}
