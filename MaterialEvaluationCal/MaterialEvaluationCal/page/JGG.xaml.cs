using Calculates;
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

namespace MaterialEvaluationCal
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class JGG : Window
    {
        IDictionary<string, IList<IDictionary<string, string>>> dataExtra = null;
        IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData = null;

        public JGG()
        {

            InitializeComponent();
        }

        private void Btn_ckbg_Click(object sender, RoutedEventArgs e)
        {
            string err = "";
            string type = "碳素结构钢";

            Dictionary<string, string> testDatas = new Dictionary<string, string>();

            //testDatas.Add("MJ", this.tx_mj.Text); //面积


            var tx_MJ1 = Math.Round(3.14159 * Math.Pow(BaseMethods.GetSafeDouble(this.tx_zj1.Text) / 2, 2), 3);
            var tx_MJ2 = Math.Round(BaseMethods.GetSafeDouble(this.tx_hd1.Text) * BaseMethods.GetSafeDouble(this.tx_hd1.Text), 4);
            testDatas.Add("CJSY1", this.cj1.Text);//冲击1
            testDatas.Add("CJSY2", this.cj2.Text);
            testDatas.Add("CJSY3", this.cj3.Text);
            testDatas.Add("HD", this.tx_hd1.Text);//厚度
            //testDatas.Add("HD2", this.tx_hd2.Text);
            testDatas.Add("KD", this.tx_kd1.Text);//宽度
            //testDatas.Add("KD2", this.tx_kd2.Text);
            testDatas.Add("ZJ", this.tx_zj1.Text);//直径
            //testDatas.Add("ZJ2", this.tx_zj2.Text);
            testDatas.Add("QFHZ1", this.tx_qfhz1.Text); //屈服荷重
            testDatas.Add("QFHZ2", this.tx_qfhz2.Text);
            testDatas.Add("KLHZ1", this.tx_klhz1.Text);//抗拉荷重
            testDatas.Add("KLHZ2", this.tx_klhz2.Text);
            testDatas.Add("SCZ1", this.tx_sc1.Text);//断后伸长值
            testDatas.Add("SCZ2", this.tx_sc2.Text);
            testDatas.Add("LW1", this.tx_lw1.Text);//冷弯
            testDatas.Add("LW2", this.tx_lw2.Text);
            testDatas.Add("CD", this.tx_bj1.Text);//原始标距
            testDatas.Add("JCXM", this.tx_jcxm.Text);//检测项目

            testDatas.Add("YD1_1", this.yd1_1.Text);//硬度
            testDatas.Add("YD1_2", this.yd1_2.Text);
            testDatas.Add("YD1_3", this.yd1_3.Text);
            testDatas.Add("YD1_4", this.yd1_4.Text);
            testDatas.Add("YD1_5", this.yd1_5.Text);
            testDatas.Add("YD1_6", this.yd1_6.Text);


            testDatas.Add("YD2_1", this.yd2_1.Text);
            testDatas.Add("YD2_2", this.yd2_2.Text);
            testDatas.Add("YD2_3", this.yd2_3.Text);
            testDatas.Add("YD2_4", this.yd2_4.Text);
            testDatas.Add("YD2_5", this.yd2_5.Text);
            testDatas.Add("YD2_6", this.yd2_6.Text);


            testDatas.Add("YD3_1", this.yd3_1.Text);
            testDatas.Add("YD3_2", this.yd3_2.Text);
            testDatas.Add("YD3_3", this.yd3_3.Text);
            testDatas.Add("YD3_4", this.yd3_4.Text);
            testDatas.Add("YD3_5", this.yd3_5.Text);
            testDatas.Add("YD3_6", this.yd3_6.Text);



            testDatas.Add("QDFS", (this.lsfs.IsChecked ?? false) ? "false" : "true");
            testDatas.Add("LWFS", (this.lwfs.IsChecked ?? false) ? "false" : "true");
            testDatas.Add("CJFS", (this.cjfs.IsChecked ?? false) ? "false" : "true");
            testDatas.Add("SFFS", ((this.lsfs.IsChecked ?? false) || (this.lwfs.IsChecked ?? false) || (this.cjfs.IsChecked ?? false)) ? "false" : "true");



            #region 查询sql1
            string sqlStr = @"
 
select  
    MJ  --面积
    ,GGXH  --规格
    ,CD  --原始标距(MM)
    ,HD  --厚度(MM)
    ,KD  --宽度(MM)
    ,ZJ  --直径(MM)
    ,XGM
,KLHZ1    --抗拉荷重（KN）1
,KLHZ2
,QFHZ1 --屈服荷重（KN）1
,QFHZ2
,QFQD1 --屈服强度1
,QFQD2
,LW1 --冷弯
,LW2
,KLQD1    --抗拉强度1
,KLQD2
 
,HG_KL1  --抗拉1合格
,HG_KL2
,HG_KL  --抗拉合格个数
 
,HG_QF1  --屈服1合格
,HG_QF2
,HG_QF  --屈服合格个数
 
,HG_LW1  ---冷弯合格
,HG_LW2
,HG_LW  --冷弯合格个数
,HG_SC1 --伸长率1合格
,HG_SC2
,HG_SC --伸长率合格个数
,CJSY1  --冲击
,CJSY2
,CJSY3
,SCZ1  --伸长值
,SCZ2
,JCXM --检测项目
,YD1,
YD1_1,
YD1_2,
YD1_3,
YD2,
YD2_1,
YD2_2,
YD2_3,
YD3,
YD3_1,
YD3_2,
YD3_3


,HG_CJ
,HG_CJ1
,HG_CJ2
,HG_CJ3
,JCJG_CJ--冲击检测结果
,GCLX_PH --牌号
,SJDJ --钢材名称
,RECID
,GCLX_LB
,QYFX
,JCJG_LS
,GG
,JCJG_YD
,JCJG_LW
,G_YD
,JCJG
,HG_YD1
,HG_YD2
,HG_YD3
--,CLLX
       from SJGG
    ";//GCLX_PH,SCL1,SCL2,SCL3   where m.recid='19054636550127926033620'

            #endregion
            #region 主表数据查询 sql2
            string sqlStr1 = @"
            select QDFS,LWFS,CJFS,SFFS,FJBH,FJJJ,FJJJ1,FJJJ2,FJJJ3,PDBZ,RECID  from MJGG  ";
            #endregion

            //获取帮助表数据
            string extraDatajson = GetDataJson(type, "select * from JGGDJ", "JGGDJ");
            var listExtraData = Base.JsonHelper.GetDictionary(extraDatajson, type);

            //获取测试数据
            var retData1 = Base.JsonHelper.GetDictionary(GetDataJson(type, sqlStr, "SJGG", testDatas), type);
            var retData2 = Base.JsonHelper.GetDictionary(GetDataJson(type, sqlStr1, "MJGG", testDatas, true), type);
            retData1.Add("MJGG", retData2["MJGG"]);
            IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData = new Dictionary<string, IDictionary<string, IList<IDictionary<string, string>>>>();

            retData.Add("碳素结构钢", retData1);
            if ((this.lsfs.IsChecked ?? false) || (this.lwfs.IsChecked ?? false) || (this.cjfs.IsChecked ?? false))
            {
                Calculates.FS_GBT_700_2006.Calc(listExtraData, ref retData, ref err);
            }
            else
                Calculates.GBT_700_2006.Calc(listExtraData, ref retData, ref err);
            this.ckbg.Text = err;
        }

        public static string GetDataJson(string type, string sqlstr, string tableName, Dictionary<string, string> testDatas = null, bool isMinTable = false)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"calcData\":{\"" + type + "\":");

            string sql = string.Format(@sqlstr);
            Base.SqlBase sqlbase = new Base.SqlBase(Base.ESqlConnType.ConnectionStringLocal);
            DataSet ds = sqlbase.ExecuteDataset(sql);

            if (testDatas != null)
            {
                if (isMinTable)
                {
                    ds.Tables[0].Rows[0][0] = testDatas["QDFS"];
                    ds.Tables[0].Rows[0][1] = testDatas["LWFS"];
                    ds.Tables[0].Rows[0][2] = testDatas["CJFS"];
                    ds.Tables[0].Rows[0][3] = testDatas["SFFS"];
                }
                else
                {
                    ds.Tables[0].Rows[0][2] = testDatas["CD"];
                    ds.Tables[0].Rows[0][3] = testDatas["HD"];
                    ds.Tables[0].Rows[0][4] = testDatas["ZJ"];

                    ds.Tables[0].Rows[0][7] = testDatas["KLHZ1"];
                    ds.Tables[0].Rows[0][8] = testDatas["KLHZ2"];
                    ds.Tables[0].Rows[0][9] = testDatas["QFHZ1"];
                    ds.Tables[0].Rows[0][10] = testDatas["QFHZ2"];
                    ds.Tables[0].Rows[0][13] = testDatas["LW1"];
                    ds.Tables[0].Rows[0][14] = testDatas["LW2"];

                    ds.Tables[0].Rows[0][29] = testDatas["CJSY1"];
                    ds.Tables[0].Rows[0][30] = testDatas["CJSY2"];
                    ds.Tables[0].Rows[0][31] = testDatas["CJSY3"];
                    ds.Tables[0].Rows[0][32] = testDatas["SCZ1"];
                    ds.Tables[0].Rows[0][33] = testDatas["SCZ2"];
                    ds.Tables[0].Rows[0][34] = testDatas["JCXM"];
                    ds.Tables[0].Rows[0][36] = testDatas["YD1_1"];
                    ds.Tables[0].Rows[0][37] = testDatas["YD1_2"];
                    ds.Tables[0].Rows[0][38] = testDatas["YD1_3"];
                    ds.Tables[0].Rows[0][40] = testDatas["YD2_1"];
                    ds.Tables[0].Rows[0][41] = testDatas["YD2_2"];
                    ds.Tables[0].Rows[0][42] = testDatas["YD2_3"];
                    ds.Tables[0].Rows[0][44] = testDatas["YD3_1"];
                    ds.Tables[0].Rows[0][45] = testDatas["YD3_2"];
                    ds.Tables[0].Rows[0][46] = testDatas["YD3_3"];

                }

            }
            ds.Tables[0].TableName = tableName;
            string json = Base.JsonHelper.SerializeObject(ds);
            sb.Append(json);
            sb.Append("},\"code\":1,\"message\":\"成功\"}");
            Base.JsonHelper.GetDictionary(sb.ToString(), type);
            return sb.ToString();
        }
    }
}