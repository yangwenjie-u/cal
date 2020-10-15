using CalDebugTools.BLL;
using CalDebugTools.Common.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace CalDebugTools
{
    public partial class CodeUpload : Form
    {
        private Common.DBUtility.SqlBase _sqlBase = null;
        private Common.DBUtility.SqlBase _sqlDebugTool = null;
        public CodeUpload(FormMain main)
        {
            InitializeComponent();
            _formMain = main;
            if (_sqlBase == null)
                _sqlBase = new SqlBase();
            if (_sqlDebugTool == null)
                _sqlDebugTool = new SqlBase(ESqlConnType.ConnectionStringDebugTool);

            txtsylb.Text = ConfigurationHelper.GetConfig("jcxmbh");
            txtextratable.Text = ConfigurationHelper.GetConfig("extratable");
            txtusername.Text = ConfigurationHelper.GetConfig("username");
            txtremark.Text = ConfigurationHelper.GetConfig("remark");

        }
        FormMain _formMain;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string sylb = txtsylb.Text.Trim();
                string code = txtcode.Text.Trim();
                string extratable_name = txtextratable.Text.Trim();
                string extratable = string.Empty;
                string username = txtusername.Text.Trim();
                string beizu = txtremark.Text.Trim();

                if (string.IsNullOrEmpty(sylb.Trim()))
                {
                    MessageBox.Show("试验类别不能为空");
                    return;
                }
                if (string.IsNullOrEmpty(code.Trim()))
                {
                    MessageBox.Show("计算代码不能为空");
                    return;
                }
                //if (string.IsNullOrEmpty(extratable_name.Trim()))
                //{
                //    MessageBox.Show("帮助表不能为空");
                //    return;
                //}
                if (string.IsNullOrEmpty(username.Trim()))
                {
                    MessageBox.Show("用户名不能为空");
                    return;
                }

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("jcxmbh", txtsylb.Text);
                dic.Add("extratable", txtextratable.Text);
                dic.Add("username", txtusername.Text);
                dic.Add("remark", txtremark.Text);

                ConfigurationHelper.SaveConfig(dic);


                //获取字段字典json
                string zdzdjson = string.Empty;
                zdzdjson += "[";
                string jcx_json = string.Empty;
                string ZDZD_sql = string.Format(@"select SJGJ_ID,SY,DEFAVAL,LX,SSJCX,SJBMC,ZDMC,ZDLX from ZDZD_" + sylb + " where (LX like '%I%' or LX like '%O%')");
                DataSet dszdzd = _sqlDebugTool.ExecuteDataset(ZDZD_sql);
                if (dszdzd != null && dszdzd.Tables[0].Rows.Count > 0)
                {
                    int recid = 1;
                    foreach (DataRow item in dszdzd.Tables[0].Rows)
                    {
                        string lx = item["LX"].ToString().Trim();
                        string SSJCX = item["SSJCX"].ToString().Trim();
                        string SCCS = string.Empty;
                        string FHCS = string.Empty;
                        if (lx.ToUpper().Split(',').Contains("I"))
                        {
                            SCCS = "1";
                            FHCS = "0";
                        }
                        else
                        {
                            SCCS = "0";
                            FHCS = "1";
                        }
                        if (string.IsNullOrEmpty(item["SSJCX"].ToString().Trim()))
                        {
                            jcx_json += string.Format("{{\"Recid\":\"{0}\",\"SJBMC\":\"{1}\",\"ZDMC\":\"{2}\",\"SY\":\"{3}\",\"DEFAVAL\":\"{4}\",\"SCCS\":\"{5}\",\"FHCS\":\"{6}\",\"JCXM\":\"{7}\",\"Field\":\"{8}\",\"ZDLX\":\"{9}\"}},", recid, item["SJBMC"], item["ZDMC"], item["SY"], item["DEFAVAL"], SCCS, FHCS, "", "", item["ZDLX"]);

                        }
                        else
                        {
                            string[] jcxmlist = item["SSJCX"].ToString().Split(',');
                            foreach (var jcxm in jcxmlist)
                            {
                                jcx_json += string.Format("{{\"Recid\":\"{0}\",\"SJBMC\":\"{1}\",\"ZDMC\":\"{2}\",\"SY\":\"{3}\",\"DEFAVAL\":\"{4}\",\"SCCS\":\"{5}\",\"FHCS\":\"{6}\",\"JCXM\":\"{7}\",\"Field\":\"{8}\",\"ZDLX\":\"{9}\"}},", recid, item["SJBMC"], item["ZDMC"], item["SY"], item["DEFAVAL"], SCCS, FHCS, jcxm.Trim(), "", item["ZDLX"]);

                            }
                        }
                        recid++;
                    }
                }
                zdzdjson += jcx_json.TrimEnd(',') + "]";
                txt_zdzd_json.Text = zdzdjson;

                //帮助表
                if (!string.IsNullOrEmpty(extratable_name))
                {
                    extratable = "{";
                    string str = string.Empty;
                    if (!string.IsNullOrEmpty(extratable_name))
                    {
                        string[] table_list = extratable_name.Split(',');
                        foreach (string item in table_list)
                        {
                            string extra_sql = string.Format("select * from " + item);
                            DataSet extra_dt = _sqlBase.ExecuteDataset(extra_sql);
                            extra_dt.Tables[0].TableName = item;
                            str += JsonHelper.SerializeObject(extra_dt).TrimStart('{').TrimEnd('}') + ",";
                        }
                    }
                    extratable += str.TrimEnd(',') + "}";
                }
                else
                {
                    extratable = "{}";
                }
                txt_help_json.Text = extratable;
                string token = TokenHeple.GetToken(username);
                string par_json = "{\"sylb\":\"" + sylb + "\",\"code\":\"" + Base64Helper.Base64Encode(code) + "\",\"zdzd\":\"" + Base64Helper.Base64Encode(zdzdjson) + "\",\"extratable\":\"" + Base64Helper.Base64Encode(extratable) + "\",\"username\":\"" + username + "\",\"beizhu\":\"" + beizu + "\"}";

                string get_json = Data.GetHtmlByPost(Data.http_setapiurl, par_json, "", null, token);
                if (get_json.Contains("成功"))
                {
                    var code_json = new { data = new object(), code = string.Empty, message = string.Empty };
                    var json_strue = JsonHelper.DeserializeAnonymousType(get_json, code_json);
                    var ddf = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(json_strue.data.ToString());
                    var ver = ddf["ver"];
                    if (this.ck_defaultLib.Checked)
                    {
                        par_json = "{\"sylb\":\"" + sylb + "\",\"ver\":\"" + ver + "\"}";
                        get_json = Data.GetHtmlByPost(Data.http_set_defaultLib_url, par_json, "", null, token);
                        //get_json = Data.GetHtmlByPost(@"http://calctest.jzyglxt.com/apiv1/SetCalcVersionDefault", par_json, "", null, token);
                        if (get_json.Contains("成功"))
                        {
                            MessageBox.Show($"代码上传成功,设置{sylb}默认版本为{ver}");
                        }
                        else
                        {
                            MessageBox.Show($"代码上传成功,设置{sylb}默认版本失败：{json_strue.message}");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"代码上传成功");
                    }
                }
                else
                {
                    var code_json = new { data = new object(), code = string.Empty, message = string.Empty };
                    var json_strue = JsonHelper.DeserializeAnonymousType(get_json, code_json);
                    MessageBox.Show(json_strue.message);
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtsylb_MouseLeave(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtextratable.Text))
            //{
            string bztxt = string.Empty;
            string sylb = txtsylb.Text.Trim();
            string sql = "select * from PR_M_SJBSM where SSXM ='" + sylb + "' and BLX ='H3'";
            DataSet dszdzd = _sqlBase.ExecuteDataset(sql);
            foreach (DataRow item in dszdzd.Tables[0].Rows)
            {
                bztxt += item["SJBMC"].ToString() + ",";
            }
            txtextratable.Text = bztxt.Trim(',');

            //}
        }

        private void CodeUpload_FormClosed(object sender, FormClosedEventArgs e)
        {
            _formMain.Show();
        }

        private void txtextratable_TextChanged(object sender, EventArgs e)
        {

        }

        private void ck_defaultLib_CheckedChanged(object sender, EventArgs e)
        {

        }

        //private void InitializeComponent()
        //{
        //    this.SuspendLayout();
        //    // 
        //    // CodeUpload
        //    // 
        //    this.ClientSize = new System.Drawing.Size(284, 261);
        //    this.Name = "CodeUpload";
        //    this.ResumeLayout(false);

        //}
    }
}
