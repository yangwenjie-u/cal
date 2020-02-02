namespace CalDebugTools.Forms
{
    partial class ZDZDTableSync
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
            this.btn_ok = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_tableName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(257, 223);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 0;
            this.btn_ok.Text = "同步";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(184, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "表名称：";
            // 
            // txt_tableName
            // 
            this.txt_tableName.Location = new System.Drawing.Point(257, 112);
            this.txt_tableName.Name = "txt_tableName";
            this.txt_tableName.Size = new System.Drawing.Size(344, 25);
            this.txt_tableName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(427, 159);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(204, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "（默认同步全部zdzd表信息）";
            // 
            // ZDZDTableSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_tableName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_ok);
            this.Name = "ZDZDTableSync";
            this.Text = "ZDZDTableSync";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ZDZDTableSync_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_tableName;
        private System.Windows.Forms.Label label2;
    }
}