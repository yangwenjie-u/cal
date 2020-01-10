using System;
using System.Collections.Generic;
using System.Windows;

namespace MaterialEvaluationCal.page
{
    /// <summary>
    /// HNT.xaml 的交互逻辑
    /// </summary>
    public partial class FDJ : Window
    {
        public FDJ()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "混凝土防冻剂";
            string err = "";

         

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from BZFDJDJ ", "BZ_FDJ_DJ");
        

            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);
          
            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_FDJ_DJ", extraDJjsonData["BZ_FDJ_DJ"]);

            //获取测试数据

            string sqlStr = "select * from SFDJ";
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MFDJ", "M_FDJ");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson("S_FDJ", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionary(retSDataJosn);

            Calculates.FDJ fDJ= new Calculates.FDJ();
            //fDJ.Calculate(listExtraData,  retSData, out err);
            fDJ.Calc();
        }
    }
}
