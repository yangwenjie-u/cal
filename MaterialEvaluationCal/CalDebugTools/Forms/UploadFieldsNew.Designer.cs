namespace CalDebugTools.Forms
{
    partial class UploadFieldsNew
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtother = new System.Windows.Forms.TextBox();
            this.rd_other = new System.Windows.Forms.RadioButton();
            this.btn_sava = new System.Windows.Forms.Button();
            this.rd_s = new System.Windows.Forms.RadioButton();
            this.rd_m = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_xmbh = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.data_result = new System.Windows.Forms.DataGridView();
            this.btn_ok = new System.Windows.Forms.Button();
            this.txt_result = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_code = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_result)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtother);
            this.panel1.Controls.Add(this.rd_other);
            this.panel1.Controls.Add(this.btn_sava);
            this.panel1.Controls.Add(this.rd_s);
            this.panel1.Controls.Add(this.rd_m);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txt_xmbh);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.data_result);
            this.panel1.Controls.Add(this.btn_ok);
            this.panel1.Controls.Add(this.txt_result);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txt_code);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(940, 523);
            this.panel1.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(675, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(173, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "其他格式:表名称多个以 | 隔开";
            // 
            // txtother
            // 
            this.txtother.Location = new System.Drawing.Point(814, 90);
            this.txtother.Name = "txtother";
            this.txtother.Size = new System.Drawing.Size(100, 21);
            this.txtother.TabIndex = 16;
            this.txtother.Visible = false;
            // 
            // rd_other
            // 
            this.rd_other.AutoSize = true;
            this.rd_other.Location = new System.Drawing.Point(761, 91);
            this.rd_other.Name = "rd_other";
            this.rd_other.Size = new System.Drawing.Size(47, 16);
            this.rd_other.TabIndex = 15;
            this.rd_other.TabStop = true;
            this.rd_other.Text = "其它";
            this.rd_other.UseVisualStyleBackColor = true;
            this.rd_other.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // btn_sava
            // 
            this.btn_sava.Location = new System.Drawing.Point(721, 183);
            this.btn_sava.Margin = new System.Windows.Forms.Padding(2);
            this.btn_sava.Name = "btn_sava";
            this.btn_sava.Size = new System.Drawing.Size(56, 27);
            this.btn_sava.TabIndex = 13;
            this.btn_sava.Text = "保存";
            this.btn_sava.UseVisualStyleBackColor = true;
            this.btn_sava.Click += new System.EventHandler(this.btn_sava_Click);
            // 
            // rd_s
            // 
            this.rd_s.AutoSize = true;
            this.rd_s.Location = new System.Drawing.Point(709, 91);
            this.rd_s.Margin = new System.Windows.Forms.Padding(2);
            this.rd_s.Name = "rd_s";
            this.rd_s.Size = new System.Drawing.Size(47, 16);
            this.rd_s.TabIndex = 12;
            this.rd_s.TabStop = true;
            this.rd_s.Text = "从表";
            this.rd_s.UseVisualStyleBackColor = true;
            this.rd_s.Click += new System.EventHandler(this.rd_s_Click);
            // 
            // rd_m
            // 
            this.rd_m.AutoSize = true;
            this.rd_m.Checked = true;
            this.rd_m.Location = new System.Drawing.Point(658, 92);
            this.rd_m.Margin = new System.Windows.Forms.Padding(2);
            this.rd_m.Name = "rd_m";
            this.rd_m.Size = new System.Drawing.Size(47, 16);
            this.rd_m.TabIndex = 11;
            this.rd_m.TabStop = true;
            this.rd_m.Text = "主表";
            this.rd_m.UseVisualStyleBackColor = true;
            this.rd_m.CheckedChanged += new System.EventHandler(this.rd_i_CheckedChanged);
            this.rd_m.Click += new System.EventHandler(this.rd_m_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(603, 95);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "字段来源";
            // 
            // txt_xmbh
            // 
            this.txt_xmbh.Location = new System.Drawing.Point(658, 44);
            this.txt_xmbh.Margin = new System.Windows.Forms.Padding(2);
            this.txt_xmbh.Name = "txt_xmbh";
            this.txt_xmbh.Size = new System.Drawing.Size(76, 21);
            this.txt_xmbh.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(603, 46);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "项目编号";
            // 
            // data_result
            // 
            this.data_result.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data_result.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.data_result.Location = new System.Drawing.Point(0, 238);
            this.data_result.Margin = new System.Windows.Forms.Padding(2);
            this.data_result.Name = "data_result";
            this.data_result.RowTemplate.Height = 27;
            this.data_result.Size = new System.Drawing.Size(940, 285);
            this.data_result.TabIndex = 5;
            this.data_result.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.data_result_CellEndEdit);
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(597, 183);
            this.btn_ok.Margin = new System.Windows.Forms.Padding(2);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(56, 27);
            this.btn_ok.TabIndex = 4;
            this.btn_ok.Text = "生成";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // txt_result
            // 
            this.txt_result.Location = new System.Drawing.Point(92, 138);
            this.txt_result.Margin = new System.Windows.Forms.Padding(2);
            this.txt_result.MaxLength = 999999999;
            this.txt_result.Multiline = true;
            this.txt_result.Name = "txt_result";
            this.txt_result.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_result.Size = new System.Drawing.Size(449, 81);
            this.txt_result.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 140);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "生成字段：";
            // 
            // txt_code
            // 
            this.txt_code.Location = new System.Drawing.Point(92, 10);
            this.txt_code.Margin = new System.Windows.Forms.Padding(2);
            this.txt_code.MaxLength = 999999999;
            this.txt_code.Multiline = true;
            this.txt_code.Name = "txt_code";
            this.txt_code.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_code.Size = new System.Drawing.Size(449, 98);
            this.txt_code.TabIndex = 1;
            this.txt_code.TextChanged += new System.EventHandler(this.txt_code_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "代码：";
            // 
            // UploadFields
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 523);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UploadFields";
            this.Text = "UploadFields";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UploadFields_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_result)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_result;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_code;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.DataGridView data_result;
        private System.Windows.Forms.TextBox txt_xmbh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rd_s;
        private System.Windows.Forms.RadioButton rd_m;
        private System.Windows.Forms.Button btn_sava;
        private System.Windows.Forms.RadioButton rd_other;
        private System.Windows.Forms.TextBox txtother;
        private System.Windows.Forms.Label label5;
    }
}