using CalDebugTools.Common;
using CalDebugTools.Common.DBUtility;
using CalDebugTools.Model;
using System;
using System.Collections;
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
        public string _outMsg = "";
        public AddFields(FormMain main)
        {
            _formMain = main;
            InitializeComponent();
            InitComboxJCJG();
        }
        /// <summary>
        /// 初始化检测机构选择框
        /// </summary>
        public void InitComboxJCJG()
        {
            string msg = "";
            List<JCJGConnectInfo> listData = new List<JCJGConnectInfo>();
            listData = Comm.InitBaseData(out msg);
            com_dataSource.DataSource = listData;
            com_dataSource.DisplayMember = "Name";
            com_dataSource.ValueMember = "Abbrevition";
            com_dataSource.SelectedIndex = -1;
        }


        private void btn_M_only_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否添加主主主主主表字段?", "Confirm Message", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
            if (Convert.ToInt16(txt_STabCount.Text) > 1 && MessageBox.Show("主表添加多个字段，是否继续?", "Confirm Message", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
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
            if (com_dataSource.SelectedIndex == -1)
            {
                MessageBox.Show("请选择检测机构！");
                return;
            }

            if (string.IsNullOrEmpty(txt_ssjcx.Text) && MessageBox.Show($"所属检测项目为空，是否继续添加字段？", "提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
            string xmbh = string.IsNullOrEmpty(txt_xmbh.Text) ? "" : txt_xmbh.Text.Trim().ToUpper();
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
                    if (string.IsNullOrEmpty(tableName))
                    {
                        MessageBox.Show("请输入自定义表！");
                        return;
                    }
                    break;
            }

            List<string> dbNameList = CheckDataBaseSet();
            _deleteSqlStr = "";
            _outMsg = "";
            int msgLen = 0;
            foreach (var item in dbNameList)
            {
                _deleteSqlStr += "检测机构:" + item.ToUpper() + "\r\n";
                _outMsg += "检测机构:" + item.ToUpper() + "\r\n";
                msgLen += _outMsg.Length;
                AddFieldToDB(item, item.ToUpper(), xmbh, type, tableName, fieldName, fieldType, fieldMS, chksfxs, ssjcx, txtLX, locstionStr);
            }
            txt_deleteSqlStr.Text = msgLen == txt_deleteSqlStr.TextLength ? "" : _deleteSqlStr;

            if (_outMsg.Length == msgLen)
            {
                MessageBox.Show("操作成功", "提示");
            }
            else
            {
                MessageBox.Show(_outMsg, "异常提示");
            }
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
                List<JCJGConnectInfo> listData = com_dataSource.DataSource as List<JCJGConnectInfo>;

                //遍历所有配置的数据库，添加字段及zdzd表
                foreach (JCJGConnectInfo item in listData)
                {
                    resultDBNameList.Add(item.Abbrevition);
                }

                resultDBNameList.Remove("ALL");
                return resultDBNameList;
            }
            resultDBNameList.Add(((CalDebugTools.Model.JCJGConnectInfo)com_dataSource.SelectedItem).Abbrevition);

            return resultDBNameList;
        }

        public void AddFieldToDB(string jcjgName, string jcjgCode, string xmbh, string insertTabType, string tableName, string fieldName, string fieldType,
            string fieldMS, string chksfxs, string ssjcx, string fielsLx, string locstionStr)
        {
            try
            {
                DataHelper jcjtService = new DataHelper($"ConnectionStringJCJT_{jcjgCode}");
                DataHelper jcjgService = new DataHelper($"ConnectionStringJCJG_{jcjgCode}");
                DataHelper debugToolsService = new DataHelper($"ConnectionStringDebugTool");

                string sqlstr = string.Format($" select count(1) FROM  M_{xmbh}");
                string customize = txt_customize.Text.ToUpper();
                string msg = "";
                #region 插入字段
                if (chk_jcjg_only.Checked == true) //是否只监管
                {
                    if (jcjgService.ExecuteReader(sqlstr) == null)
                    {
                        MessageBox.Show($"项目{xmbh}不存在！");
                        Log.Warn("AddField", $"{jcjgName}:项目{xmbh}不存在！");
                        _outMsg += $"{jcjgName}:项目{xmbh}不存在！" + "\r\n";
                        return;
                    }
                }
                else
                {
                    if (jcjtService.ExecuteReader(sqlstr) == null)
                    {
                        MessageBox.Show($"{jcjgName}检测集团:自定义表{customize}不存在！");
                        Log.Warn("AddField", $"{jcjgName}:项目{xmbh}不存在！");
                        _outMsg += $"{jcjgName}:项目{xmbh}不存在！" + "\r\n";
                        return;
                    }
                    if (chk_syncJcJG.Checked && jcjgService.ExecuteReader(sqlstr) == null) //同步监管
                    {
                        MessageBox.Show($"{jcjgName}监管:自定义表{customize}不存在！");
                        Log.Warn("AddField", $"{jcjgName}:项目{xmbh}不存在！");
                        _outMsg += $"{jcjgName}:项目{xmbh}不存在！" + "\r\n";
                        return;
                    }
                }

                //自定义表
                if (insertTabType == "C")
                {
                    sqlstr = string.Format($" select count(1) FROM  {customize}");

                    if (chk_jcjg_only.Checked) //是否只监管
                    {
                        if (jcjgService.ExecuteReader(sqlstr) == null)
                        {
                            MessageBox.Show($"{jcjgName}监管:自定义表{customize}不存在！");
                            Log.Warn("AddField", $"{jcjgName}监管数据库:自定义表{customize}不存在！");
                            _outMsg += $"{jcjgName}监管数据库:自定义表{txt_customize.Text}不存在！" + "\r\n";
                            return;
                        }
                    }
                    else
                    {
                        if (jcjtService.ExecuteReader(sqlstr) == null)
                        {
                            MessageBox.Show($"{jcjgName}检测集团:自定义表{customize}不存在！");
                            Log.Warn("AddField", $"{jcjgName}检测集团数据库:自定义表{customize}不存在！");
                            _outMsg += $"{jcjgName}检测集团数据库:自定义表{txt_customize.Text}不存在！" + "\r\n";
                            return;
                        }
                        if (chk_syncJcJG.Checked && jcjgService.ExecuteReader(sqlstr) == null) //同步监管
                        {
                            MessageBox.Show($"{jcjgName}监管:自定义表{customize}不存在！");
                            Log.Warn("AddField", $"{jcjgName}监管数据库:自定义表{customize}不存在！");
                            _outMsg += $"{jcjgName}监管数据库:自定义表{txt_customize.Text}不存在！" + "\r\n";
                            return;
                        }
                    }
                }

                try
                {
                    #region 添加主/从表字段
                    var startIndex = 0;
                    string cmdStr = "";
                    ArrayList baseCmdList = new ArrayList();
                    ArrayList zdzdCmdList = new ArrayList();
                    ArrayList zdzdCmdList_Cal = new ArrayList();
                    ArrayList cmdList = new ArrayList();
                    string sqlStrCheck = "";
                    int reFieldCount = 0;
                    if (txt_STabCount.Text == "1" && txt_SFieldeStartIndex.Text == "1")
                    {
                        sqlStrCheck = $"select * from information_schema.columns where table_name = '{tableName}' and column_name = '{fieldName}';";
                        //判断检测集团数据库是否已经添加字段

                        if (chk_jcjg_only.Checked == true) //仅添加监管
                        {
                            reFieldCount = CheckFieldIsExist(jcjgService, sqlStrCheck);
                            if (reFieldCount == 0)
                            {
                                cmdStr = $"alter table {tableName} add {fieldName} {fieldType};";
                                if (!baseCmdList.Contains(cmdStr))
                                {
                                    baseCmdList.Add(cmdStr);
                                }
                                _deleteSqlStr += $"alter table  {tableName} drop column {fieldName} ;\r\n";
                            }
                            else
                            {
                                Log.Warn("AddField", $"{jcjgName}_监管数据库:添加字段异常，{tableName}表已存在字段{fieldName}！count:{reFieldCount}");
                                _outMsg += $"{jcjgName}_监管数据库:添加字段异常，{tableName}表已存在字段{fieldName}！count:{reFieldCount}" + "\r\n";
                            }
                        }
                        else if (chk_syncJcJG.Checked == true) //两个数据库都添加
                        {
                            #region 检测集团
                            reFieldCount = CheckFieldIsExist(jcjtService, sqlStrCheck);
                            if (reFieldCount == 0)
                            {
                                cmdStr = $"alter table {tableName} add {fieldName} {fieldType};";
                                if (!baseCmdList.Contains(cmdStr))
                                {
                                    baseCmdList.Add(cmdStr);
                                }
                                _deleteSqlStr += $"alter table  {tableName} drop column {fieldName} ;\r\n";
                            }
                            else
                            {
                                Log.Warn("AddField", $"{jcjgName}_检测集团数据库:添加字段异常，{tableName}表已存在字段{fieldName}！count:{reFieldCount}");
                                _outMsg += $"{jcjgName}_检测集团数据库:添加字段异常，{tableName}表已存在字段{fieldName}！count:{reFieldCount}" + "\r\n";
                            }
                            #endregion

                            #region 监管
                            reFieldCount = CheckFieldIsExist(jcjgService, sqlStrCheck);
                            if (reFieldCount == 0)
                            {
                                cmdStr = $"alter table {tableName} add {fieldName} {fieldType};";
                                if (!baseCmdList.Contains(cmdStr))
                                {
                                    baseCmdList.Add(cmdStr);
                                }
                                _deleteSqlStr += $"alter table  {tableName} drop column {fieldName} ;\r\n";
                            }
                            else
                            {
                                Log.Warn("AddField", $"{jcjgName}_监管数据库:添加字段异常，{tableName}表已存在字段{fieldName}！count:{reFieldCount}");
                                _outMsg += $"{jcjgName}_监管数据库:添加字段异常，{tableName}表已存在字段{fieldName}！count:{reFieldCount}" + "\r\n";
                            }
                            #endregion
                        }
                        else  // 检测集团
                        {
                            reFieldCount = CheckFieldIsExist(jcjtService, sqlStrCheck);
                            if (reFieldCount == 0)
                            {
                                cmdStr = $"alter table {tableName} add {fieldName} {fieldType};";
                                if (!baseCmdList.Contains(cmdStr))
                                {
                                    baseCmdList.Add(cmdStr);
                                }
                                _deleteSqlStr += $"alter table  {tableName} drop column {fieldName} ;\r\n";
                            }
                            else
                            {
                                Log.Warn("AddField", $"{jcjgName}_检测集团数据库:添加字段异常，{tableName}表已存在字段{fieldName}！count:{reFieldCount}");
                                _outMsg += $"{jcjgName}_检测集团数据库:添加字段异常，{tableName}表已存在字段{fieldName}！count:{reFieldCount}" + "\r\n";
                            }
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

                            if (chk_jcjg_only.Checked == true) //仅添加监管
                            {
                                reFieldCount = CheckFieldIsExist(jcjgService, sqlStrCheck);
                                if (reFieldCount == 0)
                                {

                                    cmdStr = $"alter table {tableName} add {fieldName}{startIndex + i} {fieldType};";
                                    if (!baseCmdList.Contains(cmdStr))
                                    {
                                        baseCmdList.Add(cmdStr);
                                    }

                                    _deleteSqlStr += $"alter table {tableName} drop column {fieldName}{startIndex + i} ;\r\n";
                                }
                                else
                                {
                                    Log.Warn("AddField", $"{jcjgName}_监管数据库:添加字段异常，{tableName}表中已存在字段{fieldName}{startIndex + i}！count:{reFieldCount}");
                                    _outMsg += $"{jcjgName}_监管数据库:添加字段异常，{tableName}表中已存在字段{fieldName}{startIndex + i}！" + "\r\n";
                                }
                            }
                            else if (chk_syncJcJG.Checked == true) //两个数据库都添加
                            {
                                //集团数据库
                                #region 检测集团
                                reFieldCount = CheckFieldIsExist(jcjtService, sqlStrCheck);
                                if (reFieldCount == 0)
                                {
                                    cmdStr = $"alter table {tableName} add {fieldName}{startIndex + i} {fieldType};";
                                    if (!baseCmdList.Contains(cmdStr))
                                    {
                                        baseCmdList.Add(cmdStr);
                                    }

                                    _deleteSqlStr += $"alter table {tableName} drop column {fieldName}{startIndex + i} ;\r\n";
                                }
                                else
                                {
                                    Log.Warn("AddField", $"{jcjgName}_检测集团数据库:添加字段异常，{tableName}表中已存在字段{fieldName}{startIndex + i}！count:{reFieldCount}");
                                    _outMsg += $"{jcjgName}_检测集团数据库:添加字段异常，{tableName}表中已存在字段{fieldName}{startIndex + i}！" + "\r\n";
                                }
                                #endregion

                                #region 监管数据库
                                reFieldCount = CheckFieldIsExist(jcjgService, sqlStrCheck);
                                if (reFieldCount == 0)
                                {

                                    cmdStr = $"alter table {tableName} add {fieldName}{startIndex + i} {fieldType};";
                                    if (!baseCmdList.Contains(cmdStr))
                                    {
                                        baseCmdList.Add(cmdStr);
                                    }
                                    _deleteSqlStr += $"alter table {tableName} drop column {fieldName}{startIndex + i} ;\r\n";
                                }
                                else
                                {
                                    Log.Warn("AddField", $"{jcjgName}_监管数据库:添加字段异常，{tableName}表中已存在字段{fieldName}{startIndex + i}！count:{reFieldCount}");
                                    _outMsg += $"{jcjgName}_监管数据库:添加字段异常，{tableName}表中已存在字段{fieldName}{startIndex + i}！" + "\r\n";
                                }
                                #endregion
                            }
                            else //检测集团
                            {
                                reFieldCount = CheckFieldIsExist(jcjtService, sqlStrCheck);
                                if (reFieldCount == 0)
                                {
                                    cmdStr = $"alter table {tableName} add {fieldName}{startIndex + i} {fieldType};";
                                    if (!baseCmdList.Contains(cmdStr))
                                    {
                                        baseCmdList.Add(cmdStr);
                                    }
                                    _deleteSqlStr += $"alter table {tableName} drop column {fieldName}{startIndex + i} ;\r\n";
                                }
                                else
                                {
                                    Log.Warn("AddField", $"{jcjgName}_检测集团数据库:添加字段异常，{tableName}表中已存在字段{fieldName}{startIndex + i}！count:{reFieldCount}");
                                    _outMsg += $"{jcjgName}_检测集团数据库:添加字段异常，{tableName}表中已存在字段{fieldName}{startIndex + i}！" + "\r\n";

                                }
                            }
                        }
                    }

                    if (baseCmdList.Count == 0)
                    {
                        return;
                    }
                    #endregion

                    #region 添加zdzd表数据
                    string sqlStr = "";

                    bool addFiled = true;
                    List<string> lst = new List<string>();

                    if (txt_STabCount.Text == "1" && txt_SFieldeStartIndex.Text == "1")
                    {
                        #region  添加一个字段
                        sqlStr = $"insert into ZDZD_{xmbh} ( SJBMC, ZDMC, SY, ZDLX, ZDCD1, ZDCD2, INPUTZDLX, KJLX, SFBHZD, BHMS,ZDSX, SFXS, XSCD, XSSX, SFGD, MUSTIN, DEFAVAL, HELPLNK, CTRLSTRING, ZDXZ,WXSSX, WSFXS, MSGINFO, EQLFUNC, HELPWHERE, GETBYBH, SSJCX, SFBGZD,VALIDPROC, LX, ZDSXSQL, ENCRYPT, FZYC, FZCS, NOSAVE)" +
            $"VALUES('{tableName}', '{fieldName}', '{fieldMS}', 'nvarchar', '200', '0', 'nvarchar', '', '0', '', '0', '{chksfxs}', '0', '367.0000', '0', '0', '', '', '', 'S', '367.0000', '1', '', '', '', '1', '{ssjcx}', '1', '', '{fielsLx}', NULL, NULL, NULL, NULL, NULL)  ";

                        sqlStrCheck = $" select * from ZDZD_{xmbh} where ZDMC = '{fieldName}' and SJBMC = '{tableName}'";

                        //判断检测集团数据库是否已经添加字段
                        if (chk_jcjg_only.Checked == true) //仅添加监管
                        {
                            reFieldCount = CheckFieldIsExist(jcjgService, sqlStrCheck);
                            if (reFieldCount == -2 || reFieldCount > 0)
                            {
                                addFiled = false;
                                Log.Warn("AddField", $"{jcjgName}_监管数据库:添加字段异常，ZDZD_{xmbh}表已存在字段{fieldName}！count:{reFieldCount}");
                                _outMsg += $"{jcjgName}_监管数据库:添加字段异常，ZDZD_{xmbh}表已存在字段{fieldName}！" + "\r\n";
                            }
                        }
                        else if (chk_syncJcJG.Checked == true) //两个数据库都添加
                        {
                            #region 集团版本
                            reFieldCount = CheckFieldIsExist(jcjtService, sqlStrCheck);
                            if (reFieldCount == -2 || reFieldCount > 0)
                            {
                                addFiled = false;
                                Log.Warn("AddField", $"{jcjgName}_检测集团数据库:添加字段异常，ZDZD_{xmbh}表已存在字段{fieldName}！count:{reFieldCount}");
                                _outMsg += $"{jcjgName}_检测集团数据库:添加字段异常，ZDZD_{xmbh}表已存在字段{fieldName}！" + "\r\n";
                            }
                            #endregion

                            if (chk_syncJcJG.Checked) //监管
                            {
                                reFieldCount = CheckFieldIsExist(jcjgService, sqlStrCheck);
                                if (reFieldCount == -2 || reFieldCount > 0)
                                {
                                    addFiled = false;
                                    Log.Warn("AddField", $"{jcjgName}_监管数据库:添加字段异常，ZDZD_{xmbh}表已存在字段{fieldName}！count:{reFieldCount}");
                                    _outMsg += $"{jcjgName}_监管数据库:添加字段异常，ZDZD_{xmbh}表已存在字段{fieldName}！" + "\r\n";
                                }
                            }
                        }
                        else //集团版本
                        {
                            var df = jcjtService.ExecuteReader(sqlStrCheck);
                            if (df == null)
                            {

                            }
                            reFieldCount = CheckFieldIsExist(jcjtService, sqlStrCheck);

                            if (reFieldCount == -2 || reFieldCount > 0)
                            {
                                addFiled = false;
                                Log.Warn("AddField", $"{jcjgName}_检测集团数据库:添加字段异常，ZDZD_{xmbh}表已存在字段{fieldName}！count:{reFieldCount}");
                                _outMsg += $"{jcjgName}_检测集团数据库:添加字段异常，ZDZD_{xmbh}表已存在字段{fieldName}！" + "\r\n";
                            }
                        }

                        if (addFiled)
                        {
                            _deleteSqlStr += $"delete ZDZD_{xmbh} where ZDMC = '{fieldName}' and SJBMC = '{tableName}';\r\n";
                            zdzdCmdList.Add(sqlStr);

                            if (!chk_jcjg_only.Checked)
                            {
                                reFieldCount = CheckFieldIsExist(debugToolsService, sqlStrCheck);
                                if (reFieldCount == 0)
                                {
                                    zdzdCmdList_Cal.AddRange(zdzdCmdList);
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region  添加多个字段
                        startIndex = Convert.ToInt16(txt_SFieldeStartIndex.Text);
                        for (int i = 0; i < Convert.ToInt16(txt_STabCount.Text); i++)
                        {
                            addFiled = true;
                            sqlStr = $"insert into ZDZD_{xmbh} ( SJBMC, ZDMC, SY, ZDLX, ZDCD1, ZDCD2, INPUTZDLX, KJLX, SFBHZD, BHMS,ZDSX, SFXS, XSCD, XSSX, SFGD, MUSTIN, DEFAVAL, HELPLNK, CTRLSTRING, ZDXZ,WXSSX, WSFXS, MSGINFO, EQLFUNC, HELPWHERE, GETBYBH, SSJCX, SFBGZD,VALIDPROC, LX, ZDSXSQL, ENCRYPT, FZYC, FZCS, NOSAVE)" +
            $"VALUES('{tableName}', '{fieldName}{startIndex + i}', '{fieldMS}{startIndex + i}', 'nvarchar', '200', '0', 'nvarchar', '', '0', '', '0', '{chksfxs}', '0', '367.0000', '0', '0', '', '', '', 'S', '367.0000', '1', '', '', '', '1', '{ssjcx}', '1', '', '{fielsLx}', NULL, NULL, NULL, NULL, NULL)  ";

                            sqlStrCheck = $" select * from ZDZD_{xmbh} where ZDMC = '{fieldName}{startIndex + i}' and SJBMC = '{tableName}'";
                            if (chk_jcjg_only.Checked == true) //仅添加监管
                            {
                                reFieldCount = CheckFieldIsExist(jcjgService, sqlStrCheck);
                                if (reFieldCount == -2 || reFieldCount > 0)
                                {
                                    addFiled = false;
                                    Log.Warn("AddField", $"{jcjgName}_监管数据库:添加字段异常，ZDZD_{xmbh}表已存在字段{fieldName}{startIndex + i}！count:{reFieldCount}");
                                    _outMsg += $"{jcjgName}_监管数据库:添加字段异常，ZDZD_{xmbh}表已存在字段{fieldName}{startIndex + i}！" + "\r\n";
                                }
                            }
                            else if (chk_syncJcJG.Checked == true) //两个数据库都添加
                            {
                                #region 检测集团
                                reFieldCount = CheckFieldIsExist(jcjtService, sqlStrCheck);
                                if (reFieldCount == -2 || reFieldCount > 0)
                                {
                                    addFiled = false;
                                    Log.Warn("AddField", $"{jcjgName}_检测集团数据库:添加字段异常，ZDZD_{xmbh}表已存在字段{fieldName}{startIndex + i}！count:{reFieldCount}");
                                    _outMsg += $"{jcjgName}_检测集团数据库:添加字段异常，ZDZD_{xmbh}表已存在字段{fieldName}{startIndex + i}！" + "\r\n";
                                }
                                #endregion

                                if (chk_syncJcJG.Checked) //监管
                                {
                                    reFieldCount = CheckFieldIsExist(jcjgService, sqlStrCheck);
                                    if (reFieldCount == -2 || reFieldCount > 0)
                                    {
                                        addFiled = false;
                                        Log.Warn("AddField", $"{jcjgName}_监管数据库:添加字段异常，ZDZD_{xmbh}表已存在字段{fieldName}{startIndex + i}！count:{reFieldCount}");
                                        _outMsg += $"{jcjgName}_监管数据库:添加字段异常，ZDZD_{xmbh}表已存在字段{fieldName}{startIndex + i}！" + "\r\n";
                                    }
                                }
                            }
                            else //检测集团
                            {
                                reFieldCount = CheckFieldIsExist(jcjtService, sqlStrCheck);
                                if (reFieldCount == -2 || reFieldCount > 0)
                                {
                                    addFiled = false;
                                    Log.Warn("AddField", $"{jcjgName}_检测集团数据库:添加字段异常，ZDZD_{xmbh}表已存在字段{fieldName}{startIndex + i}！count:{reFieldCount}");
                                    _outMsg += $"{jcjgName}_检测集团数据库:添加字段异常，ZDZD_{xmbh}表已存在字段{fieldName}{startIndex + i}！" + "\r\n";
                                }
                            }

                            if (addFiled)
                            {
                                zdzdCmdList.Add(sqlStr);
                                _deleteSqlStr += $"delete ZDZD_{xmbh} where ZDMC = '{fieldName}{startIndex + i}' and SJBMC = '{tableName}';\r\n";

                                if (!chk_jcjg_only.Checked)
                                {
                                    reFieldCount = CheckFieldIsExist(debugToolsService, sqlStrCheck);
                                    if (reFieldCount == 0)
                                    {
                                        zdzdCmdList_Cal.AddRange(zdzdCmdList);
                                    }
                                }
                            }
                        }
                        #endregion
                    }

                    #endregion

                    cmdList.AddRange(zdzdCmdList);
                    cmdList.AddRange(baseCmdList);

                    //仅添加监管
                    if (chk_jcjg_only.Checked == true)
                    {
                        jcjgService.ExecuteSqlTran(cmdList, out msg);
                        if (!string.IsNullOrEmpty(msg))
                        {
                            Log.Warn("AddField", $"{jcjgName}_检测监管数据库:添加字段失败，数据已回滚。" + msg);
                            _outMsg += $"{jcjgName}_检测监管数据库:添加字段异常，数据已回滚." + msg + "\r\n";
                        }
                    }
                    else
                    {
                        //检测集团
                        if (cmdList.Count > 0)
                        {
                            jcjtService.ExecuteSqlTran(cmdList, out msg);
                            if (!string.IsNullOrEmpty(msg))
                            {
                                Log.Warn("AddField", $"{jcjgName}_检测集团数据库:添加字段失败，数据已回滚。" + msg);
                                _outMsg += $"{jcjgName}_检测集团数据库:添加字段异常，数据已回滚." + msg + "\r\n";
                            }
                        }
                        //caldebugTool
                        if (zdzdCmdList_Cal.Count > 0)
                        {
                            debugToolsService.ExecuteSqlTran(zdzdCmdList_Cal, out msg);
                            if (!string.IsNullOrEmpty(msg))
                            {
                                Log.Warn("AddField", $"CalDebugTools数据库_:添加字段失败：" + msg);
                                _outMsg += $"CalDebugTools数据库:添加字段失败：" + msg + "\r\n";
                            }
                        }

                        //同步到监管
                        if (chk_syncJcJG.Checked)
                        {
                            jcjgService.ExecuteSqlTran(cmdList, out msg);
                            if (!string.IsNullOrEmpty(msg))
                            {
                                Log.Warn("AddField", $"{jcjgName}_检测监管数据库:添加字段失败，数据已回滚。" + msg);
                                _outMsg += $"{jcjgName}_检测监管数据库:添加字段异常，数据已回滚." + msg + "\r\n";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Log.Error("AddField", $"{jcjgName}异常：{ex.Message}。" + "\r\n  StackTrace:" + ex.StackTrace);
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log.Error("AddField", $"{jcjgName}异常：{ex.Message}。" + "\r\n  StackTrace:" + ex.StackTrace);
            }
        }

        /// <summary>
        /// 检测是否有数据
        /// </summary>
        /// <param name="dbService"></param>
        /// <param name="sql"></param>
        /// <returns>-2：异常 -1: 没有数据 else 有数据，不需要新增</returns>
        public int CheckFieldIsExist(DataHelper dbService, string sql)
        {
            try
            {
                return dbService.GetDataSet(sql).Tables[0].Rows.Count == 0 ? 0 : 1;
            }
            catch (Exception ex)
            {
                Log.Error("AddField", $" 检测是否有数据是异常，msg:" + ex.Message);
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

        //private void chk_jcjg_only_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (((System.Windows.Forms.CheckBox)sender).CheckState == CheckState.Checked)
        //    {
        //        chk_syncJcJG.CheckState = CheckState.Checked;
        //    }
        //    //if()
        //}

        private void btn_test_Click(object sender, EventArgs e)
        {
            try
            {
                DataHelper dbHeler = new DataHelper("ConnectionStringJCJG_FY2");

                string sql = "select * from S_HNT";
                DataSet dt = dbHeler.GetDataSet(sql);

                foreach (DataRow item in dt.Tables[0].Rows)
                {
                    var dfd = item["recid"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
