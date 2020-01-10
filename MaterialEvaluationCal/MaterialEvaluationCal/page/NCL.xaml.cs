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
    /// NCL.xaml 的交互逻辑
    /// </summary>
    public partial class NCL : Window
    {
        public NCL()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "水泥基灌浆材料";
            string err = "";

            string sqlStr = "select * from SNCL where RECID='14'";
            
            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from NCLDJ ", "BZ_NCL_DJ");
            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_NCL_DJ", extraDJjsonData["BZ_NCL_DJ"]);

            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MNCL where RECID ='14' ", "M_NCL");

            //更改处 GetAfferentDataJson2  GetAfferentDictionaryNew
            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_FQ", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.NCL nCL = new Calculates.NCL();
            nCL.Calculate(listExtraData, retSData, out err);
            nCL.Calc();

        }
    }
}
