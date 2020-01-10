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
    /// LNG.xaml 的交互逻辑
    /// </summary>
    public partial class LNG : Window
    {
        public LNG()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "大六角螺栓扭矩系数";
            string err = "";

            string sqlStr = "select * from SLNJ  where recid= '610'  ";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from LNJDJ", "BZ_LNJ_DJ");
            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);
            
      
            //获取测试数据
            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson("S_LNJ", sqlStr);

            //var retMData = Base.JsonHelper.GetDictionary(m_data, type);
            var retSData = Base.JsonHelper.GetAfferentDictionary(retSDataJosn);

            IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData = new Dictionary<string, IDictionary<string, IList<IDictionary<string, string>>>>();

            Calculates.LNJ.Calc(extraDJjsonData, ref retSData, ref err);
        }
    }
}
