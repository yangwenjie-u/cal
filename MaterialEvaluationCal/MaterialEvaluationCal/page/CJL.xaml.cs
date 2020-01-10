using System;
using System.Collections.Generic;
using System.Windows;

namespace MaterialEvaluationCal.page
{
    /// <summary>
    /// XJL.xaml 的交互逻辑
    /// </summary>
    public partial class CJL : Window
    {
        public CJL()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "粗集料";
            string err = "";



            //获取帮助表数据

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            
            //获取测试数据

            string sqlStr = "select * from SCJL";
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MCJL", "M_CJL");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_CJL", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.CJL cJL = new Calculates.CJL();
            cJL.Calculate(listExtraData, retSData, out err);
            cJL.Calc();
        }
    }
}
