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
using System.Data;

namespace MaterialEvaluationCal.page
{
    /// <summary>
    /// HD.xaml 的交互逻辑
    /// </summary>
    public partial class HD : Window
    {
        public HD()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "混凝土抗冻";
            string err = "";

            string sqlStr = "select * from SHD where RECID='351'";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from HDDJ ", "BZ_HD_DJ");
            string extraGGjson = Base.JsonHelper.GetDataJson(type, "select * from HDGG ", "BZ_HD_GG");
            string extraCSjson = Base.JsonHelper.GetDataJson(type, "select * from HDDRCS ", "BZ_HD_CS");
            string extraFFjson = Base.JsonHelper.GetDataJson(type, "select * from HDDRFF ", "BZ_HD_FF");


            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);
            var extraGGjsonData = Base.JsonHelper.GetDictionary(extraGGjson, type);
            var extraCSjsonData = Base.JsonHelper.GetDictionary(extraCSjson, type);
            var extraFFjsonData = Base.JsonHelper.GetDictionary(extraFFjson, type);


            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_HD_DJ", extraDJjsonData["BZ_HD_DJ"]);
            listExtraData.Add("BZ_HD_GG", extraGGjsonData["BZ_HD_GG"]);
            listExtraData.Add("BZ_HD_CS", extraCSjsonData["BZ_HD_CS"]);
            listExtraData.Add("BZ_HD_FF", extraFFjsonData["BZ_HD_FF"]);

            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MHD where RECID='351' ", "M_HD");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson("S_HD", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionary(retSDataJosn);

             Calculates.HD hD = new Calculates.HD();
            //hD.Calculate(listExtraData,  retSData, out err);
            //hD.Calc(listExtraData, ref retSData, ref err);



        }
    }
}
