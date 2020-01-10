using System;
using System.Collections.Generic;
using System.Windows;

namespace MaterialEvaluationCal.page
{
    /// <summary>
    /// YKJ.xaml 的交互逻辑
    /// </summary>
    public partial class YKJ : Window
    {
        public YKJ()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "扣件";
            string err = "";



            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from YKJDJ ", "BZ_YKJ_DJ");


            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_YKJ_DJ", extraDJjsonData["BZ_YKJ_DJ"]);
            //获取测试数据

            string sqlStr = "select * from SYKJ";
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MYKJ", "M_YKJ");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson("S_YKJ", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionary(retSDataJosn);

            Calculates.YKJ yKJ = new Calculates.YKJ();
            //yKJ.Calculate(listExtraData, retSData, out err);
            //Calculates.YKJ.Calc(listExtraData, ref retSData, ref err);
        }
    }
}
