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
    /// ZX.xaml 的交互逻辑
    /// </summary>
    public partial class ZX : Window
    {
        public ZX()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "烧结空心砖与空心砌块";
            string err = "";

            string sqlStr = "select * from SZX where JYDBH=190500007";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from ZXDJ ", "BZ_ZX_DJ");
            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);
            string extraDJjson1 = Base.JsonHelper.GetDataJson(type, "select * from ZXWCDJ ", "BZ_ZXWCDJ");
            var extraDJjsonData1 = Base.JsonHelper.GetDictionary(extraDJjson1, type);
            string extraDJjson2 = Base.JsonHelper.GetDataJson(type, "select * from ZXKFHDJ ", "BZ_ZXKFHDJ");
            var extraDJjsonData2 = Base.JsonHelper.GetDictionary(extraDJjson2, type);
            string extraDJjson3 = Base.JsonHelper.GetDataJson(type, "select * from ZXMDDJ ", "BZ_ZXMDDJ");
            var extraDJjsonData3 = Base.JsonHelper.GetDictionary(extraDJjson3, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_ZX_DJ", extraDJjsonData["BZ_ZX_DJ"]);
            listExtraData.Add("BZ_ZXWCDJ", extraDJjsonData["BZ_ZXWCDJ"]);
            listExtraData.Add("BZ_ZXKFHDJ", extraDJjsonData["BZ_ZXKFHDJ"]);
            listExtraData.Add("BZ_ZXMDDJ", extraDJjsonData["BZ_ZXMDDJ"]);

            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from  MZX where JYDBH ='190500007' ", "M_ZX");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_ZX", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.ZX zx = new Calculates.ZX();
            zx.Calculate(listExtraData, retSData, out err);
            zx.Calc();
        }
    }
}
