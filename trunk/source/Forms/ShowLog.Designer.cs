namespace ChessCalendar.Forms
{
    partial class ShowLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowLog));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pbTimeTillNextUpdate = new System.Windows.Forms.ProgressBar();
            this.txtNextCheck = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(-2, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(332, 289);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(324, 263);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Name 1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(324, 273);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Name 2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pbTimeTillNextUpdate
            // 
            this.pbTimeTillNextUpdate.Location = new System.Drawing.Point(0, 315);
            this.pbTimeTillNextUpdate.Name = "pbTimeTillNextUpdate";
            this.pbTimeTillNextUpdate.Size = new System.Drawing.Size(330, 15);
            this.pbTimeTillNextUpdate.TabIndex = 1;
            // 
            // txtNextCheck
            // 
            this.txtNextCheck.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNextCheck.Location = new System.Drawing.Point(0, 292);
            this.txtNextCheck.Name = "txtNextCheck";
            this.txtNextCheck.Size = new System.Drawing.Size(330, 20);
            this.txtNextCheck.TabIndex = 3;
            this.txtNextCheck.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ShowLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 331);
            this.Controls.Add(this.pbTimeTillNextUpdate);
            this.Controls.Add(this.txtNextCheck);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShowLog";
            this.RightToLeftLayout = true;
            this.Text = "Log";
            this.Shown += new System.EventHandler(this.ShowLog_Shown);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ProgressBar pbTimeTillNextUpdate;
        private System.Windows.Forms.TextBox txtNextCheck;
    }
}