using System;
using System.Collections.Generic;
using System.Windows;

namespace MaterialEvaluationCal.page
{
    /// <summary>
    /// FM.xaml 的交互逻辑
    /// </summary>
    public partial class FM : Window
    {
        public FM()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "阀门";
            string err = "";

            
            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from FMDJ", "BZ_FM_DJ");
            string extraYLSJjson = Base.JsonHelper.GetDataJson(type, "select * from FMYLSJ", "BZ_FM_YLSJ");
            string extraMFXLLjson = Base.JsonHelper.GetDataJson(type, "select * from FMMFXLL", "BZ_FM_MFXLL");


            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);
            var extraYLSJjsonData = Base.JsonHelper.GetDictionary(extraYLSJjson, type);
            var extraMFXLLjsonData = Base.JsonHelper.GetDictionary(extraMFXLLjson, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_FM_DJ", extraDJjsonData["BZ_FM_DJ"]);
            listExtraData.Add("BZ_FM_YLSJ", extraYLSJjsonData["BZ_FM_YLSJ"]);
            listExtraData.Add("BZ_FM_MFXLL", extraMFXLLjsonData["BZ_FM_MFXLL"]);
            //获取测试数据

            string sqlStr = "select * from SFM";
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MFM", "M_FM");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson("S_FM", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionary(retSDataJosn);

            //Calculates.FM fM = new Calculates.FM();
            //fM.Calculate(listExtraData, retSData, out err);
            //fM.Calc(listExtraData, ref retSData, ref err);
        }
    }
}
