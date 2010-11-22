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
            this.tabs = new System.Windows.Forms.TabControl();
            this.addNewTab = new System.Windows.Forms.TabPage();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnAddFeed = new System.Windows.Forms.Button();
            this.pbTimeTillNextUpdate = new System.Windows.Forms.ProgressBar();
            this.txtNextCheck = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnPause = new System.Windows.Forms.Button();
            this.chkRecordGame = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.addNewTab);
            this.tabs.Location = new System.Drawing.Point(-2, 1);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(797, 499);
            this.tabs.TabIndex = 0;
            this.tabs.Resize += new System.EventHandler(this.tabs_Resize);
            // 
            // addNewTab
            // 
            this.addNewTab.AutoScroll = true;
            this.addNewTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addNewTab.Location = new System.Drawing.Point(4, 22);
            this.addNewTab.Margin = new System.Windows.Forms.Padding(0);
            this.addNewTab.Name = "addNewTab";
            this.addNewTab.Size = new System.Drawing.Size(789, 473);
            this.addNewTab.TabIndex = 0;
            this.addNewTab.Text = "ADD +";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(662, 506);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(121, 23);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnAddFeed
            // 
            this.btnAddFeed.Location = new System.Drawing.Point(2, 506);
            this.btnAddFeed.Name = "btnAddFeed";
            this.btnAddFeed.Size = new System.Drawing.Size(121, 23);
            this.btnAddFeed.TabIndex = 6;
            this.btnAddFeed.Text = "Add Feed";
            this.btnAddFeed.UseVisualStyleBackColor = true;
            this.btnAddFeed.Click += new System.EventHandler(this.btnAddFeed_Click);
            // 
            // pbTimeTillNextUpdate
            // 
            this.pbTimeTillNextUpdate.Location = new System.Drawing.Point(5, 535);
            this.pbTimeTillNextUpdate.Name = "pbTimeTillNextUpdate";
            this.pbTimeTillNextUpdate.Size = new System.Drawing.Size(780, 15);
            this.pbTimeTillNextUpdate.TabIndex = 1;
            // 
            // txtNextCheck
            // 
            this.txtNextCheck.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNextCheck.Location = new System.Drawing.Point(-2, 556);
            this.txtNextCheck.Name = "txtNextCheck";
            this.txtNextCheck.Size = new System.Drawing.Size(778, 20);
            this.txtNextCheck.TabIndex = 3;
            this.txtNextCheck.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtLog
            // 
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLog.Location = new System.Drawing.Point(-2, 599);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(783, 20);
            this.txtLog.TabIndex = 4;
            this.txtLog.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(318, 582);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(121, 23);
            this.btnPause.TabIndex = 4;
            this.btnPause.Text = "Pause reading RSS";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // chkRecordGame
            // 
            this.chkRecordGame.AutoSize = true;
            this.chkRecordGame.Location = new System.Drawing.Point(2, 582);
            this.chkRecordGame.Name = "chkRecordGame";
            this.chkRecordGame.Size = new System.Drawing.Size(202, 17);
            this.chkRecordGame.TabIndex = 22;
            this.chkRecordGame.Text = "Record this game in Google Calendar";
            this.chkRecordGame.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(562, 582);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(708, 580);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 23);
            this.button2.TabIndex = 23;
            this.button2.Text = "Open Calendar";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // ShowLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 620);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.chkRecordGame);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAddFeed);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.pbTimeTillNextUpdate);
            this.Controls.Add(this.txtNextCheck);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShowLog";
            this.RightToLeftLayout = true;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShowLog_FormClosing);
            this.Shown += new System.EventHandler(this.ShowLog_Shown);
            this.Resize += new System.EventHandler(this.ShowLog_Resize);
            this.tabs.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnAddFeed;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.ProgressBar pbTimeTillNextUpdate;
        private System.Windows.Forms.TextBox txtNextCheck;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.CheckBox chkRecordGame;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage addNewTab;
        private System.Windows.Forms.Button button2;
    }
}