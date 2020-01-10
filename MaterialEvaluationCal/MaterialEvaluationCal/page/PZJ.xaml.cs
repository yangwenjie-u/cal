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
    /// PZJ.xaml 的交互逻辑
    /// </summary>
    public partial class PZJ : Window
    {
        public PZJ()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "混凝土膨胀剂";
            string err = "";

            //string sqlStr = "select * from SHNT where RECID='19105378935991074185501'";
            string sqlStr = "select * from S_PZJ where RECID='19115423273623741993836'";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from BZ_PZJ_DJ ", "BZ_PZJ_DJ");

            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_PZJ_DJ", extraDJjsonData["BZ_PZJ_DJ"]);

            //获取测试数据
            //string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MHNT where RECID='67577' ", "M_PZJ");
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from M_PZJ where RECID ='19115356898275175896080' ", "M_PZJ");

            //var retSDataJosn = Base.JsonHelper.GetAfferentDataJson("S_PZJ", sqlStr, m_json);
            //var retSData = Base.JsonHelper.GetAfferentDictionary(retSDataJosn);

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson("S_PZJ", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionary(retSDataJosn);

            //Calculates.PZJ hNT = new Calculates.PZJ();
            //hNT.Calculate(listExtraData, retSData, out err);
            //Calculates.PZJ.Calc(listExtraData, ref retSData, ref err);
        }
    }
}
