using CalDebugTools.BLL;
using CalDebugTools.Common.DBUtility;
using CalDebugTools.DAL;
using CalDebugTools.Forms;
using CalDebugTools.Model;
using hh.fw.Standard;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalDebugTools
{
    public partial class FormMain : Form
    {
        public static ProjectInfos _projectInfo = null;
        FieldManage _manage = new FieldManage();
        public static string _strCode = "";
        public FormMain()
        {
            InitializeComponent();
            _projectInfo = new ProjectInfos();

            Init();
            CalInit();

        }

        public void Init()
        {
            try
            {
                txt_jcxmbh.Text = ConfigurationHelper.GetConfig("jcxmbh");
                txt_m.Text = ConfigurationHelper.GetConfig("tableM");
                txt_s.Text = ConfigurationHelper.GetConfig("tableS");
                txt_helper.Text = ConfigurationHelper.GetConfig("tableBZ");
                txt_helper.Text = ConfigurationHelper.GetConfig("tableSJ");
                txt_jydbh.Text = ConfigurationHelper.GetConfig("jydbhFrom");
                txt_yjdbh_to.Text = ConfigurationHelper.GetConfig("jydbhTo");
                txtdatafiled.Text = ConfigurationHelper.GetConfig("datafiled");
                txt_y.Text = ConfigurationHelper.GetConfig("tableY");
                txt_wtdbh.Text = ConfigurationHelper.GetConfig("wtdbh");
                var checkVal = ConfigurationHelper.GetConfig("checkWH");

                ck_other.Checked = checkVal == "1" ? true : false;
            }
            catch
            {
            }
        }

        /// <summary>
        /// 保存配置信息
        /// </summary>
        public void SaveXMinfos()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("jcxmbh", txt_jcxmbh.Text);
            dic.Add("tableM", txt_m.Text);
            dic.Add("tableS", txt_s.Text);
            dic.Add("tableBZ", txt_helper.Text);
            dic.Add("tableSJ", txt_helper.Text);
            dic.Add("jydbhFrom", txt_jydbh.Text);
            dic.Add("jydbhTo", txt_yjdbh_to.Text);
            dic.Add("datafiled", txtdatafiled.Text);
            dic.Add("tableY", txt_y.Text);
            dic.Add("checkWH", ck_other.Checked ? "1" : "0");
            dic.Add("wtdbh", txt_wtdbh.Text);
            ConfigurationHelper.SaveConfig(dic);

        }
        private void txt_jcxmbh_Leave(object sender, EventArgs e)
        {
            string bh = ((System.Windows.Forms.TextBox)sender).Text;
            var data = _projectInfo.GetProjectInfoByBH(bh);
            if (data == null)
            {
                MessageBox.Show($"获取项目失败，请确认项目{bh}已添加");
                return;
            }
            txt_s.Text = data.STable;
            txt_m.Text = data.MTable;
            txt_helper.Text = data.BZTable;
            txt_y.Text = data.YTable;
            txtdatafiled.Text = data.DataFiled;

            //获取 测试数据，计算方法，帮助表数据
        }

        private void ProjectAdd_Click(object sender, EventArgs e)
        {
            ProjectManage manage = new ProjectManage(this);
            this.Hide();
            manage.Show();
        }

        public List<string> GetJYDBHs()
        {
            List<string> listJYDBH = new List<string>();

            string txtJYDBH = txt_jydbh.Text.Trim();
            if (string.IsNullOrEmpty(txtJYDBH))
            {
                MessageBox.Show("jydbh err ");
                return null;
            }
            string xmbh = this.txt_jcxmbh.Text.Trim();
            string txtJYDBHto = txt_yjdbh_to.Text.Trim();

            DataSet ch_mdata = _projectInfo.GetChiFengDate(xmbh, txtJYDBH, txtJYDBHto);

            foreach (DataRow mDr in ch_mdata.Tables[0].Rows)
            {
                listJYDBH.Add(mDr["JYDBH"].ToString());
            }
            if (listJYDBH.Count == 0)
            {
                MessageBox.Show($"找不到试验项目{xmbh}数据，请确定 JYDBH对应的BGBH不为空 ", "调试", MessageBoxButtons.OK);
                return null;
            }

            return listJYDBH;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="zdzdParms">查询参数</param>
        /// <param name="queryNumber">查询条件</param>
        /// <param name="connType">数据库连接</param>
        /// <param name="dataType">数据源</param>
        /// <returns></returns>
        public string GetParams(List<string> zdzdParms, string queryNumber, ESqlConnType connType, string dataType = "CF")
        {
            string xmbh = this.txt_jcxmbh.Text.Trim();

            if (string.IsNullOrEmpty(xmbh))
            {
                MessageBox.Show("err");
                return "";
            }
            // zdzd表中获取需要获取的参数  

            string strParams = "";
            if (dataType == "CF")//取赤峰的数据
            {
                strParams = _projectInfo.GetPar(xmbh, zdzdParms, txt_y.Text, txtdatafiled.Text, connType, queryNumber);
            }
            else if (dataType == "WH")
            {
                strParams = _projectInfo.GetPar2(xmbh, zdzdParms, txt_y.Text, txtdatafiled.Text, connType, queryNumber);
            }
            if (string.IsNullOrWhiteSpace(strParams.Trim()))
            {
                MessageBox.Show("参数数据不能为空！", "调试", MessageBoxButtons.OK);
                return "";
            }
            return strParams;

        }

        public string GetParamsWH(List<string> zdzdParms, string mRecid)
        {
            string jcxmBH = this.txt_jcxmbh.Text.Trim();

            string xmbh = this.txt_jcxmbh.Text.Trim();

            if (string.IsNullOrEmpty(xmbh))
            {
                MessageBox.Show("err");
                return "";
            }
            // zdzd表中获取需要获取的参数  
            string strParams = "";
            //调式 时 赤峰
            //string strParams = _projectInfo.GetParWH(jcxmBH, zdzdParms, txt_y.Text, txtdatafiled.Text, ESqlConnType.ConnectionStringWH, mRecid);

            if (string.IsNullOrWhiteSpace(strParams.Trim()))
            {
                MessageBox.Show("参数数据不能为空！", "调试", MessageBoxButtons.OK);
                return "";
            }
            return strParams;

        }
        private void btn_Debug_Click(object sender, EventArgs e)
        {
            SaveXMinfos();

            //测试乌海
            if (this.ck_other.Checked)
            {
                if (!string.IsNullOrEmpty(txt_wtdbh.Text.Trim()))
                {
                    Debug("", txt_wtdbh.Text.Trim());
                }
            }
            else
                Debug();
        }

        private void Debug(string jydbh = "", string whWtdbh = "")
        {
            string jcxmBH = this.txt_jcxmbh.Text.Trim();
            List<string> listJYDBH = new List<string>();
            var quertBH = "";
            #region 传入参数
            string strIOParams = "";
            string strIParams = "";
            //查询输入输出字段
            List<string> zdzdIOParms = _manage.GetIOFields(jcxmBH);
            //查询输入字段
            List<string> zdzdIParms = _manage.GetIFields(jcxmBH);

            if (!string.IsNullOrEmpty(whWtdbh))
            {
                //获取乌海的数据
                quertBH = whWtdbh;
                //参数
                strIOParams = GetParams(zdzdIOParms, whWtdbh, ESqlConnType.ConnectionStringWH, "WH");
                strIParams = GetParams(zdzdIParms, whWtdbh, ESqlConnType.ConnectionStringWH, "WH");
            }
            else
            {
                //获取JYDBH

                if (string.IsNullOrEmpty(jydbh))
                    listJYDBH = GetJYDBHs();
                else
                    listJYDBH.Add(jydbh);


                if (listJYDBH == null || listJYDBH.Count == 0)
                {
                    MessageBox.Show("获取JYDBH失败！", "调试", MessageBoxButtons.OK);
                    return;
                }
                if (string.IsNullOrWhiteSpace(this.ritCode.Text.Trim()))
                {
                    MessageBox.Show("计算方法代码不能为空！", "调试", MessageBoxButtons.OK);
                    return;
                }
                quertBH = listJYDBH[0];
                //参数
                strIOParams = GetParams(zdzdIOParms, quertBH, ESqlConnType.ConnectionStringCF);
                strIParams = GetParams(zdzdIParms, quertBH, ESqlConnType.ConnectionStringCF);

            }

            //代码
            string strCode = this.ritCode.Text.Trim();

            //输入的参数
            IDictionary<string, IList<IDictionary<string, string>>> dicIOParams = null;
            dicIOParams = Common.DBUtility.JsonHelper.GetAfferentDictionaryNew(strIOParams);

            IDictionary<string, IList<IDictionary<string, string>>> dicIParams = null;
            dicIParams = Common.DBUtility.JsonHelper.GetAfferentDictionaryNew(strIParams);
            //帮助表
            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            if (!string.IsNullOrEmpty(this.txt_helper.Text.Trim()))
            {
                listExtraData = GetExtraData(this.txt_helper.Text.Trim());

            }

            #region 初始化dataGridView
            //创建DataSet对象
            DataSet ds = new DataSet();
            //创建DataTable对象
            DataTable dt = new DataTable();
            //创建列
            dt.Columns.Add("字段", typeof(string));
            dt.Columns.Add("计算前", typeof(string));
            dt.Columns.Add("计算后", typeof(string));
            //创建行
            DataRow row = dt.NewRow();
            //添加数据

            //赤峰数据库获取的字段值
            DataSet ch_sdata = null;
            if (!string.IsNullOrEmpty(whWtdbh))
            {
                ch_sdata = _projectInfo.GetParmsWH(jcxmBH, zdzdIOParms, quertBH);
            }
            else
            {
                ch_sdata = _projectInfo.GetParmsCF(jcxmBH, zdzdIOParms, quertBH);
            }
            //给字段列赋值
            int flag = 0;
            foreach (DataColumn mDc in ch_sdata.Tables[0].Columns)
            {
                row = dt.NewRow();
                dt.Rows.Add(row);
                dt.Rows[flag]["字段"] = mDc.ToString();
                flag++;
            }
            flag = 0;
            foreach (DataRow mDr in ch_sdata.Tables[0].Rows)
            {
                flag = 0;
                foreach (var item in mDr.ItemArray)
                {
                    dt.Rows[flag]["计算前"] = item;
                    flag++;
                }
            }

            #endregion
            #endregion
            var isSuccess = true;
            CodeDebug(listExtraData, ref dicIParams, out isSuccess, true);

            if (!isSuccess)
            {
                return;
            }
            #region 绑定dataGridView
            List<string> dicKeys = dicIParams.Keys.ToList();
            if (dicKeys.Count == 0)
            {
                MessageBox.Show($"返回试验项目{jcxmBH}数据异常");
            }

            var sItem = dicIParams[$"S_{jcxmBH}"];

            string ikey = "";
            foreach (var item in sItem)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ikey = dt.Rows[i]["字段"].ToString().ToUpper();
                    if (item.Keys.Contains(ikey))
                    {
                        dt.Rows[i]["计算后"] = item[ikey];
                    }
                }
            }


            //将数据表添加到DataSet中 
            ds.Tables.Add(dt);

            this.dataGridViewResult.DataSource = ds.Tables[0];
            #endregion
        }

        private Dictionary<string, IList<IDictionary<string, string>>> GetExtraData(string bzTables)
        {
            Dictionary<string, IList<IDictionary<string, string>>> listExtraData = new Dictionary<string, IList<IDictionary<string, string>>>();
            if (!String.IsNullOrEmpty(bzTables))
            {
                List<string> listExtras = new List<string>();
                List<string> extras = new List<string>();

                //listExtras = .Split(';').ToList();
                listExtras = bzTables.Split(';').ToList();
                string extraDJjson = "";

                IDictionary<string, IList<IDictionary<string, string>>> extraJsonData = null;

                foreach (var item in listExtras)
                {
                    extras = item.Split(',').ToList();
                    if (extras.Count == 0)
                    {
                        continue;
                    }
                    extraDJjson = JsonHelper.GetDataJson($"select * from {extras[0]} ", extras[1], ESqlConnType.ConnectionStringCF);
                    extraJsonData = JsonHelper.GetDictionary(extraDJjson);
                    listExtraData.Add(extras[1], extraJsonData[extras[1]]);
                }
            }
            return listExtraData;
        }

        private string _RootFile = "";
        private string _FileName = "";
        private string _TemplatePath = "";
        private CompilerResults _CR = null;

        private void CalInit()
        {
            Font fonRich = new System.Drawing.Font(FontFamily.GenericMonospace, 14);
            try
            {
                _TemplatePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "CalculateTemplate.cs");
                _RootFile = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "CalculateFile");
                if (!Directory.Exists(_RootFile))
                {
                    DirectoryInfo dii = new DirectoryInfo(_RootFile);
                    dii.Create();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "DebuggingFrm_Load", MessageBoxButtons.OK);
            }
        }

        private void CodeDebug(Dictionary<string, IList<IDictionary<string, string>>> listExtraData,
            ref IDictionary<string, IList<IDictionary<string, string>>> dicParams, out bool isSuccess, bool isDebug)
        {
            string strSourceFile = "", strDllPath = "", strPdbPath = "", strError = "";
            string strLine = "";
            CSharpCodeProvider cprCSharp = null;
            CompilerParameters cpaParams = null;
            isSuccess = true;
            StringBuilder stbTemplate = new StringBuilder();
            try
            {
                #region 读取模板
                FileStream fsm = new FileStream(_TemplatePath, FileMode.Open);
                StreamReader sr = new StreamReader(fsm);
                while ((strLine = sr.ReadLine()) != null)
                {
                    if (strLine.Contains("strComputingMethod"))
                    {
                        if (isDebug)
                        {
                            stbTemplate.AppendLine("System.Diagnostics.Debugger.Break();");
                        }
                        foreach (string strCodeLine in this.ritCode.Lines)
                        {
                            stbTemplate.AppendLine("                " + strCodeLine);
                        }
                    }
                    else
                    {
                        stbTemplate.AppendLine(strLine);
                    }
                }
                sr.Close(); sr.Dispose();
                fsm.Close(); fsm.Dispose();
                #endregion 读取模板

                #region 文件名
                _FileName = System.Guid.NewGuid().ToString();
                #endregion 文件名

                #region 编译
                cprCSharp = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v4.0" } });
                cpaParams = new CompilerParameters();
                cpaParams.ReferencedAssemblies.Add("System.dll");
                cpaParams.ReferencedAssemblies.Add("mscorlib.dll");
                cpaParams.ReferencedAssemblies.Add("System.Data.dll");
                cpaParams.ReferencedAssemblies.Add("System.Drawing.dll");
                cpaParams.ReferencedAssemblies.Add("System.Core.dll");
                cpaParams.ReferencedAssemblies.Add("System.Windows.Forms.dll");
                cpaParams.ReferencedAssemblies.Add("hh.fw.Standard.dll");
                cpaParams.ReferencedAssemblies.Add("Microsoft.VisualBasic.dll");


                cpaParams.OutputAssembly = Path.Combine(_RootFile, _FileName + ".dll");
                //cpaParams.CompilerOptions = "";
                cpaParams.GenerateInMemory = false;
                cpaParams.IncludeDebugInformation = true;
                cpaParams.TreatWarningsAsErrors = false;
                //byte[] bytA = System.Text.Encoding.UTF8.GetBytes(stbTemplate .ToString ());
                //string strB = Convert.ToBase64String(bytA);
                //string strA= Encoding.UTF8.GetString(Convert.FromBase64String(strB));
                //string strA = stbTemplate.ToString();
                strSourceFile = Path.Combine(_RootFile, _FileName + ".cs");
                File.WriteAllText(strSourceFile, stbTemplate.ToString());
                _CR = cprCSharp.CompileAssemblyFromFile(cpaParams, strSourceFile);
                if (_CR.Errors.HasErrors)
                {
                    StringBuilder stbError = new StringBuilder();
                    foreach (CompilerError ce in _CR.Errors)
                    {
                        stbError.Append("行号：" + ce.Line + "   ");
                        stbError.Append("错误号：" + ce.ErrorNumber + "   ");
                        stbError.AppendLine("错误描述：" + ce.ErrorText);
                    }

                    //返回错误信息
                    this.ritResult.Text = stbError.ToString();
                    this.tacDebug.SelectedTab = this.tab_debug;
                    MessageBox.Show("编译失败！", "调试", MessageBoxButtons.OK);
                    isSuccess = false;
                    return;
                }
                else
                {
                    MessageBox.Show("编译成功！", "调试", MessageBoxButtons.OK);
                }
                #endregion 编译

                if (isDebug)
                {
                    #region 调试
                    strDllPath = System.IO.Path.Combine(_RootFile, _FileName + ".dll");
                    strPdbPath = System.IO.Path.Combine(_RootFile, _FileName + ".pdb");
                    if (!File.Exists(strDllPath))
                    {
                        MessageBox.Show(strDllPath + "不存在！", "调试", MessageBoxButtons.OK);
                        isSuccess = false;
                        return;
                    }
                    if (!File.Exists(strPdbPath))
                    {
                        MessageBox.Show(strPdbPath + "不存在！", "调试", MessageBoxButtons.OK);
                        isSuccess = false;
                        return;
                    }
                    byte[] bytDll = System.IO.File.ReadAllBytes(strDllPath);
                    byte[] bytPdb = System.IO.File.ReadAllBytes(strPdbPath);
                    Assembly asm = Assembly.Load(bytDll, bytPdb);
                    Type type = asm.GetType("CalDebugTools.CalculateTemplate");
                    CalculateTemplateI obj = (CalculateTemplateI)Activator.CreateInstance(type);
                    bool booResult = obj.Calculate(listExtraData, ref dicParams, ref strError);
                    if (booResult)
                    {
                        MessageBox.Show("计算成功！", "调试", MessageBoxButtons.OK);

                        this.tacDebug.SelectedTab = this.tapCompare;
                    }
                    else
                    {
                        this.ritResult.Text = strError;
                        this.tacDebug.SelectedTab = this.tab_debug;
                        MessageBox.Show("执行代码时失败！", "调试", MessageBoxButtons.OK);
                        isSuccess = false;
                    }
                    #endregion 调试
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "调试", MessageBoxButtons.OK);
            }
            finally
            {
                if (cpaParams != null) cpaParams = null;
                if (cprCSharp != null) cprCSharp.Dispose();
            }
        }
        //编译
        private void btn_Complie_Click(object sender, EventArgs e)
        {
            SaveXMinfos();
            string strSourceFile = "";
            string strLine = "";
            CSharpCodeProvider cprCSharp = null;
            CompilerParameters cpaParams = null;
            StringBuilder stbTemplate = new StringBuilder();
            try
            {
                #region 读取模板
                FileStream fsm = new FileStream(_TemplatePath, FileMode.Open);
                StreamReader sr = new StreamReader(fsm);
                while ((strLine = sr.ReadLine()) != null)
                {
                    if (strLine.Contains("strComputingMethod"))
                    {
                        foreach (string strCodeLine in this.ritCode.Lines)
                        {
                            stbTemplate.AppendLine("                " + strCodeLine);
                        }
                    }
                    else
                    {
                        stbTemplate.AppendLine(strLine);
                    }
                }
                sr.Close(); sr.Dispose();
                fsm.Close(); fsm.Dispose();
                #endregion 读取模板

                #region 文件名
                _FileName = System.Guid.NewGuid().ToString();
                #endregion 文件名

                #region 编译
                cprCSharp = new CSharpCodeProvider();
                cpaParams = new CompilerParameters();
                cpaParams.ReferencedAssemblies.Add("System.dll");
                cpaParams.ReferencedAssemblies.Add("mscorlib.dll");
                cpaParams.ReferencedAssemblies.Add("System.Data.dll");
                cpaParams.ReferencedAssemblies.Add("System.Drawing.dll");
                cpaParams.ReferencedAssemblies.Add("System.Core.dll");
                cpaParams.ReferencedAssemblies.Add("System.Windows.Forms.dll");
                cpaParams.ReferencedAssemblies.Add("hh.fw.Standard.dll");
                cpaParams.ReferencedAssemblies.Add("Microsoft.VisualBasic.dll");
                cpaParams.OutputAssembly = Path.Combine(_RootFile, _FileName + ".dll");
                cpaParams.GenerateInMemory = false;
                cpaParams.IncludeDebugInformation = true;
                cpaParams.TreatWarningsAsErrors = false;
                strSourceFile = Path.Combine(_RootFile, _FileName + ".cs");
                File.WriteAllText(strSourceFile, stbTemplate.ToString());
                _CR = cprCSharp.CompileAssemblyFromFile(cpaParams, strSourceFile);
                if (_CR.Errors.HasErrors)
                {
                    StringBuilder stbError = new StringBuilder();
                    foreach (CompilerError ce in _CR.Errors)
                    {
                        stbError.Append("行号：" + ce.Line + "   ");
                        stbError.Append("错误号：" + ce.ErrorNumber + "   ");
                        stbError.AppendLine("错误描述：" + ce.ErrorText);
                    }
                    //返回错误信息
                    this.ritResult.Text = stbError.ToString();
                    this.tacDebug.SelectedTab = this.tab_debug;
                    MessageBox.Show("编译失败！", "调试", MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    MessageBox.Show("编译成功！", "调试", MessageBoxButtons.OK);
                }
                #endregion 编译
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "调试", MessageBoxButtons.OK);
            }
            finally
            {
                if (cpaParams != null) cpaParams = null;
                if (cprCSharp != null) cprCSharp.Dispose();
            }


        }

        //代码执行

        private void btn_Run_Click(object sender, EventArgs e)
        {
            SaveXMinfos();
            try
            {
                string errStr = "";
                #region
                //主表唯一标识，判断取数据行数
                string jcxmBH = this.txt_jcxmbh.Text.Trim();
                //获取JYDBH 
                List<string> listJYDBH = new List<string>();
                listJYDBH = GetJYDBHs();


                if (listJYDBH.Count == 0)
                {
                    MessageBox.Show("批量调试时获取JYDBH异常！", "调试", MessageBoxButtons.OK);
                    return;
                }


                //if (string.IsNullOrWhiteSpace(this.txt_helper.Text.Trim()))
                //{
                //    MessageBox.Show("帮助表不能为空！", "调试", MessageBoxButtons.OK);
                //    return;
                //}
                #region 帮助表
                Dictionary<string, IList<IDictionary<string, string>>> listExtraData = GetExtraData(this.txt_helper.Text.Trim());
                #endregion

                #endregion
                #region 初始化dataGridView
                //创建DataSet对象
                DataSet ds = new DataSet();
                //创建DataTable对象
                DataTable dt = new DataTable();
                //创建列
                dt.Columns.Add("JYDBH", typeof(string));
                dt.Columns.Add("是否合格", typeof(string));
                //创建行
                DataRow row = dt.NewRow();
                //添加数据

                #endregion

                DataSet ch_sdata = null;
                int flag = 0;

                //查询输入输出字段
                List<string> zdzdIOParms = _manage.GetIOFields(jcxmBH);
                ///输入输出的参数
                string strIOParams = GetParams(zdzdIOParms, listJYDBH[0], ESqlConnType.ConnectionStringCF);

                //查询输入字段
                List<string> zdzdIParms = _manage.GetIFields(jcxmBH);
                //输入的参数
                string strIParams = "";

                IDictionary<string, IList<IDictionary<string, string>>> dicIParams = null;
                bool isHG = true;

                for (int i = 0; i < listJYDBH.Count; i++)
                {
                    isHG = true;
                    row = dt.NewRow();
                    dt.Rows.Add(row);
                    dt.Rows[flag]["JYDBH"] = listJYDBH[i];

                    strIParams = GetParams(zdzdIParms, listJYDBH[i], ESqlConnType.ConnectionStringCF);
                    if (string.IsNullOrEmpty(strIOParams) || string.IsNullOrEmpty(strIParams))
                    {
                        MessageBox.Show("参数数据不能为空！", "调试", MessageBoxButtons.OK);
                        return;
                    }
                    //获取赤峰数据
                    ch_sdata = _projectInfo.GetParmsCF(jcxmBH, zdzdIOParms, listJYDBH[i]);
                    //调用计算

                    dicIParams = JsonHelper.GetAfferentDictionaryNew(strIParams);

                    RunCode(listExtraData, ref dicIParams, ref errStr);
                    if (!string.IsNullOrEmpty(errStr))
                    {
                        MessageBox.Show("批量执行失败");
                        return;
                    }
                    List<string> dicKeys = dicIParams.Keys.ToList();
                    if (dicKeys.Count == 0)
                    {
                        MessageBox.Show($"返回试验项目{jcxmBH}数据异常");
                    }

                    Dictionary<string, string> cfData = new Dictionary<string, string>();

                    List<Dictionary<string, string>> dicTab = new List<Dictionary<string, string>>();
                    foreach (DataColumn mDc in ch_sdata.Tables[0].Columns)
                    {
                        cfData.Add(mDc.ToString(), ch_sdata.Tables[0].Rows[0][mDc.ToString()].ToString());
                    }

                    Dictionary<string, string> dicCalResult = new Dictionary<string, string>();

                    var sItem = dicIParams[$"S_{jcxmBH}"];

                    //计算返回的数据
                    var value = "";
                    foreach (var item in sItem)
                    {
                        foreach (var itemPar in item)
                        {
                            if (cfData.Keys.Contains(itemPar.Key) && cfData[itemPar.Key] != itemPar.Value)
                            {

                                value = cfData[itemPar.Key];

                                if (Comm.CheckValueEquals(value, itemPar.Value))
                                {
                                    continue;
                                }
                                isHG = false;
                                break;
                            }
                        }
                    }

                    dt.Rows[flag]["是否合格"] = isHG;
                    flag++;

                }
                ds.Tables.Add(dt);


                #region 绑定dataGridView

                //将数据表添加到DataSet中 

                this.DataGridViewRowBitch.DataSource = ds.Tables[0];
                #endregion
            }
            catch
            {

            }
        }


        private void RunCode(Dictionary<string, IList<IDictionary<string, string>>> listExtraData,
            ref IDictionary<string, IList<IDictionary<string, string>>> dicParams, ref string strError)
        {
            try
            {
                string strDllPath = System.IO.Path.Combine(_RootFile, _FileName + ".dll");
                byte[] bytDll = System.IO.File.ReadAllBytes(strDllPath);
                Assembly asm = Assembly.Load(bytDll);
                Type type = asm.GetType("CalDebugTools.CalculateTemplate");
                CalculateTemplateI obj = (CalculateTemplateI)Activator.CreateInstance(type);
                bool booResult = obj.Calculate(listExtraData, ref dicParams, ref strError);
                this.ritResult.Text = strError;
                if (booResult)
                {
                    //MessageBox.Show("成功！", "执行", MessageBoxButtons.OK);
                    this.tacDebug.SelectedTab = this.tapCompare;
                }
                else
                {
                    this.ritResult.Text = strError;

                    MessageBox.Show("失败！", "执行", MessageBoxButtons.OK);
                    this.tacDebug.SelectedTab = this.tapCompare;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "执行", MessageBoxButtons.OK);
                strError = "err";
            }
        }

        private void dataGridViewResult_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                string cf = this.dataGridViewResult.Rows[e.RowIndex].Cells["计算前"].Value.ToString();
                string cal = this.dataGridViewResult.Rows[e.RowIndex].Cells["计算后"].Value.ToString();

                if (Comm.CheckValueEquals(cf, cal))
                {
                    return;
                }

                if (cf.ToUpper() != cal.ToUpper())
                {
                    dataGridViewResult.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }


        private void tool_AddFields_Click(object sender, EventArgs e)
        {
            FormFields manage = new FormFields(this);
            this.Hide();
            manage.Show();
        }


        private void tool_uploadFields_Click_1(object sender, EventArgs e)
        {
            UploadFields manage = new UploadFields(this);
            this.Hide();
            manage.Show();
        }

        private void 代码上传ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CodeUpload manage = new CodeUpload(this);
            this.Hide();
            manage.Show();

        }

        private void ritCode_TextChanged(object sender, EventArgs e)
        {
            _strCode = ritCode.Text;
        }

        private void DataGridViewRowBitch_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                string name0 = "";
                string jydbh = "";
                name0 = this.DataGridViewRowBitch.Columns[e.ColumnIndex].Name;
                if (name0 == "JYDBH")
                    jydbh = this.DataGridViewRowBitch.CurrentCell.Value.ToString();


                if (!string.IsNullOrEmpty(jydbh))
                {
                    Debug(jydbh);
                }
                else
                {
                    MessageBox.Show("请双击需要调试的JYDBH");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ck_other_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ck_other.Checked)
            {
                this.txt_wtdbh.Visible = true;
                //Debug("","");
            }
            else
            {
                this.txt_wtdbh.Visible = false;
            }
        }

        private void pan_table_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// 配置字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tool_fieldSet_Click(object sender, EventArgs e)
        {
            SettingFields manage = new SettingFields(this);
            this.Hide();
            manage.Show();
        }

        private void tool_StrConver_Click(object sender, EventArgs e)
        {
            FieldToUpper manage = new FieldToUpper(this);
            this.Hide();
            manage.Show();
        }

        private void tool_StrReplace_Click(object sender, EventArgs e)
        {
            StringConvert manage = new StringConvert(this);
            this.Hide();
            manage.Show();
        }

        private void tool_dataFieldSync_Click(object sender, EventArgs e)
        {
            SyncDataField manage = new SyncDataField(this);
            this.Hide();
            manage.Show();
        }

        private void 查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
