﻿using CalDebugTools.BLL;
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
    public partial class AddProject : Form
    {
        FormMain _formMain;
        ProjectService projectService = new ProjectService();
        public AddProject(FormMain main)
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

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(com_dataSource.SelectedValue.ToString()))
            {
                MessageBox.Show("请选择要添加到检测结构");
                return;
            }
            if (string.IsNullOrEmpty(txt_ProjectName.Text))
            {
                return;
            }
            if (MessageBox.Show($"是否添加项目{txt_ProjectName.Text}?", "Confirm Message", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }

            string msg = "";
            //projectService.CreateProjectTable(com_dataSource.SelectedValue.ToString(), txt_ProjectName.Text.ToUpper(), this.chk_addjcjt.Checked, this.chk_addjcjg.Checked, out msg);

            List<string> dbNameList = CheckDataBaseSet();
            var _deleteSqlStr = "";
            var _outMsg = "";
            int msgLen = 0;
            foreach (var item in dbNameList)
            {
                _deleteSqlStr += "检测机构:" + item.ToUpper() + "\r\n";
                _outMsg += "检测机构:" + item.ToUpper() + "\r\n";
                msgLen += _outMsg.Length;
                projectService.CreateProjectTable(item, txt_ProjectName.Text.ToUpper(), this.chk_addjcjt.Checked, this.chk_addjcjg.Checked, out msg);

            }
            if (string.IsNullOrEmpty(msg))
            {
                MessageBox.Show("添加成功", "提示");
            }
            else
            {
                MessageBox.Show("添加失败,请查看日志信息", "提示");
            }
        }

        private void AddProject_FormClosed(object sender, FormClosedEventArgs e)
        {
            _formMain.Show();
        }
    }
}
