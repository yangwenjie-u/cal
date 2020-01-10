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
    public partial class TB : Window
    {
        public TB()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "轻质条板";
            string err = "";

            string sqlStr = "select * from STB where RECID='43'";
            
            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from TBDJ ", "BZ_TB_DJ");

            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_TB_DJ", extraDJjsonData["BZ_TB_DJ"]);


            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MTB where RECID ='43' ", "M_TB");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_TB", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.TB tB = new Calculates.TB();
            tB.Calculate(listExtraData, retSData, out err);
            tB.Calc();
        }
    }
}
