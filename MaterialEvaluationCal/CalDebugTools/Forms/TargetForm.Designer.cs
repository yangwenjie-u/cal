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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(105, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目代号：";
            // 
            // txt_xmbh
            // 
            this.txt_xmbh.Location = new System.Drawing.Point(176, 44);
            this.txt_xmbh.Name = "txt_xmbh";
            this.txt_xmbh.Size = new System.Drawing.Size(100, 21);
            this.txt_xmbh.TabIndex = 1;
            // 
            // txt_fieldMs
            // 
            this.txt_fieldMs.Location = new System.Drawing.Point(409, 101);
            this.txt_fieldMs.Name = "txt_fieldMs";
            this.txt_fieldMs.Size = new System.Drawing.Size(100, 21);
            this.txt_fieldMs.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(338, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "字段描述：";
            // 
            // txt_fieldName
            // 
            this.txt_fieldName.Location = new System.Drawing.Point(176, 95);
            this.txt_fieldName.Name = "txt_fieldName";
            this.txt_fieldName.Size = new System.Drawing.Size(100, 21);
            this.txt_fieldName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(105, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "字段名：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(619, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "添加字段";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_fieldType
            // 
            this.txt_fieldType.Location = new System.Drawing.Point(176, 141);
            this.txt_fieldType.Name = "txt_fieldType";
            this.txt_fieldType.Size = new System.Drawing.Size(100, 21);
            this.txt_fieldType.TabIndex = 8;
            this.txt_fieldType.Text = "nvarchar(20)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(105, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "字段类型：";
            // 
            // TargetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txt_fieldType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_fieldName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_fieldMs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_xmbh);
            this.Controls.Add(this.label1);
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
    }
}