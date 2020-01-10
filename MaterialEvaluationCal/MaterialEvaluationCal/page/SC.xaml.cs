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
    /// SC.xaml 的交互逻辑
    /// </summary>
    public partial class SC : Window
    {
        public SC()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "SC 采暖散热器";
            string err = "";

            //string sqlStr = "select * from SSC where RECID='19125709313666109107825'";
            string sqlStr = "select * from S_SC where RECID='19125709313666109107825'";

            //获取帮助表数据
            //string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from SCDJ ", "BZ_SC_DJ");
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from BZ_SC_DJ ", "BZ_SC_DJ");
            //string extraHSBjson = Base.JsonHelper.GetDataJson(type, "select * from CGLhsb ", "BZ_SC_HSB");
            //string extraCSjson = Base.JsonHelper.GetDataJson(type, "select * from CGLDRCS ", "BZ_CGL_CS");
            //string extraFFjson = Base.JsonHelper.GetDataJson(type, "select * from CGLDRFF ", "BZ_CGL_FF");
            //string extraGMDJBjson = Base.JsonHelper.GetDataJson(type, "select * from CGLGMDJB ", "BZ_CGLGMDJB");


            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);
            //var extraHSBjsonData = Base.JsonHelper.GetDictionary(extraHSBjson, type);
            //var extraCSjsonData = Base.JsonHelper.GetDictionary(extraCSjson, type);
            //var extraFFjsonData = Base.JsonHelper.GetDictionary(extraFFjson, type);
            //var extraGMDJBjsonData = Base.JsonHelper.GetDictionary(extraGMDJBjson, type);


            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_SC_DJ", extraDJjsonData["BZ_SC_DJ"]);
            //listExtraData.Add("BZ_CGL_HSB", extraHSBjsonData["BZ_CGL_HSB"]);
            //listExtraData.Add("BZ_CGL_CS", extraCSjsonData["BZ_CGL_CS"]);
            //listExtraData.Add("BZ_CGL_FF", extraFFjsonData["BZ_CGL_FF"]);
            //listExtraData.Add("BZ_CGLGMDJB", extraGMDJBjsonData["BZ_CGLGMDJB"]);

            //获取测试数据
            //string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MSC where RECID='19125709313666109107825' ", "M_SC");
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from M_SC where RECID='19125709313666109107825' ", "M_SC");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_SC", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.SC sC = new Calculates.SC();
            sC.Calculate(listExtraData, retSData, out err);
            try
            {
                sC.Calc();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
            
         
        }
    }
}
