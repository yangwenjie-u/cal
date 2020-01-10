using System;
using System.Collections.Generic;
using System.Windows;

namespace MaterialEvaluationCal.page
{
    /// <summary>
    /// aqw.xaml 的交互逻辑
    /// </summary>
    public partial class AW : Window
    {
        public AW()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "安全网";
            string err = "";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from AWDJ ", "BZ_AW_DJ");


            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_AW_DJ", extraDJjsonData["BZ_AW_DJ"]);
            //获取测试数据

            string sqlStr = "select * from SAW";
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MAW", "M_AW");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson("S_AW", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionary(retSDataJosn);

            //Calculates.AW aW = new Calculates.AW();
            //aW.Calculate(listExtraData, retSData, out err);
            //aW.Calc();
        }
    }
}
