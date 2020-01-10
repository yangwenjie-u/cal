using CalDebugTools.DAL;
using CalDebugTools.Model;
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
    public partial class SettingFields : Form
    {
        FormMain _formMain;
        FieldManage _fieldManage;
        public SettingFields(FormMain main)
        {
            _formMain = main;
            _fieldManage = new FieldManage();
            InitializeComponent();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                string xmbh = txt_xmbh.Text.Trim();
                string jcxm = txt_jcxm.Text.Trim();
                string localT = txt_localT.Text.Trim();
                string localF = txt_localF.Text.Trim();
                string remoteT = txt_remoteT.Text.Trim();
                string remoteF = txt_remoteF.Text.Trim();

                if (string.IsNullOrEmpty(xmbh) || string.IsNullOrEmpty(jcxm) || string.IsNullOrEmpty(localT)
                    || string.IsNullOrEmpty(localF) || string.IsNullOrEmpty(remoteT) || string.IsNullOrEmpty(remoteF))
                {
                    MessageBox.Show("err:输入信息不完整");
                    return;
                }
                CalculateParam param = new CalculateParam();
                param.SYXMBH = xmbh;
                param.JCXM = jcxm;
                param.LocalTableName = localT;
                param.LocalZdName = localF;
                param.RemoteTableName = remoteT;
                param.RemoteZdName = remoteF;
                var dd = _fieldManage.InsertParam(param);

                if (dd == -2)
                {
                    MessageBox.Show($"已存在配置字段" + remoteF);
                }
                else if (dd == 1)
                {
                    MessageBox.Show($"成功");
                    ReflashViews();
                }
                else
                {
                    MessageBox.Show("err");
                }
            }
            catch
            {
            }
        }

        private void SettingFields_FormClosed(object sender, FormClosedEventArgs e)
        {
            _formMain.Show();
        }

        private void SettingFields_Load(object sender, EventArgs e)
        {
            ReflashViews();
        }
        public void ReflashViews()
        {
            DataSet Ds = _fieldManage.GetSettingFieldsInfos();
            //_fieldManage.df();
            //使用DataSet绑定时，必须同时指明DateMember 
            this.dataGridView1.DataSource = Ds;
            this.dataGridView1.DataMember = "H_Calculate_Param";
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string recid = dataGridView1.Rows[e.RowIndex].Cells["Recid"].Value.ToString(); ;
            string xmbh = dataGridView1.Rows[e.RowIndex].Cells["SYXMBH"].Value.ToString(); ;
            string jcxm = dataGridView1.Rows[e.RowIndex].Cells["JCXM"].Value.ToString(); ;
            string localT = dataGridView1.Rows[e.RowIndex].Cells["LocalTableName"].Value.ToString(); ;
            string localF = dataGridView1.Rows[e.RowIndex].Cells["LocalZdName"].Value.ToString(); ;
            string remoteT = dataGridView1.Rows[e.RowIndex].Cells["RemoteTableName"].Value.ToString(); ;
            string remoteF = dataGridView1.Rows[e.RowIndex].Cells["RemoteZdName"].Value.ToString(); ;
            CalculateParam param = new CalculateParam();

            param.Recid = recid;
            param.SYXMBH = xmbh;
            param.JCXM = jcxm;
            param.LocalTableName = localT;
            param.LocalZdName = localF;
            param.RemoteTableName = remoteT;
            param.RemoteZdName = remoteF;

            int ret = _fieldManage.UpdateParam(param);

            if (ret == -1)
            {
                MessageBox.Show("修改失败");
            }
        }
    }
}
