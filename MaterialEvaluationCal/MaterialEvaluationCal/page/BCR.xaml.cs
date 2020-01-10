using System;
using System.Collections.Generic;
using System.Windows;

namespace MaterialEvaluationCal.page
{
    /// <summary>
    /// aqw.xaml 的交互逻辑
    /// </summary>
    public partial class BCR : Window
    {
        public BCR()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "玻璃传热系数";
            string err = "";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from BCRDJ ", "BZ_BCR_DJ");
            string extraBWFJjson = Base.JsonHelper.GetDataJson(type, "select * from BCRBWFJ ", "BZ_BCR_BWFJ");


            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);
            var extraBWFJjsonData = Base.JsonHelper.GetDictionary(extraBWFJjson, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_BCR_DJ", extraDJjsonData["BZ_BCR_DJ"]);
            listExtraData.Add("BZ_BCR_BWFJ", extraBWFJjsonData["BZ_BCR_BWFJ"]);
            //获取测试数据

            string sqlStr = "select * from SBCR";
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MBCR", "M_BCR");
            string e_json = Base.JsonHelper.GetMdataJson("local", "select * from MS_BW", "MS_BW");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_BCR", sqlStr, m_json,e_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.BCR a = new Calculates.BCR();
            a.Calculate(listExtraData, retSData, out err);
            a.Calc();
        }
    }
}
