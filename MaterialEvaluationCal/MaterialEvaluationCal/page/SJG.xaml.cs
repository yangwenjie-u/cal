using System;
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
    /// SJG.xaml 的交互逻辑
    /// </summary>
    public partial class SJG : Window
    {
        public SJG()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            #region
            string type = "SJG 蒸压加气混凝土用砂浆[2017]";
            string err = "";

            string sqlStr = "select * from SSJG where RECID='210'";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from SJGDJ ", "BZ_SJG_DJ");
            string extraHSBjson = Base.JsonHelper.GetDataJson(type, "select * from SJGhsb ", "BZ_SJG_HSB");
            //string extraCSjson = Base.JsonHelper.GetDataJson(type, "select * from CGLDRCS ", "BZ_CGL_CS");
            //string extraFFjson = Base.JsonHelper.GetDataJson(type, "select * from CGLDRFF ", "BZ_CGL_FF");
            //string extraGMDJBjson = Base.JsonHelper.GetDataJson(type, "select * from CGLGMDJB ", "BZ_CGLGMDJB");


            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);
            var extraHSBjsonData = Base.JsonHelper.GetDictionary(extraHSBjson, type);
            //var extraCSjsonData = Base.JsonHelper.GetDictionary(extraCSjson, type);
            //var extraFFjsonData = Base.JsonHelper.GetDictionary(extraFFjson, type);
            //var extraGMDJBjsonData = Base.JsonHelper.GetDictionary(extraGMDJBjson, type);


            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_SJG_DJ", extraDJjsonData["BZ_SJG_DJ"]);
            listExtraData.Add("BZ_SJG_HSB", extraHSBjsonData["BZ_SJG_HSB"]);
            //listExtraData.Add("BZ_CGL_CS", extraCSjsonData["BZ_CGL_CS"]);
            //listExtraData.Add("BZ_CGL_FF", extraFFjsonData["BZ_CGL_FF"]);
            //listExtraData.Add("BZ_CGLGMDJB", extraGMDJBjsonData["BZ_CGLGMDJB"]);

            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MSJG where RECID='210' ", "M_SJG");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson("S_SJG", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionary(retSDataJosn);

            //Calculates.SJG sJG = new Calculates.SJG();
            //sJG.Calculate(listExtraData, retSData, out err);
            //sJG.Calc(listExtraData, ref retSData, ref err);
            #endregion
        }
    }
}
