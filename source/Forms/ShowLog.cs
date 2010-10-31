using System;
using System.ComponentModel;
using System.Windows.Forms;
using ChessCalendar.Interfaces;

namespace ChessCalendar.Forms
{
    public partial class ShowLog : Form
    {
        protected int _fail = 0;

        #region Properties

            public Log Log { get; set; }
            public MessageList MessageList { get; set; }
            public bool DebugMode { get; set; }
            
        #endregion

        public ShowLog()
        {
            InitializeComponent();

            this.pbTimeTillNextUpdate.Maximum = 100;
            this.pbTimeTillNextUpdate.Minimum = 0;
            this.pbTimeTillNextUpdate.Increment(1);

            this.MessageList = new MessageList();
        }

        #region Events
        private void ShowLog_Shown(object sender, System.EventArgs e)
        {
            this.RunLoop();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        #endregion

        private void RunLoop()
        {
            while (true)
            {
                this.txtNextCheck.Text = "Next check will be at: " + this.Log.NextCheck.ToShortTimeString();

                Application.DoEvents();

                this.pbTimeTillNextUpdate.Value = this.Log.WaitProgress;
                this.pbTimeTillNextUpdate.Show();

                if((DateTime.Now > this.Log.NextCheck) || this.Log.NewMessage)
                {
                    this.UpdateText();
                    this.UpdateDataGridView();    
                }
                else
                {
                    this.MessageList = new MessageList();
                }
            }
        }
        private void UpdateDataGridView()
        {
            if (this.Log != null)
            {
                if (this.Log.NewMoves != null)
                {
                    if (this.Log.NewMoves.Count > 0)
                    {
                        this.MessageList.Clear();

                        for (int i = this.Log.NewMoves.Count; i > 0; i--)
                        {
                            this.MessageList.Add(this.Log.NewMoves.Dequeue()); 
                        }

                        this.SetMovesDataSource(this.MessageList);
                        this.Log.NewMessage = true;
                    }
                }
                else
                {
                    _fail += 1;
                    this.txtLog.Text = "Log is Null: " + _fail + Environment.NewLine + this.txtLog.Text;

                    if (this.txtLog.Text.Length > 5001)
                    {
                        this.txtLog.Text.Remove(5000);
                    }
                }
            }
        }
        private void UpdateText()
        {
            if (this.Log != null)
            {
                if (this.Log.Messages != null)
                {
                    if (this.Log.Messages.Count > 0)
                    {
                        this.txtLog.Text = (this.Log.Messages.Dequeue() + Environment.NewLine + this.txtLog.Text);

                        if (this.txtLog.Text.Length > 5001)
                        {
                            this.txtLog.Text.Remove(5000);
                        }

                        this.Log.NewMessage = true;
                    }
                }
                else
                {
                    this.txtLog.Text += "Log is Null" + Environment.NewLine;
                }
            }
        }

        protected delegate void MovesDelegate(BindingList<IChessItem> dataSource);
        public void SetMovesDataSource(BindingList<IChessItem> dataSource)
        {
            if (this.dgvAvailableMoves.InvokeRequired)
            {
                this.dgvAvailableMoves.Invoke(new MovesDelegate(this.SetMovesDataSource), dataSource);
            }
            else
            {
                this.dgvAvailableMoves.DataSource = dataSource;
            }
        }
    }
}
