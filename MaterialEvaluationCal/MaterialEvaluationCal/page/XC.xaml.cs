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
    /// XC.xaml 的交互逻辑
    /// </summary>
    public partial class XC : Window
    {
        public XC()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "现场气密性";
            string err = "";

            string sqlStr = "select * from SXC where RECID='19115423273623741993836'";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from XCDJ ", "BZ_XC_DJ");
            string extraMS_MCjson = Base.JsonHelper.GetDataJson(type, "select * from MS_MC", "MS_MC");

            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);
            var extraMS_MCjsonData = Base.JsonHelper.GetDictionary(extraMS_MCjson, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_XC_DJ", extraDJjsonData["BZ_XC_DJ"]);
            listExtraData.Add("MS_MC", extraDJjsonData["MS_MC"]);

            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MXC where RECID ='19115356898275175896080' ", "M_XC");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson("S_XC", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionary(retSDataJosn);

            //Calculates.XC xC = new Calculates.XC();
            //xC.Calculate(listExtraData, retSData, out err);
            //xC.Calc(listExtraData, ref retSData, ref err);
        }
    }
}
