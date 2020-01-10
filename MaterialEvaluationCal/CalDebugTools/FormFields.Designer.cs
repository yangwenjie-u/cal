namespace CalDebugTools
{
    partial class FormFields
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
            this.btn_add = new System.Windows.Forms.Button();
            this.txt_field = new System.Windows.Forms.TextBox();
            this.txt_tableName = new System.Windows.Forms.TextBox();
            this.txt_jcxm = new System.Windows.Forms.TextBox();
            this.rd_o = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.rd_i = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_xmbh = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_add);
            this.panel1.Controls.Add(this.txt_field);
            this.panel1.Controls.Add(this.txt_tableName);
            this.panel1.Controls.Add(this.txt_jcxm);
            this.panel1.Controls.Add(this.rd_o);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.rd_i);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txt_xmbh);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1067, 138);
            this.panel1.TabIndex = 0;
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(804, 88);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(75, 29);
            this.btn_add.TabIndex = 11;
            this.btn_add.Text = "新增";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // txt_field
            // 
            this.txt_field.Location = new System.Drawing.Point(122, 88);
            this.txt_field.Name = "txt_field";
            this.txt_field.Size = new System.Drawing.Size(90, 25);
            this.txt_field.TabIndex = 10;
            // 
            // txt_tableName
            // 
            this.txt_tableName.Location = new System.Drawing.Point(503, 34);
            this.txt_tableName.Name = "txt_tableName";
            this.txt_tableName.Size = new System.Drawing.Size(90, 25);
            this.txt_tableName.TabIndex = 9;
            // 
            // txt_jcxm
            // 
            this.txt_jcxm.Location = new System.Drawing.Point(309, 34);
            this.txt_jcxm.Name = "txt_jcxm";
            this.txt_jcxm.Size = new System.Drawing.Size(90, 25);
            this.txt_jcxm.TabIndex = 8;
            // 
            // rd_o
            // 
            this.rd_o.AutoSize = true;
            this.rd_o.Location = new System.Drawing.Point(373, 92);
            this.rd_o.Name = "rd_o";
            this.rd_o.Size = new System.Drawing.Size(58, 19);
            this.rd_o.TabIndex = 7;
            this.rd_o.Text = "计算";
            this.rd_o.UseVisualStyleBackColor = true;
            this.rd_o.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(249, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "类型：";
            // 
            // rd_i
            // 
            this.rd_i.AutoSize = true;
            this.rd_i.Checked = true;
            this.rd_i.Location = new System.Drawing.Point(309, 92);
            this.rd_i.Name = "rd_i";
            this.rd_i.Size = new System.Drawing.Size(58, 19);
            this.rd_i.TabIndex = 5;
            this.rd_i.TabStop = true;
            this.rd_i.Text = "输出";
            this.rd_i.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "字段名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(233, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "检测项目：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(443, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "表名称：";
            // 
            // txt_xmbh
            // 
            this.txt_xmbh.Location = new System.Drawing.Point(122, 34);
            this.txt_xmbh.Name = "txt_xmbh";
            this.txt_xmbh.Size = new System.Drawing.Size(90, 25);
            this.txt_xmbh.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目编号：";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 138);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1067, 424);
            this.panel2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(1067, 424);
            this.dataGridView1.TabIndex = 0;
            // 
            // FormFields
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 562);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormFields";
            this.Text = "字段管理";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormFields_FormClosed);
            this.Load += new System.EventHandler(this.FormFields_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rd_o;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rd_i;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_xmbh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_field;
        private System.Windows.Forms.TextBox txt_tableName;
        private System.Windows.Forms.TextBox txt_jcxm;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}