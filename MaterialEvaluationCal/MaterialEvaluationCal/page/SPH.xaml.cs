using System;
using System.Collections.Generic;
using System.Windows;

namespace MaterialEvaluationCal.page
{
    /// <summary>
    /// XJL.xaml 的交互逻辑
    /// </summary>
    public partial class SPH : Window
    {
        public SPH()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "细集料";
            string err = "";

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            //获取测试数据

            string sqlStr = "select * from SSPH";
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MSPH", "M_SPH");
            string ejl_json = Base.JsonHelper.GetMdataJson("local", "select * from EJL_SH", "EJL_SH");
            string e_json = Base.JsonHelper.GetMdataJson("local", "select * from E_JLPB", "E_JLPB");
            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_SPH", sqlStr, m_json, ejl_json + e_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.XJL xJL = new Calculates.XJL();
            xJL.Calculate(listExtraData, retSData, out err);
            xJL.Calc();
        }
    }
}
