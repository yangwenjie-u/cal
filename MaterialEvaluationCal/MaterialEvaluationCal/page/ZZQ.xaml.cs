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
    /// ZZQ.xaml 的交互逻辑
    /// </summary>
    public partial class ZZQ : Window
    {
        public ZZQ()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "钢管(结构用)";
            string err = "";

            string sqlStr = "select * from SZZQ where RECID='180'";
            
            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from ZZQDJ ", "BZ_GX_DJ");
            //获取数据表数据
            string extraMJZ1json = Base.JsonHelper.GetDataJson(type, "select * from MJZ1 ", "MJZ1");
            string extraSJZ1json = Base.JsonHelper.GetDataJson(type, "select * from SJZ1 ", "SJZ1");

            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);
            var extraMJZ1jsonData = Base.JsonHelper.GetDictionary(extraMJZ1json, type);
            var extraSJZ1jsonData = Base.JsonHelper.GetDictionary(extraSJZ1json, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_ZZQ_DJ", extraDJjsonData["BZ_ZZQ_DJ"]);
            listExtraData.Add("MJZ1", extraDJjsonData["MJZ1"]);
            listExtraData.Add("SJZ1", extraDJjsonData["SJZ1"]);


            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MZZQ where RECID ='180' ", "M_ZZQ");

            //更改处 GetAfferentDataJson2  GetAfferentDictionaryNew
            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_ZZQ", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.ZZQ zZQ = new Calculates.ZZQ();
            zZQ.Calculate(listExtraData, retSData, out err);
            zZQ.Calc();

        }
    }
}
