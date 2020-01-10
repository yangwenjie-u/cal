using System;
using System.Collections.Generic;
using System.Windows;

namespace MaterialEvaluationCal.page
{
    /// <summary>
    /// SPA.xaml 的交互逻辑
    /// </summary>
    public partial class WCX : Window
    {
        public WCX()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "无侧限抗压";
            string err = "";
            

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            
            //获取测试数据

            string sqlStr = "select * from SWCX";
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MWCX", "M_WCX");
            string e_json = Base.JsonHelper.GetMdataJson("local", "select * from E_WCX", "E_WCX");
            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_WCX", sqlStr, m_json,e_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.WCX wCX = new Calculates.WCX();
            wCX.Calculate(listExtraData, retSData, out err);
            wCX.Calc();
        }
    }
}
