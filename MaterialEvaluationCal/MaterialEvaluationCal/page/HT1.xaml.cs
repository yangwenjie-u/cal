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

namespace MaterialEvaluationCal.page
{
    /// <summary>
    /// HT1.xaml 的交互逻辑
    /// </summary>
    public partial class HT1 : Window
    {
        public HT1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "混凝土回弹";
            string err = "";

            //获取从表数据
            string sqlStr = "select * from SHT1 where RECID='6545'";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from HTDJ ", "HTDJ");
            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);
            string extraDJjson1 = Base.JsonHelper.GetDataJson(type, "select * from XHT1 ", "XHT1");
            var extraDJjsonData1 = Base.JsonHelper.GetDictionary(extraDJjson1, type);
            string extraDJjson2 = Base.JsonHelper.GetDataJson(type, "select * from EDITH ", "EDITH");
            var extraDJjsonData2 = Base.JsonHelper.GetDictionary(extraDJjson2, type);
            string extraDJjson3 = Base.JsonHelper.GetDataJson(type, "select * from EDITS ", "EDITS");
            var extraDJjsonData3 = Base.JsonHelper.GetDictionary(extraDJjson3, type);
            string extraDJjson4 = Base.JsonHelper.GetDataJson(type, "select * from HTHSBNEW ", "HTHSBNEW");
            var extraDJjsonData4 = Base.JsonHelper.GetDictionary(extraDJjson4, type);
            string extraDJjson5 = Base.JsonHelper.GetDataJson(type, "select * from BSHTHSB ", "BSHTHSB");
            var extraDJjsonData5 = Base.JsonHelper.GetDictionary(extraDJjson5, type);
            string extraDJjson6 = Base.JsonHelper.GetDataJson(type, "select * from HTHSB ", "HTHSB");
            var extraDJjsonData6 = Base.JsonHelper.GetDictionary(extraDJjson6, type);
            //string extraDJjson7 = Base.JsonHelper.GetDataJson(type, "select * from YHT1 where WTBH = '201900004'", "YHT1");
            //var extraDJjsonData7 = Base.JsonHelper.GetDictionary(extraDJjson7, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_HT1_DJ", extraDJjsonData["HTDJ"]);
            listExtraData.Add("BZ_XHT1", extraDJjsonData1["XHT1"]);
            listExtraData.Add("BZ_EDITH", extraDJjsonData2["EDITH"]);
            listExtraData.Add("BZ_EDITS", extraDJjsonData3["EDITS"]);
            listExtraData.Add("BZ_HTHSBNEW", extraDJjsonData4["HTHSBNEW"]);
            listExtraData.Add("BZ_BSHTHSB", extraDJjsonData5["BSHTHSB"]);
            listExtraData.Add("BZ_HTHSB", extraDJjsonData6["HTHSB"]);
            //listExtraData.Add("BZ_YHT1", extraDJjsonData7["YHT1"]);

            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MHT1 where WTBH = '201900004' ", "M_HT1");
            //获取Y数据表
            string y_json = Base.JsonHelper.GetMdataJson("local", "selecture* from YHT1 where WTBH = '201900004'' ", "Y_HT1");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_HT1", sqlStr, m_json, y_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.HT1 ht1 = new Calculates.HT1();
            ht1.Calculate(listExtraData, retSData, out err);
            ht1.Calc();



        }
    }
}