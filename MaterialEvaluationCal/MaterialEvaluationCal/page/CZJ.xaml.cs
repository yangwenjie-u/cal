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
    /// CZJ.xaml 的交互逻辑
    /// </summary>
    public partial class CZJ : Window
    {
        public CZJ()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "非结构承载用石材胶粘剂";
            string err = "";

            string sqlStr = "select * from SCZJ where RECID='88'";
            
            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from CZJDJ ", "BZ_CZJ_DJ");
            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_CZJ_DJ", extraDJjsonData["BZ_CZJ_DJ"]);

            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MCZJ where RECID ='88' ", "M_CZJ");

            //更改处 GetAfferentDataJson2  GetAfferentDictionaryNew
            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_CZJ", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.CZJ cZJ = new Calculates.CZJ();
            cZJ.Calculate(listExtraData, retSData, out err);
            cZJ.Calc();

        }
    }
}
