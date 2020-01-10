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
    /// HNT.xaml 的交互逻辑
    /// </summary>
    public partial class HNT : Window
    {
        public HNT()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "混凝土";
            string err = "";

            //string sqlStr = "select * from SHNT where RECID='19105378935991074185501'";
            string sqlStr = "select * from S_HNT where RECID='19085366893097163898493'";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from BZ_HNT_DJ ", "BZ_HNT_DJ");
            string extraGGjson = Base.JsonHelper.GetDataJson(type, "select * from BZ_HNT_GG ", "BZ_HNT_GG");

            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);
            var extraGGjsonData = Base.JsonHelper.GetDictionary(extraGGjson, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_HNT_DJ", extraDJjsonData["BZ_HNT_DJ"]);
            listExtraData.Add("BZ_HNT_GG", extraGGjsonData["BZ_HNT_GG"]);

            //获取测试数据
            //string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MHNT where RECID='67577' ", "M_HNT");
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from M_HNT where RECID ='19084687063924018159069' ", "M_HNT");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_HNT", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);


            Calculates.HNT2 hNT = new Calculates.HNT2();  
            hNT.Calculate(listExtraData, retSData, out err);
            hNT.Calc();
        }
    }
}
