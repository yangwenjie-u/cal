namespace CalDebugTools.Forms
{
    partial class TestForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_bh = new System.Windows.Forms.TextBox();
            this.txt_bhpc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_start = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_count = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_tabName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(272, 272);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "添加";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(143, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "壁厚";
            // 
            // txt_bh
            // 
            this.txt_bh.Location = new System.Drawing.Point(213, 91);
            this.txt_bh.Name = "txt_bh";
            this.txt_bh.Size = new System.Drawing.Size(100, 25);
            this.txt_bh.TabIndex = 2;
            // 
            // txt_bhpc
            // 
            this.txt_bhpc.Location = new System.Drawing.Point(420, 91);
            this.txt_bhpc.Name = "txt_bhpc";
            this.txt_bhpc.Size = new System.Drawing.Size(100, 25);
            this.txt_bhpc.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(333, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "壁厚偏差";
            // 
            // txt_start
            // 
            this.txt_start.Location = new System.Drawing.Point(213, 142);
            this.txt_start.Name = "txt_start";
            this.txt_start.Size = new System.Drawing.Size(100, 25);
            this.txt_start.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(143, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "起始值";
            // 
            // txt_count
            // 
            this.txt_count.Location = new System.Drawing.Point(213, 192);
            this.txt_count.Name = "txt_count";
            this.txt_count.Size = new System.Drawing.Size(100, 25);
            this.txt_count.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(158, 202);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "个数";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(143, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "表名称";
            // 
            // txt_tabName
            // 
            this.txt_tabName.Location = new System.Drawing.Point(213, 32);
            this.txt_tabName.Name = "txt_tabName";
            this.txt_tabName.Size = new System.Drawing.Size(100, 25);
            this.txt_tabName.TabIndex = 10;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txt_tabName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_count);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_start);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_bhpc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_bh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TestForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_bh;
        private System.Windows.Forms.TextBox txt_bhpc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_start;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_count;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_tabName;
    }
}