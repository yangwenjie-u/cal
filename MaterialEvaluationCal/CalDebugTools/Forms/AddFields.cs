using CalDebugTools.Common;
using CalDebugTools.Common.DBUtility;
using CalDebugTools.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalDebugTools.Forms
{
    public partial class AddFields : Form
    {
        FormMain _formMain;
        public string _deleteSqlStr = "";
        public AddFields(FormMain main)
        {
            _formMain = main;
            InitializeComponent();
            InitBaseData();
        }
        /// <summary>
        /// 初始话数据库选择
        /// </summary>
        public void InitBaseData()
        {
            List<string> jcjgInfos = Common.StringsOper.GetTextList(AppDomain.CurrentDomain.BaseDirectory + @"Resources\检测机构配置.txt");

            //数据库信息
            List<BaseDataInfo> listData = new List<BaseDataInfo>();
            List<string> arrInfo = new List<string>();
            if (jcjgInfos.Count == 0)
            {
                MessageBox.Show("获取数据库配置信息异常，请确认配置文件格式！");
                return;
            }
            BaseDataInfo data = new BaseDataInfo();

            data.Id = "0";
            data.Abbrevition = "ALL";
            data.Name = "全部";
            data.Code = "";
            listData.Add(data);
            foreach (var info in jcjgInfos)
            {
                if (info.StartsWith("--"))
                {
                    continue;
                }

                arrInfo = info.Split('-').ToList();

                if (arrInfo.Count != 4)
                {
                    continue;
                }
                data = new BaseDataInfo();

                data.Id = arrInfo[0];
                data.Abbrevition = arrInfo[1];
                data.Name = arrInfo[2];
                data.Code = arrInfo[3];
                listData.Add(data);

            }
            com_dataSource.DataSource = listData;
            com_dataSource.DisplayMember = "Name";
            com_dataSource.ValueMember = "Abbrevition";
        }

        private void btn_AddField_All_Click(object sender, EventArgs e)
        {
            InitBaseData();
        }

        private void btn_M_only_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否添加主主主主主表字段?", "Confirm Message", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
            CreateTableColumn("M");
        }

        private void btn_S_only_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("添加从表字段?", "Confirm Message", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
            CreateTableColumn("S");
        }

        private void btn_helper_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("添加到帮助表?", "Confirm Message", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
            CreateTableColumn("H");
        }

        private void btn_Customize_Click(object sender, EventArgs e)
        {
            string tableName = txt_customize.Text;
            if (string.IsNullOrEmpty(tableName))
            {
                MessageBox.Show("请输入自定义表名称");
                return;
            }
            if (MessageBox.Show($"添加到自定义表{tableName}表?", "Confirm Message", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
            CreateTableColumn("C");
        }

        /// <summary>
        /// 添加字段
        /// </summary>
        /// <param name="type"></param>
        public void CreateTableColumn(string type)
        {
            string xmbh = string.IsNullOrEmpty(txt_xmbh.Text) ? "" : txt_xmbh.Text.Trim();
            string fieldName = string.IsNullOrEmpty(txt_fieldName.Text) ? "" : txt_fieldName.Text.Trim();
            string fieldMS = string.IsNullOrEmpty(txt_fieldMs.Text) ? "" : txt_fieldMs.Text.Trim();
            string fieldType = string.IsNullOrEmpty(txt_fieldType.Text) ? "" : txt_fieldType.Text.Trim();
            string txtLX = string.IsNullOrEmpty(txt_lx.Text) ? "H" : txt_lx.Text.Trim();
            string chksfxs = this.chk_SFXS.Checked ? "1" : "0";
            string ssjcx = string.IsNullOrEmpty(this.txt_ssjcx.Text) ? "" : "、" + this.txt_ssjcx.Text.Trim() + "、";
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
                case "C":
                    tableName = txt_customize.Text;
                    break;
            }

            List<string> dbNameList = CheckDataBaseSet();
            _deleteSqlStr = "";
            foreach (var item in dbNameList)
            {
                _deleteSqlStr+="检测机构:"+ item.ToUpper()+"\r\n";
                AddFieldToDB(item, item.ToUpper(), xmbh, type, tableName, fieldName, fieldType, fieldMS, chksfxs, ssjcx, txtLX, locstionStr);
            }
            txt_deleteSqlStr.Text = _deleteSqlStr;
            MessageBox.Show("Success!");
        }

        /// <summary>
        /// 判断往哪个数据库添加字段
        /// </summary>
        public List<string> CheckDataBaseSet()
        {
            List<string> resultDBNameList = new List<string>();
            var selectedDataBaseName = "";
            List<string> dataSourceList = new List<string>();
            selectedDataBaseName = com_dataSource.Text.ToString();

            if (selectedDataBaseName.Equals("全部"))
            {
                List<BaseDataInfo> listData = com_dataSource.DataSource as List<BaseDataInfo>;

                //遍历所有配置的数据库，添加字段及zdzd表
                foreach (BaseDataInfo item in listData)
                {
                    resultDBNameList.Add(item.Abbrevition);
                }

                resultDBNameList.Remove("ALL");
                return resultDBNameList;
            }



            resultDBNameList.Add(selectedDataBaseName);

            return resultDBNameList;
        }

        public void AddFieldToDB(string jcjgName, string jcjgCode, string xmbh, string insertTabType, string tableName, string fieldName, string fieldType,
            string fieldMS, string chksfxs, string ssjcx, string fielsLx, string locstionStr)
        {
            SqlBase jcjtService = new SqlBase(ESqlConnType.ConnectionStringJCJT, jcjgCode);
            SqlBase jcjgService = new SqlBase(ESqlConnType.ConnectionStringJCJG, jcjgCode);
            SqlBase debugToolsService = new SqlBase(ESqlConnType.ConnectionStringDebugTool, jcjgCode);

            string sqlstr = string.Format($" select top 1 * FROM  M_{xmbh}");

            #region 插入字段
            if (jcjtService.ExecuteDataset(sqlstr) == null)
            {
                MessageBox.Show($"项目{xmbh}不存在！");
                Log.Warn("AddField", $"{jcjgName}:项目{xmbh}不存在！");
                return;
            }

            //自定义表
            if (insertTabType == "C")
            {
                sqlstr = string.Format($" select top 1 * FROM  {txt_customize.Text}");

                if (jcjtService.ExecuteDataset(sqlstr) == null)
                {
                    MessageBox.Show($"自定义表{txt_customize.Text}不存在！");
                    Log.Warn("AddField", $"{jcjgName}:自定义表{txt_customize.Text}不存在！");
                    return;
                }
            }

            try
            {
                #region 添加主/从表字段
                var startIndex = 0;
                List<string> baseCmdList = new List<string>();
                List<string> zdzdCmdList = new List<string>();
                List<string> zdzdCmdList_Cal = new List<string>();
                List<string> cmdList = new List<string>();
                string sqlStrCheck = "";
                int reFieldCount = 0;
                if (txt_STabCount.Text == "1" && txt_SFieldeStartIndex.Text == "1")
                {
                    sqlStrCheck = $"select * from information_schema.columns where table_name = '{tableName}' and column_name = '{fieldName}';";
                    //判断检测集团数据库是否已经添加字段
                    reFieldCount = CheckFieldIsExist(jcjtService, sqlStrCheck);
                    if (reFieldCount == -2 || reFieldCount > 0)
                    {
                        Log.Warn("AddField", $"{jcjgName}_检测集团数据库:添加字段异常，{tableName}标中已存在字段{fieldName}！count:{reFieldCount}");
                    }
                    else
                    {
                        baseCmdList.Add($"alter table {tableName} add {fieldName} {fieldType};");
                        _deleteSqlStr += $"alter table  {tableName} drop column {fieldName} ;\r\n";
                    }
                }
                else
                {
                    if (Convert.ToInt16(txt_SFieldeStartIndex.Text) <= 1)
                    {
                        startIndex = 1;
                    }
                    else
                    {
                        startIndex = Convert.ToInt16(txt_SFieldeStartIndex.Text);
                    }

                    for (int i = 0; i < Convert.ToInt16(txt_STabCount.Text); i++)
                    {
                        sqlStrCheck = $"select * from information_schema.columns where table_name = '{tableName}' and column_name = '{fieldName}{startIndex + i}';";
                        //判断检测集团数据库是否已经添加字段
                        reFieldCount = CheckFieldIsExist(jcjtService, sqlStrCheck);
                        if (reFieldCount == -2 || reFieldCount > 0)
                        {
                            Log.Warn("AddField", $"{jcjgName}_检测集团数据库:添加字段异常，{tableName}标中已存在字段{fieldName}{startIndex + i}！count:{reFieldCount}");
                        }
                        else
                        {
                            baseCmdList.Add($"alter table {tableName} add {fieldName}{startIndex + i} {fieldType};");
                            _deleteSqlStr += $"alter table {tableName} drop column {fieldName}{startIndex + i} ;\r\n";
                        }
                    }
                }
                #endregion

                #region 添加zdzd表数据
                string sqlStr = "";

                bool addFiled = true;
                List<string> lst = new List<string>();
                if (txt_STabCount.Text == "1" && txt_SFieldeStartIndex.Text == "1")
                {
                    sqlStr = $"insert into ZDZD_{xmbh} ( SJBMC, ZDMC, SY, ZDLX, ZDCD1, ZDCD2, INPUTZDLX, KJLX, SFBHZD, BHMS,ZDSX, SFXS, XSCD, XSSX, SFGD, MUSTIN, DEFAVAL, HELPLNK, CTRLSTRING, ZDXZ,WXSSX, WSFXS, MSGINFO, EQLFUNC, HELPWHERE, GETBYBH, SSJCX, SFBGZD,VALIDPROC, LX, ZDSXSQL, ENCRYPT, FZYC, FZCS, NOSAVE, location)" +
        $"VALUES('{tableName}', '{fieldName}', '{fieldMS}', 'nvarchar', '200', '0', 'nvarchar', '', 'False', '', 'False', '{chksfxs}', '0', '367.0000', 'False', 'False', '', '', '', 'S', '367.0000', 'True', '', '', '', 'True', '{ssjcx}', 'True', '', '{fielsLx}', NULL, NULL, NULL, NULL, NULL, '{locstionStr}')  ";

                    sqlStrCheck = $" select* from ZDZD_{xmbh} where ZDMC = '{fieldName}' and SJBMC = '{tableName}'";
                    //判断检测集团数据库是否已经添加字段
                    reFieldCount = CheckFieldIsExist(jcjtService, sqlStrCheck);
                    if (reFieldCount == -2 || reFieldCount > 0)
                    {
                        addFiled = false;
                        Log.Warn("AddField", $"{jcjgName}_检测集团数据库:添加字段异常，ZDZD_{xmbh}标中已存在字段{fieldName}！count:{reFieldCount}");
                    }

                    reFieldCount = CheckFieldIsExist(debugToolsService, sqlStrCheck);
                    if (reFieldCount == -2 || reFieldCount > 0)
                    {
                        Log.Warn("AddField", $"CalDebugTools数据库:添加字段异常，ZDZD_{xmbh}标中已存在字段{fieldName}！count:{reFieldCount}");
                    }
                    else
                    {
                        zdzdCmdList_Cal.Add(sqlStr);
                        _deleteSqlStr += $"delete CalDebugTool.dbo.ZDZD_{xmbh} where ZDMC = '{fieldName}' and SJBMC = '{tableName}';\r\n";

                    }

                    if (chk_syncJcJG.Checked)
                    {
                        //判断监管系统数据库是否已经添加字段
                        reFieldCount = CheckFieldIsExist(jcjgService, sqlStrCheck);
                        if (reFieldCount == -2 || reFieldCount > 0)
                        {
                            addFiled = false;
                            Log.Warn("AddField", $"{jcjgName}_检测监管数据库:添加字段异常，ZDZD_{xmbh}标中已存在字段{fieldName}！count:{reFieldCount}");
                        }
                    }

                    if (addFiled)
                    {
                        _deleteSqlStr += $"delete ZDZD_{xmbh} where ZDMC = '{fieldName}' and SJBMC = '{tableName}';\r\n";
                        zdzdCmdList.Add(sqlStr);
                    }
                }
                else
                {
                    startIndex = Convert.ToInt16(txt_SFieldeStartIndex.Text);

                    for (int i = 0; i < Convert.ToInt16(txt_STabCount.Text); i++)
                    {
                        addFiled = true;
                        sqlStr = $"insert into ZDZD_{xmbh} ( SJBMC, ZDMC, SY, ZDLX, ZDCD1, ZDCD2, INPUTZDLX, KJLX, SFBHZD, BHMS,ZDSX, SFXS, XSCD, XSSX, SFGD, MUSTIN, DEFAVAL, HELPLNK, CTRLSTRING, ZDXZ,WXSSX, WSFXS, MSGINFO, EQLFUNC, HELPWHERE, GETBYBH, SSJCX, SFBGZD,VALIDPROC, LX, ZDSXSQL, ENCRYPT, FZYC, FZCS, NOSAVE, location)" +
        $"VALUES('{tableName}', '{fieldName}{startIndex + i}', '{fieldMS}{startIndex + i}', 'nvarchar', '200', '0', 'nvarchar', '', 'False', '', 'False', '{chksfxs}', '0', '367.0000', 'False', 'False', '', '', '', 'S', '367.0000', 'True', '', '', '', 'True', '{ssjcx}', 'True', '', '{fielsLx}', NULL, NULL, NULL, NULL, NULL, '{locstionStr}')  ";

                        sqlStrCheck = $" select * from ZDZD_{xmbh} where ZDMC = '{fieldName}{startIndex + i}' and SJBMC = '{tableName}'";
                        //判断检测集团数据库是否已经添加字段
                        reFieldCount = CheckFieldIsExist(jcjtService, sqlStrCheck);
                        if (reFieldCount == -2 || reFieldCount > 0)
                        {
                            addFiled = false;
                            Log.Warn("AddField", $"{jcjgName}_检测集团数据库:添加字段异常，ZDZD_{xmbh}标中已存在字段{fieldName}{startIndex + i}！count:{reFieldCount}");
                        }

                        //判断上传字段数据库是否已经添加字段
                        reFieldCount = CheckFieldIsExist(debugToolsService, sqlStrCheck);
                        if (reFieldCount == -2 || reFieldCount > 0)
                        {
                            Log.Warn("AddField", $"CalDebugTools数据库:添加字段异常，ZDZD_{xmbh}标中已存在字段{fieldName}{startIndex + i}！count:{reFieldCount}");
                        }
                        else
                        {
                            zdzdCmdList_Cal.Add(sqlStr);
                            _deleteSqlStr += $"delete CalDebugTool.dbo.ZDZD_{xmbh} where ZDMC = '{fieldName}{startIndex + i}' and SJBMC = '{tableName}';\r\n";
                        }

                        if (chk_syncJcJG.Checked)
                        {
                            //判断监管系统数据库是否已经添加字段
                            reFieldCount = CheckFieldIsExist(jcjgService, sqlStrCheck);
                            if (reFieldCount == -2 || reFieldCount > 0)
                            {
                                addFiled = false;
                                Log.Warn("AddField", $"{jcjgName}_检测监管数据库:添加字段异常，ZDZD_{xmbh}标中已存在字段{fieldName}{startIndex + i}！count:{reFieldCount}");
                            }
                        }

                        if (addFiled)
                        {
                            zdzdCmdList.Add(sqlStr);
                            _deleteSqlStr += $"delete ZDZD_{xmbh} where ZDMC = '{fieldName}{startIndex + i}' and SJBMC = '{tableName}';\r\n";
                        }
                    }
                }
                #endregion

                cmdList.AddRange(baseCmdList);
                cmdList.AddRange(zdzdCmdList);
                if (cmdList.Count > 0 && !jcjtService.ExecuteTrans(cmdList))
                {
                    Log.Warn("AddField", $"{jcjgName}_检测集团数据库:添加字段失败，数据已回滚。");
                }
                if (zdzdCmdList_Cal.Count > 0 && !debugToolsService.ExecuteTrans(zdzdCmdList_Cal))
                {
                    Log.Warn("AddField", $"{jcjgName}CalDebugTools数据库:添加字段失败，数据已回滚。");
                }
                if (cmdList.Count > 0 && chk_syncJcJG.Checked)
                {
                    if (!jcjgService.ExecuteTrans(cmdList))
                    {
                        Log.Warn("AddField", $"{jcjgName}_检测监管数据库:添加字段失败，数据已回滚。");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion
        }

        /// <summary>
        /// 检测是否有数据
        /// </summary>
        /// <param name="dbService"></param>
        /// <param name="sql"></param>
        /// <returns>-2：异常 -1: 没有数据 else 有数据，不需要新增</returns>
        public int CheckFieldIsExist(SqlBase dbService, string sql)
        {
            try
            {
                return dbService.ExecuteScalar(sql) == null ? 0 : 1;
            }
            catch
            {
                return -2;
            }
        }

        private void AddFields_FormClosed(object sender, FormClosedEventArgs e)
        {
            _formMain.Show();
        }

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

        private void txt_SFieldeStartIndex_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))     //判断按键输入字符是不是数字
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;   //表示按键输入已经被处理,这样按键将不会给应用程序,丢掉不想要的按键值,这样的缺点是backspace也会被返回
                }
            }
        }
    }
}
