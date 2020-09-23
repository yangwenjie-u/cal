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

namespace CalDebugTools
{
    public partial class ProjectManage : Form
    {
        ProjectInfos _projectInfos = new ProjectInfos();

        FormMain _formMain;
        public ProjectManage(FormMain main)
        {
            InitializeComponent();
            _formMain = main;
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            ProjectInfo info = new ProjectInfo();

            if (string.IsNullOrEmpty(txt_jcxmbh.Text.Trim()))
            {
                MessageBox.Show("err");
                return;
            }

            if (string.IsNullOrEmpty(txt_m.Text.Trim()))
            {
                MessageBox.Show("err");
                return;
            }
            if (string.IsNullOrEmpty(txt_s.Text.Trim()))
            {
                MessageBox.Show("err");
                return;
            }
            //if (string.IsNullOrEmpty(txt_helper.Text.Trim()))
            //{
            //    MessageBox.Show("err");
            //    return;
            //}
            //if (string.IsNullOrEmpty(txt_cal.Text.Trim()))
            //{
            //    MessageBox.Show("err");
            //    return;
            //}

            info.BH = txt_jcxmbh.Text;
            info.MTable = txt_m.Text;
            info.STable = txt_s.Text;
            info.BZTable = txt_helper.Text;
            info.YTable = txt_y.Text;
            info.DataFiled = txtdatafiled.Text;
            //info.Cal = txt_cal.Text;
            int result = _projectInfos.DataInsert(info);      

            if (result == -3)
            {
                MessageBox.Show("添加失败，请检测项目编号");
            }
            else if (result == -2)
            {
                MessageBox.Show("添加失败，已存在相同的项目");
            }
            else
            {
                MessageBox.Show("添加成功", "提示", MessageBoxButtons.OK);
                this.Close();
                _formMain.Show();
            }
        }

        private void ProjectManage_FormClosed(object sender, FormClosedEventArgs e)
        {
            _formMain.Show();
        }

        private void ProjectManage_Load(object sender, EventArgs e)
        {

            DataSet Ds = _projectInfos.GetAllProjectInfos();
            //使用DataSet绑定时，必须同时指明DateMember 
            this.dataGridView1.DataSource = Ds;
            this.dataGridView1.DataMember = "ProjectInfo";

            //也可以直接用DataTable来绑定 
            //this.dataGridView1.DataSource = Ds.Tables["ProjectInfo"];

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // SJBMC,ZDMC,SY,SSJCX,LX 
            string ID = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
            string BH = dataGridView1.Rows[e.RowIndex].Cells["BH"].Value.ToString();
            string MTable = dataGridView1.Rows[e.RowIndex].Cells["MTable"].Value.ToString();
            string STable = dataGridView1.Rows[e.RowIndex].Cells["STable"].Value.ToString();
            string BZTable = dataGridView1.Rows[e.RowIndex].Cells["BZTable"].Value.ToString();
            string YTable = dataGridView1.Rows[e.RowIndex].Cells["YTable"].Value.ToString();
            string DataFiled = dataGridView1.Rows[e.RowIndex].Cells["DataFiled"].Value.ToString();

            ProjectInfos project = new ProjectInfos();
            ProjectInfo info = new ProjectInfo();
            info.ID = Convert.ToInt16(ID);
            info.BH = BH;
            info.MTable = MTable;
            info.STable = STable;
            info.BZTable = BZTable;
            info.YTable = YTable;
            info.DataFiled = DataFiled;
            int ret = project.UpdateProInfos(info);

            if (ret == -1)
            {
                MessageBox.Show("修改失败");
            }
        }

        private void txt_jcxmbh_MouseLeave(object sender, EventArgs e)
        {
            var val = txt_jcxmbh.Text.Trim();
            if (!string.IsNullOrEmpty(val))
            {
                txt_s.Text = "S_" + val;
                txt_m.Text = "M_" + val;
            }
        }
    }
}
