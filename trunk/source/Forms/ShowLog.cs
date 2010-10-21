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
                Application.DoEvents();

                //TODO:  why is this 101??
                this.pbTimeTillNextUpdate.Value = this.Log.WaitProgress;
                this.pbTimeTillNextUpdate.Show();
                this.percentage.Text = this.pbTimeTillNextUpdate.Value.ToString();
            }
        }

        public Log Log { get; set; }

        private void ShowLog_Shown(object sender, System.EventArgs e)
        {
            this.RunLoop();
        }
    }
}
