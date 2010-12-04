using System;
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

            public ChessSiteInfo SiteInfo { get; set; }
            public CalendarInfo Calendar { get; set; }

            //TODO: These need to be made into a ConfigInfo object to be passed around
            public bool DebugMode { get; set; }
            public bool Beep_On_New_Move { get; set; }
            public bool ValidatedForm { get; set; }
            public bool AutoOpenLog { get; set; }

        #endregion

        public Login_Form(string version)
        {
            this.InitializeComponent();

            this.SiteInfo = new ChessSiteInfo();
            this.Calendar = new CalendarInfo();

            this.lblVersion.Text = version;

            this.btnStart.Enabled = false;
            this.btnOK.Enabled = false;

            this.cmbOpponents.Visible = this.rdoFollowGames.Checked;

            this.Calendar.Logging = false;
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

            //TODO:  If user typed in multiple names, then hide Calendar selection, and enable start button.
        }
        private void txtChessDotComName_Leave(object sender, EventArgs e)
        {
            //TODO: This can be validated right here, and focus set back to this box if wrong (and box turned red

            this.Conditionally_Load_Opponents();
        }
        private void rdoFollowGames_CheckedChanged(object sender, EventArgs e)
        {
            this.cmbOpponents.Visible = this.rdoFollowGames.Checked;

            this.Conditionally_Load_Opponents();
        }
        private void chkLogToCalendar_CheckedChanged(object sender, EventArgs e)
        {
            this.Calendar.Logging = this.chkLogToCalendar.Checked;

            this.Show_Calendar_Controls();
        }
        private void cmbGoogleCalendar_Leave(object sender, EventArgs e)
        {
            this.btnStart.Enabled = this.ValidateForm();

            if (this.Calendar.Logging)
            {
                this.SetPostURI();
            }
        }

        #endregion

        #region Buttons

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Calendar.UserName = this.txtLogin.Text;
            this.Calendar.Password = this.txtPassword.Text;

            cmbGoogleCalendar.Items.Clear();
            CalendarFeed calFeed = (new GoogleCalendar()).RetrieveCalendars(this.Calendar.UserName, this.Calendar.Password);
            foreach (CalendarEntry entry in calFeed.Entries)
            {
                cmbGoogleCalendar.Items.Add(entry);
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
            //These can be put into a config class 
            this.DebugMode = chkDebugMode.Checked;
            this.Beep_On_New_Move = chkBeep.Checked;
            this.AutoOpenLog = this.chkLogFormOpen.Checked;

            //TODO: this.SiteInfo.UserNames.Add(all names separated by commas)
            //

            this.SiteInfo.UserName = txtChessDotComName.Text;
            this.SiteInfo.UriToWatch = new Uri(Constants.CHESS_DOT_COM_RSS_ECHESS + this.SiteInfo.UserName); 
            this.Calendar.DownloadPGNs = this.chkDownloadPGNs.Checked;

            if (this.chkLogToCalendar.Checked)
            {
                this.SetPostURI();
            }

            this.Hide();
        }

        #endregion

        public bool ValidateForm()
        {
            //TODO: Validate Commas
            bool haveChessDotComName = (this.txtChessDotComName.Text != string.Empty);
            //bool haveGoogleLogin = (this.txtLogin.TextLength > 0);
            //bool haveGooglePassword = (this.txtPassword.TextLength > 0);

            bool validated =  haveChessDotComName;

            this.ValidatedForm = validated;

            return validated;
        }
        private void Show_Calendar_Controls()
        {
            if (this.Calendar.Logging)
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
                var entry = ((Google.GData.Calendar.CalendarEntry) cmbGoogleCalendar.SelectedItem);

                this.Calendar.Name = entry.Title.Text;
                this.Calendar.Uri = new Uri(Constants.CALENDAR_FEEDS +
                                            entry.SelfUri.ToString().Substring(entry.SelfUri.ToString().LastIndexOf("/") + 1) + Constants.PRIVATE_FULL);
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
