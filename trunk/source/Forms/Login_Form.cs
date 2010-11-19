﻿using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Google.GData.Calendar;

namespace ChessCalendar.Forms
{
    /// <summary>
    /// This form is designed to pop up whenever login information is not known (and pre-populated)
    /// normally, the user would never see this form, and if they do, it would be rare.
    /// </summary>
    public partial class Login_Form : Form
    {
        #region Properties

            public string ChessDotComName { get; set; }
            public string User { get; set; }
            public string Password { get; set; }
            public System.Uri PostURI { get; set; }

            public bool DebugMode { get; set; }
            public bool Beep_On_New_Move { get; set; }
            public bool ValidatedForm { get; set; }
            public bool AutoOpenLog { get; set; }
            public bool DownloadPGNs { get; set; }
            public bool LogToCalendar { get; set; }

        #endregion

        public Login_Form(string version)
        {
            this.InitializeComponent();
            this.lblVersion.Text = version;

            this.btnStart.Enabled = false;
            this.btnOK.Enabled = false;

            this.cmbOpponents.Visible = this.rdoFollowGames.Checked;

            this.LogToCalendar = true;
            this.Show_Calendar_Controls();
        }

        #region TextBoxes

        private void LoginTextControls_TextChanged(object sender, EventArgs e)
        {
            this.btnOK.Enabled = this.ValidateForm();
            if (Login_Form.UserHitReturn(sender))
            {
                if (this.btnOK.Enabled)
                {
                    this.btnOK_Click(sender, e);
                    this.btnOK.Enabled = false;
                }
            }
        }
        private void txtChessDotComName_TextChanged(object sender, EventArgs e)
        {
            this.txtChessDotComName.ForeColor = Color.Black;

            if (!this.chkLogToCalendar.Checked)
            {
                this.btnStart.Enabled = this.ValidateForm();
            }

            if (Login_Form.UserHitReturn(sender))
            {
                if (this.btnStart.Enabled || (!this.chkLogToCalendar.Checked))
                {
                    this.btnStart_Click(sender, e);
                }
            }
        }
        private void txtChessDotComName_Leave(object sender, EventArgs e)
        {
            this.Conditionally_Load_Opponents();
        }
        private void rdoFollowGames_CheckedChanged(object sender, EventArgs e)
        {
            this.cmbOpponents.Visible = this.rdoFollowGames.Checked;

            this.Conditionally_Load_Opponents();
        }
        private void chkLogToCalendar_CheckedChanged(object sender, EventArgs e)
        {
            this.LogToCalendar = this.chkLogToCalendar.Checked;

            this.Show_Calendar_Controls();
        }
        private void cmbGoogleCalendar_Leave(object sender, EventArgs e)
        {
            this.btnStart.Enabled = this.ValidateForm();

            if (this.LogToCalendar)
            {
                this.SetPostURI();
            }
        }

        #endregion


        #region Buttons

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.User = this.txtLogin.Text;
            this.Password = this.txtPassword.Text;

            cmbGoogleCalendar.Items.Clear();
            CalendarFeed calFeed = GoogleCalendar.RetrieveCalendars(this.User, this.Password);
            foreach (CalendarEntry centry in calFeed.Entries)
            {
                cmbGoogleCalendar.Items.Add(centry);
            }

            cmbGoogleCalendar.DisplayMember = "Title";
            cmbGoogleCalendar.ValueMember = "Title";
            if (cmbGoogleCalendar.Items.Count > 0)
            {
                cmbGoogleCalendar.SelectedIndex = 0;
            }

            this.btnOK.Enabled = false;
            cmbGoogleCalendar.Focus();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            this.ChessDotComName = txtChessDotComName.Text;
            this.DebugMode = chkDebugMode.Checked;
            this.Beep_On_New_Move = chkBeep.Checked;
            this.AutoOpenLog = this.chkLogFormOpen.Checked;
            this.DownloadPGNs = this.chkDownloadPGNs.Checked;

            if (this.chkLogToCalendar.Checked)
            {
                this.SetPostURI();
            }

            this.Hide();
        }

        #endregion

        public bool ValidateForm()
        {
            bool haveChessDotComName = (this.txtChessDotComName.Text != string.Empty);
            //bool haveGoogleLogin = (this.txtLogin.TextLength > 0);
            //bool haveGooglePassword = (this.txtPassword.TextLength > 0);

            bool validated =  haveChessDotComName;

            this.ValidatedForm = validated;

            return validated;
        }
        private void Show_Calendar_Controls()
        {
            if (this.LogToCalendar)
            {
                this.lblVersion.Location = new Point(9, 534);
                this.btnStart.Location = new Point(182, 501);
                this.Height = 586;
            }
            else
            {
                this.btnStart.Location = new Point(182, 257);
                this.lblVersion.Location = new Point(9, 3);
                this.Height = 310;
            }
        }

        private void SetPostURI()
        {
            if (cmbGoogleCalendar.SelectedItem != null)
            {
                this.PostURI = new Uri(Constants.CALENDAR_FEEDS +
                                        ((CalendarEntry) (cmbGoogleCalendar.SelectedItem)).SelfUri.ToString().
                                            Substring(
                                                ((CalendarEntry) (cmbGoogleCalendar.SelectedItem)).SelfUri.ToString()
                                                    .LastIndexOf("/") + 1) +
                                        Constants.PRIVATE_FULL);
            }
        }

        private static bool UserHitReturn(object sender)
        {
            return ((TextBoxBase)sender).Text.Contains(Environment.NewLine);
        }

        private void Conditionally_Load_Opponents()
        {
            if (this.rdoFollowGames.Checked)
            {
                this.Load_Opponents();
            }
        }
        private void Load_Opponents()
        {
            try
            {
                ChessFeed chessDotComFeed = new ChessFeed(new Uri(Constants.CHESS_DOT_COM_RSS_ECHESS + this.txtChessDotComName.Text));
                this.cmbOpponents.DataSource = chessDotComFeed.GetOpponents().Distinct().ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        } 
    }
}
