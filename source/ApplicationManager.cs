using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ChessCalendar.Enums;
using ChessCalendar.Forms;

namespace ChessCalendar
{
    public class ApplicationManager
    {
        public const string VERSION = @"Chess Calendar v11.7.10c **Prototype** ";
        //public const string CONFIG_FILE_PATH = @"..\..\GamesToLog.xml"; //Not used.. yet

        private List<Thread> Logs { get; set; }
        private ShowLog LogViewer { get; set; }
        private Login_Form Login { get; set; }

        //TODO: make these into props
        private Log _runningLog = new Log();
        public NotifyIcon _trayIcon;
        private ContextMenu _trayMenu;

        private bool _loggedIn = false;

        public ApplicationManager()
        {
            //TODO: Laying out for right now.  This will be altered later..
            this.Logs = new List<Thread>();
            this.Logs.Add(new Thread(newShowLogThread));

            this.Logs[0].SetApartmentState(ApartmentState.STA); 
        }

        public void Start()
        {
            this.ReloadTray();
            this.GetLogin(this, null);
        }

        private void ReloadTray()
        {
            _trayMenu = new ContextMenu();
            _trayMenu.MenuItems.Add("Start", GetLogin);//todo: animated gif with logo phasing in and out.
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

            //_trayIcon.ShowBalloonTip(10, "Start", "Right-Click 'Start' to begin",ToolTipIcon.Info);
        }

        private void GetLogin(object sender, EventArgs e)
        {
            var loginInfo = GetLoginInfo();

            _runningLog = new Log();
            _runningLog.LogVersion = VERSION;
            _runningLog.DebugMode = loginInfo.DebugMode;
            _runningLog.UserLogged = loginInfo.ChessDotComName;
            _runningLog.GetPGNs = loginInfo.DownloadPGNs;
            _runningLog.Beep_On_New_Move = loginInfo.Beep_On_New_Move;
            _runningLog.NotifyIcon = _trayIcon;
            _runningLog.ContextMenu = _trayMenu;
            _runningLog.OutputMode = OutputMode.Form;
            _runningLog.Output(string.Empty, VERSION + DateTime.Now.ToShortTimeString(), OutputMode.Form);

            if (loginInfo.AutoOpenLog)
            {
                this.ShowLog(sender, e);
            }

            this.ValidateAndStart(loginInfo);
        }

        private void ValidateAndStart(Login_Form userInfoForm)
        {
            if (userInfoForm.ValidatedForm)
            {
                this._loggedIn = true;

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

        private Login_Form GetLoginInfo()
        {
            Login = new Login_Form(VERSION);
            Login.ShowDialog();

            //*** Code Execution will stop at this point and wait until user has dismissed the Login form. ***//

            return Login;
        }
        private void ShowLog(object sender, EventArgs e)
        {
            this.Logs[0].Start();
        }
        private void newShowLogThread(object arg)
        {
            LogViewer = new ShowLog();
            LogViewer.Log = _runningLog;
            LogViewer.ShowDialog();

            //*** Code Execution will stop at this point and wait until user has dismissed the Login form. ***//
        }

        private static void Options(object sender, EventArgs e)
        {
            var optionsForm = new Options();
            optionsForm.ShowDialog();
        }

        private static void OnExit(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
