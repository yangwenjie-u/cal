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
    /// BTS.xaml 的交互逻辑
    /// </summary>
    public partial class BTS : Window
    {
        public BTS()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "加固正拉粘结强度";
            string err = "";

            string sqlStr = "select * from SBTS where RECID='77'";
            
            //获取帮助表数据
            //string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from BTSDJ ", "BZ_BTS_DJ");
            //var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            //listExtraData.Add("BZ_BTS_DJ", extraDJjsonData["BZ_BTS_DJ"]);

            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MBTS where RECID ='77' ", "M_BTS");

            //更改处 GetAfferentDataJson2  GetAfferentDictionaryNew
            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_BTS", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.BTS bTS = new Calculates.BTS();
            bTS.Calculate(listExtraData, retSData, out err);
            bTS.Calc();

        }
    }
}
