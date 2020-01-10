
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
    /// GHF.xaml 的交互逻辑
    /// </summary>
    public partial class GHF : Window
    {
        public GHF()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "钢筋焊接复试";
            string err = "";


            //List<string> listJYD = new List<string>();
            //List<string> listDataJson = new List<string>();
            //listJYD = "180400137,180400550,180400780,180400858,180400866,180400886,180401163,180500180,180500188,180500483".Split(',').ToList();

            //foreach (var item in listJYD)
            //{
            //    try
            //    {
            //        string sqlStr = $"select top 1  * from SGHF where JYDBH='{item}'";

            //        //获取帮助表数据
            //        string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from GHFDJ ", "BZ_GHF_DJ");
            //        var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);

            //        Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            //        listExtraData.Add("BZ_GHF_DJ", extraDJjsonData["BZ_GHF_DJ"]);

            //        //获取测试数据
            //        string m_json = Base.JsonHelper.GetMdataJson("local", $"select * from  MGHF where JYDBH ='{item}' ", "M_GHF");

            //        var retSDataJosn = Base.JsonHelper.GetAfferentDataJson("S_GHF", sqlStr, m_json);
            //        listDataJson.Add(retSDataJosn);
            //    }
            //    catch
            //    {

            //    }
            //}
            //StringBuilder sb = new StringBuilder("");

            //for (int i = 0; i < listDataJson.Count; i++)
            //{
            //    sb.Append(i + ":"+listDataJson[i]+ "\r\n");
            //}
            //err = sb.ToString();
            string sqlStr = "select top 1  * from SGHF where JYDBH='180400550'";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from GHFDJ ", "BZ_GHF_DJ");
            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_GHF_DJ", extraDJjsonData["BZ_GHF_DJ"]);

            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from  MGHF where JYDBH ='180400550' ", "M_GHF");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_GHF", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.GHF2 gHJ = new Calculates.GHF2();
            gHJ.Calculate(listExtraData, retSData, out err);
            gHJ.Calc();
        }
    }
}