using System;
using System.Windows.Forms;
using Google.GData.Calendar;

namespace ChessCalendar
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
        #endregion

        public Login_Form(string version)
        {
            this.InitializeComponent();
            this.lblVersion.Text = version;

            this.btnStart.Enabled = false;
            this.btnOK.Enabled = false;
        }

        #region TextBoxes
        private void LoginTextControls_TextChanged(object sender, EventArgs e)
        {
            this.btnOK.Enabled = this.ValidateForm();
            if (UserHitReturn(sender))
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
            this.btnStart.Enabled = this.ValidateForm();
            if (UserHitReturn(sender))
            {
                if (this.btnStart.Enabled)
                {
                    this.btnStart_Click(sender, e);
                }
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
            this.SetPostURI();

            this.Hide();
        }

        #endregion

        public bool ValidateForm()
        {
            bool haveChessDotComName = (this.ChessDotComName != string.Empty);
            bool haveGoogleLogin = (this.txtLogin.TextLength > 0);
            bool haveGooglePassword = (this.txtPassword.TextLength > 0);

            bool validated = haveGoogleLogin && haveGooglePassword && haveChessDotComName;

            this.ValidatedForm = validated;

            return validated;
        }

        private void cmbGoogleCalendar_Leave(object sender, EventArgs e)
        {
            this.SetPostURI();
        }

        private void SetPostURI()
        {
            this.PostURI = new Uri("http://www.google.com/calendar/feeds/" +
                      ((CalendarEntry)(cmbGoogleCalendar.SelectedItem)).SelfUri.ToString().Substring(((CalendarEntry)(cmbGoogleCalendar.SelectedItem)).SelfUri.ToString().LastIndexOf("/") + 1) +
                      "/private/full");
        }

        private static bool UserHitReturn(object sender)
        {
            return ((TextBoxBase)sender).Text.Contains(Environment.NewLine);
        }

    }
}
