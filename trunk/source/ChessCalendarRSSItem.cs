using ChessCalendar.Interfaces;

namespace ChessCalendar
{
    public class ChessCalendarRSSItem: RssToolkit.Rss.RssItem, IChessItem
    {
        public string Message { get; set; }
        public string PGN { get; set; }
        public string GameID
        {
            get
            {
                return ParseUtility.GetGameID(this.Link);
            }
        }
    }
}
