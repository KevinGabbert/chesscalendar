using System;
using System.Windows.Forms;

namespace ChessCalendar.Forms
{
    public partial class ShowLog : Form
    {
        public ShowLog()
        {
            InitializeComponent();

            this.pbTimeTillNextUpdate.Maximum = 100;
            this.pbTimeTillNextUpdate.Minimum = 0;
            this.pbTimeTillNextUpdate.Increment(1);
        }

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
                        this.txtLog.Text = this.Log.Messages.Dequeue() + Environment.NewLine + this.txtLog.Text;
                        this.Log.NewMessage = true;
                    }
                }
                else
                {
                    this.txtLog.Text += "Log is Null" + Environment.NewLine;
                }
            }
        }

        public Log Log { get; set; }

        private void ShowLog_Shown(object sender, System.EventArgs e)
        {
            this.RunLoop();
        }
    }
}
