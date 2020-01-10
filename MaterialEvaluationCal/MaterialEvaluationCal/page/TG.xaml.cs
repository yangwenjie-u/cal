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
    /// TG.xaml 的交互逻辑
    /// </summary>
    public partial class TG : Window
    {
        public TG()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "电工套管";
            string err = "";

            string sqlStr = "select * from S_TG where RECID='00000833'  ";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from BZ_TG_DJ", "BZ_TG_DJ");
            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);


            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from M_TG where RECID='00000786' ", "M_TG");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson("S_TG", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionary(retSDataJosn);

            IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData = new Dictionary<string, IDictionary<string, IList<IDictionary<string, string>>>>();

            Calculates.TG.Calc(extraDJjsonData, ref retSData, ref err);
        }
    }
}
