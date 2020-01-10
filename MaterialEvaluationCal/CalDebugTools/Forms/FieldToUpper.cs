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
    public partial class FieldToUpper : Form
    {

        FormMain _formMain;
        public FieldToUpper(FormMain main)
        {
            InitializeComponent();
            _formMain = main;
        }

        private void btn_convert_Click(object sender, EventArgs e)
        {
            var codeStr = this.richTextBox1.Text.Trim();

            if (string.IsNullOrEmpty(codeStr))
            {
                MessageBox.Show("请输入需要转换的内容");
                return;
            }
            List<string> listFileds = new List<string>();

            Regex rg = new Regex(@"\[""(?<name>[^\]]*)\]");
            MatchCollection match = rg.Matches(codeStr);
            foreach (Match ma in match)
            {
                string filed = ma.Groups["name"].Value;
                filed = filed.Substring(0, filed.IndexOf("\""));
                if (!listFileds.Contains(filed))
                    listFileds.Add(filed);
            }


            foreach (var item in listFileds)
            {
                codeStr = codeStr.Replace("\"" + item + "\"", "\"" + item.ToUpper() + "\"");
            }

            this.richTextBox2.Text = codeStr;

        }

        private void FieldToUpper_FormClosed(object sender, FormClosedEventArgs e)
        {
            _formMain.Show();
        }
      
    }
}
