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
    /// GYF.xaml 的交互逻辑
    /// </summary>
    public partial class GYF : Window
    {
        public GYF()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "钢筋原材料复试";
            string err = "";

            string sqlStr = "select * from sGYF where RECID='1'";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from GYFDJ ", "BZ_GYF_DJ");
            //string extraGGjson = Base.JsonHelper.GetDataJson(type, "select * from QK3GG ", "BZ_QK3_GG");
            //string extraCSjson = Base.JsonHelper.GetDataJson(type, "select * from QK3DRCS ", "BZ_QK3_CS");
            //string extraFFjson = Base.JsonHelper.GetDataJson(type, "select * from QK3DRFF ", "BZ_QK3_FF");
            string extraZLPCBjson = Base.JsonHelper.GetDataJson(type, "select * from ZLPCB ", "BZ_ZLPCB");
            string extraMBYjson = Base.JsonHelper.GetDataJson(type, "select * from M_BY ", "M_BY");


            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);
            //var extraGGjsonData = Base.JsonHelper.GetDictionary(extraGGjson, type);
            //var extraCSjsonData = Base.JsonHelper.GetDictionary(extraCSjson, type);
            //var extraFFjsonData = Base.JsonHelper.GetDictionary(extraFFjson, type);
            var extraZLPCBjsonData = Base.JsonHelper.GetDictionary(extraZLPCBjson, type);
            var extraMBYjsonData = Base.JsonHelper.GetDictionary(extraMBYjson, type);


            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_GYF_DJ", extraDJjsonData["BZ_GYF_DJ"]);
            //listExtraData.Add("BZ_QK3_GG", extraGGjsonData["BZ_QK3_GG"]);
            //listExtraData.Add("BZ_QK3_CS", extraCSjsonData["BZ_QK3_CS"]);
            //listExtraData.Add("BZ_QK3_FF", extraFFjsonData["BZ_QK3_FF"]);
            listExtraData.Add("BZ_GYF_ZLPCB", extraZLPCBjsonData["BZ_GYF_ZLPCB"]);
            listExtraData.Add("M_BY", extraMBYjsonData["M_BY"]);

            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MGYF where RECID='00000285' ", "M_GYF");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson("S_GYF", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionary(retSDataJosn);

            //Calculates.GYF gYF = new Calculates.GYF();
            //gYF.Calculate();
            //gYF.Calc();
        }
    }
}
