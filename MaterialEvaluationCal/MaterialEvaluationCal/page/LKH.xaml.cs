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
    /// LKH.xaml 的交互逻辑
    /// </summary>
    public partial class LKH : Window
    {
        public LKH()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            #region
            string type = "LKH 抗滑移系数";
            string err = "";

            string sqlStr = "select * from SLKH where RECID='710'";

            //获取帮助表数据
            string extraDJjson = Base.JsonHelper.GetDataJson(type, "select * from LKHDJ ", "BZ_LKH_DJ");
            //string extraZLPCBjson = Base.JsonHelper.GetDataJson(type, "select * from ZLPCB ", "BZ_ZLPCB");
            //string extraCSjson = Base.JsonHelper.GetDataJson(type, "select * from CGLDRCS ", "BZ_CGL_CS");
            //string extraFFjson = Base.JsonHelper.GetDataJson(type, "select * from CGLDRFF ", "BZ_CGL_FF");
            //string extraGMDJBjson = Base.JsonHelper.GetDataJson(type, "select * from CGLGMDJB ", "BZ_CGLGMDJB");


            var extraDJjsonData = Base.JsonHelper.GetDictionary(extraDJjson, type);
            //var extraZLPCBjsonData = Base.JsonHelper.GetDictionary(extraZLPCBjson, type);
            //var extraCSjsonData = Base.JsonHelper.GetDictionary(extraCSjson, type);
            //var extraFFjsonData = Base.JsonHelper.GetDictionary(extraFFjson, type);
            //var extraGMDJBjsonData = Base.JsonHelper.GetDictionary(extraGMDJBjson, type);


            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            listExtraData.Add("BZ_LKH_DJ", extraDJjsonData["BZ_LKH_DJ"]);
            //listExtraData.Add("BZ_GYF_ZLPCB", extraZLPCBjsonData["BZ_GYF_ZLPCB"]);
            //listExtraData.Add("BZ_CGL_CS", extraCSjsonData["BZ_CGL_CS"]);
            //listExtraData.Add("BZ_CGL_FF", extraFFjsonData["BZ_CGL_FF"]);
            //listExtraData.Add("BZ_CGLGMDJB", extraGMDJBjsonData["BZ_CGLGMDJB"]);

            //获取测试数据
            string m_json = Base.JsonHelper.GetMdataJson("local", "select * from MLKH where RECID='710' ", "M_LKH");

            var retSDataJosn = Base.JsonHelper.GetAfferentDataJson2("S_LKH", sqlStr, m_json);
            var retSData = Base.JsonHelper.GetAfferentDictionaryNew(retSDataJosn);

            //Calculates.LKH lKH = new Calculates.LKH();
            //lKH.Calculate(listExtraData, retSData, out err);
            //lKH.Calc(listExtraData, ref retSData, ref err);

            Calculates.LKH lKH = new Calculates.LKH();
            lKH.Calculate(listExtraData, retSData, out err);
            try
            {
                lKH.Calc();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }

            #endregion
        }
    }
}
