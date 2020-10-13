namespace CalDebugTools
{
    partial class CodeUpload
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtsylb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtusername = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtremark = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtextratable = new System.Windows.Forms.TextBox();
            this.txtcode = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ck_defaultLib = new System.Windows.Forms.CheckBox();
            this.lab1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_help_json = new System.Windows.Forms.RichTextBox();
            this.txt_zdzd_json = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "试验类别：";
            // 
            // txtsylb
            // 
            this.txtsylb.Location = new System.Drawing.Point(96, 10);
            this.txtsylb.Name = "txtsylb";
            this.txtsylb.Size = new System.Drawing.Size(325, 21);
            this.txtsylb.TabIndex = 2;
            this.txtsylb.MouseLeave += new System.EventHandler(this.txtsylb_MouseLeave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "计算代码：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 300);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "帮助表：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(426, 300);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(137, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "（多个表名以逗号隔开）";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 327);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "用户名：";
            this.label8.Visible = false;
            // 
            // txtusername
            // 
            this.txtusername.Location = new System.Drawing.Point(96, 324);
            this.txtusername.Name = "txtusername";
            this.txtusername.Size = new System.Drawing.Size(325, 21);
            this.txtusername.TabIndex = 12;
            this.txtusername.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 354);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 13;
            this.label9.Text = "备注：";
            this.label9.Visible = false;
            // 
            // txtremark
            // 
            this.txtremark.Location = new System.Drawing.Point(96, 351);
            this.txtremark.Name = "txtremark";
            this.txtremark.Size = new System.Drawing.Size(325, 21);
            this.txtremark.TabIndex = 14;
            this.txtremark.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(143, 411);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "生成json数据";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtextratable
            // 
            this.txtextratable.Location = new System.Drawing.Point(96, 297);
            this.txtextratable.Name = "txtextratable";
            this.txtextratable.Size = new System.Drawing.Size(324, 21);
            this.txtextratable.TabIndex = 9;
            this.txtextratable.TextChanged += new System.EventHandler(this.txtextratable_TextChanged);
            // 
            // txtcode
            // 
            this.txtcode.Location = new System.Drawing.Point(96, 70);
            this.txtcode.Margin = new System.Windows.Forms.Padding(2);
            this.txtcode.Name = "txtcode";
            this.txtcode.Size = new System.Drawing.Size(325, 208);
            this.txtcode.TabIndex = 16;
            this.txtcode.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 384);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "设置默认版本：";
            this.label3.Visible = false;
            // 
            // ck_defaultLib
            // 
            this.ck_defaultLib.AutoSize = true;
            this.ck_defaultLib.Checked = true;
            this.ck_defaultLib.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck_defaultLib.Location = new System.Drawing.Point(96, 384);
            this.ck_defaultLib.Margin = new System.Windows.Forms.Padding(2);
            this.ck_defaultLib.Name = "ck_defaultLib";
            this.ck_defaultLib.Size = new System.Drawing.Size(36, 16);
            this.ck_defaultLib.TabIndex = 18;
            this.ck_defaultLib.Text = "是";
            this.ck_defaultLib.UseVisualStyleBackColor = true;
            this.ck_defaultLib.Visible = false;
            this.ck_defaultLib.CheckedChanged += new System.EventHandler(this.ck_defaultLib_CheckedChanged);
            // 
            // lab1
            // 
            this.lab1.AutoSize = true;
            this.lab1.Location = new System.Drawing.Point(487, 99);
            this.lab1.Name = "lab1";
            this.lab1.Size = new System.Drawing.Size(53, 12);
            this.lab1.TabIndex = 20;
            this.lab1.Text = "字段json";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(487, 209);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 21;
            this.label4.Text = "帮助表json";
            // 
            // txt_help_json
            // 
            this.txt_help_json.Location = new System.Drawing.Point(558, 187);
            this.txt_help_json.Margin = new System.Windows.Forms.Padding(2);
            this.txt_help_json.Name = "txt_help_json";
            this.txt_help_json.Size = new System.Drawing.Size(219, 91);
            this.txt_help_json.TabIndex = 23;
            this.txt_help_json.Text = "";
            // 
            // txt_zdzd_json
            // 
            this.txt_zdzd_json.Location = new System.Drawing.Point(558, 69);
            this.txt_zdzd_json.Margin = new System.Windows.Forms.Padding(2);
            this.txt_zdzd_json.Name = "txt_zdzd_json";
            this.txt_zdzd_json.Size = new System.Drawing.Size(219, 91);
            this.txt_zdzd_json.TabIndex = 24;
            this.txt_zdzd_json.Text = "";
            // 
            // CodeUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 479);
            this.Controls.Add(this.txt_zdzd_json);
            this.Controls.Add(this.txt_help_json);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lab1);
            this.Controls.Add(this.ck_defaultLib);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtcode);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtremark);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtusername);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtextratable);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtsylb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CodeUpload";
            this.Text = "代码上传";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CodeUpload_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtsylb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtusername;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtremark;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtextratable;
        private System.Windows.Forms.RichTextBox txtcode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ck_defaultLib;
        private System.Windows.Forms.Label lab1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox txt_help_json;
        private System.Windows.Forms.RichTextBox txt_zdzd_json;
    }
}