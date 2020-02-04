using CalDebugTools.Common.DBUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalDebugTools.Forms
{
    public partial class ZDZDTableSync : Form
    {
        FormMain _formMain;
        public ZDZDTableSync(FormMain main)
        {
            InitializeComponent();
            _formMain = main;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            string tabName = "";
            string sqlStr = "";
            if (string.IsNullOrEmpty(this.txt_tableName.Text))
            {
                //同步所有的zdzdbiao
                sqlStr = $"  select name from sysobjects where xtype='u'  and name like 'ZDZD_%'";
            }
            else
            {
                //指定的表
                tabName = this.txt_tableName.Text;
                sqlStr = $" select name from sysobjects where xtype='u'  and name = 'ZDZD_{tabName}'";
            }
            //ZDZD_ZX
            SqlBase _sqlBase = new SqlBase(ESqlConnType.ConnectionStringMain);
            SqlBase _DebugTool = new SqlBase(ESqlConnType.ConnectionStringDebugTool);

            //string sqlStr = string.Format($"SELECT 表名 = D.name,字段序号 = A.colorder,字段名 = A.name,字段说明 = isnull(G.[value], ''),类型 = B.name,占用字节数 = A.Length " +
            //   $"FROM syscolumns A  Left Join systypes B On A.xusertype = B.xusertype Inner Join sysobjects D On A.id = D.id and D.xtype = 'U' and D.name <> 'dtproperties'" +
            //   $" Left Join syscomments E on A.cdefault = E.id Left Join sys.extended_properties G on A.id = G.major_id and A.colid = G.minor_id Left Join sys.extended_properties F On D.id = F.major_id and F.minor_id = 0 " +
            //   $"where d.name = '{tabName}' "
            //   + wheresql +
            //   $"  Order By A.id, A.colorder");

            var dtTableName = _sqlBase.ExecuteDataset(sqlStr);
            if (dtTableName.Tables.Count == 0)
            {
                return;
            }
            var dt = dtTableName.Tables[0];

            var sql = "";
            var syxmbh = "";
            DataSet ds = null;
            foreach (DataRow item in dt.Rows)
            {
                syxmbh = item["name"].ToString().ToUpper().Replace("ZDZD_", "");
                ds = _DebugTool.ExecuteDataset($" select name from sysobjects where xtype='u'  and name = 'ZDZD_{syxmbh}'");
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    //如果已经同步过，则执行下一个
                    //MessageBox.Show($"项目{syxmbh}已经同步");
                    continue;
                }
                sql = $"select * into ZDZD_{syxmbh} from jcjt_wh.dbo.ZDZD_{syxmbh} where sjbmc like 'S_{syxmbh}' or sjbmc like 'M_{syxmbh}'";
                //sql = $"select * into ZDZD_{syxmbh} from ITSV.jcjt_wh.dbo.ZDZD_{syxmbh} where sjbmc like 'S_{syxmbh}' or sjbmc like 'M_{syxmbh}'";
                _DebugTool.ExecuteNonQuery(sql);
            }
            //select* from ITSV.jcjt_wh.dbo.ZDZD_ZX where sjbmc like 'S_%' or sjbmc like 'M_%'
        }

        private void ZDZDTableSync_FormClosing(object sender, FormClosingEventArgs e)
        {
            _formMain.Show();
        }
    }
}
