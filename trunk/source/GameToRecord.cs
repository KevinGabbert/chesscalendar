using System;

namespace ChessCalendar
{
    public class RecordedGame
    {
        public int Id { get; set; }
        public string GameName { get; set;}
        public bool StillRunning { get; set; }
        public DateTime StartTime { get; set; }
        public string MainWindowTitle { get; set; }
    }
}
