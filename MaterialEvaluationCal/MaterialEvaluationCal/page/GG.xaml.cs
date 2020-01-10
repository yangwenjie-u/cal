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
    /// GG.xaml 的交互逻辑
    /// </summary>
    public partial class GG : Window
    {
        public GG()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "钢管（国际）";
            string err = "";

            string sqlStr = "select top 1 * from SGG   ";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from GGDJ", "BZ_GG_DJ");
            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);


            //获取测试数据
            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson("S_GG", sqlStr);

            var retSData = Base.JsonHelper.GetAfferentDictionary(retSDataJosn);

            IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData = new Dictionary<string, IDictionary<string, IList<IDictionary<string, string>>>>();

            Calculates.GBT3091_2015.Calc(extraDJjsonData, ref retSData, ref err);
        }
    }
}
