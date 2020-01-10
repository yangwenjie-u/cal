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
    /// SPB.xaml 的交互逻辑
    /// </summary>
    public partial class SPB : Window
    {
        public SPB()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String type = "砂浆配合比";
            string err = "";
            string sqlStr = "select * from SSPB where JYDBH =110900057";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from SPBDJ ", "BZ_SPB_DJ");
            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_TBH_DJ", extraDJjsonData["BZ_TBH_DJ"]);

            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from  MSPB where JYDBH =110900057", "M_SPB");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_SPB", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.SPB spb = new Calculates.SPB();
            spb.Calculate(listExtraData, retSData, out err);
            spb.Calc();
        }
    }
}
