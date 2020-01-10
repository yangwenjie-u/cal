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
    /// QNX.xaml 的交互逻辑
    /// </summary>
    public partial class QNX : Window
    {
        public QNX()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            #region
            string type = "QNX 蒸压加气混凝土用砂浆[2017]";
            string err = "";

            //string sqlStr = "select * from SQNX where RECID='19125192353795645278146'";
            string sqlStr = "select * from S_QNX where RECID='19125759542332900579927'";
            //获取帮助表数据
            //string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from QNXDJ ", "BZ_QNX_DJ");
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from BZ_QNX_DJ ", "BZ_QNX_DJ");
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
            listExtraData.Add("BZ_QNX_DJ", extraDJjsonData["BZ_QNX_DJ"]);
            //listExtraData.Add("BZ_CGL_HSB", extraHSBjsonData["BZ_CGL_HSB"]);
            //listExtraData.Add("BZ_CGL_CS", extraCSjsonData["BZ_CGL_CS"]);
            //listExtraData.Add("BZ_CGL_FF", extraFFjsonData["BZ_CGL_FF"]);
            //listExtraData.Add("BZ_CGLGMDJB", extraGMDJBjsonData["BZ_CGLGMDJB"]);

            //获取测试数据
            //string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MQNX where RECID='19125192353795645278146' ", "M_QNX");
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from M_QNX where RECID='19125759542332900579927' ", "M_QNX");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_QNX", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.QNX qNX = new Calculates.QNX();
            qNX.Calculate(listExtraData, retSData, out err);
            qNX.Calc();
            #endregion
        }
    }
}
