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
    /// YJ.xaml 的交互逻辑
    /// </summary>
    public partial class YJ : Window
    {
        public YJ()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "干挂石材幕墙用环氧胶粘剂";
            string err = "";

            string sqlStr = "select * from S_YJ where RECID = 19125121629941829960892";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from BZ_YJ_DJ ", "BZ_YJ_DJ");

            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_YJ_DJ", extraDJjsonData["BZ_YJ_DJ"]);

            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from M_YJ where RECID ='19125214499257735705763' ", "M_YJ");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_YJ", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.YJ yJ = new Calculates.YJ();
            yJ.Calculate(listExtraData, retSData, out err);
            yJ.Calc();
        }
    }
}
