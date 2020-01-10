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
    /// CGL.xaml 的交互逻辑
    /// </summary>
    public partial class CGL : Window
    {
        public CGL()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "石子(国标)";
            string err = "";

            string sqlStr = "select * from SCGL where RECID='26'";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from CGLDJ ", "BZ_CGL_DJ");
            string extraHSBjson = Base.JsonHelper.GetDataJson(type, "select * from CGLhsb ", "BZ_CGLHSB");
            //string extraCSjson = Base.JsonHelper.GetDataJson(type, "select * from CGLDRCS ", "BZ_CGL_CS");
            //string extraFFjson = Base.JsonHelper.GetDataJson(type, "select * from CGLDRFF ", "BZ_CGL_FF");
            //string extraGMDJBjson = Base.JsonHelper.GetDataJson(type, "select * from CGLGMDJB ", "BZ_CGLGMDJB");


            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);
            var extraHSBjsonData = Base.JsonHelper.GetDictionary(extraHSBjson, type);
            //var extraCSjsonData = Base.JsonHelper.GetDictionary(extraCSjson, type);
            //var extraFFjsonData = Base.JsonHelper.GetDictionary(extraFFjson, type);
            //var extraGMDJBjsonData = Base.JsonHelper.GetDictionary(extraGMDJBjson, type);


            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_CGL_DJ", extraDJjsonData["BZ_CGL_DJ"]);
            listExtraData.Add("BZ_CGL_HSB", extraHSBjsonData["BZ_CGL_HSB"]);
            //listExtraData.Add("BZ_CGL_CS", extraCSjsonData["BZ_CGL_CS"]);
            //listExtraData.Add("BZ_CGL_FF", extraFFjsonData["BZ_CGL_FF"]);
            //listExtraData.Add("BZ_CGLGMDJB", extraGMDJBjsonData["BZ_CGLGMDJB"]);

            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MCGL where RECID='26' ", "M_CGL");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson("S_CGL", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionary(retSDataJosn);

            Calculates.CGL cGL = new Calculates.CGL();
            //cGL.Calculate(listExtraData, retSData, out err);
            //cGL.Calc(listExtraData, ref retSData, ref err);
        }
    }
}
