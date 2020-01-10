using System;
using System.Collections.Generic;
using System.Windows;

namespace MaterialEvaluationCal.page
{
    /// <summary>
    /// SPA.xaml 的交互逻辑
    /// </summary>
    public partial class SPA : Window
    {
        public SPA()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "沥青混泥土配合比";
            string err = "";

            

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            
            //获取测试数据

            string sqlStr = "select * from SSPA";
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MSPA", "M_SPA");
            string e_json = Base.JsonHelper.GetMdataJson("local", "select * from E_JLPB", "E_JLPB");
            string elq_json = Base.JsonHelper.GetMdataJson("local", "select * from E_LQ", "E_LQ");
            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_SPA", sqlStr, m_json, e_json + elq_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.SPA sPA = new Calculates.SPA();
            sPA.Calculate(listExtraData, retSData, out err);
            sPA.Calc();
        }
    }
}
