namespace ChessCalendar.Forms
{
    partial class Login_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login_Form));
            this.pbTitle = new System.Windows.Forms.PictureBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.cmbGoogleCalendar = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtChessDotComName = new System.Windows.Forms.TextBox();
            this.chkDebugMode = new System.Windows.Forms.CheckBox();
            this.chkBeep = new System.Windows.Forms.CheckBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.chkLogFormOpen = new System.Windows.Forms.CheckBox();
            this.chkDownloadPGNs = new System.Windows.Forms.CheckBox();
            this.cmbOpponents = new System.Windows.Forms.ComboBox();
            this.grpCalendar = new System.Windows.Forms.GroupBox();
            this.grpChess = new System.Windows.Forms.GroupBox();
            this.rdoFollowAll = new System.Windows.Forms.RadioButton();
            this.rdoFollowGames = new System.Windows.Forms.RadioButton();
            this.chkLogToCalendar = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).BeginInit();
            this.grpCalendar.SuspendLayout();
            this.grpChess.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbTitle
            // 
            this.pbTitle.BackColor = System.Drawing.Color.Transparent;
            this.pbTitle.Image = ((System.Drawing.Image)(resources.GetObject("pbTitle.Image")));
            this.pbTitle.Location = new System.Drawing.Point(12, 12);
            this.pbTitle.Name = "pbTitle";
            this.pbTitle.Size = new System.Drawing.Size(250, 47);
            this.pbTitle.TabIndex = 1;
            this.pbTitle.TabStop = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(6, 97);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(150, 17);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Save Login and Password";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(88, 183);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(6, 32);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(250, 20);
            this.txtLogin.TabIndex = 0;
            this.txtLogin.TextChanged += new System.EventHandler(this.LoginTextControls_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Login";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.AcceptsReturn = true;
            this.txtPassword.Location = new System.Drawing.Point(6, 71);
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(249, 20);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.TextChanged += new System.EventHandler(this.LoginTextControls_TextChanged);
            // 
            // cmbGoogleCalendar
            // 
            this.cmbGoogleCalendar.FormattingEnabled = true;
            this.cmbGoogleCalendar.Location = new System.Drawing.Point(6, 133);
            this.cmbGoogleCalendar.Name = "cmbGoogleCalendar";
            this.cmbGoogleCalendar.Size = new System.Drawing.Size(249, 21);
            this.cmbGoogleCalendar.TabIndex = 4;
            this.cmbGoogleCalendar.Leave += new System.EventHandler(this.cmbGoogleCalendar_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Log to this Google Calendar:";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(182, 501);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(88, 23);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "User Name";
            // 
            // txtChessDotComName
            // 
            this.txtChessDotComName.AcceptsReturn = true;
            this.txtChessDotComName.Location = new System.Drawing.Point(7, 33);
            this.txtChessDotComName.Multiline = true;
            this.txtChessDotComName.Name = "txtChessDotComName";
            this.txtChessDotComName.Size = new System.Drawing.Size(250, 20);
            this.txtChessDotComName.TabIndex = 5;
            this.txtChessDotComName.TextChanged += new System.EventHandler(this.txtChessDotComName_TextChanged);
            this.txtChessDotComName.Leave += new System.EventHandler(this.txtChessDotComName_Leave);
            // 
            // chkDebugMode
            // 
            this.chkDebugMode.AutoSize = true;
            this.chkDebugMode.Enabled = false;
            this.chkDebugMode.Location = new System.Drawing.Point(12, 505);
            this.chkDebugMode.Name = "chkDebugMode";
            this.chkDebugMode.Size = new System.Drawing.Size(88, 17);
            this.chkDebugMode.TabIndex = 7;
            this.chkDebugMode.Text = "Debug Mode";
            this.chkDebugMode.UseVisualStyleBackColor = true;
            this.chkDebugMode.Visible = false;
            // 
            // chkBeep
            // 
            this.chkBeep.AutoSize = true;
            this.chkBeep.Location = new System.Drawing.Point(7, 131);
            this.chkBeep.Name = "chkBeep";
            this.chkBeep.Size = new System.Drawing.Size(118, 17);
            this.chkBeep.TabIndex = 6;
            this.chkBeep.Text = "Beep on new move";
            this.chkBeep.UseVisualStyleBackColor = true;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(9, 534);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(42, 13);
            this.lblVersion.TabIndex = 15;
            this.lblVersion.Text = "Version";
            // 
            // chkLogFormOpen
            // 
            this.chkLogFormOpen.AutoSize = true;
            this.chkLogFormOpen.Checked = true;
            this.chkLogFormOpen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLogFormOpen.Enabled = false;
            this.chkLogFormOpen.Location = new System.Drawing.Point(7, 154);
            this.chkLogFormOpen.Name = "chkLogFormOpen";
            this.chkLogFormOpen.Size = new System.Drawing.Size(144, 17);
            this.chkLogFormOpen.TabIndex = 16;
            this.chkLogFormOpen.Text = "Start with Log Form open";
            this.chkLogFormOpen.UseVisualStyleBackColor = true;
            // 
            // chkDownloadPGNs
            // 
            this.chkDownloadPGNs.AutoSize = true;
            this.chkDownloadPGNs.Location = new System.Drawing.Point(7, 160);
            this.chkDownloadPGNs.Name = "chkDownloadPGNs";
            this.chkDownloadPGNs.Size = new System.Drawing.Size(166, 17);
            this.chkDownloadPGNs.TabIndex = 17;
            this.chkDownloadPGNs.Text = "Put PGN\'s in Calendar Entries";
            this.chkDownloadPGNs.UseVisualStyleBackColor = true;
            // 
            // cmbOpponents
            // 
            this.cmbOpponents.FormattingEnabled = true;
            this.cmbOpponents.Location = new System.Drawing.Point(7, 98);
            this.cmbOpponents.Name = "cmbOpponents";
            this.cmbOpponents.Size = new System.Drawing.Size(252, 21);
            this.cmbOpponents.TabIndex = 20;
            // 
            // grpCalendar
            // 
            this.grpCalendar.Controls.Add(this.label3);
            this.grpCalendar.Controls.Add(this.txtLogin);
            this.grpCalendar.Controls.Add(this.cmbGoogleCalendar);
            this.grpCalendar.Controls.Add(this.checkBox1);
            this.grpCalendar.Controls.Add(this.btnOK);
            this.grpCalendar.Controls.Add(this.chkDownloadPGNs);
            this.grpCalendar.Controls.Add(this.label1);
            this.grpCalendar.Controls.Add(this.label2);
            this.grpCalendar.Controls.Add(this.txtPassword);
            this.grpCalendar.Location = new System.Drawing.Point(5, 283);
            this.grpCalendar.Name = "grpCalendar";
            this.grpCalendar.Size = new System.Drawing.Size(265, 212);
            this.grpCalendar.TabIndex = 22;
            this.grpCalendar.TabStop = false;
            this.grpCalendar.Text = "Google Calendar";
            // 
            // grpChess
            // 
            this.grpChess.Controls.Add(this.rdoFollowAll);
            this.grpChess.Controls.Add(this.rdoFollowGames);
            this.grpChess.Controls.Add(this.txtChessDotComName);
            this.grpChess.Controls.Add(this.cmbOpponents);
            this.grpChess.Controls.Add(this.label4);
            this.grpChess.Controls.Add(this.chkLogFormOpen);
            this.grpChess.Controls.Add(this.chkBeep);
            this.grpChess.Location = new System.Drawing.Point(5, 62);
            this.grpChess.Name = "grpChess";
            this.grpChess.Size = new System.Drawing.Size(265, 179);
            this.grpChess.TabIndex = 23;
            this.grpChess.TabStop = false;
            this.grpChess.Text = "Chess.com";
            // 
            // rdoFollowAll
            // 
            this.rdoFollowAll.AutoSize = true;
            this.rdoFollowAll.Checked = true;
            this.rdoFollowAll.Location = new System.Drawing.Point(8, 59);
            this.rdoFollowAll.Name = "rdoFollowAll";
            this.rdoFollowAll.Size = new System.Drawing.Size(105, 17);
            this.rdoFollowAll.TabIndex = 22;
            this.rdoFollowAll.TabStop = true;
            this.rdoFollowAll.Text = "Follow All Games";
            this.rdoFollowAll.UseVisualStyleBackColor = true;
            // 
            // rdoFollowGames
            // 
            this.rdoFollowGames.AutoSize = true;
            this.rdoFollowGames.Location = new System.Drawing.Point(8, 75);
            this.rdoFollowGames.Name = "rdoFollowGames";
            this.rdoFollowGames.Size = new System.Drawing.Size(129, 17);
            this.rdoFollowGames.TabIndex = 21;
            this.rdoFollowGames.Text = "Follow Games Against";
            this.rdoFollowGames.UseVisualStyleBackColor = true;
            this.rdoFollowGames.CheckedChanged += new System.EventHandler(this.rdoFollowGames_CheckedChanged);
            // 
            // chkLogToCalendar
            // 
            this.chkLogToCalendar.AutoSize = true;
            this.chkLogToCalendar.Checked = true;
            this.chkLogToCalendar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLogToCalendar.Enabled = false;
            this.chkLogToCalendar.Location = new System.Drawing.Point(5, 260);
            this.chkLogToCalendar.Name = "chkLogToCalendar";
            this.chkLogToCalendar.Size = new System.Drawing.Size(101, 17);
            this.chkLogToCalendar.TabIndex = 23;
            this.chkLogToCalendar.Text = "Log to Calendar";
            this.chkLogToCalendar.UseVisualStyleBackColor = true;
            // 
            // Login_Form
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(274, 559);
            this.Controls.Add(this.chkLogToCalendar);
            this.Controls.Add(this.grpChess);
            this.Controls.Add(this.grpCalendar);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.chkDebugMode);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.pbTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            ((System.ComponentModel.ISupportInitialize)(this.pbTitle)).EndInit();
            this.grpCalendar.ResumeLayout(false);
            this.grpCalendar.PerformLayout();
            this.grpChess.ResumeLayout(false);
            this.grpChess.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbTitle;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.ComboBox cmbGoogleCalendar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtChessDotComName;
        private System.Windows.Forms.CheckBox chkDebugMode;
        private System.Windows.Forms.CheckBox chkBeep;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.CheckBox chkLogFormOpen;
        private System.Windows.Forms.CheckBox chkDownloadPGNs;
        private System.Windows.Forms.ComboBox cmbOpponents;
        private System.Windows.Forms.GroupBox grpCalendar;
        private System.Windows.Forms.GroupBox grpChess;
        private System.Windows.Forms.RadioButton rdoFollowAll;
        private System.Windows.Forms.RadioButton rdoFollowGames;
        private System.Windows.Forms.CheckBox chkLogToCalendar;
    }
}