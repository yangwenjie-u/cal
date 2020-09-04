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
            this.label14 = new System.Windows.Forms.Label();
            this.txtSerchFields = new System.Windows.Forms.TextBox();
            this.txt_ssjcx = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_SFieldeStartIndex = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Customize = new System.Windows.Forms.Button();
            this.txt_customize = new System.Windows.Forms.TextBox();
            this.btn_helper = new System.Windows.Forms.Button();
            this.btn_S_only = new System.Windows.Forms.Button();
            this.btn_M_only = new System.Windows.Forms.Button();
            this.chk_SFXS = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chk_syncJcJG = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_lx = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_bzCount = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_where = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radio_m = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.radio_s = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.txt_fieldMs.Location = new System.Drawing.Point(502, 32);
            this.txt_fieldMs.Name = "txt_fieldMs";
            this.txt_fieldMs.Size = new System.Drawing.Size(100, 21);
            this.txt_fieldMs.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(431, 35);
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
            this.button1.Location = new System.Drawing.Point(814, 85);
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
            this.btn_save.Location = new System.Drawing.Point(448, 131);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 12;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_load
            // 
            this.btn_load.Location = new System.Drawing.Point(339, 130);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(75, 23);
            this.btn_load.TabIndex = 14;
            this.btn_load.Text = "加载标准表";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.txtSerchFields);
            this.panel1.Controls.Add(this.txt_ssjcx);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.txt_SFieldeStartIndex);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.chk_SFXS);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.chk_syncJcJG);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.txt_lx);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txt_bzCount);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txt_where);
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
            this.panel1.Size = new System.Drawing.Size(1293, 191);
            this.panel1.TabIndex = 15;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(14, 158);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(101, 12);
            this.label14.TabIndex = 39;
            this.label14.Text = "自定义查询字段：";
            // 
            // txtSerchFields
            // 
            this.txtSerchFields.Location = new System.Drawing.Point(115, 155);
            this.txtSerchFields.Name = "txtSerchFields";
            this.txtSerchFields.Size = new System.Drawing.Size(546, 21);
            this.txtSerchFields.TabIndex = 40;
            // 
            // txt_ssjcx
            // 
            this.txt_ssjcx.Location = new System.Drawing.Point(502, 94);
            this.txt_ssjcx.Name = "txt_ssjcx";
            this.txt_ssjcx.Size = new System.Drawing.Size(159, 21);
            this.txt_ssjcx.TabIndex = 38;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(409, 96);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 12);
            this.label13.TabIndex = 37;
            this.label13.Text = "所属检测项：";
            // 
            // txt_SFieldeStartIndex
            // 
            this.txt_SFieldeStartIndex.Location = new System.Drawing.Point(351, 89);
            this.txt_SFieldeStartIndex.Name = "txt_SFieldeStartIndex";
            this.txt_SFieldeStartIndex.Size = new System.Drawing.Size(23, 21);
            this.txt_SFieldeStartIndex.TabIndex = 36;
            this.txt_SFieldeStartIndex.Text = "1";
            this.txt_SFieldeStartIndex.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_SFieldeStartIndex_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(257, 90);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 12);
            this.label12.TabIndex = 35;
            this.label12.Text = "字段初始序号：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_Customize);
            this.groupBox1.Controls.Add(this.txt_customize);
            this.groupBox1.Controls.Add(this.btn_helper);
            this.groupBox1.Controls.Add(this.btn_S_only);
            this.groupBox1.Controls.Add(this.btn_M_only);
            this.groupBox1.Location = new System.Drawing.Point(932, 16);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(159, 174);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "独立添加";
            // 
            // btn_Customize
            // 
            this.btn_Customize.Location = new System.Drawing.Point(30, 110);
            this.btn_Customize.Name = "btn_Customize";
            this.btn_Customize.Size = new System.Drawing.Size(75, 23);
            this.btn_Customize.TabIndex = 29;
            this.btn_Customize.Text = "添加自定义表字典";
            this.btn_Customize.UseVisualStyleBackColor = true;
            this.btn_Customize.Click += new System.EventHandler(this.btn_Customize_Click);
            // 
            // txt_customize
            // 
            this.txt_customize.Location = new System.Drawing.Point(30, 139);
            this.txt_customize.Margin = new System.Windows.Forms.Padding(2);
            this.txt_customize.Name = "txt_customize";
            this.txt_customize.Size = new System.Drawing.Size(76, 21);
            this.txt_customize.TabIndex = 28;
            // 
            // btn_helper
            // 
            this.btn_helper.Location = new System.Drawing.Point(30, 82);
            this.btn_helper.Name = "btn_helper";
            this.btn_helper.Size = new System.Drawing.Size(75, 23);
            this.btn_helper.TabIndex = 27;
            this.btn_helper.Text = "添加帮助表字典";
            this.btn_helper.UseVisualStyleBackColor = true;
            this.btn_helper.Click += new System.EventHandler(this.btn_helper_Click);
            // 
            // btn_S_only
            // 
            this.btn_S_only.Location = new System.Drawing.Point(30, 50);
            this.btn_S_only.Name = "btn_S_only";
            this.btn_S_only.Size = new System.Drawing.Size(75, 22);
            this.btn_S_only.TabIndex = 20;
            this.btn_S_only.Text = "添加从表字段";
            this.btn_S_only.UseVisualStyleBackColor = true;
            this.btn_S_only.Click += new System.EventHandler(this.btn_S_only_Click);
            // 
            // btn_M_only
            // 
            this.btn_M_only.Location = new System.Drawing.Point(30, 21);
            this.btn_M_only.Name = "btn_M_only";
            this.btn_M_only.Size = new System.Drawing.Size(75, 23);
            this.btn_M_only.TabIndex = 26;
            this.btn_M_only.Text = "添加主表字段";
            this.btn_M_only.UseVisualStyleBackColor = true;
            this.btn_M_only.Click += new System.EventHandler(this.btn_M_only_Click);
            // 
            // chk_SFXS
            // 
            this.chk_SFXS.AutoSize = true;
            this.chk_SFXS.Location = new System.Drawing.Point(760, 70);
            this.chk_SFXS.Margin = new System.Windows.Forms.Padding(2);
            this.chk_SFXS.Name = "chk_SFXS";
            this.chk_SFXS.Size = new System.Drawing.Size(48, 16);
            this.chk_SFXS.TabIndex = 33;
            this.chk_SFXS.Text = "显示";
            this.chk_SFXS.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(689, 70);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 32;
            this.label7.Text = "是否显示:";
            // 
            // chk_syncJcJG
            // 
            this.chk_syncJcJG.AutoSize = true;
            this.chk_syncJcJG.Location = new System.Drawing.Point(760, 106);
            this.chk_syncJcJG.Margin = new System.Windows.Forms.Padding(2);
            this.chk_syncJcJG.Name = "chk_syncJcJG";
            this.chk_syncJcJG.Size = new System.Drawing.Size(48, 16);
            this.chk_syncJcJG.TabIndex = 30;
            this.chk_syncJcJG.Text = "同步";
            this.chk_syncJcJG.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(689, 106);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 12);
            this.label11.TabIndex = 29;
            this.label11.Text = "同步监管:";
            // 
            // txt_lx
            // 
            this.txt_lx.Location = new System.Drawing.Point(612, 66);
            this.txt_lx.Margin = new System.Windows.Forms.Padding(2);
            this.txt_lx.Name = "txt_lx";
            this.txt_lx.Size = new System.Drawing.Size(49, 21);
            this.txt_lx.TabIndex = 28;
            this.txt_lx.Text = "S";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(551, 70);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 27;
            this.label10.Text = "字段类型:";
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
            this.label8.Location = new System.Drawing.Point(26, 137);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 22;
            this.label8.Text = "标准表查询条件";
            // 
            // txt_where
            // 
            this.txt_where.Location = new System.Drawing.Point(117, 130);
            this.txt_where.Multiline = true;
            this.txt_where.Name = "txt_where";
            this.txt_where.Size = new System.Drawing.Size(199, 21);
            this.txt_where.TabIndex = 23;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radio_m);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.radio_s);
            this.panel2.Location = new System.Drawing.Point(625, 20);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(265, 40);
            this.panel2.TabIndex = 19;
            // 
            // radio_m
            // 
            this.radio_m.AutoSize = true;
            this.radio_m.Location = new System.Drawing.Point(110, 12);
            this.radio_m.Name = "radio_m";
            this.radio_m.Size = new System.Drawing.Size(47, 16);
            this.radio_m.TabIndex = 15;
            this.radio_m.Text = "主表";
            this.radio_m.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "判定字段(HG)保存";
            // 
            // radio_s
            // 
            this.radio_s.AutoSize = true;
            this.radio_s.Checked = true;
            this.radio_s.Location = new System.Drawing.Point(169, 11);
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 191);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.1737F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.8263F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1293, 403);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToOrderColumns = true;
            this.dataGridView2.ColumnHeadersHeight = 29;
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
            this.dataGridView1.ColumnHeadersHeight = 29;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 44);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 40;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1287, 356);
            this.dataGridView1.TabIndex = 11;
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.TextBox txt_bzCount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btn_M_only;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_lx;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chk_syncJcJG;
        private System.Windows.Forms.Button btn_helper;
        private System.Windows.Forms.CheckBox chk_SFXS;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_SFieldeStartIndex;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_ssjcx;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtSerchFields;
        private System.Windows.Forms.Button btn_Customize;
        private System.Windows.Forms.TextBox txt_customize;
    }
}