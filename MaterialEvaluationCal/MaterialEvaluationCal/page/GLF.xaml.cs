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
    /// GLF.xaml 的交互逻辑
    /// </summary>
    public partial class GLF : Window
    {
        public GLF()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "机械连接复试";
            string tablename = "S_GLF";
            string err = "";
            string sqlStr = "select top  1 * from S_GLF where  RECID ='00000259' ";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from BZ_GLF_DJ ", "BZ_GLF_DJ");
            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);
            string extraXBDJjson = Base.JsonHelper.GetDataJson(type, "select * from BZ_GLJ_XBDJ ", "BZ_GLJ_XBDJ");
            var extraXBDJjsonData = Base.JsonHelper.GetDictionary(extraXBDJjson, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_GLF_DJ", extraDJjsonData["BZ_GLF_DJ"]);
            listExtraData.Add("BZ_GLJ_XBDJ", extraXBDJjsonData["BZ_GLJ_XBDJ"]);

            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from  M_GLF where recid ='00000242' ", "M_GLF");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson("S_GLF", sqlStr, m_json);

            var retData = Base.JsonHelper.GetAfferentDictionary(retSDataJosn);
            Calculates.GLF gLF = new Calculates.GLF();
            //gLF.Calculate(listExtraData,  retData, out err);

            //gLF.Calc(listExtraData, ref retData, ref err);

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
