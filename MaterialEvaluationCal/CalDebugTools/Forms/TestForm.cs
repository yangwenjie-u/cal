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

namespace CalDebugTools.Forms
{
    public partial class TestForm : Form
    {
        FieldManage _manage = new FieldManage();
        FormMain _formMain;
        public TestForm(FormMain main)
        {
            _formMain = main;
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                List<string> list = new List<string>();
                string sql = "";

                decimal val1 = Math.Round(Convert.ToDecimal(txt_bh.Text), 1);
                decimal val2 = val1 + 1;
                decimal val3 = Math.Round((val2 + 1) / 10, 1);
                int count = Convert.ToInt32(txt_count.Text);
                for (int i = 0; i < count; i++)
                {
                    //insert into  BZ_GCBHGC(MC, GCBH, JXPC, GCYL) values('给水用聚乙烯(PE)管材', '41.0～42.0', '4.3', '----')

                    sql = $"insert into  BZ_GCBHGC (MC,GCBH, JXPC,GCYL) values('冷热水用耐热聚乙烯(PE-RT)管道系统','{val1}～{val2}','{val3}','----')";
                    val1++;
                    val2 = val1 + 1;
                    val3 = Math.Round((val2 + 1) / 10, 1);
                    list.Add(sql);

                    if (count < val1)
                    {
                        break;
                    }
                }
                _manage.InsertBZInfos(list);
            }
            catch
            {
                MessageBox.Show("err");
            }
        }

        private void TestForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _formMain.Show();
        }
    }
}
