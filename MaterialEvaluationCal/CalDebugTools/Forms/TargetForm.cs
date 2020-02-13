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
    public partial class TargetForm : Form
    {
        FormMain _formMain;
        private Common.DBUtility.SqlBase _sqlBase = null;
        private Common.DBUtility.SqlBase _sqlDebugTool = null;
        public TargetForm(FormMain main)
        {
            InitializeComponent();
            _formMain = main;
            if (_sqlBase == null)
                _sqlBase = new SqlBase();
            if (_sqlDebugTool == null)
                _sqlDebugTool = new SqlBase(ESqlConnType.ConnectionStringDebugTool);
        }

        private void TargetForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
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
            string alterSql = $"alter table BZ_{xmbh.Trim()}_DJ add {fieldName} {txt_fieldType.Text.Trim()}";

            try
            {
                queryCount = _sqlBase.ExecuteNonQuery(alterSql);

                //主表 从表添加记录
                var alterM = $"alter table M_{xmbh} add HG_{fieldName} nvarchar(15);";
                alterM += $"alter table M_{xmbh} add G_{fieldName} {fieldType};";

                if (txt_STabCount.Text == "0" || txt_STabCount.Text == "1")
                {
                    alterM += $"alter table S_{xmbh} add {fieldName} {fieldType}";
                }
                else
                    for (int i = 1; i < Convert.ToInt16(txt_STabCount.Text) + 1; i++)
                    {
                        alterM += $"alter table S_{xmbh} add {fieldName}{i} {fieldType}";

                    }
                queryCount = _sqlBase.ExecuteNonQuery(alterM);

                string sqlStr = $"select 1 from ZDZD_{xmbh} where ZDMC like '%{fieldName}%'";

                var ds2 = _sqlBase.ExecuteDataset(sqlStr);
                List<string> lst = new List<string>();

                //zdzd表添加记录
                if (ds2 == null || ds2.Tables[0].Rows.Count == 0)
                {
                    sqlStr = $"insert into ZDZD_{xmbh} ( SJBMC, ZDMC, SY, ZDLX, ZDCD1, ZDCD2, INPUTZDLX, KJLX, SFBHZD, BHMS,ZDSX, SFXS, XSCD, XSSX, SFGD, MUSTIN, DEFAVAL, HELPLNK, CTRLSTRING, ZDXZ,WXSSX, WSFXS, MSGINFO, EQLFUNC, HELPWHERE, GETBYBH, SSJCX, SFBGZD,VALIDPROC, LX, ZDSXSQL, ENCRYPT, FZYC, FZCS, NOSAVE, location)" +
$"VALUES('M_{xmbh}', 'HG_{fieldName}', '{fieldMS}是否合格', 'nvarchar', '200', '0', 'nvarchar', '', 'False', '', 'False', 'False', '0', '367.0000', 'False', 'False', '', '', '', 'S', '367.0000', 'True', '', '', '', 'True', '', 'True', '', 'H,I', NULL, NULL, NULL, NULL, NULL, NULL)  " +
$"";
                    lst.Add(sqlStr);
                    sqlStr = $"insert into ZDZD_{xmbh} ( SJBMC, ZDMC, SY, ZDLX, ZDCD1, ZDCD2, INPUTZDLX, KJLX, SFBHZD, BHMS,ZDSX, SFXS, XSCD, XSSX, SFGD, MUSTIN, DEFAVAL, HELPLNK, CTRLSTRING, ZDXZ,WXSSX, WSFXS, MSGINFO, EQLFUNC, HELPWHERE, GETBYBH, SSJCX, SFBGZD,VALIDPROC, LX, ZDSXSQL, ENCRYPT, FZYC, FZCS, NOSAVE, location)" +
$"VALUES('M_{xmbh}', 'G_{fieldName}', '{fieldMS}判断标准', 'nvarchar', '200', '0', 'nvarchar', '', 'False', '', 'False', 'False', '0', '367.0000', 'False', 'False', '', '', '', 'S', '367.0000', 'True', '', '', '', 'True', '', 'True', '', 'H,I', NULL, NULL, NULL, NULL, NULL, NULL)  " +
$"";
                    lst.Add(sqlStr);

                    if (txt_STabCount.Text == "0" || txt_STabCount.Text == "1")
                    {

                        sqlStr = $"insert into ZDZD_{xmbh} ( SJBMC, ZDMC, SY, ZDLX, ZDCD1, ZDCD2, INPUTZDLX, KJLX, SFBHZD, BHMS,ZDSX, SFXS, XSCD, XSSX, SFGD, MUSTIN, DEFAVAL, HELPLNK, CTRLSTRING, ZDXZ,WXSSX, WSFXS, MSGINFO, EQLFUNC, HELPWHERE, GETBYBH, SSJCX, SFBGZD,VALIDPROC, LX, ZDSXSQL, ENCRYPT, FZYC, FZCS, NOSAVE, location)" +
      $"VALUES('S_{xmbh}', '{fieldName}', '{fieldMS}', 'nvarchar', '200', '0', 'nvarchar', '', 'False', '', 'False', 'False', '0', '367.0000', 'False', 'False', '', '', '', 'S', '367.0000', 'True', '', '', '', 'True', '', 'True', '', 'H,I', NULL, NULL, NULL, NULL, NULL, NULL)  " +
      $"";
                        lst.Add(sqlStr);
                    }
                    else
                        for (int i = 1; i < Convert.ToInt16(txt_STabCount.Text) + 1; i++)
                        {
                            sqlStr = $"insert into ZDZD_{xmbh} ( SJBMC, ZDMC, SY, ZDLX, ZDCD1, ZDCD2, INPUTZDLX, KJLX, SFBHZD, BHMS,ZDSX, SFXS, XSCD, XSSX, SFGD, MUSTIN, DEFAVAL, HELPLNK, CTRLSTRING, ZDXZ,WXSSX, WSFXS, MSGINFO, EQLFUNC, HELPWHERE, GETBYBH, SSJCX, SFBGZD,VALIDPROC, LX, ZDSXSQL, ENCRYPT, FZYC, FZCS, NOSAVE, location)" +
     $"VALUES('S_{xmbh}', '{fieldName}{i}', '{fieldMS}{i}', 'nvarchar', '200', '0', 'nvarchar', '', 'False', '', 'False', 'False', '0', '367.0000', 'False', 'False', '', '', '', 'S', '367.0000', 'True', '', '', '', 'True', '', 'True', '', 'H,I', NULL, NULL, NULL, NULL, NULL, NULL)  " +
     $"";
                            lst.Add(sqlStr);
                        }

                    sqlStr = $"insert into ZDZD_{xmbh} ( SJBMC, ZDMC, SY, ZDLX, ZDCD1, ZDCD2, INPUTZDLX, KJLX, SFBHZD, BHMS,ZDSX, SFXS, XSCD, XSSX, SFGD, MUSTIN, DEFAVAL, HELPLNK, CTRLSTRING, ZDXZ,WXSSX, WSFXS, MSGINFO, EQLFUNC, HELPWHERE, GETBYBH, SSJCX, SFBGZD,VALIDPROC, LX, ZDSXSQL, ENCRYPT, FZYC, FZCS, NOSAVE, location)" +
 $"VALUES('BZ_{xmbh}_DJ', '{fieldName}', '{fieldMS}', 'nvarchar', '200', '0', 'nvarchar', '', 'False', '', 'False', 'False', '0', '367.0000', 'False', 'False', '', '', '', 'S', '367.0000', 'True', '', '', '', 'True', '', 'True', '', 'H,I', NULL, NULL, NULL, NULL, NULL, NULL)  " +
 $"";
                    lst.Add(sqlStr);
                }

                foreach (var item in lst)
                {
                    try
                    {
                        //两个数据添加
                        _sqlBase.ExecuteNonQuery(item);
                        _sqlDebugTool.ExecuteNonQuery(item);
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
            }
            finally
            {
                MessageBox.Show("Success!");

            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txt_STabCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))     //判断按键输入字符是不是数字
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;   //表示按键输入已经被处理,这样按键将不会给应用程序,丢掉不想要的按键值,这样的缺点是backspace也会被返回
                }
            }
        }
    }
}
