using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ChessCalendar.Controls;

namespace ChessCalendar.Forms
{
    public partial class ShowLog : Form
    {
        private bool _pause = false;
        private bool _progressBarFlash;
        private bool _stop = false;

        #region Properties

            private MessageList MessageList { get; set; }
            public bool DebugMode { get; set; }
            public DateTime NextCheck { get; set; }
            private int _waitProgress;
            public int WaitProgress
            {
                get
                {
                    return _waitProgress < 0 ? 0 : _waitProgress;
                }
                set
                {
                    _waitProgress = value;
                }
            }
            public int WaitSeconds { get; set; }
            public bool ResetWait { get; set; }
            public bool NewMessage { get; set; }
            
        #endregion

        public ShowLog()
        {
            InitializeComponent();

            this.pbTimeTillNextUpdate.Maximum = 100;
            this.pbTimeTillNextUpdate.Minimum = 0;
            this.pbTimeTillNextUpdate.Increment(1);

            this.MessageList = new MessageList();

            this.WaitSeconds = 1000;

            //TODO:  make this into a popup.
            this.txtNextCheck.Text = "Querying RSS Feed and Google Calendar....";
        }

        public ShowLog(ChessFeed feed)
        {
            InitializeComponent();

            //this.Processors.Add((CalendarProcessor)feed);

            //this.pbTimeTillNextUpdate.Maximum = 100;
            //this.pbTimeTillNextUpdate.Minimum = 0;
            //this.pbTimeTillNextUpdate.Increment(1);

            //this.MessageList = new MessageList();

            //FormatDataGrid(this.dgvAvailableMoves);

            ////TODO:  make this into a popup.
            //this.txtNextCheck.Text = "Querying RSS Feed and Google Calendar....";
        }

        #region Events

        private void btnAddFeed_Click(object sender, EventArgs e)
        {
            //opens Login form for the user to select a feed

            var login = new Login_Form("**");
            login.ShowDialog();

            //*** Code Execution will stop at this point and wait until user has dismissed the Login form. ***//

            if (login.ValidatedForm)
            {
                this.Add_Feed_Tab(login);
            }
            else
            {
                MessageBox.Show("Some of the Login information wasn't right, try again.", "ummmmmmm...");
            }

            //newPage.Start();
            //RunLoop() will update all the tabs.

            //To ref the control again later..
            //this.Controls.Find("TabPageName_chkLogToCalendar", true);
        }

        public void Add_Feed_Tab(Login_Form login)
        {
            ProcessorTab newPage = new ProcessorTab(login.ChessDotComName, 
                                                    "PTab_" + login.ChessDotComName,
                                                    new Uri(Constants.CHESS_DOT_COM_RSS_ECHESS + login.ChessDotComName),
                                                    login.User,
                                                    login.Password,
                                                    login.PostURI,
                                                    login.LogToCalendar);

            newPage.Processor.DebugMode = login.DebugMode;
            newPage.Processor.UserLogged = login.ChessDotComName;
            newPage.Processor.GetPGNs = login.DownloadPGNs;
            newPage.Processor.Beep_On_New_Move = login.Beep_On_New_Move;
            newPage.Text = login.ChessDotComName;
            newPage.RefreshTab();
            newPage.Focus();

            this.tabs.TabPages.Add(newPage);

            this.ResetControls();
        }

        private void ShowLog_Shown(object sender, System.EventArgs e)
        {
            this.RunLoop();
        }
        private void ShowLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }
        private void ShowLog_Resize(object sender, EventArgs e)
        {
            //TabControl
            this.ResetControls();

            //each of the controls on each tab should be resized on thab's resize event..
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            this._pause = !_pause;

            this.btnPause.Text = this._pause ? "Resume Reading RSS" : "Pause Reading RSS";

            //if (this._pause)
            //{
            //    this.Stop();
            //}
            //else
            //{
            //    this.Go();
            //}
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.txtNextCheck.Text = "Checking...";
            this.ResetWait = true;
        }

        private void tabs_Resize(object sender, EventArgs e)
        {
            //TODO is this wehere the tabpages get resized?
        }

        #endregion
        private void ResetControls()
        {
            this.tabs.Width = this.Width - 10;
            this.tabs.Height = this.Height - 105;

            //button
            this.btnPause.Top = this.Height - 95;
            this.btnRefresh.Top = this.Height - 95;
            this.btnAddFeed.Top = this.Height - 95;
            this.btnOpenChessDotCom.Top = this.Height - 95;

            //TextBox
            this.txtNextCheck.Top = this.Height - 67;
            this.txtNextCheck.Width = this.Width - 8;

            //ProgressBar
            this.pbTimeTillNextUpdate.Top = this.Height - 45;
            this.pbTimeTillNextUpdate.Width = this.Width - 8;

            //update all the tabs (shouldn't each tab do it themselves?
            foreach (var tab in tabs.TabPages.Cast<object>().Where(tab => ((TabPage) tab).Text != Constants.NEW))
            {
                ((ProcessorTab) tab).ResetControls();               
            }

            this.tabs.TabPages["addNewTab"].SendToBack();
        }

        private void RunLoop()
        {
            while (true)
            {
                foreach (var currentTab in from object tab in tabs.TabPages select ((TabPage) tab))
                {
                    if (currentTab.Name.StartsWith(Constants.PTAB))
                    {
                        var currentProcessorTab = ((ProcessorTab)currentTab);
                        currentProcessorTab.RefreshTab();

                        if (!string.IsNullOrEmpty(currentProcessorTab.Error))
                        {
                            this.txtNextCheck.Text = this.txtNextCheck.Text + " ~ " + currentProcessorTab.Error;
                        }
                    }

                    Application.DoEvents();
                    this.Wait(this.WaitSeconds);
                }
            }
        }

        private void Update_ProgressBar()
        {
            if (this.NextCheck == new DateTime())
            {
                this._progressBarFlash = !this._progressBarFlash;

                this.pbTimeTillNextUpdate.Value = 100;
                this.pbTimeTillNextUpdate.ForeColor = Color.Green;
                
                if(this._progressBarFlash)
                {
                    this.pbTimeTillNextUpdate.Show();
                    Application.DoEvents();
                    //Thread.Sleep(100);
                }
                else
                {
                    this.pbTimeTillNextUpdate.Hide();
                    Application.DoEvents();
                    //Thread.Sleep(100);
                }
            }
            else
            {
                if (this.WaitProgress > 100)
                {
                    this.pbTimeTillNextUpdate.ForeColor = Color.Red;
                    this.pbTimeTillNextUpdate.Value = 100;
                }
                else
                {
                    this.pbTimeTillNextUpdate.ForeColor = Color.Blue;
                    this.pbTimeTillNextUpdate.Value = this.WaitProgress;
                }

                this.pbTimeTillNextUpdate.Show();
            } 
        }
        private void Update_NextCheck()
        {
            if(this.NextCheck == new DateTime())
            {
                this.txtNextCheck.Text = "Unknown";
            }
            else
            {
                this.txtNextCheck.Text = "Next check will be at: " + this.NextCheck.ToShortTimeString();
            }
        }
        //private void UpdateText()
        //{
        //    if (this.Processor != null)
        //    {
        //        if (this.Processor.Messages != null)
        //        {
        //            if (this.Processor.Messages.Count > 0)
        //            {
        //                this.txtLog.Text = (this.Processor.Messages.Dequeue() + Environment.NewLine + this.txtLog.Text);

        //                if (this.txtLog.Text.Length > 5001)
        //                {
        //                    this.txtLog.Text.Remove(5000);
        //                }

        //                this.Processor.NewMessage = true;
        //            }
        //        }
        //        else
        //        {
        //            this.txtLog.Text += "Log is Null" + Environment.NewLine;
        //        }
        //    }
        //}

        private void Wait(int waitSeconds)
        {
            DateTime start = DateTime.Now;
            TimeSpan waitTime = new TimeSpan(0, 0, 0, waitSeconds);

            DateTime finish = start + waitTime;
            this.NextCheck = finish;

            DateTime current = DateTime.Now;
            while (current < finish)
            {
                this.Update_NextCheck();
                this.Update_ProgressBar();
                Application.DoEvents();
                var difference = (finish.Subtract(DateTime.Now));
                this.WaitProgress = Convert.ToInt32(100 - ((difference.TotalSeconds / waitTime.TotalSeconds) * 100));
                current = DateTime.Now;

                if (this.ResetWait)
                {
                    this.ResetWait = false;
                    break;
                }

                if (this._stop)
                {
                    this.WaitProgress = 0;
                    this.NextCheck = new DateTime();
                    break;
                }
            }

            this.NewMessage = false;
        }
    }
}
