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
    public partial class SyncDataField : Form
    {
        FormMain _formMain;
        FieldManage _FieldManage = new FieldManage();

        public SyncDataField(FormMain main)
        {
            InitializeComponent();
            _formMain = main;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            var tableName = txt_tableName.Text.Trim();
            var tableZDZD = txt_zdzd.Text.Trim();

            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(tableZDZD))
            {
                MessageBox.Show("err，请输入数据");
            }

            if (!_FieldManage.CheckZDZDIsTrue(tableName))
            {
                MessageBox.Show("err,zdzd表不存在");
            }
            if (_FieldManage.InsertTableFieldToZDZD(tableZDZD, tableName))
            {
                MessageBox.Show("success!");
            }
            else
            {
                MessageBox.Show("err!");

            }
        }

        private void SyncDataField_FormClosed(object sender, FormClosedEventArgs e)
        {
            _formMain.Show();
        }
    }
}
