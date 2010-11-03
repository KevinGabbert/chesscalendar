﻿namespace ChessCalendar.Forms
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
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.dgvAvailableMoves = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pbTimeTillNextUpdate = new System.Windows.Forms.ProgressBar();
            this.txtNextCheck = new System.Windows.Forms.TextBox();
            this.tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableMoves)).BeginInit();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabPage1);
            this.tabs.Controls.Add(this.tabPage2);
            this.tabs.Location = new System.Drawing.Point(-2, 1);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(573, 406);
            this.tabs.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtLog);
            this.tabPage1.Controls.Add(this.dgvAvailableMoves);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(565, 380);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLog.Location = new System.Drawing.Point(3, 3);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(559, 20);
            this.txtLog.TabIndex = 4;
            this.txtLog.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dgvAvailableMoves
            // 
            this.dgvAvailableMoves.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAvailableMoves.Location = new System.Drawing.Point(3, 29);
            this.dgvAvailableMoves.Name = "dgvAvailableMoves";
            this.dgvAvailableMoves.Size = new System.Drawing.Size(559, 348);
            this.dgvAvailableMoves.TabIndex = 0;
            this.dgvAvailableMoves.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvAvailableMoves_CellFormatting);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(565, 380);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pbTimeTillNextUpdate
            // 
            this.pbTimeTillNextUpdate.Location = new System.Drawing.Point(0, 436);
            this.pbTimeTillNextUpdate.Name = "pbTimeTillNextUpdate";
            this.pbTimeTillNextUpdate.Size = new System.Drawing.Size(571, 15);
            this.pbTimeTillNextUpdate.TabIndex = 1;
            // 
            // txtNextCheck
            // 
            this.txtNextCheck.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNextCheck.Location = new System.Drawing.Point(0, 413);
            this.txtNextCheck.Name = "txtNextCheck";
            this.txtNextCheck.Size = new System.Drawing.Size(571, 20);
            this.txtNextCheck.TabIndex = 3;
            this.txtNextCheck.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ShowLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 453);
            this.Controls.Add(this.pbTimeTillNextUpdate);
            this.Controls.Add(this.txtNextCheck);
            this.Controls.Add(this.tabs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShowLog";
            this.RightToLeftLayout = true;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Shown += new System.EventHandler(this.ShowLog_Shown);
            this.Resize += new System.EventHandler(this.ShowLog_Resize);
            this.tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableMoves)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ProgressBar pbTimeTillNextUpdate;
        private System.Windows.Forms.TextBox txtNextCheck;
        private System.Windows.Forms.DataGridView dgvAvailableMoves;
        private System.Windows.Forms.TextBox txtLog;
    }
}