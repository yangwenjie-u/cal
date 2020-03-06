namespace CalDebugTools.Forms
{
    partial class TargetForm
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
            this.txt_xmbh = new System.Windows.Forms.TextBox();
            this.txt_fieldMs = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_fieldName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txt_fieldType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_STabCount = new System.Windows.Forms.TextBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_load = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_bzCount = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_where = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_S_only = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radio_m = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.radio_s = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_M_only = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目编号：";
            // 
            // txt_xmbh
            // 
            this.txt_xmbh.Location = new System.Drawing.Point(118, 33);
            this.txt_xmbh.Name = "txt_xmbh";
            this.txt_xmbh.Size = new System.Drawing.Size(100, 21);
            this.txt_xmbh.TabIndex = 1;
            // 
            // txt_fieldMs
            // 
            this.txt_fieldMs.Location = new System.Drawing.Point(550, 33);
            this.txt_fieldMs.Name = "txt_fieldMs";
            this.txt_fieldMs.Size = new System.Drawing.Size(100, 21);
            this.txt_fieldMs.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(479, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "字段描述：";
            // 
            // txt_fieldName
            // 
            this.txt_fieldName.Location = new System.Drawing.Point(317, 32);
            this.txt_fieldName.Name = "txt_fieldName";
            this.txt_fieldName.Size = new System.Drawing.Size(100, 21);
            this.txt_fieldName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(257, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "字段名：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(575, 69);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "添加字段";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_fieldType
            // 
            this.txt_fieldType.Location = new System.Drawing.Point(119, 66);
            this.txt_fieldType.Name = "txt_fieldType";
            this.txt_fieldType.Size = new System.Drawing.Size(100, 21);
            this.txt_fieldType.TabIndex = 8;
            this.txt_fieldType.Text = "nvarchar(20)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "字段类型：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(257, 69);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "从表字段数量：";
            // 
            // txt_STabCount
            // 
            this.txt_STabCount.Location = new System.Drawing.Point(351, 67);
            this.txt_STabCount.Name = "txt_STabCount";
            this.txt_STabCount.Size = new System.Drawing.Size(23, 21);
            this.txt_STabCount.TabIndex = 10;
            this.txt_STabCount.Text = "1";
            this.txt_STabCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_STabCount_KeyPress);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(1161, 101);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 12;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_load
            // 
            this.btn_load.Location = new System.Drawing.Point(535, 107);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(75, 23);
            this.btn_load.TabIndex = 14;
            this.btn_load.Text = "加载标准表";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_M_only);
            this.panel1.Controls.Add(this.txt_bzCount);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txt_where);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.btn_S_only);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_load);
            this.panel1.Controls.Add(this.txt_xmbh);
            this.panel1.Controls.Add(this.btn_save);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txt_fieldMs);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txt_STabCount);
            this.panel1.Controls.Add(this.txt_fieldName);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.txt_fieldType);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1293, 141);
            this.panel1.TabIndex = 15;
            // 
            // txt_bzCount
            // 
            this.txt_bzCount.Location = new System.Drawing.Point(503, 68);
            this.txt_bzCount.Name = "txt_bzCount";
            this.txt_bzCount.Size = new System.Drawing.Size(23, 21);
            this.txt_bzCount.TabIndex = 25;
            this.txt_bzCount.Text = "1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(409, 70);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 12);
            this.label9.TabIndex = 24;
            this.label9.Text = "标准字段数量：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(48, 112);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 22;
            this.label8.Text = "标准表查询条件";
            // 
            // txt_where
            // 
            this.txt_where.Location = new System.Drawing.Point(143, 109);
            this.txt_where.Multiline = true;
            this.txt_where.Name = "txt_where";
            this.txt_where.Size = new System.Drawing.Size(320, 21);
            this.txt_where.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(716, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(203, 12);
            this.label7.TabIndex = 21;
            this.label7.Text = "仅添加从表字段（不添加hg，G字段）";
            // 
            // btn_S_only
            // 
            this.btn_S_only.Location = new System.Drawing.Point(925, 80);
            this.btn_S_only.Name = "btn_S_only";
            this.btn_S_only.Size = new System.Drawing.Size(75, 23);
            this.btn_S_only.TabIndex = 20;
            this.btn_S_only.Text = "添加从表字段";
            this.btn_S_only.UseVisualStyleBackColor = true;
            this.btn_S_only.Click += new System.EventHandler(this.btn_S_only_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radio_m);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.radio_s);
            this.panel2.Location = new System.Drawing.Point(711, 32);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(263, 33);
            this.panel2.TabIndex = 19;
            // 
            // radio_m
            // 
            this.radio_m.AutoSize = true;
            this.radio_m.Location = new System.Drawing.Point(92, 5);
            this.radio_m.Name = "radio_m";
            this.radio_m.Size = new System.Drawing.Size(47, 16);
            this.radio_m.TabIndex = 15;
            this.radio_m.Text = "主表";
            this.radio_m.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "添加字段到";
            // 
            // radio_s
            // 
            this.radio_s.AutoSize = true;
            this.radio_s.Checked = true;
            this.radio_s.Location = new System.Drawing.Point(176, 5);
            this.radio_s.Name = "radio_s";
            this.radio_s.Size = new System.Drawing.Size(47, 16);
            this.radio_s.TabIndex = 16;
            this.radio_s.TabStop = true;
            this.radio_s.Text = "从表";
            this.radio_s.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 141);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.061489F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.93851F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1293, 453);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(3, 3);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 40;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(1287, 35);
            this.dataGridView2.TabIndex = 13;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 44);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 40;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1287, 406);
            this.dataGridView1.TabIndex = 11;
            // 
            // btn_M_only
            // 
            this.btn_M_only.Location = new System.Drawing.Point(775, 112);
            this.btn_M_only.Name = "btn_M_only";
            this.btn_M_only.Size = new System.Drawing.Size(75, 23);
            this.btn_M_only.TabIndex = 26;
            this.btn_M_only.Text = "添加主表字段";
            this.btn_M_only.UseVisualStyleBackColor = true;
            this.btn_M_only.Click += new System.EventHandler(this.btn_M_only_Click);
            // 
            // TargetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1293, 594);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Name = "TargetForm";
            this.Text = "TargetForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TargetForm_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_xmbh;
        private System.Windows.Forms.TextBox txt_fieldMs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_fieldName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt_fieldType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_STabCount;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton radio_s;
        private System.Windows.Forms.RadioButton radio_m;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_S_only;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_where;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_bzCount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btn_M_only;
    }
}