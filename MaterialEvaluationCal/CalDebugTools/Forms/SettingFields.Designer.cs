namespace CalDebugTools.Forms
{
    partial class SettingFields
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tab_xmbh = new System.Windows.Forms.Label();
            this.txt_xmbh = new System.Windows.Forms.TextBox();
            this.txt_jcxm = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_localT = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_localF = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_remoteF = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_remoteT = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_submit = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_submit);
            this.panel1.Controls.Add(this.txt_remoteF);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txt_remoteT);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txt_localF);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txt_localT);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txt_jcxm);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txt_xmbh);
            this.panel1.Controls.Add(this.tab_xmbh);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 100);
            this.panel1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 100);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(984, 361);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // tab_xmbh
            // 
            this.tab_xmbh.AutoSize = true;
            this.tab_xmbh.Location = new System.Drawing.Point(38, 25);
            this.tab_xmbh.Name = "tab_xmbh";
            this.tab_xmbh.Size = new System.Drawing.Size(59, 12);
            this.tab_xmbh.TabIndex = 0;
            this.tab_xmbh.Text = "项目编号:";
            // 
            // txt_xmbh
            // 
            this.txt_xmbh.Location = new System.Drawing.Point(107, 22);
            this.txt_xmbh.Name = "txt_xmbh";
            this.txt_xmbh.Size = new System.Drawing.Size(69, 21);
            this.txt_xmbh.TabIndex = 1;
            // 
            // txt_jcxm
            // 
            this.txt_jcxm.Location = new System.Drawing.Point(284, 22);
            this.txt_jcxm.Name = "txt_jcxm";
            this.txt_jcxm.Size = new System.Drawing.Size(69, 21);
            this.txt_jcxm.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(215, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "检测项目:";
            // 
            // txt_localT
            // 
            this.txt_localT.Location = new System.Drawing.Point(433, 22);
            this.txt_localT.Name = "txt_localT";
            this.txt_localT.Size = new System.Drawing.Size(69, 21);
            this.txt_localT.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(380, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "关联表:";
            // 
            // txt_localF
            // 
            this.txt_localF.Location = new System.Drawing.Point(626, 22);
            this.txt_localF.Name = "txt_localF";
            this.txt_localF.Size = new System.Drawing.Size(69, 21);
            this.txt_localF.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(549, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "关联字段:";
            // 
            // txt_remoteF
            // 
            this.txt_remoteF.Location = new System.Drawing.Point(284, 62);
            this.txt_remoteF.Name = "txt_remoteF";
            this.txt_remoteF.Size = new System.Drawing.Size(69, 21);
            this.txt_remoteF.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(207, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "被关联字段:";
            // 
            // txt_remoteT
            // 
            this.txt_remoteT.Location = new System.Drawing.Point(103, 62);
            this.txt_remoteT.Name = "txt_remoteT";
            this.txt_remoteT.Size = new System.Drawing.Size(69, 21);
            this.txt_remoteT.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "被关联表:";
            // 
            // btn_submit
            // 
            this.btn_submit.Location = new System.Drawing.Point(433, 59);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(75, 23);
            this.btn_submit.TabIndex = 12;
            this.btn_submit.Text = "提交";
            this.btn_submit.UseVisualStyleBackColor = true;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // SettingFields
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "SettingFields";
            this.Text = "SettingFields";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingFields_FormClosed);
            this.Load += new System.EventHandler(this.SettingFields_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txt_jcxm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_xmbh;
        private System.Windows.Forms.Label tab_xmbh;
        private System.Windows.Forms.TextBox txt_remoteF;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_remoteT;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_localF;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_localT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_submit;
    }
}