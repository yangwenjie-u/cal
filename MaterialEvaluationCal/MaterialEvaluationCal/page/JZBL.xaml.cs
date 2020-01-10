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
    /// JZBL.xaml 的交互逻辑
    /// </summary>
    public partial class JZBL : Window
    {
        public JZBL()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "建筑玻璃";
            string err = "";

            string sqlStr = "select * from S_ZBL where RECID='00000250'  ";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from BZ_ZBL_DJ", "BZ_ZBL_DJ");
            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);


            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from M_ZBL where RECID='00000233' ", "M_ZBL");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson("S_ZBL", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionary(retSDataJosn);

            IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData = new Dictionary<string, IDictionary<string, IList<IDictionary<string, string>>>>();

            Calculates.JZBL.Calc(extraDJjsonData, ref retSData, ref err);
        }
    }
}
