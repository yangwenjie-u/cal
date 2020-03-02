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
    public partial class StringConvert : Form
    {
        FormMain _formMain;
        public StringConvert(FormMain main)
        {
            InitializeComponent();
            _formMain = main;
        }

        private void btn_convert_Click(object sender, EventArgs e)
        {
            string code = this.richTextBox1.Text;
            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show("请输入要转换的内容！");
                return;
            }

            Dictionary<string, string> dicReplaceStr = new Dictionary<string, string>();

            if (!this.checkBox1.Checked)
            {
                dicReplaceStr.Add("mrsmainTable.Fields(\"", "MItem[0][\"");
                dicReplaceStr.Add("mrsmainTable.Fields", "MItem[0][\"");
                dicReplaceStr.Add("mrsmainTable!", "MItem[0][\"");
            }
            else
            {
                dicReplaceStr.Add("mrsmainTable.Fields(\"", "mItem[\"");
                dicReplaceStr.Add("mrsmainTable.Fields", "mItem[\"");
                dicReplaceStr.Add("mrsmainTable!", "mItem[\"");
            }

            dicReplaceStr.Add("mrsDj.Fields(\"", "mrsDj[\"");
            dicReplaceStr.Add("mrssubTable!", "sItem[\"");

            dicReplaceStr.Add("mrssubTable.Fields(\"", "sItem[\"");
            dicReplaceStr.Add(".Fields(\"", "sItem[\"");
            dicReplaceStr.Add(".mrssubTable(\"", "sItem[\"");
            dicReplaceStr.Add("sitem[\"", "sItem[\"");
            dicReplaceStr.Add("mrsDj!", "mrsDj[\"");
            dicReplaceStr.Add("\") = \"", "\"]= \"");

            dicReplaceStr.Add(" = \"", "\"]= \"");
            dicReplaceStr.Add(" \"----\"", "\"----\";");
           // dicReplaceStr.Add(" \"), ", "\"],");
            dicReplaceStr.Add(" \") ", "\"]");
            dicReplaceStr.Add("\"),", "\"],");

          //  dicReplaceStr.Add(")", "\"]");

            dicReplaceStr.Add(")) =", "\"] = ");
            dicReplaceStr.Add(")),", "\"]),");
            dicReplaceStr.Add(") + \"、", "\"] +\"、");
            dicReplaceStr.Add(") +\"、", "\"] +\"、");
            dicReplaceStr.Add("Val", " Conversion.Val");

            //dicReplaceStr.Add("CDec(Val", " Conversion.Val");
            dicReplaceStr.Add(" = False", " = false;");
            dicReplaceStr.Add("False", "false");
            dicReplaceStr.Add("= True", "= true;");
            dicReplaceStr.Add("True", "true");
            dicReplaceStr.Add("&", "+");
            dicReplaceStr.Add("IF", "if(");
            dicReplaceStr.Add("If", "if(");
            dicReplaceStr.Add("Else", "else ");
            dicReplaceStr.Add("calc_PB", "IsQualified");
            dicReplaceStr.Add(" And ", " && ");
            dicReplaceStr.Add(" Or ", " || ");
            dicReplaceStr.Add("CDec", "Double.Parse");
            dicReplaceStr.Add("Then", "){");

            dicReplaceStr.Add("\") Like", "\"].Contains(");





            if (!string.IsNullOrEmpty(this.txt_box.Text))
            {
                List<string> lst = this.txt_box.Text.Split('|').ToList();

                List<string> listVal = new List<string>();
                foreach (var item in lst)
                {
                    listVal = item.Split(',').ToList();
                    if (listVal.Count != 2)
                    {
                        MessageBox.Show("err:自定义替换内容格式异常");
                        return;
                    }

                    if (!dicReplaceStr.Keys.Contains(listVal[0]))
                        dicReplaceStr.Add(listVal[0], listVal[1]);
                }
            }
            foreach (var item in dicReplaceStr)
            {
                code = code.Replace(item.Key, item.Value);
            }

            this.richTextBox2.Text = code;

        }

        private void StringConvert_FormClosing(object sender, FormClosingEventArgs e)
        {
            _formMain.Show();
        }
    }
}
