using System.Windows.Forms;

namespace ChessCalendar.Controls
{
    public class FeedTab: TabPage
    {
        public CalendarProcessor Feed { get; set; }

        public FeedTab()
        {
            //this.Feed = feed;

            var messages = new TextBox();
            messages.Name = "TabPage_txtMessages";

            var grid = new DataGridView();
            grid.Name = "TabPage_Grid";

            var logToCalendar = new CheckBox();
            logToCalendar.Name = "TabPageName_chkLogToCalendar";

            this.Controls.Add(logToCalendar);
            this.Controls.Add(grid);
            this.Controls.Add(messages);
        }
    }
}
