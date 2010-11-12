using System;
using System.Windows.Forms;

namespace ChessCalendar.Controls
{
    public class ProcessorTab: TabPage
    {
        public CalendarProcessor Calendar { get; set; }

        public ProcessorTab(string tabName, Uri uriToWatch, string userName, string password, Uri logToCalendar)
        {
            this.Calendar = new CalendarProcessor(uriToWatch, userName, password, logToCalendar, true);

            var messages = new TextBox();
            messages.Name = "TabPage_txtMessages";

            var grid = new DataGridView();
            grid.Name = "TabPage_Grid";

            var chkLogToCalendar = new CheckBox();
            chkLogToCalendar.Name = "TabPageName_chkLogToCalendar";

            this.Controls.Add(chkLogToCalendar);
            this.Controls.Add(grid);
            this.Controls.Add(messages);
        }
    }
}
