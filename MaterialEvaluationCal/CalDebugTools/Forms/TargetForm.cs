using CalDebugTools.Common.DBUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalDebugTools.Forms
{
    public partial class TargetForm : Form
    {
        FormMain _formMain;
        private Common.DBUtility.SqlBase _sqlBase = null;
        private Common.DBUtility.SqlBase _sqlDebugTool = null;
        /// <summary>
        /// 检测监管数据库连接
        /// </summary>
        private Common.DBUtility.SqlBase _sqlJGJG = null;
        public TargetForm(FormMain main)
        {
            InitializeComponent();
            //Init();
            _formMain = main;
            if (_sqlBase == null)
                _sqlBase = new SqlBase();
            if (_sqlDebugTool == null)
                _sqlDebugTool = new SqlBase(ESqlConnType.ConnectionStringDebugTool);
            if (_sqlJGJG == null)
                _sqlJGJG = new SqlBase(ESqlConnType.ConnectionStringJCJG);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否创建帮助表数据?", "Confirm Message", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }

            string xmbh = string.IsNullOrEmpty(txt_xmbh.Text) ? "" : txt_xmbh.Text.Trim();
            string fieldName = string.IsNullOrEmpty(txt_fieldName.Text) ? "" : txt_fieldName.Text.Trim();
            string fieldMS = string.IsNullOrEmpty(txt_fieldMs.Text) ? "" : txt_fieldMs.Text.Trim();
            string fieldType = string.IsNullOrEmpty(txt_fieldType.Text) ? "" : txt_fieldType.Text.Trim();

            if (string.IsNullOrEmpty(xmbh))
            {
                MessageBox.Show("输入项目编号！");
                return;
            }
            if (string.IsNullOrEmpty(fieldName))
            {
                MessageBox.Show("输入字段名！");
                return;
            }
            if (string.IsNullOrEmpty(fieldMS))
            {
                MessageBox.Show("输入字段描述！");
                return;
            }
            if (string.IsNullOrEmpty(fieldType))
            {
                MessageBox.Show("输入字段类型！");
                return;
            }
            string sqlstr = string.Format($" select top 1 * FROM  M_{xmbh}");

            if (_sqlBase.ExecuteDataset(sqlstr) == null)
            {
                MessageBox.Show($"项目{xmbh}不存在！");
                return;
            }
            int queryCount = 0;//返回受影响的行数
            try
            {
                //判定是否已有字段
                string sqlStr = $"select 1 from ZDZD_{xmbh} where ZDMC in ('HG_{fieldName}','HG_{fieldName}','{fieldName}')";

                var ds2 = _sqlBase.ExecuteDataset(sqlStr);
                var tableType = radio_m.Checked ? "M_" : "S_";
                List<string> lst = new List<string>();

                //zdzd表添加记录
                if (ds2 != null && ds2.Tables[0].Rows.Count != 0)
                {
                    MessageBox.Show($"添加失败,ZDZD_{xmbh}中已存在{fieldName}相关字段");
                    return;
                }
                else
                {
                    string txtLX = string.IsNullOrEmpty(txt_lx.Text) ? "H" : txt_lx.Text.Trim();
                    string chksfxs = this.chk_SFXS.Checked ? "1" : "0";
                    string locstionStr = "1,1";
                    sqlStr = $"insert into ZDZD_{xmbh} ( SJBMC, ZDMC, SY, ZDLX, ZDCD1, ZDCD2, INPUTZDLX, KJLX, SFBHZD, BHMS,ZDSX, SFXS, XSCD, XSSX, SFGD, MUSTIN, DEFAVAL, HELPLNK, CTRLSTRING, ZDXZ,WXSSX, WSFXS, MSGINFO, EQLFUNC, HELPWHERE, GETBYBH, SSJCX, SFBGZD,VALIDPROC, LX, ZDSXSQL, ENCRYPT, FZYC, FZCS, NOSAVE, location)" +
    $"VALUES('{tableType}{xmbh}', 'HG_{fieldName}', '判定{fieldMS}', 'nvarchar', '200', '0', 'nvarchar', '', 'False', '', 'False', '{chksfxs}', '0', '367.0000', 'False', 'False', '', '', '', 'S', '367.0000', 'True', '', '', '', 'True', '', 'True', '', '{txtLX}', NULL, NULL, NULL, NULL, NULL, '{locstionStr}')  " +
    $"";
                    lst.Add(sqlStr);
                    sqlStr = $"insert into ZDZD_{xmbh} ( SJBMC, ZDMC, SY, ZDLX, ZDCD1, ZDCD2, INPUTZDLX, KJLX, SFBHZD, BHMS,ZDSX, SFXS, XSCD, XSSX, SFGD, MUSTIN, DEFAVAL, HELPLNK, CTRLSTRING, ZDXZ,WXSSX, WSFXS, MSGINFO, EQLFUNC, HELPWHERE, GETBYBH, SSJCX, SFBGZD,VALIDPROC, LX, ZDSXSQL, ENCRYPT, FZYC, FZCS, NOSAVE, location)" +
    $"VALUES('M_{xmbh}', 'G_{fieldName}', '要求{fieldMS}', 'nvarchar', '200', '0', 'nvarchar', '', 'False', '', 'False', '{chksfxs}', '0', '367.0000', 'False', 'False', '', '', '', 'S', '367.0000', 'True', '', '', '', 'True', '', 'True', '', '{txtLX}', NULL, NULL, NULL, NULL, NULL, '{locstionStr}')  " +
    $"";
                    lst.Add(sqlStr);


                    if (txt_bzCount.Text == "1")
                    {
                        sqlStr = $"insert into ZDZD_{xmbh} ( SJBMC, ZDMC, SY, ZDLX, ZDCD1, ZDCD2, INPUTZDLX, KJLX, SFBHZD, BHMS,ZDSX, SFXS, XSCD, XSSX, SFGD, MUSTIN, DEFAVAL, HELPLNK, CTRLSTRING, ZDXZ,WXSSX, WSFXS, MSGINFO, EQLFUNC, HELPWHERE, GETBYBH, SSJCX, SFBGZD,VALIDPROC, LX, ZDSXSQL, ENCRYPT, FZYC, FZCS, NOSAVE, location)" +
   $"VALUES('BZ_{xmbh}_DJ', 'G_{fieldName}', '{fieldMS}', 'nvarchar', '200', '0', 'nvarchar', '', 'False', '', 'False', '{chksfxs}', '0', '367.0000', 'False', 'False', '', '', '', 'S', '367.0000', 'True', '', '', '', 'True', '', 'True', '', '{txtLX}', NULL, NULL, NULL, NULL, NULL, '{locstionStr}')  ";
                        lst.Add(sqlStr);
                    }
                    else
                    {
                        for (int i = 1; i < Convert.ToInt16(txt_bzCount.Text) + 1; i++)
                        {
                            sqlStr = $"insert into ZDZD_{xmbh} ( SJBMC, ZDMC, SY, ZDLX, ZDCD1, ZDCD2, INPUTZDLX, KJLX, SFBHZD, BHMS,ZDSX, SFXS, XSCD, XSSX, SFGD, MUSTIN, DEFAVAL, HELPLNK, CTRLSTRING, ZDXZ,WXSSX, WSFXS, MSGINFO, EQLFUNC, HELPWHERE, GETBYBH, SSJCX, SFBGZD,VALIDPROC, LX, ZDSXSQL, ENCRYPT, FZYC, FZCS, NOSAVE, location)" +
   $"VALUES('BZ_{xmbh}_DJ', 'G_{fieldName}{i}', '{fieldMS}{i}', 'nvarchar', '200', '0', 'nvarchar', '', 'False', '', 'False', '{chksfxs}', '0', '367.0000', 'False', 'False', '', '', '', 'S', '367.0000', 'True', '', '', '', 'True', '', 'True', '', '{txtLX}', NULL, NULL, NULL, NULL, NULL, '{locstionStr}')  ";
                            lst.Add(sqlStr);
                        }
                    }

                    if (txt_STabCount.Text == "1")
                    {
                        sqlStr = $"insert into ZDZD_{xmbh} ( SJBMC, ZDMC, SY, ZDLX, ZDCD1, ZDCD2, INPUTZDLX, KJLX, SFBHZD, BHMS,ZDSX, SFXS, XSCD, XSSX, SFGD, MUSTIN, DEFAVAL, HELPLNK, CTRLSTRING, ZDXZ,WXSSX, WSFXS, MSGINFO, EQLFUNC, HELPWHERE, GETBYBH, SSJCX, SFBGZD,VALIDPROC, LX, ZDSXSQL, ENCRYPT, FZYC, FZCS, NOSAVE, location)" +
    $"VALUES('S_{xmbh}', '{fieldName}', '{fieldMS}', 'nvarchar', '200', '0', 'nvarchar', '', 'False', '', 'False', '{chksfxs}', '0', '367.0000', 'False', 'False', '', '', '', 'S', '367.0000', 'True', '', '', '', 'True', '', 'True', '', '{txtLX}', NULL, NULL, NULL, NULL, NULL, '{locstionStr}')";
                        lst.Add(sqlStr);
                    }
                    else
                    {
                        for (int i = 1; i < Convert.ToInt16(txt_STabCount.Text) + 1; i++)
                        {
                            sqlStr = $"insert into ZDZD_{xmbh} ( SJBMC, ZDMC, SY, ZDLX, ZDCD1, ZDCD2, INPUTZDLX, KJLX, SFBHZD, BHMS,ZDSX, SFXS, XSCD, XSSX, SFGD, MUSTIN, DEFAVAL, HELPLNK, CTRLSTRING, ZDXZ,WXSSX, WSFXS, MSGINFO, EQLFUNC, HELPWHERE, GETBYBH, SSJCX, SFBGZD,VALIDPROC, LX, ZDSXSQL, ENCRYPT, FZYC, FZCS, NOSAVE, location)" +
        $"VALUES('S_{xmbh}', '{fieldName}{i}', '{fieldMS}{i}', 'nvarchar', '200', '0', 'nvarchar', '', 'False', '', 'False', '{chksfxs}', '0', '367.0000', 'False', 'False', '', '', '', 'S', '367.0000', 'True', '', '', '', 'True', '', 'True', '', '{txt_lx}', NULL, NULL, NULL, NULL, NULL, '{locstionStr}')";
                            lst.Add(sqlStr);
                        }
                    }

                }
                string alterSql = "";
                if (txt_bzCount.Text == "1")
                {
                    alterSql = $"alter table BZ_{xmbh.Trim()}_DJ add G_{fieldName} {txt_fieldType.Text.Trim()};";
                }
                else
                {
                    for (int i = 1; i < Convert.ToInt16(txt_bzCount.Text) + 1; i++)
                    {
                        alterSql += $"alter table BZ_{xmbh.Trim()}_DJ add G_{fieldName}{i} {txt_fieldType.Text.Trim()};";
                    }
                }

                queryCount = _sqlBase.ExecuteNonQuery(alterSql);
                //主表 从表添加记录
                var alterM = "";


                //添加主/从表字段
                alterM = $"alter table {tableType}{xmbh} add HG_{fieldName} nvarchar(15);";
                alterM += $"alter table M_{xmbh} add G_{fieldName} {fieldType};";

                if (txt_STabCount.Text == "1")
                {
                    alterM += $"alter table S_{xmbh} add {fieldName} {fieldType};";
                }
                else
                {
                    {
                        for (int i = 1; i < Convert.ToInt16(txt_STabCount.Text) + 1; i++)
                        {
                            alterM += $"alter table S_{xmbh} add {fieldName}{i} {fieldType};";
                        }
                    }
                }

                if (chk_syncJcJG.Checked)
                {
                    _sqlJGJG.ExecuteNonQuery(alterM);
                }
                queryCount = _sqlBase.ExecuteNonQuery(alterM);
                foreach (var item in lst)
                {
                    try
                    {
                        //两个数据添加
                        _sqlBase.ExecuteNonQuery(item);
                        _sqlDebugTool.ExecuteNonQuery(item);

                        if (chk_syncJcJG.Checked)
                        {
                            queryCount = _sqlJGJG.ExecuteNonQuery(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Success!");
        }

        private DataTable DT = new DataTable();
        private SqlDataAdapter SDA = new SqlDataAdapter();

        private void txt_STabCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))     //判断按键输入字符是不是数字
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;   //表示按键输入已经被处理,这样按键将不会给应用程序,丢掉不想要的按键值,这样的缺点是backspace也会被返回
                }
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommandBuilder SCB = new SqlCommandBuilder(SDA);
                SDA.Update(DT);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

            MessageBox.Show("更新成功！");
        }

        private void TargetForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _formMain.Show();
        }

        private void Init(string syxmbh)
        {
            string str_conn = ConfigurationManager.ConnectionStrings["ConnectionStringMain"].ConnectionString;//integrated Security=true";

            //1、用于从数据库中获取数据的查询字符串
            //2、开始建立建立并打开连接
            SqlConnection myconn = new SqlConnection(str_conn);

            myconn.Open();
            //var syxmbh = "RF";
            var sqlFields = "";
            var sqlFields2 = "";
            var sqlWhere = "";
            DataSet ds = _sqlBase.ExecuteDataset($"select  zdmc,sy from ZDZD_{syxmbh} where  SJBMC ='BZ_{syxmbh}_DJ'");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    Console.Write(item[0].ToString() + "\t");
                    sqlFields += item[0].ToString() + $" as '{item[1].ToString()}',";
                    sqlFields2 += item[0].ToString() + ",";
                }
            }

            sqlFields = sqlFields.TrimEnd(',');
            sqlFields2 = sqlFields2.TrimEnd(',');

            if (!string.IsNullOrEmpty(txt_where.Text))
            {
                sqlWhere = " and  " + txt_where.Text;
            }
            string str_select = $"select {sqlFields2} from BZ_{syxmbh}_DJ where 1=1  {sqlWhere}";
            DT.Clear();
            SqlCommand SCD = new SqlCommand(str_select, myconn);
            SDA.SelectCommand = SCD;
            SDA.Fill(DT);
            dataGridView1.DataSource = DT;

            str_select = $"select {sqlFields} from BZ_{syxmbh}_DJ where 1=2";
            SqlDataAdapter sql_Adapter = new SqlDataAdapter(str_select, myconn);

            DataSet dataset1 = new DataSet();

            //5、使用SqlDataAdapater.Fill(DataSet_Name,index_name)方法将读取的数据存入DataSet定义的名为任意名的Datatable中，其中任意名表用于数据的标识(索引)
            sql_Adapter.Fill(dataset1, "任意名");

            //6、将DataTable表中employee表的视图赋值给控件DataGridView以便输出
            dataGridView2.DataSource = dataset1.Tables["任意名"].DefaultView;
            //7、关闭数据库连接
            myconn.Close();
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            string xmbh = string.IsNullOrEmpty(txt_xmbh.Text) ? "" : txt_xmbh.Text.Trim();

            if (string.IsNullOrEmpty(xmbh))
            {
                MessageBox.Show("输入项目编号！");
                return;
            }
            Init(xmbh);
        }

        private void btn_S_only_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("添加从表字段?", "Confirm Message", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
            CreateTableColumn("S");
        }

        private void btn_M_only_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否添加主主主主主表字段?", "Confirm Message", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
            CreateTableColumn("M");

        }

        private void btn_helper_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("添加到帮助表?", "Confirm Message", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
            CreateTableColumn("H");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">M:添加到主表 S:添加到从表 H:添加到帮助表</param>
        public void CreateTableColumn(string type)
        {
            string xmbh = string.IsNullOrEmpty(txt_xmbh.Text) ? "" : txt_xmbh.Text.Trim();
            string fieldName = string.IsNullOrEmpty(txt_fieldName.Text) ? "" : txt_fieldName.Text.Trim();
            string fieldMS = string.IsNullOrEmpty(txt_fieldMs.Text) ? "" : txt_fieldMs.Text.Trim();
            string fieldType = string.IsNullOrEmpty(txt_fieldType.Text) ? "" : txt_fieldType.Text.Trim();
            string txtLX = string.IsNullOrEmpty(txt_lx.Text) ? "H" : txt_lx.Text.Trim();
            string chksfxs = this.chk_SFXS.Checked ? "1" : "0";
            string locstionStr = "1,1";

            if (string.IsNullOrEmpty(xmbh))
            {
                MessageBox.Show("输入项目编号！");
                return;
            }
            if (string.IsNullOrEmpty(fieldName))
            {
                MessageBox.Show("输入字段名！");
                return;
            }
            if (string.IsNullOrEmpty(fieldMS))
            {
                MessageBox.Show("输入字段描述！");
                return;
            }
            if (string.IsNullOrEmpty(fieldType))
            {
                MessageBox.Show("输入字段类型！");
                return;
            }
            string sqlstr = string.Format($" select top 1 * FROM  M_{xmbh}");

            if (_sqlBase.ExecuteDataset(sqlstr) == null)
            {
                MessageBox.Show($"项目{xmbh}不存在！");
                return;
            }

            int queryCount = 0;//返回受影响的行数


            string tableName = "";
            switch (type)
            {
                case "M":
                    tableName = "M_" + xmbh;
                    break;
                case "S":
                    tableName = "S_" + xmbh;
                    break;
                case "H":
                    tableName = "BZ_" + xmbh + "_DJ";
                    break;
            }
            try
            {
                //主表 从表添加记录
                var alterM = "";
                if (txt_STabCount.Text == "1")
                {
                    alterM += $"alter table {tableName} add {fieldName} {fieldType};";
                }
                else
                {
                    for (int i = 1; i < Convert.ToInt16(txt_STabCount.Text) + 1; i++)
                    {
                        alterM += $"alter table {tableName} add {fieldName}{i} {fieldType};";
                    }
                }
                queryCount = _sqlBase.ExecuteNonQuery(alterM);

                if (chk_syncJcJG.Checked)
                {
                    queryCount = _sqlJGJG.ExecuteNonQuery(alterM);

                }
                string sqlStr = "";

                List<string> lst = new List<string>();
                if (txt_STabCount.Text == "1")
                {
                    sqlStr = $"insert into ZDZD_{xmbh} ( SJBMC, ZDMC, SY, ZDLX, ZDCD1, ZDCD2, INPUTZDLX, KJLX, SFBHZD, BHMS,ZDSX, SFXS, XSCD, XSSX, SFGD, MUSTIN, DEFAVAL, HELPLNK, CTRLSTRING, ZDXZ,WXSSX, WSFXS, MSGINFO, EQLFUNC, HELPWHERE, GETBYBH, SSJCX, SFBGZD,VALIDPROC, LX, ZDSXSQL, ENCRYPT, FZYC, FZCS, NOSAVE, location)" +
        $"VALUES('{tableName}', '{fieldName}', '{fieldMS}', 'nvarchar', '200', '0', 'nvarchar', '', 'False', '', 'False', '{chksfxs}', '0', '367.0000', 'False', 'False', '', '', '', 'S', '367.0000', 'True', '', '', '', 'True', '', 'True', '', '{txtLX}', NULL, NULL, NULL, NULL, NULL, '{locstionStr}')  ";
                    lst.Add(sqlStr);
                }
                else
                {
                    for (int i = 1; i < Convert.ToInt16(txt_STabCount.Text) + 1; i++)
                    {
                        sqlStr = $"insert into ZDZD_{xmbh} ( SJBMC, ZDMC, SY, ZDLX, ZDCD1, ZDCD2, INPUTZDLX, KJLX, SFBHZD, BHMS,ZDSX, SFXS, XSCD, XSSX, SFGD, MUSTIN, DEFAVAL, HELPLNK, CTRLSTRING, ZDXZ,WXSSX, WSFXS, MSGINFO, EQLFUNC, HELPWHERE, GETBYBH, SSJCX, SFBGZD,VALIDPROC, LX, ZDSXSQL, ENCRYPT, FZYC, FZCS, NOSAVE, location)" +
        $"VALUES('{tableName}', '{fieldName}{i}', '{fieldMS}{i}', 'nvarchar', '200', '0', 'nvarchar', '', 'False', '', 'False', '{chksfxs}', '0', '367.0000', 'False', 'False', '', '', '', 'S', '367.0000', 'True', '', '', '', 'True', '', 'True', '', '{txtLX}', NULL, NULL, NULL, NULL, NULL, '{locstionStr}')  ";
                        lst.Add(sqlStr);
                    }
                }
                foreach (var item in lst)
                {
                    try
                    {
                        //两个数据添加
                        _sqlBase.ExecuteNonQuery(item);
                        _sqlDebugTool.ExecuteNonQuery(item);

                        if (chk_syncJcJG.Checked)
                        {
                            queryCount = _sqlJGJG.ExecuteNonQuery(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            MessageBox.Show("Success!");

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
