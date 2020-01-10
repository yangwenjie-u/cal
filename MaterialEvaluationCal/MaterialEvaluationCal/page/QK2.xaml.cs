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
    /// QK2.xaml 的交互逻辑
    /// </summary>
    public partial class QK2 : Window
    {
        public QK2()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "加气切块";
            string err = "";

            string sqlStr = "select top 1 * from SQK2   ";


            //获取帮助表数据
            //string extraDatajson = GetDataJson(type, "select * from BZ_GLJ_DJ", "BZ_GLJ_DJ");
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from QK2DJ", "BZ_QK2_DJ");
            string extraQDJBjson = Base.JsonHelper.GetDataJson(type, "select * from QK2QDJB", "BZ_QK2_QDJB");
            string extraGMDJBjson = Base.JsonHelper.GetDataJson(type, "select * from QK2GMDJB", "BZ_QK2_GMDJB");
            string extraGKDjson = Base.JsonHelper.GetDataJson(type, "select * from QK2GKD", "BZ_QK2_GKD");

            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);
            var extraaQDJBjsonData = Base.JsonHelper.GetDictionary(extraQDJBjson, type);
            var extraGMDJBjsonData = Base.JsonHelper.GetDictionary(extraGMDJBjson, type);
            var extraGKDjsonData = Base.JsonHelper.GetDictionary(extraGKDjson, type);


            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();

            listExtraData.Add("BZ_QK2_DJ", extraDJjsonData["BZ_QK2_DJ"]);
            listExtraData.Add("BZ_QK2_QDJB", extraaQDJBjsonData["BZ_QK2_QDJB"]);
            listExtraData.Add("BZ_QK2_GMDJB", extraGMDJBjsonData["BZ_QK2_GMDJB"]);
            listExtraData.Add("BZ_QK2_GKD", extraGKDjsonData["BZ_QK2_GKD"]);

            //获取测试数据
            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson("S_QK2", sqlStr);


            //var retMData = Base.JsonHelper.GetDictionary(m_data, type);
            var retSData = Base.JsonHelper.GetAfferentDictionary(retSDataJosn);

            IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData = new Dictionary<string, IDictionary<string, IList<IDictionary<string, string>>>>();

            Calculates.GBT_11969_2008.Calc(listExtraData, ref retSData, ref err);
        }
    }
}
