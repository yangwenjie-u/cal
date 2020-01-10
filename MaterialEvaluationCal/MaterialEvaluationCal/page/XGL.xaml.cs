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
    /// XGL.xaml 的交互逻辑
    /// </summary>
    public partial class XGL : Window
    {
        public XGL()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //string type = "砂(国标砌筑用)";
            //string err = "";

            //string sqlStr = "select * from S_XGL where RECID='00000275'  ";

            ////获取帮助表数据
            //string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from jmcdj", "BZ_XGL_DJ");
            //string extraHSBjson = Base.JsonHelper.GetDataJson(type, "select * from BZ_XGLHSB", "BZ_XGLHSB");
            //var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);
            //var extraHSBjsonData = Base.JsonHelper.GetDictionary(extraHSBjson, type);

            //Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            //listExtraData.Add("BZ_XGL_DJ", extraDJjsonData["BZ_XGL_DJ"]);
            //listExtraData.Add("BZ_XGLHSB", extraHSBjsonData["BZ_JMCBWFJ"]);

            ////获取测试数据
            //string m_json = Base.JsonHelper.GetMdataJson("local", "select * from M_XGL where RECID='00000275' ", "M_XGL");

            //var retSDataJosn = Base.JsonHelper.GetAfferentDataJson("S_XGL", sqlStr, m_json);
            //var retSData = Base.JsonHelper.GetAfferentDictionary(retSDataJosn);

            //IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData = new Dictionary<string, IDictionary<string, IList<IDictionary<string, string>>>>();

            //Calculates.XGL.Calc(listExtraData, ref retSData, ref err);
        }
    }
}
