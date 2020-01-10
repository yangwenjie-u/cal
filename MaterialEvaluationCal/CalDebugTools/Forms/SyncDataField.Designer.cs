namespace CalDebugTools.Forms
{
    partial class SyncDataField
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
            this.btn_OK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_tableName = new System.Windows.Forms.TextBox();
            this.txt_zdzd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(308, 240);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 0;
            this.btn_OK.Text = "确定";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(251, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "表名称：";
            // 
            // txt_tableName
            // 
            this.txt_tableName.Location = new System.Drawing.Point(298, 142);
            this.txt_tableName.Name = "txt_tableName";
            this.txt_tableName.Size = new System.Drawing.Size(100, 21);
            this.txt_tableName.TabIndex = 2;
            // 
            // txt_zdzd
            // 
            this.txt_zdzd.Location = new System.Drawing.Point(298, 188);
            this.txt_zdzd.Name = "txt_zdzd";
            this.txt_zdzd.Size = new System.Drawing.Size(100, 21);
            this.txt_zdzd.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 191);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "ZDZD表：";
            // 
            // SyncDataField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txt_zdzd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_tableName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_OK);
            this.Name = "SyncDataField";
            this.Text = "SyncDataField";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SyncDataField_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_tableName;
        private System.Windows.Forms.TextBox txt_zdzd;
        private System.Windows.Forms.Label label2;
    }
}