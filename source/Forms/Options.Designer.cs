namespace ChessCalendar.Forms
{
    partial class Options
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.chkBeep = new System.Windows.Forms.CheckBox();
            this.chkDownloadPGNs = new System.Windows.Forms.CheckBox();
            this.chkLogFormOpen = new System.Windows.Forms.CheckBox();
            this.chkDebugMode = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkBeep
            // 
            this.chkBeep.AutoSize = true;
            this.chkBeep.Location = new System.Drawing.Point(12, 12);
            this.chkBeep.Name = "chkBeep";
            this.chkBeep.Size = new System.Drawing.Size(162, 17);
            this.chkBeep.TabIndex = 7;
            this.chkBeep.Text = "Beep when new move found";
            this.chkBeep.UseVisualStyleBackColor = true;
            // 
            // chkDownloadPGNs
            // 
            this.chkDownloadPGNs.AutoSize = true;
            this.chkDownloadPGNs.Location = new System.Drawing.Point(12, 35);
            this.chkDownloadPGNs.Name = "chkDownloadPGNs";
            this.chkDownloadPGNs.Size = new System.Drawing.Size(78, 17);
            this.chkDownloadPGNs.TabIndex = 18;
            this.chkDownloadPGNs.Text = "D/L PGN\'s";
            this.chkDownloadPGNs.UseVisualStyleBackColor = true;
            // 
            // chkLogFormOpen
            // 
            this.chkLogFormOpen.AutoSize = true;
            this.chkLogFormOpen.Checked = true;
            this.chkLogFormOpen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLogFormOpen.Enabled = false;
            this.chkLogFormOpen.Location = new System.Drawing.Point(12, 58);
            this.chkLogFormOpen.Name = "chkLogFormOpen";
            this.chkLogFormOpen.Size = new System.Drawing.Size(144, 17);
            this.chkLogFormOpen.TabIndex = 19;
            this.chkLogFormOpen.Text = "Start with Log Form open";
            this.chkLogFormOpen.UseVisualStyleBackColor = true;
            // 
            // chkDebugMode
            // 
            this.chkDebugMode.AutoSize = true;
            this.chkDebugMode.Enabled = false;
            this.chkDebugMode.Location = new System.Drawing.Point(12, 81);
            this.chkDebugMode.Name = "chkDebugMode";
            this.chkDebugMode.Size = new System.Drawing.Size(88, 17);
            this.chkDebugMode.TabIndex = 20;
            this.chkDebugMode.Text = "Debug Mode";
            this.chkDebugMode.UseVisualStyleBackColor = true;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(193, 125);
            this.Controls.Add(this.chkDebugMode);
            this.Controls.Add(this.chkLogFormOpen);
            this.Controls.Add(this.chkDownloadPGNs);
            this.Controls.Add(this.chkBeep);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkBeep;
        private System.Windows.Forms.CheckBox chkDownloadPGNs;
        private System.Windows.Forms.CheckBox chkLogFormOpen;
        private System.Windows.Forms.CheckBox chkDebugMode;


    }
}