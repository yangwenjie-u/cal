﻿namespace CalDebugTools.Forms
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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(140, 58);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目代号：";
            // 
            // txt_xmbh
            // 
            this.txt_xmbh.Location = new System.Drawing.Point(235, 55);
            this.txt_xmbh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_xmbh.Name = "txt_xmbh";
            this.txt_xmbh.Size = new System.Drawing.Size(132, 25);
            this.txt_xmbh.TabIndex = 1;
            // 
            // txt_fieldMs
            // 
            this.txt_fieldMs.Location = new System.Drawing.Point(545, 126);
            this.txt_fieldMs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_fieldMs.Name = "txt_fieldMs";
            this.txt_fieldMs.Size = new System.Drawing.Size(132, 25);
            this.txt_fieldMs.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(451, 130);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "字段描述：";
            // 
            // txt_fieldName
            // 
            this.txt_fieldName.Location = new System.Drawing.Point(235, 119);
            this.txt_fieldName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_fieldName.Name = "txt_fieldName";
            this.txt_fieldName.Size = new System.Drawing.Size(132, 25);
            this.txt_fieldName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(155, 122);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "字段名：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(825, 116);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 29);
            this.button1.TabIndex = 6;
            this.button1.Text = "添加字段";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_fieldType
            // 
            this.txt_fieldType.Location = new System.Drawing.Point(235, 176);
            this.txt_fieldType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txt_fieldType.Name = "txt_fieldType";
            this.txt_fieldType.Size = new System.Drawing.Size(132, 25);
            this.txt_fieldType.TabIndex = 8;
            this.txt_fieldType.Text = "nvarchar(20)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(140, 180);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "字段类型：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(110, 240);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "从表字段数量：";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // txt_STabCount
            // 
            this.txt_STabCount.Location = new System.Drawing.Point(235, 237);
            this.txt_STabCount.Margin = new System.Windows.Forms.Padding(4);
            this.txt_STabCount.Name = "txt_STabCount";
            this.txt_STabCount.Size = new System.Drawing.Size(132, 25);
            this.txt_STabCount.TabIndex = 10;
            this.txt_STabCount.Text = "1";
            this.txt_STabCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_STabCount_KeyPress);
            // 
            // TargetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 562);
            this.Controls.Add(this.txt_STabCount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_fieldType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_fieldName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_fieldMs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_xmbh);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "TargetForm";
            this.Text = "TargetForm";
            this.Load += new System.EventHandler(this.TargetForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}