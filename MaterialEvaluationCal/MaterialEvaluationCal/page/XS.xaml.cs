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
    /// XS.xaml 的交互逻辑
    /// </summary>
    public partial class XS : Window
    {
        public XS()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "柔性泡沫橡塑绝热制品";
            string tablename = "S_XS";
            string err = "";
            string sqlStr = "select top  1 * from SXS where  RECID ='00000259' ";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from XSDJ ", "BZ_XS_DJ");
            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);

            string extraYZSKBjson = Base.JsonHelper.GetDataJson(type, "select * from YZSKB ", "YZSKB");
            var extraYZSKBjsonData = Base.JsonHelper.GetDictionary(extraYZSKBjson, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_XS_DJ", extraDJjsonData["BZ_XS_DJ"]);

            listExtraData.Add("YZSKB", extraDJjsonData["YZSKB"]);

            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from  MXS where recid ='00000242' ", "M_XS");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson("S_XS", sqlStr, m_json);

            var retData = Base.JsonHelper.GetAfferentDictionary(retSDataJosn);
            //Calculates.XS xS = new Calculates.XS();
            //xS.Calculate(listExtraData,  retData, out err);

            //xS.Calc(listExtraData, ref retData, ref err);

        }
        public static string GetDataJson(string type, string sqlstr, string tableName, Dictionary<string, string> testDatas = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"calcData\":{\"" + type + "\":");

            string sql = string.Format(@sqlstr);
            Base.SqlBase sqlbase = new Base.SqlBase("jcjt");
            DataSet ds = sqlbase.ExecuteDataset(sql);

            if (testDatas != null)
            {
                ds.Tables[0].Rows[0][0] = "100";

                ds.Tables[0].Rows[0][1] = testDatas["KLHZ1"];
                ds.Tables[0].Rows[0][2] = testDatas["KLHZ2"];
                ds.Tables[0].Rows[0][3] = testDatas["KLHZ3"];

                ds.Tables[0].Rows[0][4] = testDatas["DKJ1"];
                ds.Tables[0].Rows[0][5] = testDatas["DKJ2"];
                ds.Tables[0].Rows[0][6] = testDatas["DKJ3"];

                ds.Tables[0].Rows[0][7] = testDatas["SCZJ1"];
                ds.Tables[0].Rows[0][8] = testDatas["SCZJ2"];
                ds.Tables[0].Rows[0][9] = testDatas["SCZJ3"];

            }
            ds.Tables[0].TableName = tableName;
            string json = Base.JsonHelper.SerializeObject(ds);
            sb.Append(json);
            sb.Append("},\"code\":1,\"message\":\"成功\"}");
            Base.JsonHelper.GetDictionary(sb.ToString(), type);
            return sb.ToString();
        }
    }
}
