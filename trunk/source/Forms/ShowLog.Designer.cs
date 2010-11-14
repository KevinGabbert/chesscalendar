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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkRecordGame = new System.Windows.Forms.CheckBox();
            this.lnkNoMovesFound = new System.Windows.Forms.LinkLabel();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.dgvAvailableMoves = new System.Windows.Forms.DataGridView();
            this.pbTimeTillNextUpdate = new System.Windows.Forms.ProgressBar();
            this.txtNextCheck = new System.Windows.Forms.TextBox();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnAddFeed = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
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
            this.tabs.Size = new System.Drawing.Size(797, 499);
            this.tabs.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.chkRecordGame);
            this.tabPage1.Controls.Add(this.lnkNoMovesFound);
            this.tabPage1.Controls.Add(this.btnPause);
            this.tabPage1.Controls.Add(this.txtLog);
            this.tabPage1.Controls.Add(this.txtNextCheck);
            this.tabPage1.Controls.Add(this.dgvAvailableMoves);
            this.tabPage1.Controls.Add(this.pbTimeTillNextUpdate);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(789, 473);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chkRecordGame
            // 
            this.chkRecordGame.AutoSize = true;
            this.chkRecordGame.Location = new System.Drawing.Point(3, 402);
            this.chkRecordGame.Name = "chkRecordGame";
            this.chkRecordGame.Size = new System.Drawing.Size(202, 17);
            this.chkRecordGame.TabIndex = 22;
            this.chkRecordGame.Text = "Record this game in Google Calendar";
            this.chkRecordGame.UseVisualStyleBackColor = true;
            // 
            // lnkNoMovesFound
            // 
            this.lnkNoMovesFound.AutoSize = true;
            this.lnkNoMovesFound.Location = new System.Drawing.Point(329, 182);
            this.lnkNoMovesFound.Name = "lnkNoMovesFound";
            this.lnkNoMovesFound.Size = new System.Drawing.Size(89, 13);
            this.lnkNoMovesFound.TabIndex = 5;
            this.lnkNoMovesFound.TabStop = true;
            this.lnkNoMovesFound.Text = "No Moves Found";
            this.lnkNoMovesFound.Visible = false;
            // 
            // txtLog
            // 
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLog.Location = new System.Drawing.Point(3, 3);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(783, 20);
            this.txtLog.TabIndex = 4;
            this.txtLog.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dgvAvailableMoves
            // 
            this.dgvAvailableMoves.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAvailableMoves.Location = new System.Drawing.Point(3, 29);
            this.dgvAvailableMoves.Name = "dgvAvailableMoves";
            this.dgvAvailableMoves.RowHeadersVisible = false;
            this.dgvAvailableMoves.Size = new System.Drawing.Size(783, 363);
            this.dgvAvailableMoves.TabIndex = 0;
            this.dgvAvailableMoves.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAvailableMoves_CellClick);
            this.dgvAvailableMoves.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvAvailableMoves_CellFormatting);
            // 
            // pbTimeTillNextUpdate
            // 
            this.pbTimeTillNextUpdate.Location = new System.Drawing.Point(3, 451);
            this.pbTimeTillNextUpdate.Name = "pbTimeTillNextUpdate";
            this.pbTimeTillNextUpdate.Size = new System.Drawing.Size(780, 15);
            this.pbTimeTillNextUpdate.TabIndex = 1;
            // 
            // txtNextCheck
            // 
            this.txtNextCheck.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNextCheck.Location = new System.Drawing.Point(3, 425);
            this.txtNextCheck.Name = "txtNextCheck";
            this.txtNextCheck.Size = new System.Drawing.Size(778, 20);
            this.txtNextCheck.TabIndex = 3;
            this.txtNextCheck.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(321, 402);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(121, 23);
            this.btnPause.TabIndex = 4;
            this.btnPause.Text = "Pause reading RSS";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
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
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(789, 473);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(660, 398);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ShowLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 533);
            this.Controls.Add(this.btnAddFeed);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.tabs);
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
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableMoves)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ProgressBar pbTimeTillNextUpdate;
        private System.Windows.Forms.TextBox txtNextCheck;
        private System.Windows.Forms.DataGridView dgvAvailableMoves;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.LinkLabel lnkNoMovesFound;
        private System.Windows.Forms.Button btnAddFeed;
        private System.Windows.Forms.CheckBox chkRecordGame;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}