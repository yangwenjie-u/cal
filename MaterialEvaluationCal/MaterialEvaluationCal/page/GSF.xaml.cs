using System;
using System.Collections.Generic;
using System.Data;
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
    /// DXL.xaml 的交互逻辑
    /// </summary>
    public partial class GSF : Window
    {
        public GSF()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "砂浆";
            string err = "";

            string sqlStr = "select * from S_GSF where RECID='351'";
            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from GSFDJ ", "BZ_GSF_DJ");

            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson,type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_GSF_DJ", extraDJjsonData["BZ_GSF_DJ"]);

            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from S_GSF where RECID='351' ", "S_GSF");
            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson("S_GSF", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionary(retSDataJosn);

            //Calculates.GSF gSF = new Calculates.GSF();
            //gSF.Calculate(listExtraData,  retSData, out err);
            //gSF.Calc(listExtraData, ref retSData, ref err);
        }
    }
}
