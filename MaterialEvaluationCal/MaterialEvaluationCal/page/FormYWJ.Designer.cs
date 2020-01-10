namespace MaterialEvaluationCal.page
{
    partial class FormYWJ
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
            this.btn_GHF = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_GHF
            // 
            this.btn_GHF.Location = new System.Drawing.Point(161, 76);
            this.btn_GHF.Name = "btn_GHF";
            this.btn_GHF.Size = new System.Drawing.Size(75, 23);
            this.btn_GHF.TabIndex = 0;
            this.btn_GHF.Text = "钢筋焊接复试";
            this.btn_GHF.UseVisualStyleBackColor = true;
            this.btn_GHF.Click += new System.EventHandler(this.btn_GHF_Click);
            // 
            // FormYWJ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_GHF);
            this.Name = "FormYWJ";
            this.Text = "FormYWJ";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_GHF;
    }
}