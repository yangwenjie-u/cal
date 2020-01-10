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
    /// JMC.xaml 的交互逻辑
    /// </summary>
    public partial class JMC : Window
    {
        public JMC()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "门窗传热系数";
            string err = "";

            string sqlStr = "select top 1 * from SJMC where recid ='3430'  ";

            //获取帮助表数据
            //string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from jmcdj", "BZ_JMC_DJ");
            //string extraBWFJjson = Base.JsonHelper.GetDataJson(type, "select * from jmcbwfj", "BZ_JMCBWFJ");
            //var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);
            //var extraBWFjsonData = Base.JsonHelper.GetDictionary(extraBWFJjson, type);

            //Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            //listExtraData.Add("BZ_JMC_DJ", extraDJjsonData["BZ_JMC_DJ"]);
            //listExtraData.Add("BZ_JMCBWFJ", extraBWFjsonData["BZ_JMCBWFJ"]);

            ////获取测试数据
            //string m_json = Base.JsonHelper.GetMdataJson("local", "select * from  MJMC  where recid ='3430' ", "M_JMC");

            //var retSDataJosn = Base.JsonHelper.GetAfferentDataJson("S_JMC", sqlStr, m_json);
            //var retSData = Base.JsonHelper.GetAfferentDictionary(retSDataJosn);

            //Calculates.JMC.Calc(listExtraData, ref retSData, ref err);
        }
    }
}
