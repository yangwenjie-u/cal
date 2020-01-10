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
    /// WZ.xaml 的交互逻辑
    /// </summary>
    public partial class WZ : Window
    {
        public WZ()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "节能取芯";
            string tablename = "SWZ";
            string err = "";
            string sqlStr = "select top  1 * from SWZ where  RECID ='1381' ";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from WZDJ ", "BZ_WZ_DJ");
            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);


            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_WZ_DJ", extraDJjsonData["BZ_WZ_DJ"]);

            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from  MWZ where recid ='1381' ", "M_WZ");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_WZ", sqlStr, m_json);

            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);
            Calculates.WZ wZ = new Calculates.WZ();
            wZ.Calculate(listExtraData, retSData, out err);
            wZ.Calc();
            //wZ.Calculate(listExtraData,  retData, out err);

            //wZ.Calc(listExtraData, ref retData, ref err);

        }
        //public static string GetDataJson(string type, string sqlstr, string tableName, Dictionary<string, string> testDatas = null)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("{\"calcData\":{\"" + type + "\":");

        //    string sql = string.Format(@sqlstr);
        //    Base.SqlBase sqlbase = new Base.SqlBase("jcjt");
        //    DataSet ds = sqlbase.ExecuteDataset(sql);

        //    if (testDatas != null)
        //    {
        //        ds.Tables[0].Rows[0][0] = "100";

        //        ds.Tables[0].Rows[0][1] = testDatas["KLHZ1"];
        //        ds.Tables[0].Rows[0][2] = testDatas["KLHZ2"];
        //        ds.Tables[0].Rows[0][3] = testDatas["KLHZ3"];

        //        ds.Tables[0].Rows[0][4] = testDatas["DKJ1"];
        //        ds.Tables[0].Rows[0][5] = testDatas["DKJ2"];
        //        ds.Tables[0].Rows[0][6] = testDatas["DKJ3"];

        //        ds.Tables[0].Rows[0][7] = testDatas["SCZJ1"];
        //        ds.Tables[0].Rows[0][8] = testDatas["SCZJ2"];
        //        ds.Tables[0].Rows[0][9] = testDatas["SCZJ3"];

        //    }
        //    ds.Tables[0].TableName = tableName;
        //    string json = Base.JsonHelper.SerializeObject(ds);
        //    sb.Append(json);
        //    sb.Append("},\"code\":1,\"message\":\"成功\"}");
        //    Base.JsonHelper.GetDictionary(sb.ToString(), type);
        //    return sb.ToString();
        //}
    }
}
