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
    /// GLJ.xaml 的交互逻辑
    /// </summary>
    public partial class GLJ : Window
    {
        public GLJ()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "机械连接";
            string tablename = "S_GLJ";
            string err = "";
            string sqlStr = "select top  1 * from S_GLJ ";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from BZ_GLJ_DJ ", "BZ_GLJ_DJ");
            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);
            string extraXBDJjson = Base.JsonHelper.GetDataJson(type, "select * from BZ_GLJXBDJ ", "BZ_GLJXBDJ");
            var extraXBDJjsonData = Base.JsonHelper.GetDictionary(extraXBDJjson, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_GLJ_DJ", extraDJjsonData["BZ_GLJ_DJ"]);
            listExtraData.Add("BZ_GLJXBDJ", extraXBDJjsonData["BZ_GLJXBDJ"]);

            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select top 1 * from  M_GLJ ", "M_GLJ");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_GLJ", sqlStr, m_json);

            var retData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);
            //Calculates.GLJ2 gLJ = new Calculates.GLJ2();
            //gLJ.Calculate(listExtraData,  retData, out err);

            //gLJ.Calc();

        }
     }
}
