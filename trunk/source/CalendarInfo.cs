using System;

namespace ChessCalendar
{
    public class CalendarInfo
    {
        private string _name = "* None Selected *";
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string UserName { get; set; }
        public string Password { get; set; }

        public Uri Uri { get; set; }
        public bool Logging { get; set; }
        public bool DownloadPGNs { get; set; }
    }
}
