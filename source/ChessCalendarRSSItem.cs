using ChessCalendar.Interfaces;

namespace ChessCalendar
{
    public class ChessCalendarRSSItem: RssToolkit.Rss.RssItem, IChessItem
    {
        public string Message { get; set; }
    }
}
