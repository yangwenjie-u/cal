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
    /// HSZ.xaml 的交互逻辑
    /// </summary>
    public partial class HSZ : Window
    {
        public HSZ()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "蒸压灰砂砖";
            string tablename = "S_HSZ";
            string err = "";

            Dictionary<string, string> testDatas = new Dictionary<string, string>();

            #region 前段字段赋值
            testDatas.Add("CD1", this.cd1.Text);
            testDatas.Add("KD1", this.kd1.Text);
            testDatas.Add("KYHZ1", this.klhz1.Text);
            testDatas.Add("CD2", this.cd2.Text);
            testDatas.Add("KD2", this.kd2.Text);
            testDatas.Add("KYHZ2", this.klhz2.Text);
            testDatas.Add("CD3", this.cd3.Text);
            testDatas.Add("KD3", this.kd3.Text);
            testDatas.Add("KYHZ3", this.klhz3.Text);
            testDatas.Add("CD4", this.cd4.Text);
            testDatas.Add("KD4", this.kd4.Text);
            testDatas.Add("KYHZ4", this.klhz4.Text);
            testDatas.Add("CD5", this.cd5.Text);
            testDatas.Add("KD5", this.kd5.Text);
            testDatas.Add("KYHZ5", this.klhz5.Text);

            testDatas.Add("QCD1", this.qcd1.Text);
            testDatas.Add("GD1", this.gd1.Text);
            testDatas.Add("KZHZ1", this.kzhz1.Text);
            testDatas.Add("QCD2", this.qcd2.Text);
            testDatas.Add("GD2", this.gd2.Text);
            testDatas.Add("KZHZ2", this.kzhz2.Text);
            testDatas.Add("QCD3", this.qcd3.Text);
            testDatas.Add("GD3", this.gd3.Text);
            testDatas.Add("KZHZ3", this.kzhz3.Text);
            testDatas.Add("QCD4", this.qcd4.Text);
            testDatas.Add("GD4", this.gd4.Text);
            testDatas.Add("KZHZ4", this.kzhz4.Text);
            testDatas.Add("QCD5", this.qcd5.Text);
            testDatas.Add("GD5", this.gd5.Text);
            testDatas.Add("KZHZ5", this.kzhz5.Text);

            testDatas.Add("DHCD1", this.qcd1.Text);
            testDatas.Add("DHKD1", this.dhkd1.Text);
            testDatas.Add("DHKYHZ1", this.dhkyhz1.Text);
            testDatas.Add("DHCD2", this.qcd2.Text);
            testDatas.Add("DHKD2", this.dhkd2.Text);
            testDatas.Add("DHKYHZ2", this.dhkyhz2.Text);
            testDatas.Add("DHCD3", this.qcd3.Text);
            testDatas.Add("DHKD3", this.dhkd3.Text);
            testDatas.Add("DHKYHZ3", this.dhkyhz3.Text);
            testDatas.Add("DHCD4", this.qcd4.Text);
            testDatas.Add("DHKD4", this.dhkd4.Text);
            testDatas.Add("DHKYHZ4", this.dhkyhz4.Text);
            testDatas.Add("DHCD5", this.qcd5.Text);
            testDatas.Add("DHKD5", this.dhkd5.Text);
            testDatas.Add("DHKYHZ5", this.dhkyhz5.Text);

            testDatas.Add("DQZL1", this.dqzl1.Text);
            testDatas.Add("DHZL1", this.dhzl1.Text);
            testDatas.Add("DQZL2", this.dqzl2.Text);
            testDatas.Add("DHZL2", this.dhzl2.Text);
            testDatas.Add("DQZL3", this.dqzl3.Text);
            testDatas.Add("DHZL3", this.dhzl3.Text);
            testDatas.Add("DQZL4", this.dqzl4.Text);
            testDatas.Add("DHZL4", this.dhzl4.Text);
            testDatas.Add("DQZL5", this.dqzl5.Text);
            testDatas.Add("DHZL5", this.dhzl5.Text);

            testDatas.Add("JCXM", this.jcxm.Text);

            #endregion

            #region 查询sql
            string sqlStr = @" select top 100 
                       CD1,CD2,CD3,CD4,CD5,KD1,KD2,KD3,KD4,KD5,KYHZ1,KYHZ2,KYHZ3,KYHZ4,KYHZ5
					   ,QCD1,QCD2,QCD3,QCD4,QCD5,GD1,GD2,GD3,GD4,GD5,KZHZ1,KZHZ2,KZHZ3,KZHZ4,KZHZ5
                       ,DHCD1,DHCD2,DHCD3,DHCD4,DHCD5
                       ,DHKD1,DHKD2,DHKD3,DHKD4,DHKD5
                       ,DHKYHZ1,DHKYHZ2,DHKYHZ3,DHKYHZ4,DHKYHZ5
                       ,DQZL1,DQZL2,DQZL3,DQZL4,DQZL5
					   ,DHZL1,DHZL2,DHZL3,DHZL4,DHZL5
					   ,JCXM,DKZX
					   ,KYQD1,KYQD2,KYQD3,KYQD4,KYQD5
					   ,KZQD1,KZQD2,KZQD3,KZQD4,KZQD5
					   ,ZLSSL1,ZLSSL2,ZLSSL3,ZLSSL4,ZLSSL5
					   ,KYPJ,KYQDMIN,KYMIN_HG,KYPJZ_HG,KZPJ,KZQDMIN,KZMIN_HG,KZPJZ_HG
					   ,DHPJZ_HG,DHZLSSL_HG,DHPJZ,DHZLSSL,LQ,BZC,BYXS,BZZ,JCJG,ZZRQ,SJDJ,RECID,GG,GGXH
                        from SHSZ
                        ";


            #endregion
            //获取测试数据
            string JsonhelperData = GetDataJson(type, sqlStr, tablename, testDatas);

            sqlStr = "select top 100 * from  MHSZ";
            string m_data = GetDataJson(type, sqlStr, "MHSZ");

            //获取帮助表数据
            string extraDatajson = GetDataJson(type, "select * from HSZDJ", "HSZDJ");

            var listExtraData = Base.JsonHelper.GetDictionary(extraDatajson, type);
            var retSData = Base.JsonHelper.GetDictionary(JsonhelperData, type);
            var retMData = Base.JsonHelper.GetDictionary(m_data, type);

            IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData = new Dictionary<string, IDictionary<string, IList<IDictionary<string, string>>>>();

            foreach (string key in retMData.Keys)
            {
                if (!retSData.ContainsKey(key))
                    retSData.Add(key, retMData[key]);
            }
            retData.Add("蒸压灰砂砖", retSData);


            //Calculates.HSZ.Calc();
            //this.jcjg.Text = err;
        }
        public static string GetDataJson(string type, string sqlstr, string tableName, Dictionary<string, string> testDatas = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"calcData\":{\"" + type + "\":");

            string sql = string.Format(@sqlstr);
            Base.SqlBase sqlbase = new Base.SqlBase(Base.ESqlConnType.ConnectionStringLocal);
            DataSet ds = sqlbase.ExecuteDataset(sql);

            if (testDatas != null)
            {
                ds.Tables[0].Rows[0][0] = testDatas["CD1"];
                ds.Tables[0].Rows[0][1] = testDatas["CD2"];
                ds.Tables[0].Rows[0][2] = testDatas["CD3"];
                ds.Tables[0].Rows[0][3] = testDatas["CD4"];
                ds.Tables[0].Rows[0][4] = testDatas["CD5"];
                ds.Tables[0].Rows[0][5] = testDatas["KD1"];
                ds.Tables[0].Rows[0][6] = testDatas["KD2"];
                ds.Tables[0].Rows[0][7] = testDatas["KD3"];
                ds.Tables[0].Rows[0][8] = testDatas["KD4"];
                ds.Tables[0].Rows[0][9] = testDatas["KD5"];
                ds.Tables[0].Rows[0][10] = testDatas["KYHZ1"];
                ds.Tables[0].Rows[0][11] = testDatas["KYHZ2"];
                ds.Tables[0].Rows[0][12] = testDatas["KYHZ3"];
                ds.Tables[0].Rows[0][13] = testDatas["KYHZ4"];
                ds.Tables[0].Rows[0][14] = testDatas["KYHZ5"];
                ds.Tables[0].Rows[0][15] = testDatas["QCD1"];
                ds.Tables[0].Rows[0][16] = testDatas["QCD2"];
                ds.Tables[0].Rows[0][17] = testDatas["QCD3"];
                ds.Tables[0].Rows[0][18] = testDatas["QCD4"];
                ds.Tables[0].Rows[0][19] = testDatas["QCD5"];
                ds.Tables[0].Rows[0][20] = testDatas["GD1"];
                ds.Tables[0].Rows[0][21] = testDatas["GD2"];
                ds.Tables[0].Rows[0][22] = testDatas["GD3"];
                ds.Tables[0].Rows[0][23] = testDatas["GD4"];
                ds.Tables[0].Rows[0][24] = testDatas["GD5"];
                ds.Tables[0].Rows[0][25] = testDatas["KZHZ1"];
                ds.Tables[0].Rows[0][26] = testDatas["KZHZ2"];
                ds.Tables[0].Rows[0][27] = testDatas["KZHZ3"];
                ds.Tables[0].Rows[0][28] = testDatas["KZHZ4"];
                ds.Tables[0].Rows[0][29] = testDatas["KZHZ5"];
                ds.Tables[0].Rows[0][30] = testDatas["DHCD1"];
                ds.Tables[0].Rows[0][31] = testDatas["DHCD2"];
                ds.Tables[0].Rows[0][32] = testDatas["DHCD3"];
                ds.Tables[0].Rows[0][33] = testDatas["DHCD4"];
                ds.Tables[0].Rows[0][34] = testDatas["DHCD5"];
                ds.Tables[0].Rows[0][35] = testDatas["DHKD1"];
                ds.Tables[0].Rows[0][36] = testDatas["DHKD2"];
                ds.Tables[0].Rows[0][37] = testDatas["DHKD3"];
                ds.Tables[0].Rows[0][38] = testDatas["DHKD4"];
                ds.Tables[0].Rows[0][39] = testDatas["DHKD5"];
                ds.Tables[0].Rows[0][40] = testDatas["DHKYHZ1"];
                ds.Tables[0].Rows[0][41] = testDatas["DHKYHZ2"];
                ds.Tables[0].Rows[0][42] = testDatas["DHKYHZ3"];
                ds.Tables[0].Rows[0][43] = testDatas["DHKYHZ4"];
                ds.Tables[0].Rows[0][44] = testDatas["DHKYHZ5"];
                ds.Tables[0].Rows[0][45] = testDatas["DQZL1"];
                ds.Tables[0].Rows[0][46] = testDatas["DQZL2"];
                ds.Tables[0].Rows[0][47] = testDatas["DQZL3"];
                ds.Tables[0].Rows[0][48] = testDatas["DQZL4"];
                ds.Tables[0].Rows[0][49] = testDatas["DQZL5"];
                ds.Tables[0].Rows[0][50] = testDatas["DHZL1"];
                ds.Tables[0].Rows[0][51] = testDatas["DHZL2"];
                ds.Tables[0].Rows[0][52] = testDatas["DHZL3"];
                ds.Tables[0].Rows[0][53] = testDatas["DHZL4"];
                ds.Tables[0].Rows[0][54] = testDatas["DHZL5"];
                ds.Tables[0].Rows[0][55] = testDatas["JCXM"];

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
