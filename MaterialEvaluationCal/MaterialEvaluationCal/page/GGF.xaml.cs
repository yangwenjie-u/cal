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
    /// GGF.xaml 的交互逻辑
    /// </summary>
    public partial class GGF : Window
    {
        public GGF()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "钢精原材";
            string err = "";

            string sqlStr = "select * from S_GGF where RECID='19085366893097163898493'";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from BZ_GGF_DJ ", "BZ_GGF_DJ");

            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_GGF_DJ", extraDJjsonData["BZ_GGF_DJ"]);

            //获取测试数据
            //string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MHNT where RECID='67577' ", "M_HNT");
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from M_GGF where RECID ='19084687063924018159069' ", "M_GGF");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_GGF", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);


            Calculates.GGF ggf = new Calculates.GGF();
            ggf.Calculate(listExtraData, retSData, out err);
            ggf.Calc();
        }
    }
}
