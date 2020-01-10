namespace CalDebugTools
{
    partial class ProjectManage
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
            this.components = new System.ComponentModel.Container();
            this.btn_Add = new System.Windows.Forms.Button();
            this.txt_helper = new System.Windows.Forms.TextBox();
            this.txt_s = new System.Windows.Forms.TextBox();
            this.txt_m = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_jcxmbh = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtdatafiled = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_y = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.projectInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectInfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(279, 74);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 19;
            this.btn_Add.Text = "添加";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // txt_helper
            // 
            this.txt_helper.Location = new System.Drawing.Point(447, 30);
            this.txt_helper.Name = "txt_helper";
            this.txt_helper.Size = new System.Drawing.Size(98, 21);
            this.txt_helper.TabIndex = 17;
            // 
            // txt_s
            // 
            this.txt_s.Location = new System.Drawing.Point(324, 30);
            this.txt_s.Name = "txt_s";
            this.txt_s.Size = new System.Drawing.Size(58, 21);
            this.txt_s.TabIndex = 16;
            // 
            // txt_m
            // 
            this.txt_m.Location = new System.Drawing.Point(208, 30);
            this.txt_m.Name = "txt_m";
            this.txt_m.Size = new System.Drawing.Size(58, 21);
            this.txt_m.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(388, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "帮助表：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(277, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "从表：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "主表：";
            // 
            // txt_jcxmbh
            // 
            this.txt_jcxmbh.Location = new System.Drawing.Point(92, 31);
            this.txt_jcxmbh.Name = "txt_jcxmbh";
            this.txt_jcxmbh.Size = new System.Drawing.Size(63, 21);
            this.txt_jcxmbh.TabIndex = 11;
            this.txt_jcxmbh.MouseLeave += new System.EventHandler(this.txt_jcxmbh_MouseLeave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "项目编号：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtdatafiled);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txt_y);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btn_Add);
            this.panel1.Controls.Add(this.txt_jcxmbh);
            this.panel1.Controls.Add(this.txt_helper);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txt_s);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txt_m);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(937, 135);
            this.panel1.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(734, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(179, 12);
            this.label8.TabIndex = 25;
            this.label8.Text = "(多个数据表以 | 隔开，可不填)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(629, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "(说明：多个数据表以 | 隔开，字段以，隔开，从表和数据表中间以 ：隔开，从表和数据表字段一致可只填从表字段)";
            // 
            // txtdatafiled
            // 
            this.txtdatafiled.Location = new System.Drawing.Point(133, 74);
            this.txtdatafiled.Name = "txtdatafiled";
            this.txtdatafiled.Size = new System.Drawing.Size(126, 21);
            this.txtdatafiled.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "数据表查询字段：";
            // 
            // txt_y
            // 
            this.txt_y.Location = new System.Drawing.Point(628, 30);
            this.txt_y.Name = "txt_y";
            this.txt_y.Size = new System.Drawing.Size(100, 21);
            this.txt_y.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(551, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "数据表(Y)：";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 135);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(937, 315);
            this.panel2.TabIndex = 21;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(937, 315);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // projectInfoBindingSource
            // 
            this.projectInfoBindingSource.DataMember = "ProjectInfo";
            // 
            // 
            // ProjectManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ProjectManage";
            this.Text = "项目管理";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProjectManage_FormClosed);
            this.Load += new System.EventHandler(this.ProjectManage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projectInfoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.TextBox txt_helper;
        private System.Windows.Forms.TextBox txt_s;
        private System.Windows.Forms.TextBox txt_m;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_jcxmbh;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource projectInfoBindingSource;
        private System.Windows.Forms.TextBox txt_y;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtdatafiled;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
    }
}