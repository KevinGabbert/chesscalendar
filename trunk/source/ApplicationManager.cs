using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ChessCalendar.Controls;
using ChessCalendar.Forms;

namespace ChessCalendar
{
    public class ApplicationManager
    {
        public const string VERSION = @"Chess Calendar v11.14.10 *Broken* ";
        //public const string CONFIG_FILE_PATH = @"..\..\GamesToLog.xml"; //Not used.. yet

        public Thread Thread { get; set; }
        public ShowLog LogViewer { get; set; }
        public Login_Form Login { get; set; }

        //TODO: make these into props
        public ProcessorManager_Deprecated FeedProcessor { get; set; }
        public NotifyIcon TrayIcon { get; set; }
        public ContextMenu Menu { get; set; }

        private bool _loggedIn = false;
        private ProcessorTab _startTab;

        public ApplicationManager()
        {
            //TODO: Laying out for right now.  This will be altered later..
            this.Thread = new Thread(newShowLogThread);
            this.Thread.SetApartmentState(ApartmentState.STA); 
        }

        public void Start()
        {
            this.ReloadTray();
            this.GetLogin(this, null);
        }

        private void ReloadTray()
        {
            this.Menu = new ContextMenu();
            this.Menu.MenuItems.Add("Start", GetLogin);//todo: animated gif with logo phasing in and out.
            //_trayMenu.MenuItems.Add("Stop", Login); //todo: this should change Icon to icon with red circle and line through it.

            if (_loggedIn)
            {
                this.Menu.MenuItems.Add("Show Log", ShowLog);
            }

            this.Menu.MenuItems.Add("Options", Options);
            this.Menu.MenuItems.Add("Exit", OnExit);

            this.TrayIcon = new NotifyIcon();
            this.TrayIcon.Text = "Chess Calendar";
            //_trayIcon.Icon = new Icon(@"..\..\Images\Logo.ico", 50, 50);
            this.TrayIcon.Icon = new Icon(@"Logo.ico", 50, 50);

            // Add menu to tray icon and show it.
            this.TrayIcon.ContextMenu = Menu;
            this.TrayIcon.Visible = true;

            //_trayIcon.ShowBalloonTip(10, "Start", "Right-Click 'Start' to begin",ToolTipIcon.Info);
        }

        private void GetLogin(object sender, EventArgs e)
        {
            var loginInfo = GetLoginInfo();

            this.FeedProcessor = new ProcessorManager_Deprecated();
            this.FeedProcessor.LogVersion = VERSION;
            this.FeedProcessor.DebugMode = loginInfo.DebugMode;
            this.FeedProcessor.UserLogged = loginInfo.ChessDotComName;
            this.FeedProcessor.GetPGNs = loginInfo.DownloadPGNs;
            this.FeedProcessor.Beep_On_New_Move = loginInfo.Beep_On_New_Move;
            this.FeedProcessor.NotifyIcon = TrayIcon;
            this.FeedProcessor.ContextMenu = Menu;
            //this.FeedProcessor.Post(string.Empty, VERSION + DateTime.Now.ToShortTimeString());

            this.SetNewLogForm(loginInfo);

            if (loginInfo.AutoOpenLog)
            {
                this.ShowLog(sender, e);
            }
        }

        private void SetNewLogForm(Login_Form userInfoForm)
        {
            if (userInfoForm.ValidatedForm)
            {
                _startTab = new ProcessorTab("PTab_" + userInfoForm.ChessDotComName,
                                                         new Uri("http://www.chess.com/rss/echess/" + userInfoForm.ChessDotComName),
                                                         userInfoForm.User,
                                                         userInfoForm.Password,
                                                         userInfoForm.PostURI);

                _startTab.Processor.DebugMode = userInfoForm.DebugMode;
                _startTab.Processor.UserLogged = userInfoForm.ChessDotComName;
                _startTab.Processor.GetPGNs = userInfoForm.DownloadPGNs;
                _startTab.Processor.Beep_On_New_Move = userInfoForm.Beep_On_New_Move;
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
            this.Thread.Start();
        }
        private void newShowLogThread(object arg)
        {
            this.LogViewer = new ShowLog(_startTab);
            this.LogViewer.ShowDialog();

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
