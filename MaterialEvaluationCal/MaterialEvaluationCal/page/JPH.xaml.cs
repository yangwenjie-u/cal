using System;
using System.Collections.Generic;
using System.Windows;

namespace MaterialEvaluationCal.page
{
    /// <summary>
    /// XJL.xaml 的交互逻辑
    /// </summary>
    public partial class JPH : Window
    {
        public JPH()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "集料配合比";
            string err = "";



            //获取帮助表数据
            
            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            //获取测试数据

            string sqlStr = "select * from SJPH";
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MJPH", "M_JPH");
            string e_json = Base.JsonHelper.GetMdataJson("local", "select * from E_JLPB", "E_JLPB");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_JPH", sqlStr, m_json,e_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.JPH jPH = new Calculates.JPH();
            jPH.Calculate(listExtraData, retSData, out err);
            jPH.Calc();
        }
    }
}
