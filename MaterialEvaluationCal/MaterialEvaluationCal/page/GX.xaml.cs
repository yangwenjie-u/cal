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
    /// TB.xaml 的交互逻辑
    /// </summary>
    public partial class GX : Window
    {
        public GX()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "钢管(结构用)";
            string err = "";

            string sqlStr = "select * from SGX where RECID='180'";
            
            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from GXDJ ", "BZ_GX_DJ");

            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_GX_DJ", extraDJjsonData["BZ_GX_DJ"]);


            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MGX where RECID ='180' ", "M_GX");

            //更改处 GetAfferentDataJson2  GetAfferentDictionaryNew
            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_GX", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.GX gX = new Calculates.GX();
            gX.Calculate(listExtraData, retSData, out err);
            gX.Calc();

        }
    }
}
