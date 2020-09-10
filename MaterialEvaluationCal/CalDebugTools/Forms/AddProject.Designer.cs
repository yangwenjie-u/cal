namespace CalDebugTools.Forms
{
    partial class AddProject
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
            this.btn_add = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_ProjectName = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.com_dataSource = new System.Windows.Forms.ComboBox();
            this.chk_addjcjt = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chk_addjcjg = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(237, 237);
            this.btn_add.Margin = new System.Windows.Forms.Padding(2);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(63, 26);
            this.btn_add.TabIndex = 0;
            this.btn_add.Text = "添加";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(198, 134);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "项目代号";
            // 
            // txt_ProjectName
            // 
            this.txt_ProjectName.Location = new System.Drawing.Point(264, 131);
            this.txt_ProjectName.Margin = new System.Windows.Forms.Padding(2);
            this.txt_ProjectName.Name = "txt_ProjectName";
            this.txt_ProjectName.Size = new System.Drawing.Size(76, 21);
            this.txt_ProjectName.TabIndex = 2;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(176, 105);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 12);
            this.label15.TabIndex = 37;
            this.label15.Text = "检测机构选择";
            // 
            // com_dataSource
            // 
            this.com_dataSource.FormattingEnabled = true;
            this.com_dataSource.Location = new System.Drawing.Point(264, 98);
            this.com_dataSource.Margin = new System.Windows.Forms.Padding(2);
            this.com_dataSource.Name = "com_dataSource";
            this.com_dataSource.Size = new System.Drawing.Size(75, 20);
            this.com_dataSource.TabIndex = 39;
            // 
            // chk_addjcjt
            // 
            this.chk_addjcjt.AutoSize = true;
            this.chk_addjcjt.Checked = true;
            this.chk_addjcjt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_addjcjt.Location = new System.Drawing.Point(264, 170);
            this.chk_addjcjt.Name = "chk_addjcjt";
            this.chk_addjcjt.Size = new System.Drawing.Size(36, 16);
            this.chk_addjcjt.TabIndex = 40;
            this.chk_addjcjt.Text = "是";
            this.chk_addjcjt.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 41;
            this.label2.Text = "添加集团";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(200, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 43;
            this.label3.Text = "添加监管";
            // 
            // chk_addjcjg
            // 
            this.chk_addjcjg.AutoSize = true;
            this.chk_addjcjg.Location = new System.Drawing.Point(266, 200);
            this.chk_addjcjg.Name = "chk_addjcjg";
            this.chk_addjcjg.Size = new System.Drawing.Size(36, 16);
            this.chk_addjcjg.TabIndex = 42;
            this.chk_addjcjg.Text = "是";
            this.chk_addjcjg.UseVisualStyleBackColor = true;
            // 
            // AddProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 360);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chk_addjcjg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chk_addjcjt);
            this.Controls.Add(this.com_dataSource);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txt_ProjectName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_add);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AddProject";
            this.Text = "添加项目";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddProject_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_ProjectName;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox com_dataSource;
        private System.Windows.Forms.CheckBox chk_addjcjt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chk_addjcjg;
    }
}