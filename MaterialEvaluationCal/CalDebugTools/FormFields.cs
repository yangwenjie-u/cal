using CalDebugTools.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalDebugTools
{
    public partial class FormFields : Form
    {
        FormMain _formMain;
        FieldManage _manage = new FieldManage();
        public FormFields(FormMain main)
        {
            InitializeComponent();
            _formMain = main;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void FormFields_Load(object sender, EventArgs e)
        {

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            string xmbh = txt_xmbh.Text.Trim();
            string jcxm = txt_jcxm.Text.Trim();
            string tableName = txt_tableName.Text.Trim();
            string field = txt_field.Text.Trim();
            string lx = "";
            #region
            if (string.IsNullOrEmpty(xmbh))
            {
                MessageBox.Show("err");
                return;
            }
            if (string.IsNullOrEmpty(jcxm))
            {
                MessageBox.Show("err");
                return;
            }
            if (string.IsNullOrEmpty(tableName))
            {
                MessageBox.Show("err");
                return;
            }
            if (string.IsNullOrEmpty(field))
            {
                MessageBox.Show("err");
                return;
            }
            #endregion

            lx = rd_i.Checked ? "I" : "O";
            int result = 0;
            foreach (var item in field.Split(','))
            {
                 result = _manage.InsertFields(xmbh, jcxm, tableName, item, lx);
            }

            if (result != -1)
            {
                MessageBox.Show("添加成功");
            }
        }

        private void FormFields_FormClosed(object sender, FormClosedEventArgs e)
        {
            _formMain.Show();
        }
    }
}
