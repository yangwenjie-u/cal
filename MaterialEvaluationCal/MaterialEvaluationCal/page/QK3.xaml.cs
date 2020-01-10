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
    /// QK3.xaml 的交互逻辑
    /// </summary>
    public partial class QK3 : Window
    {
        public QK3()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "轻集料空心砌块";
            string err = "";

            string sqlStr = "select * from SQK3 where RECID='16384'";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from QK3DJ ", "BZ_QK3_DJ");
            //string extraGGjson = Base.JsonHelper.GetDataJson(type, "select * from QK3GG ", "BZ_QK3_GG");
            //string extraCSjson = Base.JsonHelper.GetDataJson(type, "select * from QK3DRCS ", "BZ_QK3_CS");
            //string extraFFjson = Base.JsonHelper.GetDataJson(type, "select * from QK3DRFF ", "BZ_QK3_FF");
            string extraGMDJBjson = Base.JsonHelper.GetDataJson(type, "select * from QK3GMDJB ", "BZ_QK3GMDJB");


            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);
            //var extraGGjsonData = Base.JsonHelper.GetDictionary(extraGGjson, type);
            //var extraCSjsonData = Base.JsonHelper.GetDictionary(extraCSjson, type);
            //var extraFFjsonData = Base.JsonHelper.GetDictionary(extraFFjson, type);
            var extraGMDJBjsonData = Base.JsonHelper.GetDictionary(extraGMDJBjson, type);


            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_QK3_DJ", extraDJjsonData["BZ_QK3_DJ"]);
            //listExtraData.Add("BZ_QK3_GG", extraGGjsonData["BZ_QK3_GG"]);
            //listExtraData.Add("BZ_QK3_CS", extraCSjsonData["BZ_QK3_CS"]);
            //listExtraData.Add("BZ_QK3_FF", extraFFjsonData["BZ_QK3_FF"]);
            listExtraData.Add("BZ_QK3GMDJB", extraGMDJBjsonData["BZ_QK3GMDJB"]);

            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MQK3 where RECID='16384' ", "M_QK3");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_QK3", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.QK3 qK3 = new Calculates.QK3();
            qK3.Calculate(listExtraData, retSData, out err);
            qK3.Calc();
        }
    }
}
