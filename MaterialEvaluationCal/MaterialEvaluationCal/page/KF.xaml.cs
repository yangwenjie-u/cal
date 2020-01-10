using System;
using System.Collections.Generic;
using System.Windows;

namespace MaterialEvaluationCal.page
{
    /// <summary>
    /// FM.xaml 的交互逻辑
    /// </summary>
    public partial class KF : Window
    {
        public KF()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "矿粉";
            string err = "";



            //获取帮助表数据

            
            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            //获取测试数据

            string sqlStr = "select * from SKF";
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MKF", "M_KF");
            string e_json = Base.JsonHelper.GetMdataJson("local", "select * from E_SF", "E_SF");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_KF", sqlStr, m_json,e_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.KF kF = new Calculates.KF();
            kF.Calculate(listExtraData, retSData, out err);
            kF.Calc();
        }
    }
}
