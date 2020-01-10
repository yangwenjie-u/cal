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
    /// TBH.xaml 的交互逻辑
    /// </summary>
    public partial class TBH : Window
    {
        public TBH()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "钢筋保护层";
            string err = "";

            string sqlStr = "select top 1  * from STBH where RECID=997";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from TBHDJ ", "BZ_TBH_DJ");
            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_TBH_DJ", extraDJjsonData["BZ_TBH_DJ"]);

            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from  MTBH where WTBH ='201700033' ", "M_TBH");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_TBH", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.TBH tbh = new Calculates.TBH();
            tbh.Calculate(listExtraData, retSData, out err);
            tbh.Calc();
        }
    }
}
