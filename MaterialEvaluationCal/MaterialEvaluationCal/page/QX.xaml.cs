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
    /// QX.xaml 的交互逻辑
    /// </summary>
    public partial class QX : Window
    {
        public QX()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            #region
            string type = "QX 取芯(按个)";
            string err = "";

            string sqlStr = "select * from SQX where RECID='432'";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from QXDJ ", "BZ_QX_DJ");
            string extraHSBjson = Base.JsonHelper.GetDataJson(type, "select * from QXHSB ", "BZ_QX_HSB");
            string extraQJBjson = Base.JsonHelper.GetDataJson(type, "select * from QXQJXS ", "BZ_QX_QJB");
            //string extraFFjson = Base.JsonHelper.GetDataJson(type, "select * from CGLDRFF ", "BZ_CGL_FF");
            //string extraGMDJBjson = Base.JsonHelper.GetDataJson(type, "select * from CGLGMDJB ", "BZ_CGLGMDJB");


            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);
            var extraHSBjsonData = Base.JsonHelper.GetDictionary(extraHSBjson, type);
            var extraQJBjsonData = Base.JsonHelper.GetDictionary(extraQJBjson, type);
            //var extraFFjsonData = Base.JsonHelper.GetDictionary(extraFFjson, type);
            //var extraGMDJBjsonData = Base.JsonHelper.GetDictionary(extraGMDJBjson, type);


            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_QX_DJ", extraDJjsonData["BZ_QX_DJ"]);
            listExtraData.Add("BZ_QX_HSB", extraHSBjsonData["BZ_QX_HSB"]);
            listExtraData.Add("BZ_QX_QJB", extraQJBjsonData["BZ_QX_QJB"]);
            //listExtraData.Add("BZ_CGL_FF", extraFFjsonData["BZ_CGL_FF"]);
            //listExtraData.Add("BZ_CGLGMDJB", extraGMDJBjsonData["BZ_CGLGMDJB"]);

            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MQX where RECID='432' ", "M_QX");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson("S_QX", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionary(retSDataJosn);

            Calculates.QX qX = new Calculates.QX();
            qX.Calculate(listExtraData, retSData, out err);
            qX.Calc(listExtraData, ref retSData, ref err);
            #endregion
        }
    }
}
