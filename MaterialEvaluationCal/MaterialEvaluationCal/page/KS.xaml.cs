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
    public partial class KS : Window
    {
        public KS()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //混泥土抗渗
            string type = "S_KS";
            string tablename = "SKS";
            string err = "";

            Dictionary<string, string> testDatas = new Dictionary<string, string>();

            #region 前段字段赋值
            testDatas.Add("QDYQ1", this.qdyq1.Text);
            testDatas.Add("QDYQ2", this.qdyq2.Text);
            testDatas.Add("QDYQ3", this.qdyq3.Text);
            testDatas.Add("QDYQ4", this.qdyq4.Text);
            testDatas.Add("QDYQ5", this.qdyq5.Text);
            testDatas.Add("QDYQ6", this.qdyq6.Text);
            testDatas.Add("SFBSS1", this.sfbss1.Text);
            testDatas.Add("SFBSS2", this.sfbss2.Text);
            testDatas.Add("SFBSS3", this.sfbss3.Text);
            testDatas.Add("SFBSS4", this.sfbss4.Text);
            testDatas.Add("SFBSS5", this.sfbss5.Text);
            testDatas.Add("SFBSS6", this.sfbss6.Text);


            #endregion

            #region 查询sql
            string sqlStr = @" select top 100 
QDYQ1,QDYQ2,QDYQ3,QDYQ4,QDYQ5,QDYQ6,SFBSS1,SFBSS2,SFBSS3,SFBSS4,SFBSS5,SFBSS6,GDYQ1,GDYQ2,GDYQ3,GDYQ4,GDYQ5,GDYQ6,KS_HG,JCJG,SJDJ

                        from SKS
                        ";


            #endregion
            //获取测试数据
            string JsonhelperData = Base.JsonHelper.GetAfferentDataJson(type, tablename);

            //获取帮助表数据
            string extraDatajson = Base.JsonHelper.GetExtraDataJson("BZ_KS_DJ", "KSDJ");

            var listExtraData = Base.JsonHelper.GetDictionary(extraDatajson, "BZ_KS_DJ");
            var retSData = Base.JsonHelper.GetAfferentDictionary(JsonhelperData);

            Calculates.KS.Calc();
            this.jcjg.Text = Base.JsonHelper.SerializeObject(retSData["BGJG"]);
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
                ds.Tables[0].Rows[0][0] = testDatas["QDYQ1"];
                ds.Tables[0].Rows[0][1] = testDatas["QDYQ2"];
                ds.Tables[0].Rows[0][2] = testDatas["QDYQ3"];
                ds.Tables[0].Rows[0][3] = testDatas["QDYQ4"];
                ds.Tables[0].Rows[0][4] = testDatas["QDYQ5"];
                ds.Tables[0].Rows[0][5] = testDatas["QDYQ6"];
                ds.Tables[0].Rows[0][6] = testDatas["SFBSS1"];
                ds.Tables[0].Rows[0][7] = testDatas["SFBSS2"];
                ds.Tables[0].Rows[0][8] = testDatas["SFBSS3"];
                ds.Tables[0].Rows[0][9] = testDatas["SFBSS4"];
                ds.Tables[0].Rows[0][10] = testDatas["SFBSS5"];
                ds.Tables[0].Rows[0][11] = testDatas["SFBSS6"];
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
