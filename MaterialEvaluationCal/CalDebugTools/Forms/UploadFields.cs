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
    public partial class UploadFields : Form
    {
        FormMain _formMain;
        FieldManage _manage = new FieldManage();
        public UploadFields(FormMain main)
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

                            if (itemVal.Contains("JCJG"))
                            {
                            }
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

                List<string> Ifields = new List<string>();
                List<string> Ofields = new List<string>();
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

                var zdmc = "";
                foreach (DataRow item in redata.Tables[0].Rows)
                {
                    zdmc = item["ZDMC"].ToString();
                    if (zdmc.Contains("JCJG"))
                    {
                    }

                    //lx = listInput.Contains(zdmc) ? "I" : "O";

                    dtLX = item["LX"].ToString();
                    //if (dtLX.Length == 0)
                    //{
                    //    dtLX = lx;
                    //}
                    //else
                    //{
                    //    dtLX += dtLX.Contains(lx) ? "" : "," + lx;
                    //}

                    //if (Ifields.Contains(zdmc))
                    //{
                    //    dtLX += dtLX.Contains("I") ? "" : ",I";
                    //}

                    //if (Ofields.Contains(zdmc.Substring(0, zdmc.Length - 1)))
                    //{
                    //    dtLX += dtLX.Contains("O") ? "" : ",O";

                    //}
                    foreach (var field in Ifields)
                    {
                        if (zdmc.Contains(field))
                        {
                            dtLX += dtLX.Contains("I") ? "" : ",I";
                            break;
                        }
                    }
                    foreach (var field in Ofields)
                    {

                        if (zdmc.Contains(field))
                        {
                            dtLX += dtLX.Contains("O") ? "" : ",O";
                            break;
                        }
                    }
                    item["LX"] = dtLX;
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


            //根据DataRowState的状态获取新增、修改、删除的表数据
            //dtAdd = dtCopy.GetChanges(DataRowState.Added);
            //  dtEdit = dtCopy.GetChanges(DataRowState.Modified);
            //dtDel = dtCopy.GetChanges(DataRowState.Deleted);

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
        /// 判断字段是I/O类型
        /// </summary>
        /// <param name="code"></param>
        /// <param name="Ifields"></param>
        /// <param name="Ofields"></param>
        public void GetFiledIO(string code, List<string> listStr, ref List<string> Ifields, ref List<string> Ofields)
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
                foreach (var item in listfiled)
                {

                    for (int i = 0; i < codelist.Count(); i++)
                    {
                        foreach (var comPre in listStr)
                        {
                            var sd = comPre + "[\"" + item + "\"";
                            if (codelist[i].Contains(sd))
                            {
                                int num = codelist[i].IndexOf(sd);
                                string str1 = codelist[i].Substring(0, num);
                                string str2 = codelist[i].Substring(num);
                                if (str1.Contains("="))
                                {
                                    if (!Ifields.Contains(item))
                                        Ifields.Add(item);
                                }
                                if (str2.Contains("="))
                                {
                                    if (!Ofields.Contains(item))
                                        Ofields.Add(item);
                                }
                            }
                        }

                    }
                    //foreach (var cod in codelist)
                    //{
                    //    var sd = comPre + "[\"" + item + "\"";
                    //    if (cod.Contains(sd))
                    //    {
                    //        int num = cod.IndexOf("\"" + item + "\"");
                    //        string str1 = cod.Substring(0, num);
                    //        string str2 = cod.Substring(num);
                    //        if (str1.Contains("="))
                    //        {
                    //            if (!Ifields.Contains(item))
                    //                Ifields.Add(item);
                    //        }
                    //        if (str2.Contains("="))
                    //        {
                    //            if (!Ofields.Contains(item))
                    //                Ofields.Add(item);
                    //        }
                    //    }
                    //}
                }
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
