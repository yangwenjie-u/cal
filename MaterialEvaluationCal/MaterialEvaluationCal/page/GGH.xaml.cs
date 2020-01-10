﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MaterialEvaluationCal.page
{
    /// <summary>
    /// GGH.xaml 的交互逻辑
    /// </summary>
    public partial class GGH : Window
    {
        public GGH()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "焊接钢管(结构用)";
            string err = "";

            string sqlStr = "select * from SGGH where RECID='9'";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from GGHDJ ", "GGHDJ");

            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_GGH_DJ", extraDJjsonData["GGHDJ"]);

            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MGGH where RECID ='9' ", "M_GGH");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_GGH", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.GGH gGH = new Calculates.GGH();
            gGH.Calculate(listExtraData, retSData, out err);
            gGH.Calc();
        }
    }
}
