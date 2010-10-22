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
            }
        }

        public Log Log { get; set; }

        private void ShowLog_Shown(object sender, System.EventArgs e)
        {
            this.RunLoop();
        }
    }
}
