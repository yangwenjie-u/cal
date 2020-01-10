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
    /// WB.xaml 的交互逻辑
    /// </summary>
    public partial class WB : Window
    {
        public WB()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "膨胀保温砂浆";
            string err = "";

            string sqlStr = "select * from SWB where RECID='33'";
            
            //获取帮助表数据
            //string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from WBDJ ", "BZ_WB_DJ");
            string extraZM_DRJLjson = Base.JsonHelper.GetDataJson(type, "select * from ZM_DRJL ", "ZM_DRJL");


            //var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);
            var extraZM_DRJLjsonData = Base.JsonHelper.GetDictionary(extraZM_DRJLjson, type);


            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            //listExtraData.Add("BZ_WB_DJ", extraDJjsonData["BZ_WB_DJ"]);
            listExtraData.Add("ZM_DRJL", extraZM_DRJLjsonData["ZM_DRJL"]);


            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MWB where RECID ='33' ", "M_WB");

            //var retSDataJosn = Base.JsonHelper.GetAfferentDataJson("S_PZJ", sqlStr, m_json);
            //var retSData = Base.JsonHelper.GetAfferentDictionary(retSDataJosn);

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_WB", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.WB wB = new Calculates.WB();
            wB.Calculate(listExtraData, retSData, out err);
            wB.Calc();
        }
    }
}
