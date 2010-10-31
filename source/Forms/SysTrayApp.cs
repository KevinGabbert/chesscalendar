using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ChessCalendar.Enums;

namespace ChessCalendar.Forms
{
    public class SysTrayApp : Form
    {
        public NotifyIcon  _trayIcon;
        private ContextMenu _trayMenu;
        private Log _runningLog = new Log();
        private bool _loggedIn = false;

        public const string VERSION = "Chess Calendar v10.30.10 Prototype ";
        //public const string CONFIG_FILE_PATH = @"..\..\GamesToLog.xml"; //Not used.. yet

        public SysTrayApp()
        {
            this.ReloadTray();

        }

        private void ReloadTray()
        {
            _trayMenu = new ContextMenu();
            _trayMenu.MenuItems.Add("Start", Login);//todo: animated gif with logo phasing in and out.
            //_trayMenu.MenuItems.Add("Stop", Login); //todo: this should change Icon to icon with red circle and line through it.

            if (_loggedIn)
            {
                _trayMenu.MenuItems.Add("Show Log", ShowLog);
            }

            _trayMenu.MenuItems.Add("Options", Options);
            _trayMenu.MenuItems.Add("Exit", OnExit);

            _trayIcon = new NotifyIcon();
            _trayIcon.Text = "Chess Calendar";
            //_trayIcon.Icon = new Icon(@"..\..\Images\Logo.ico", 50, 50);
            _trayIcon.Icon = new Icon(@"Logo.ico", 50, 50);

            // Add menu to tray icon and show it.
            _trayIcon.ContextMenu = _trayMenu;
            _trayIcon.Visible = true;

            _trayIcon.ShowBalloonTip(10, "Start", "Right-Click 'Start' to begin",ToolTipIcon.Info);
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Visible = false; // Hide form window.
            this.ShowInTaskbar = false; // Remove from taskbar.
            base.OnLoad(e);
        }

        private void Login(object sender, EventArgs e)
        {
            var userInfoForm = new Login_Form(VERSION);
            userInfoForm.ShowDialog();

            //*** Code Execution will stop at this point and wait until user has dismissed the Login form. ***//

            if (userInfoForm.ValidatedForm)
            {
                this._loggedIn = true;

                if(userInfoForm.AutoOpenLog)
                {
                    this.ShowLog(sender, e);
                }

                _runningLog = new Log();
                _runningLog.LogVersion = VERSION;
                _runningLog.DebugMode = userInfoForm.DebugMode;
                _runningLog.Beep_On_New_Move = userInfoForm.Beep_On_New_Move;
                _runningLog.NotifyIcon = _trayIcon;
                _runningLog.ContextMenu = _trayMenu;
                _runningLog.OutputMode = OutputMode.Form;
                _runningLog.Output(string.Empty, VERSION + DateTime.Now.ToShortTimeString(), OutputMode.Form);
                _runningLog.Log_All_Games(new Uri("http://www.chess.com/rss/echess/" + userInfoForm.ChessDotComName),
                                  userInfoForm.User, 
                                  userInfoForm.Password, 
                                  userInfoForm.PostURI);
            }
            else
            {
                MessageBox.Show("Some of the Login information wasn't right, try again.", "ummmmmmm...");
            }
        }
        private void ShowLog(object sender, EventArgs e)
        {
            Thread logForm = new Thread(threadedForm);
            logForm.SetApartmentState(ApartmentState.STA);
            logForm.Start();
        }

        private void threadedForm(object arg)
        {
            var logViewer = new ShowLog();
            logViewer.Log = _runningLog;
            logViewer.ShowDialog();

            //*** Code Execution will stop at this point and wait until user has dismissed the Login form. ***//
        }

        private void Options(object sender, EventArgs e)
        {
            var optionsForm = new Options();
            optionsForm.ShowDialog();

            //this.ReloadTray();
        }
        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }
        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
               // Release the icon resource.
               _trayIcon.Dispose();
            }
           base.Dispose(isDisposing);
        }
    }
}
