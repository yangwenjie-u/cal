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
    /// GHJ.xaml 的交互逻辑
    /// </summary>
    public partial class GHJ : Window
    {
        public GHJ()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //string type = "焊接头";
            //string tablename = "S_GHJ";
            string type = "钢筋焊接";
            string err = ""; 
            string sqlStr = "select top  1 * from S_GHJ";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from BZ_GHJ_DJ ", "BZ_GHJ_DJ");
            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);

            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_GHJ_DJ", extraDJjsonData["BZ_GHJ_DJ"]);

            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select top 1 * from  M_GHJ ", "M_GHJ");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_GHJ", sqlStr , m_json);

            //retSDataJosn = "{\"calcData\": [{\"拉伸\": {\"S_GHJ\": [{\"RECID\": \"19115392236262378830078\",\"FJ\": \"\",\"GJLB\": \"电渣压力焊\",\"SJDJ\": \"\",\"G_KLQD\": \"\",\"JCJG_LS\": \"\",\"DKJ1\": \"\",\"DKJ2\": \"\",\"DKJ3\": \"\",\"JCJG_LW\": \"\",\"LW1\": \"\",\"LW2\": \"\",\"LW3\": \"\",\"HG_LW\": \"\"}],\"S_BY_RW_XQ\": [{\"RECID\": \"19085206791636631332933\",\"SJWCJSSJ\": \"2019/11/15 11:08:42\",\"JCJG\": \"\",\"JCJGMS\": \"\"}]},\"BGJG\": {\"M_BY_BG\": [{\"JCJG\": \"不合格\",\"JCJGMS\": \"\"}]}}],\"code\": 1,\"message\": \"成功\"}";
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);
            Calculates.GHJ gHJ = new Calculates.GHJ();
            gHJ.Calculate(listExtraData,  retSData, out err);


            //gHJ.Calc(listExtraData, ref retSData, ref err);
        }

    }
}
