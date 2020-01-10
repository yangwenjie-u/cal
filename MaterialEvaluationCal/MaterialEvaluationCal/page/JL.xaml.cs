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
    /// JL.xaml 的交互逻辑
    /// </summary>
    public partial class JL : Window
    {
        public JL()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "加固正拉粘结强度";
            string err = "";

            string sqlStr = "select * from S_JL where RECID='19125335838518211443134'";
            
            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from BZ_JL_DJ ", "BZ_JL_DJ");
            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_JL_DJ", extraDJjsonData["BZ_JL_DJ"]);

            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from M_JL where RECID ='19125639139754282113594' ", "M_JL");

            //更改处 GetAfferentDataJson2  GetAfferentDictionaryNew
            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_JL", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            Calculates.JL jL = new Calculates.JL();
            jL.Calculate(listExtraData, retSData, out err);
            jL.Calc();

        }
    }
}
