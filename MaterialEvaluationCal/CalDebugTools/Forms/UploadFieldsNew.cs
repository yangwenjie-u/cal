using CalDebugTools.BLL;
using CalDebugTools.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalDebugTools.Forms
{
    public partial class UploadFieldsNew : Form
    {
        FormMain _formMain;
        FieldManage _manage = new FieldManage();
        public UploadFieldsNew(FormMain main)
        {
            InitializeComponent();
            _formMain = main;

            if (!string.IsNullOrEmpty(FormMain._strCode))
            {
                this.txt_code.Text = FormMain._strCode;
            }
            txt_xmbh.Text = ConfigurationHelper.GetConfig("jcxmbh");
        }

        private void UploadFields_FormClosed(object sender, FormClosedEventArgs e)
        {
            _formMain.Show();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            try
            {
                string strCode = txt_code.Text.ToUpper();
                string xmbh = txt_xmbh.Text.Trim();
                string lx = "";

                if (string.IsNullOrEmpty(xmbh))
                {
                    MessageBox.Show("err");
                    return;
                }

                string resultCode = ",";
                string regexStr = "";
                List<string> listStr = new List<string>();
                MatchCollection matches = null;
                Regex rg = null;
                Regex rg1 = new Regex("//(.+?)\r\n");
                string itemVal = "";

                string table_name = string.Empty;
                if (rd_s.Checked)
                {
                    //获取代码中item["***"]的字段
                    //string regexStr = rd_i.Checked? "ITEM\\[\"(.+?)\\] *": " *=(.+?)ITEM\\[\"(.+?)\\]";
                    regexStr = "SITEM\\[\"(.+?)\\]";
                    //注释内容不处理
                    listStr.Add("SITEM");

                    strCode = rg1.Replace(strCode, "");
                    strCode = strCode.Replace(" ", "");

                    rg = new Regex(regexStr, RegexOptions.Multiline | RegexOptions.Singleline);

                    matches = rg.Matches(strCode);
                    foreach (Match item in matches)
                    {
                        try
                        {
                            itemVal = item.Groups[1].Value.Replace("\"", "");
                            itemVal = itemVal.Contains("+") ? itemVal.Split('+')[0] : itemVal;

                            if (!resultCode.Contains("," + itemVal + ","))
                            {
                                resultCode += itemVal + ",";
                            }
                        }
                        catch
                        {
                            resultCode = "";
                        }
                    }
                    table_name = "S_" + xmbh;
                }

                List<string> listMregexStr = new List<string>();
                listMregexStr.Add("MITEM\\[0\\]\\[\"(.+?)\\]");
                listMregexStr.Add("MITEM\\[\"(.+?)\\]");

                if (rd_m.Checked)
                {
                    listStr.Add("MITEM");
                    listStr.Add("MITEM[0]");

                    //MItem[0]
                    regexStr = "MITEM\\[0\\]\\[\"(.+?)\\]";

                    strCode = rg1.Replace(strCode, "");
                    strCode = strCode.Replace(" ", "");
                    rg = new Regex(regexStr, RegexOptions.Multiline | RegexOptions.Singleline);
                    matches = rg.Matches(strCode);

                    foreach (Match item in matches)
                    {
                        try
                        {
                            itemVal = item.Groups[1].Value.Replace("\"", "");
                            itemVal = itemVal.Contains("+") ? itemVal.Split('+')[0] : itemVal;
                            if (!resultCode.Contains("," + itemVal + ","))
                            {
                                resultCode += itemVal + ",";
                            }
                        }
                        catch
                        {
                            resultCode = "";
                        }
                    }

                    regexStr = "MITEM\\[\"(.+?)\\]";

                    strCode = rg1.Replace(strCode, "");
                    strCode = strCode.Replace(" ", "");
                    rg = new Regex(regexStr, RegexOptions.Multiline | RegexOptions.Singleline);
                    matches = rg.Matches(strCode);
                    foreach (Match item in matches)
                    {
                        try
                        {
                            itemVal = item.Groups[1].Value.Replace("\"", "");
                            itemVal = itemVal.Contains("+") ? itemVal.Split('+')[0] : itemVal;
                            if (!resultCode.Contains("," + itemVal + ","))
                            {
                                resultCode += itemVal + ",";
                            }
                        }
                        catch
                        {
                            resultCode = "";
                        }
                    }

                    table_name = "M_" + xmbh;
                }

                //数据表
                if (rd_other.Checked)
                {
                    string[] otherlist = txtother.Text.Split('|');
                    foreach (string tableName in otherlist)
                    {
                        listStr.Add(tableName);
                        regexStr = tableName.ToUpper() + "\\[\"(.+?)\\]";
                        string data_tablename = tableName;

                        rg = new Regex(regexStr, RegexOptions.Multiline | RegexOptions.Singleline);
                        matches = rg.Matches(strCode);
                        foreach (Match item2 in matches)
                        {
                            try
                            {
                                itemVal = item2.Groups[1].Value.Replace("\"", "");
                                itemVal = itemVal.Contains("+") ? itemVal.Split('+')[0] : itemVal;
                                if (!resultCode.Contains("," + itemVal + ","))
                                {
                                    resultCode += itemVal + ",";

                                    //判断zdzd表是否有字段，没有的话添加

                                    _manage.InsertTableFieldToZDZD("ZDZD_" + xmbh, tableName, itemVal);
                                }
                            }
                            catch
                            {
                                resultCode = "";
                            }
                        }
                        table_name += data_tablename + ",";
                    }
                    table_name = table_name.TrimEnd(',');
                }

                List<Fields> Ifields = new List<Fields>();
                List<Fields> Ofields = new List<Fields>();
                GetFiledIO(strCode, listStr, ref Ifields, ref Ofields);

                if (resultCode.Length < 2)
                {
                    MessageBox.Show("err" + "获取字段失败");
                    return;
                }
                resultCode = resultCode.Substring(1, resultCode.Length - 2);
                txt_result.Text = resultCode;

                DataSet redata = _manage.GetZdzdByZdmc(xmbh, resultCode.Split(',').ToList(), table_name);

                if (redata != null)
                    this.data_result.DataSource = redata.Tables[0];
                string dtLX = "";
                string dtSSJCX = "";

                //主从表I类型字段（计算需要用到的字段）
                //List<string> listInput = _manage.GetInputFields(xmbh, table_name);

                List<string> IfieldsAllMatch = new List<string>();
                List<string> IfieldsContain = new List<string>();

                List<string> OfieldsAllMatch = new List<string>();
                List<string> OfieldsContain = new List<string>();

                foreach (var field in Ifields)
                {
                    //判定是否已经O类型
                    if (field.AllMatch)
                    {
                        IfieldsAllMatch.Add(field.Name);
                    }
                    else
                    {
                        IfieldsContain.Add(field.Name);
                    }
                }
                foreach (var field in Ofields)
                {
                    //判定是否已经O类型
                    if (field.AllMatch)
                    {
                        OfieldsAllMatch.Add(field.Name);
                    }
                    else
                    {
                        OfieldsContain.Add(field.Name);
                    }
                }

                var zdmc = "";
                foreach (DataRow item in redata.Tables[0].Rows)
                {
                    zdmc = item["ZDMC"].ToString().ToUpper();
                    dtLX = item["LX"].ToString().ToUpper();

                    if (IfieldsAllMatch.Contains(zdmc))
                    {
                        dtLX += dtLX.Contains("I") ? "" : ",I";
                        item["LX"] = dtLX;
                        continue;
                    }

                    foreach (var field in IfieldsContain)
                    {
                        if (OfieldsAllMatch.Contains(zdmc))
                        {
                            continue;
                        }
                        if (zdmc.StartsWith(field))
                        {
                            dtLX += dtLX.Contains("I") ? "" : ",I";
                            item["LX"] = dtLX;
                            continue;
                        }
                    }

                    if (OfieldsAllMatch.Contains(zdmc))
                    {
                        dtLX += dtLX.Contains("O") ? "" : ",O";
                        item["LX"] = dtLX;
                        continue;
                    }

                    foreach (var field in OfieldsContain)
                    {
                        if (zdmc.StartsWith(field))
                        {
                            dtLX += dtLX.Contains("O") ? "" : ",O";
                            item["LX"] = dtLX;
                            continue;
                        }
                    }

                }

                this.data_result.DataSource = redata.Tables[0];
            }
            catch (Exception ex)
            {

            }
        }


        private void data_result_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string xmbh = txt_xmbh.Text.Trim();
            // SJBMC,ZDMC,SY,SSJCX,LX 
            string SSJCX = data_result.Rows[e.RowIndex].Cells["SSJCX"].Value.ToString();
            string LX = data_result.Rows[e.RowIndex].Cells["LX"].Value.ToString();
            string Recid = data_result.Rows[e.RowIndex].Cells["SJGJ_ID"].Value.ToString();

            int ret = _manage.UpdateZdzd(xmbh, Recid, LX, SSJCX);

            if (ret == -1)
            {
                MessageBox.Show("修改失败");
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_sava_Click(object sender, EventArgs e)
        {
            DataTable dtCopy = this.data_result.DataSource as DataTable;

            if (dtCopy == null)
            {
                return;
            }
            DataSet dsUsers = new DataSet();
            DataTable dtEdit = dtCopy.GetChanges(DataRowState.Modified);

            //修改
            if (dtEdit != null)
            {
                dtEdit.TableName = "Edit";
                dsUsers.Tables.Add(dtEdit);
            }

            //保存数据
            if (SaveUser(dsUsers))
            {
                MessageBox.Show("保存成功!");
                //重新加载数据
            }
            else
            {
                MessageBox.Show("保存失败!");
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        private bool SaveUser(DataSet ds)
        {
            bool tf = false;
            string xmbh = txt_xmbh.Text.Trim();
            //修改
            if (ds.Tables["Edit"] != null)
            {
                foreach (DataRow dr in ds.Tables["Edit"].Rows)
                {

                    _manage.UpdateZdzd(xmbh, dr["SJGJ_ID"].ToString(), dr["LX"].ToString(), dr["SSJCX"].ToString());
                }

            }
            return true;
        }

        private void rd_i_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txt_code_TextChanged(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        public struct Fields
        {
            /// <summary>
            /// 字段名
            /// </summary>
            public string Name;
            /// <summary>
            /// 全字符匹配
            /// </summary>
            public bool AllMatch;
        };
        /// <summary>
        /// 判断字段是I/O类型
        /// </summary>
        /// <param name="code"></param>
        /// <param name="Ifields"></param>
        /// <param name="Ofields"></param>
        public void GetFiledIO(string code, List<string> listStr, ref List<Fields> Ifields, ref List<Fields> Ofields)
        {
            List<string> listfiled = new List<string>();

            //code = code.Replace("==", "=");
            Regex rg = new Regex(@"\[""(?<name>[^\]]*)\]");
            MatchCollection match = rg.Matches(code);
            foreach (Match ma in match)
            {
                string filed = ma.Groups["name"].Value;
                filed = filed.Substring(0, filed.IndexOf("\""));
                listfiled.Add(filed);
            }
            string[] codelist = code.Replace("\r\n", "#").Split('#');

            try
            {
                Fields fieldInfo = new Fields();
                foreach (var fieldName in listfiled)
                {
                    for (int i = 0; i < codelist.Count(); i++)
                    {
                        foreach (var comPre in listStr)
                        {
                            //全字符匹配
                            var sd = comPre + "[\"" + fieldName + "\"]";
                            if (codelist[i].Contains(sd))
                            {
                                SetFieldsType(codelist[i], sd, fieldName, fieldInfo, true, ref Ifields, ref Ofields);
                                break;
                            }

                            sd = comPre + "[\"" + fieldName + "\"";
                            if (codelist[i].Contains(sd))
                            {
                                SetFieldsType(codelist[i], sd, fieldName, fieldInfo, false, ref Ifields, ref Ofields);
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }



        public void SetFieldsType(string str, string regexStr, string fieldName, Fields field, bool allMatch, ref List<Fields> Ifields, ref List<Fields> Ofields)
        {
            //字段左侧的字符
            string fieldLeft = "";
            //字段又侧的字符
            string fieldRight = "";

            int num = str.IndexOf(regexStr);
            /*   I类型
             * 字段左侧没有“=”号，肯定是I类型
             *  eg： 
             *      if (IsQualified(sItem["GH_KD_KYAVG"], sItem["KD_KYAVG"], true) == "不符合")
             *   
             *  字段左侧有“=”号,可能是I类型也可能是上面是O类型，
             *  这时候需要判定是否已经O类型
             *   
             *   I 类型代码
             *     if (Conversion.Val(sItem["HSLKZZ"]) >= 5)
             *   
             *   多等号的现象
             *         var mrsDj = extraDJ.FirstOrDefault(u => u["PH"] == sItem["GCLX_PH"] && u["GJLB"] == mGjlb.Trim());
             *   O类型
             *   位于第一个“=”号的左边
             */

            fieldLeft = str.Substring(0, num);
            fieldRight = str.Substring(num);
            field.Name = fieldName;
            field.AllMatch = allMatch;
            // 字段的左边只有一个等号（没有》=，《=）
            // 字段左侧没有字符串
            if (string.IsNullOrEmpty(fieldLeft) || (fieldLeft.IndexOf('=') == 1 && fieldLeft.IndexOf(">=") == -1 && fieldLeft.IndexOf("<=") == -1))
            {
                if (!Ofields.Contains(field))
                    Ofields.Add(field);
            }

            //字段左侧没有“=”号，肯定是I类型
            if (string.IsNullOrEmpty(fieldLeft) || fieldLeft.IndexOf('=') == -1)
            {
                if (!Ofields.Contains(field) && !Ifields.Contains(field))
                    Ifields.Add(field);
            }

            //字段的左侧有“=”号,表示可能是I类型也可能是上面是O类型,这时候需要判定是否已经O类型
            if (fieldLeft.Contains("="))
            {
                if (!Ofields.Contains(field) && !Ifields.Contains(field))
                    Ifields.Add(field);
            }
        }


        private void radioButton1_Click(object sender, EventArgs e)
        {
            txtother.Visible = true;
        }

        private void rd_m_Click(object sender, EventArgs e)
        {
            txtother.Visible = false;
        }

        private void rd_s_Click(object sender, EventArgs e)
        {
            txtother.Visible = false;
        }
    }
}
