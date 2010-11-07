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

        public const string VERSION = @"Chess Calendar v11.7.10 *Prototype* ";
        //public const string CONFIG_FILE_PATH = @"..\..\GamesToLog.xml"; //Not used.. yet

        public Thread _log;
        public ShowLog _logViewer;
        public Login_Form _login;

        public SysTrayApp()
        {
            this.LoadTray();
        }
        private void LoadTray()
        {
            this.ReloadTray();
            this.Login(this, null);
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

            //_trayIcon.ShowBalloonTip(10, "Start", "Right-Click 'Start' to begin",ToolTipIcon.Info);
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Visible = false; // Hide form window.
            this.ShowInTaskbar = false; // Remove from taskbar.
            base.OnLoad(e);
        }

        private void Login(object sender, EventArgs e)
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
            _login = new Login_Form(VERSION);
            _login.ShowDialog();

            //*** Code Execution will stop at this point and wait until user has dismissed the Login form. ***//

            return _login;
        }
        private void ShowLog(object sender, EventArgs e)
        {
            _log = new Thread(newShowLogThread);
            _log.SetApartmentState(ApartmentState.STA);
            _log.Start();
        }
        private void newShowLogThread(object arg)
        {
            _logViewer = new ShowLog();
            _logViewer.Log = _runningLog;
            _logViewer.ShowDialog();

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

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
               // Release the icon resource.
               _trayIcon.Dispose();
            }
           base.Dispose(isDisposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SysTrayApp
            // 
            this.ClientSize = new System.Drawing.Size(104, 0);
            this.Name = "SysTrayApp";
            this.ResumeLayout(false);
        }
    }
}


//using System;
//using System.Windows.Forms;
//using System.Drawing;

//public class AnimatedSystemTrayIcon : System.Windows.Forms.Form {

//    // (Designer code omitted.)

//    Icon[] images;
//    int offset = 0;

//    private void Form1_Load(object sender, System.EventArgs e) {
    
//        // Load the basic set of eight icons.
//        images = new Icon[8];
//        images[0] = new Icon("moon01.ico");
//        images[1] = new Icon("moon02.ico");
//        images[2] = new Icon("moon03.ico");
//        images[3] = new Icon("moon04.ico");
//        images[4] = new Icon("moon05.ico");
//        images[5] = new Icon("moon06.ico");
//        images[6] = new Icon("moon07.ico");
//        images[7] = new Icon("moon08.ico");
//    }

//    private void timer_Elapsed(object sender, 
//      System.Timers.ElapsedEventArgs e) {
    
//        // Change the icon.
//        // This event handler fires once every second (1000 ms).
//        notifyIcon.Icon = images[offset];
//        offset++;
//        if (offset > 7) offset = 0;
//    }
//}