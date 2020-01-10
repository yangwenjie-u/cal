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
    /// WJJ.xaml 的交互逻辑
    /// </summary>
    public partial class WJJ : Window
    {
        public WJJ()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "外加剂";
            string err = "";
            //string sqlStr = "select top 1  * from S_WJJ where RECID='00031975'";
            string sqlStr = "select top 1 RECID,JCXM   from SWJJ";

            //获取帮助表数据
            //string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from BZ_WJJ_DJ ", "BZ_WJJ_DJ");
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from WJJDJ ", "BZ_WJJ_DJ");
            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_WJJ_DJ", extraDJjsonData["BZ_WJJ_DJ"]);

            //获取测试数据
            //string m_json = Base.JsonHelper.GetMdataJson("local", "select * from  M_WJJ where recid ='00003437' ", "M_WJJ");
            string m_json = Base.JsonHelper.GetMdataJson("local", "select top 1  RECID,RECID from  MWJJ  ", "M_WJJ");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_WJJ", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.WJJ2 gHJ = new Calculates.WJJ2();
            gHJ.Calculate(listExtraData, retSData, out err);
            gHJ.Calc();
        }
    }
}
