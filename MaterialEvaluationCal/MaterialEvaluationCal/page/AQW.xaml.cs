using System;
using System.Collections.Generic;
using System.Windows;

namespace MaterialEvaluationCal.page
{
    /// <summary>
    /// aqw.xaml 的交互逻辑
    /// </summary>
    public partial class AQW : Window
    {
        public AQW()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "安全网";
            string err = "";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from AQWDJ ", "BZ_AQW_DJ");


            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_AQW_DJ", extraDJjsonData["BZ_AQW_DJ"]);
            //获取测试数据

            string sqlStr = "select * from SAQW";
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MAQW", "M_AQW");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_AQW", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.AQW aQW = new Calculates.AQW();
            aQW.Calculate(listExtraData, retSData, out err);
            aQW.Calc();
        }
    }
}
